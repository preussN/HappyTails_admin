using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HappyTails_admin.HappyTailsServiceRef;

namespace HappyTails_admin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCustomerPage : Page
    {
        public EditCustomerPage()
        {
            this.InitializeComponent();
            LoadCustomers();
        }

        public async void LoadCustomers()
        {
            try
            {
                HappyTailsServiceRef.HappyTailsServiceClient
                    client = new HappyTailsServiceRef.HappyTailsServiceClient();
                custList.ItemsSource = await client.GetAllCustomersAsync();

            }
            catch (Exception ex)
            {
                Message mess = new Message();
                mess.ShowMessage(
                    "Unfortunately we are experiencing problems contacting the web service at the moment.\nPlease try again later.\nTechnical information: " +
                    ex.Message);

            }
        }

        private void custList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem == null)
            {
                return;
            }

            var selectedCust = (CustomerInformation)e.ClickedItem;
            Frame.Navigate(typeof(EditCustomer2Page), selectedCust);
        }
    }
}
