using Acr.XamForms.Mobile.Net;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using System;
using System.Net;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class NetworkAvailabilityService : INetworkAvailabilityService
    {
        private readonly INetworkService _networkService;
        private readonly IUserDialogService _dialogService;

        public NetworkAvailabilityService(INetworkService networkService, IUserDialogService dialogService)
        {
            _networkService = networkService;
            _dialogService = dialogService;
            _networkService.StatusChanged += HandleStatusChanged;
        }

        private void HandleStatusChanged (object sender, EventArgs e)
        {
            if (!_networkService.IsConnected) CloseApp();
        }

        public void CloseApp()
        {
            _dialogService.Alert("Aplikacja wymaga połaczenia z internetem do poprawnego działania.\nWciśnij OK aby zamknąć aplikację", "Brak połączenia z siecią", "OK", () => {
                throw new WebException();
            });
        }

        public bool IsConnected { get { return _networkService.IsConnected; } }
    }
}

