using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;

using FoodSearch.Presentation.Mobile.Common;

namespace FoodSearch.Presentation.Mobile.Android
{
	[Activity (Label = "FoodSearch.Presentation.Mobile.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Forms.Init (this, bundle);
			SetPage (App.GetMainPage ());
		}
	}
}


