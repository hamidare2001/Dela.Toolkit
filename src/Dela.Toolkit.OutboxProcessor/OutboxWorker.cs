using Dela.Toolkit.Application.Events;
using Dela.Toolkit.OutboxProcessor.DataStore;
using Dela.Toolkit.OutboxProcessor.EventBus;
using Dela.Toolkit.OutboxProcessor.EventTransformation;
using Dela.Toolkit.OutboxProcessor.Filtering;
using Dela.Toolkit.OutboxProcessor.Serialization;
using Dela.Toolkit.OutboxProcessor.Types;
using Dela.Toolkit.Persistence;

namespace Dela.Toolkit.OutboxProcessor;

 public class OutboxWorker : BackgroundService, IDataStoreChangeTracker
    {
        private readonly ILogger<OutboxWorker> _logger;
        private readonly IDataStore _store;
        private readonly IEventBus _eventBus;
        private readonly IEventTypeResolver _typeResolver;
        private readonly IFilter _filter;
        private readonly IEventTransformerLookUp _transformerLookUp;

        public OutboxWorker(ILogger<OutboxWorker> logger,
            IDataStore store,
            IEventBus eventBus, 
            IEventTypeResolver typeResolver,
            IFilter filter,
            IEventTransformerLookUp transformerLookUp)
        {
            _logger = logger;
            _store = store;
            _store.SetSubscriber(this);
            _eventBus = eventBus;
            _typeResolver = typeResolver;
            _filter = filter;
            _transformerLookUp = transformerLookUp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventBus.Start();
            _logger.LogInformation("Event bus started.");
            _store.SubscribeForChanges();
            _logger.LogInformation("Subscribed to data store");
        }

        public void ChangeDetected(List<OutboxItem> items)
        {
            foreach (var item in items)
            {
                var type = _typeResolver.GetType(item.EventType);
                if (type == null)
                {
                    _logger.LogError($"Type of '{item.EventType}' not found in event types");
                    continue;
                }

                var eventToPublish = EventDeserializer.Deserialize(type, item.EventBody);

                if (_filter.ShouldPublish(eventToPublish))
                {
                    eventToPublish = TransformEvent(eventToPublish, item);
                    _eventBus.Publish(eventToPublish);
                    _logger.LogInformation($"Event '{item.EventType}-{item.EventId}' Published on bus.");
                }
                else
                {
                    _logger.LogInformation($"Publishing Event '{item.EventType}-{item.EventId}' Skipped because of filter");
                }
            }
        }
        private IEvent TransformEvent(IEvent eventToPublish, OutboxItem item)
        {
            var transformer = _transformerLookUp.LookUpTransformer(eventToPublish);
            if (transformer != null)
            {
                eventToPublish = transformer.Transform(eventToPublish);
                _logger.LogInformation($"Event '{item.EventType}-{item.EventId}' Transformed");
            }
            return eventToPublish;
        }
    }