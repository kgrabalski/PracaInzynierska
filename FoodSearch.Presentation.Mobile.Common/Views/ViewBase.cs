using System;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
	public abstract class ViewBase
	{
		protected readonly Page _view;
		public Page View {get {return _view;}}

	    protected ViewBase (Page view)
		{
			_view = view;
		}
	}

	public abstract class ViewBase<T> : ViewBase where T : Page, new()
	{
	    protected ViewBase () : base(new T())
		{
			ComposeView ((T)_view);
		}

		protected abstract void ComposeView(T view);
	}
}
