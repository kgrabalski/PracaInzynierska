using System;
using Acr.XamForms.Mobile.Net;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;
using Acr.XamForms.UserDialogs;
using System.Net;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public static class NetworkAvailabilityService
    {
        private static readonly INetworkService _networkService;
        private static readonly IUserDialogService _dialogService;

        static NetworkAvailabilityService()
        {
            _networkService = DependencyResolver.Current.Get<INetworkService>();
            _dialogService = DependencyResolver.Current.Get<IUserDialogService>();
            _networkService.StatusChanged += HandleStatusChanged;
        }

        private static void HandleStatusChanged (object sender, EventArgs e)
        {
            if (!_networkService.IsConnected) CloseApp();
        }

        public static void CloseApp()
        {
            _dialogService.Alert("Aplikacja wymaga połaczenia z internetem do poprawnego działania.\nWciśnij OK aby zamknąć aplikację", "Brak połączenia z siecią", "OK", () => {
                throw new WebException();
            });
        }

        public static bool IsConnected { get { return _networkService.IsConnected; } }
    }
}

