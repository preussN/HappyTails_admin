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
    public sealed partial class EditCustomer2Page : Page
    {
        private CustomerInformation customer;
        private HappyTailsServiceClient client = new HappyTailsServiceClient();
        Message mess = new Message();

        public EditCustomer2Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            customer = (CustomerInformation) e.Parameter;
            txtId.Text = customer.Id.ToString();
            txtUsername.Text = customer.Username;
            txtFirstname.Text = customer.Firstname;
            txtSurname.Text = customer.Surname;
            txtEmail.Text = customer.Email;
            //The current password should not be shown in the future
            //txtPassword.Text = customer.Password;
            txtPassword.Text = "";
            txtZipCode.Text = customer.ZipCode.ToString();

            txtStreet.Text = customer.Street == null ? "" : customer.Street;

        }

        private void btnUpdateCust_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomer(customer);
        }

        private async void UpdateCustomer(CustomerInformation cust)
        {
            try
            {
                //Firstname and Username will not be able to change

                //This field cannot be null
                if (!String.IsNullOrEmpty(txtSurname.Text))
                {
                    cust.Surname = txtSurname.Text;
                }
                else
                {
                    mess.ShowMessage("Please fill in Customers Surname.");
                }

                //This field cannot be null
                if (!String.IsNullOrEmpty(txtEmail.Text))
                {
                    cust.Email = txtEmail.Text;
                }
                else
                {
                    mess.ShowMessage("Please fill in Customers Email.");
                }

                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    cust.Password = txtPassword.Text;
                }

                if (!String.IsNullOrEmpty(txtZipCode.Text))
                {
                    try
                    {
                        cust.ZipCode = int.Parse(txtZipCode.Text);
                    }
                    catch (Exception ex)
                    {
                        mess.ShowMessage("Please enter the ZipCode in numbers.\nTechnical information: " + ex.Message);
                    }
                }

                cust.Street = txtStreet.Text;

                await client.EditCustomerAsync(cust);
                mess.ShowMessage("Customer with id " + cust.Id + " has been updated successfully!");
            }
            catch (Exception ex)
            {
                mess.ShowMessage("Unfortunately we are experiencing problems contacting the web service at the moment.\nPlease try again later.\nTechnical information: " + ex.Message);
            }
        }

    }
}
