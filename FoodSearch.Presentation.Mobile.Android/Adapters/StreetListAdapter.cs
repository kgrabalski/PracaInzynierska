
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodSearch.Service.Contracts.Response;

namespace FoodSearch.Presentation.Mobile.Android.Adapters
{
	public class StreetListAdapter : BaseAdapter
	{
		private Activity context;
		public List<Street> Items;

		public StreetListAdapter(Activity context) : base(){
			this.context = context;
			Items = new List<Street> () {};
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return position;
		}

		public override long GetItemId (int position)
		{
			return Items [position].StreetId;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = Items [position];
			var view = context.LayoutInflater.Inflate (Resource.Layout.StreetItem, parent, false) as TableLayout;
			var itemName = view.FindViewById<TextView> (Resource.Id.itemName);

			itemName.Text = item.Name;
			view.Tag = item.StreetId;

			return view;
		}

		public override int Count {
			get { return Items.Count; }
		}	
	}
}

