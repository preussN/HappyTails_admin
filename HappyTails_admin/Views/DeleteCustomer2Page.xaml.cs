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
    public sealed partial class DeleteCustomer2Page : Page
    {
        private CustomerInformation customer;
        private HappyTailsServiceClient client = new HappyTailsServiceClient();
        Message mess = new Message();

        public DeleteCustomer2Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            customer = (CustomerInformation)e.Parameter;
            if (customer != null)
            {
                txtId.Text = customer.Id.ToString();
                txtUsername.Text = customer.Username;
                txtFirstname.Text = customer.Firstname;
                txtSurname.Text = customer.Surname;
                txtEmail.Text = customer.Email;
                txtZipCode.Text = customer.ZipCode.ToString();
                txtStreet.Text = customer.Street == null ? "" : customer.Street;
            }
        }

        private void btnDeleteCust_Click(object sender, RoutedEventArgs e)
        {
            DisplayDeleteCustDialog();
        }

        private async void DisplayDeleteCustDialog()
        {
            ContentDialog deleteCustDialog = new ContentDialog
            {
                Title = "Delete Customer Permanently?",
                Content =
                    "If you delete this customer, you will not be able to recover the information. Are you sure you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteCustDialog.ShowAsync();

            //If user clicked on "Delete", the method for deleting customers will be called
            if (result == ContentDialogResult.Primary)
            {
                DeleteCustomer(customer);
            }
            else
            {
                //If the user clicked on "Cancel", nothing will happen
            }
        }

        private async void DeleteCustomer(CustomerInformation cust)
        {
            var customerId = cust.Id;
            await client.DeleteCustomerAsync(customerId);

            mess.ShowMessage("Customer with id " + customerId + " has been deleted successfully.");

            Frame.Navigate(typeof(CustomersPage));
        }
    }
}
