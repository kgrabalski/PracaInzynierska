using System;
using System.Collections;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Components
{
    public class ListPicker : Picker
{
    public ListPicker()
    {
        this.SelectedIndexChanged += OnSelectedIndexChanged;
    }
 
    public static BindableProperty ItemsSourceProperty =
        BindableProperty.Create<ListPicker, IEnumerable>(o => o.ItemsSource, default(IEnumerable), BindingMode.TwoWay,null,
        new BindableProperty.BindingPropertyChangedDelegate<IEnumerable>(OnItemsSourceChanged), null, null);
 
    public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<ListPicker, object>(o => o.SelectedItem, default(object), BindingMode.TwoWay, null,
             new BindableProperty.BindingPropertyChangedDelegate<object>(OnSelectedItemChanged), null, null);
 
 
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
 
    public object SelectedItem
    {
        get { return (object)GetValue(SelectedItemProperty); }
        set
        {
            SetValue(SelectedItemProperty, value);
        }
    }
 
    private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
    {
        try
        {
            var picker = bindable as ListPicker;
            picker.Items.Clear();
            if (newvalue != null)
            {
                foreach (var item in newvalue)
                {
                    picker.Items.Add(item.ToString());
                }
            }
        }
        catch
        {
        }
    }
 
    private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
    {
        if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
        {
            SelectedItem = null;
        }
        else
        {
            SelectedItem = ItemsSource[SelectedIndex];
        }
    }
    private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var picker = bindable as ListPicker;
        if (newvalue != null)
        {
            picker.SelectedIndex = picker.ItemsSource.IndexOf(newvalue);
        }
         
    }
}
}
