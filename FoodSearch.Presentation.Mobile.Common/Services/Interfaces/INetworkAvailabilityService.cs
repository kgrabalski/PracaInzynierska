
namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface INetworkAvailabilityService
    {
        bool IsConnected { get; }
        void CloseApp();
    }
}

