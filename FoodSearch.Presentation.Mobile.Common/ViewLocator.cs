using FoodSearch.Presentation.Mobile.Common.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common
{
	public static class ViewLocator
	{
		private static readonly Dictionary<Type, Page> ViewCache;
		private static NavigationPage _startScreen;

	    static ViewLocator ()
		{
			ViewCache = new Dictionary<Type, Page> ();
		}

		private static Page GetView<T> (bool newInstance = false) where T : Page, new()
		{
            if (newInstance)
                return new T();

			Type viewType = typeof(T);
			if (!ViewCache.ContainsKey(viewType))
			{
				ViewCache.Add (viewType, new T ());
			} 
            return ViewCache[viewType];
		}

		public static Page Main { get { return GetView<MainView>(); } }
		public static Page Authorize { get { return GetView<AuthorizeView>(true); } }
        public static Page RestaurantList { get { return GetView<RestaurantsListView>(true); } }
        public static Page RestaurantMenu { get { return GetView<RestaurantMenuView>(true); } }
        public static Page Basket { get { return GetView<BasketView>(true); } } 
        public static Page Order { get { return GetView<OrderView>(true); } }
        public static Page OrderSucceded { get { return GetView<OrderSuccededView>(true); } }
        public static Page OrderFailed { get { return GetView<OrderFailedView>(); } }
        public static Page Payment { get { return GetView<PaymentView>(); } }
        public static Page UserPanel { get { return GetView<UserPanelView>(true); } }
        public static Page OrderDetails { get { return GetView<OrderDetailsView>(true); } }

		public static Page StartScreen { get { return _startScreen ?? (_startScreen = new NavigationPage (Main)); } }
	}
}

