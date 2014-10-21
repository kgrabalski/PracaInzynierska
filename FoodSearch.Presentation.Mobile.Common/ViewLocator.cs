using System;

using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Contracts;

using Xamarin.Forms;

using System.Linq;

using FoodSearch.Presentation.Mobile.Common.Views;
using System.Collections.Generic;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common
{
	public static class ViewLocator
	{
		private static Dictionary<Type, Page> _viewCache;
		private static NavigationPage _startScreen;
        public readonly static IKernel DependencyResolver;

	    static ViewLocator ()
		{
			_viewCache = new Dictionary<Type, Page> ();
            DependencyResolver = new StandardKernel();
            DependencyResolver.Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();
            DependencyResolver.Bind<RestaurantListViewModel>().To<RestaurantListViewModel>();
            DependencyResolver.Bind<DishListViewModel>().To<DishListViewModel>();
            DependencyResolver.Bind<OpinionListViewModel>().To<OpinionListViewModel>();
		}

		private static Page GetView<T> (bool newInstance = false) where T : Page, new()
		{
		    if (newInstance)
		    {
		        return CurrentView = new T();
		    }

			Type viewType = typeof(T);
			if (!_viewCache.ContainsKey(viewType))
			{
				_viewCache.Add (viewType, new T ());
			} 
			CurrentView = _viewCache [viewType];
		    return CurrentView;
		}
        
        public static Page CurrentView { get; private set; }
		public static Page Main { get { return GetView<MainView>(); } }
//		public static Page Authorize { get { return GetView<AuthorizeView>(); } }
        public static Page RestaurantList { get { return GetView<RestaurantsListView>(true); } }
        public static Page RestaurantMenu { get { return GetView<RestaurantMenuView>(true); } }

		public static Page StartScreen { get { return _startScreen ?? (_startScreen = new NavigationPage (Main)); } }
	}
}

