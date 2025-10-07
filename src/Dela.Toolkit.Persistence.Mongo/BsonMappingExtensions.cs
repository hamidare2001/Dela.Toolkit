using System.Reflection;

namespace Dela.Toolkit.Persistence.Mongo;

public static class BsonMappingExtensions
{
    public static void RegisterAll(Assembly assembly)
    {
        var mappings = assembly.GetExportedTypes()
            .Where(type => typeof(IBsonMapping).IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false })
            .Select(Activator.CreateInstance)
            .OfType<IBsonMapping>()
            .ToList();

        mappings.ForEach(mapping => mapping.Register());
    }
}

/*
Sample 
 
public class RuleMapping : IBsonMapping
{
    public void Register()
    {
        BsonClassMap.RegisterClassMap<Specification<ApplicantCondition>>(map =>
        {
            map.AutoMap();
            map.SetIsRootClass(true);
            map.AddKnownType(typeof(WorkingExperience));
            map. AddKnownType(typeof(AndSpecification<ApplicantCondition>));
            map. AddKnownType(typeof(OrSpecification<ApplicantCondition>));
        });
    }
}

using in unit test for example

BsonMappingExtensions.RegisterAll(typeof(UnitTest.Assembly));

*/