using System;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface INetworkAvailabilityService
    {
        void HandleStatusChanged (object sender, EventArgs e);
        void CloseApp();
        bool IsConnected { get; }
    }
}