using System;

using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Contracts;

using Xamarin.Forms;

using System.Linq;

using FoodSearch.Presentation.Mobile.Common.Views;
using System.Collections.Generic;

namespace FoodSearch.Presentation.Mobile.Common
{
	public static class ViewLocator
	{
		private static Dictionary<Type, Page> _viewCache;
		private static NavigationPage _startScreen;

	    static ViewLocator ()
		{
			_viewCache = new Dictionary<Type, Page> ();
		}

		private static Page GetView<T> (bool newInstance = false) where T : ViewBase, new()
		{
		    if (newInstance)
		    {
		        return CurrentView = new T().View;
		    }

			Type viewType = typeof(T);
			if (!_viewCache.ContainsKey(viewType))
			{
				_viewCache.Add (viewType, new T ().View);
			} 
			CurrentView = _viewCache [viewType];
		    return CurrentView;
		}
        
        public static Page CurrentView { get; private set; }
		public static Page Main { get { return GetView<MainView>(); } }
		public static Page Authorize { get { return GetView<AuthorizeView>(); } }
        public static Page RestaurantList { get { return GetView<RestaurantsListView>(true); } }
        public static Page DishList { get { return GetView<DishListView>(true); } }
        public static Page OpinionList { get { return GetView<OpinionListView>(true); } }
        public static Page RestaurantMenu { get { return GetView<RestaurantMenuView>(true); } }

		public static Page StartScreen { get { return _startScreen ?? (_startScreen = new NavigationPage (Main)); } }
	}
}

