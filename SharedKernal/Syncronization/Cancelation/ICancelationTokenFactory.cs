namespace SharedKernal.Syncronization.Cancelation
{
    public interface ICancelationTokenFactory
    {
        CancellationToken GetCancellationToken();
    }
}
