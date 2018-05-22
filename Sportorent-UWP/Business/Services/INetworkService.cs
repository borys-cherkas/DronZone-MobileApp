namespace Sportorent_UWP.Business.Services
{
    public interface INetworkService
    {
        bool IsInternetConnectionAvailable { get; }
    }
}
