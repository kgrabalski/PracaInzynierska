using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using FoodSearch.Service.Client;

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

            FoodSearchServiceClient client = new FoodSearchServiceClient();
            Button buttonGet = FindViewById<Button>(Resource.Id.buttonGet);

            buttonGet.Click += async delegate
            {
                AlertDialog ad = (new AlertDialog.Builder(this)).Create();
                string msg = "";
                try
                {
                    var s = await client.GetStudent(1);
                    msg = s.FirstName + " " + s.LastName;

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                ad.SetMessage(msg);
                ad.SetTitle("Student");
                ad.SetButton("OK", (a, b) => ad.Hide());
                ad.Show();
            };
        }
    }
}


