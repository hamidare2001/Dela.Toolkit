namespace Dela.Toolkit.Application.Events;

public class DelegationSubscription:ISubscription
{
    private readonly Action _unsubscribeAction;

    public DelegationSubscription(Action unsubscribeAction)
    {
        _unsubscribeAction = unsubscribeAction;
    }
    public void UnSubscribe()
    {
        _unsubscribeAction();
    }
    
    public void Dispose()
    {
        UnSubscribe();
    }
}