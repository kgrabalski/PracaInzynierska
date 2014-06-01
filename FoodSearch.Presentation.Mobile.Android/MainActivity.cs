using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using FoodSearch.Service.Client;
using FoodSearch.Presentation.Mobile.Android.Adapters;

namespace FoodSearch.Presentation.Mobile.Android
{
    [Activity(Label = "FoodSearch.Presentation.Mobile.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			Button btnSearchStreet = FindViewById<Button>(Resource.Id.searchStreet);
			EditText searchStreet = FindViewById<EditText> (Resource.Id.streetName);
			ListView streetList = FindViewById<ListView> (Resource.Id.streetsList);
			ListView streetNumbersList = FindViewById<ListView> (Resource.Id.streetNumbersList);
			FoodSearchServiceClient client = new FoodSearchServiceClient();

			btnSearchStreet.Click += async delegate {
				StreetListAdapter adapter = new StreetListAdapter(this);
				adapter.Items = await client.GetStreets (searchStreet.Text);
				streetList.Adapter = adapter;

				StreetNumberListAdapter adapter2 = new StreetNumberListAdapter(this);
				adapter2.Items = new System.Collections.Generic.List<FoodSearch.Service.Contracts.Response.StreetNumber>();
				streetNumbersList.Adapter = adapter2;
			};

			streetList.ItemClick += async (sender, e) => 
			{
				StreetNumberListAdapter adapter = new StreetNumberListAdapter(this);
				adapter.Items = await client.GetStreetNumbers ((int)e.Id);
				streetNumbersList.Adapter = adapter;
			};

			streetNumbersList.ItemClick += (sender, e) => 
			{
				AlertDialog ad = (new AlertDialog.Builder(this)).Create ();
				ad.SetMessage (e.Id.ToString ());
				ad.SetTitle ("AddressId");
				ad.SetButton ("OK", (x,y) => ad.Hide ());
				ad.Show ();
			};


        }
    }
}


