using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace RecipieList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Items  
            var itemList = await App.SQLiteDb.GetItemsAsync();
            if (itemList != null)
            {
                lstItems.ItemsSource = itemList;
            }
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                Item item = new Item()
                {
                    Title = txtTitle.Text,
                    aother = txtaother.Text,
                    ingredients = txtin.Text,
                    Steps = txSteps.Text
                };

                //Add New Item  
                await App.SQLiteDb.SaveItemAsync(item);
                txtTitle.Text = item.Title;
                txtaother.Text = item.aother;
                txtin.Text = item.ingredients;
                txtTitle.Text = item.Steps;
                await DisplayAlert("Success", "Item added Successfully", "OK");
                //Get All Items  
                var itemList = await App.SQLiteDb.GetItemsAsync();
                if (itemList != null)
                {
                    lstItems.ItemsSource = itemList;
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter name!", "OK");
            }
        }
        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemId.Text))
            {
                //Get Item  
                var item = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtItemId.Text));
                if (item != null)
                {
                    txtTitle.Text = item.Title;
                    txtaother.Text = item.aother;
                    txtin.Text = item.ingredients;
                    txtTitle.Text = item.Steps;
                    string action = await DisplayActionSheet("Title: " + item.Title, "OK", null, "Ingredients: " + item.ingredients, "Steps: " + item.Steps, "Aother: " + item.aother);
                    Debug.WriteLine("Action: " + action);
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter ItemID", "OK");
            }
        }
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemId.Text))
            {
                Item item = new Item()
                {
                    ItemID = Convert.ToInt32(txtItemId.Text),
                    Title = txtTitle.Text,
                    aother = txtaother.Text,
                    ingredients = txtin.Text,
                    Steps = txSteps.Text
                };

                //Update Item  
                await App.SQLiteDb.SaveItemAsync(item);

                txtItemId.Text = string.Empty;
                txtTitle.Text = string.Empty;
                txtaother.Text = string.Empty;
                txtin.Text = string.Empty;
                txSteps.Text = string.Empty;
                await DisplayAlert("Success", "Item Updated Successfully", "OK");
                //Get All Items  
                var itemList = await App.SQLiteDb.GetItemsAsync();
                if (itemList != null)
                {
                    lstItems.ItemsSource = itemList;
                }

            }
            else
            {
                await DisplayAlert("Required", "Please Enter ItemID", "OK");
            }
        }
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemId.Text))
            {
                //Get Item  
                var item = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtItemId.Text));
                if (item != null)
                {
                    //Delete Item  
                    await App.SQLiteDb.DeleteItemAsync(item);
                    txtItemId.Text = string.Empty;
                    await DisplayAlert("Success", "Item Deleted", "OK");

                    //Get All Items  
                    var itemList = await App.SQLiteDb.GetItemsAsync();
                    if (itemList != null)
                    {
                        lstItems.ItemsSource = itemList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter ItemID", "OK");
            }
        }
    }
}
