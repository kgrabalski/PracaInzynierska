using System;
using Xamarin.Forms;

using System.Linq;

using FoodSearch.Presentation.Mobile.Common.Views;
using System.Collections.Generic;

namespace FoodSearch.Presentation.Mobile.Common
{
	public static class ViewLocator
	{
		private static Dictionary<Type, Page> _viewCache;

		static ViewLocator ()
		{
			_viewCache = new Dictionary<Type, Page> ();
		}

		public static Page GetView<T> () where T : ViewBase, new() 
		{	
			Type viewType = typeof(T);
			if (!_viewCache.ContainsKey(viewType))
			{
				_viewCache.Add (viewType, new T ().View);
			} 
			return _viewCache [viewType];
		}
	}
}

