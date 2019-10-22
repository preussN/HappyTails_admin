using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HappyTails_admin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateCustomerPage : Page
    {
        public CreateCustomerPage()
        {
            this.InitializeComponent();
        }

        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            AddNewCustomer();
        }

        public async void AddNewCustomer()
        {
            try
            {
                HappyTailsServiceRef.HappyTailsServiceClient
                    client = new HappyTailsServiceRef.HappyTailsServiceClient();
                HappyTailsServiceRef.CustomerInformation customer = new HappyTailsServiceRef.CustomerInformation();

                //Customers with a username and/or email that already exists can also be added - should not work in the future
                //Checks is the text box has been filled in, and if has has a value (not null) it will be added to the customer
                if (!String.IsNullOrEmpty(txtFirstname.Text))
                {
                    customer.Firstname = txtFirstname.Text;
                }
                else
                {
                    Message m = new Message();
                    m.ShowMessage("Fill in Firstname.");
                }

                if (!String.IsNullOrEmpty(txtSurname.Text))
                {
                    customer.Surname = txtSurname.Text;
                }
                else
                {
                    Message m = new Message();
                    m.ShowMessage("Fill in Surname.");
                }

                if (!String.IsNullOrEmpty(txtUsername.Text))
                {
                    customer.Username = txtUsername.Text;
                }
                else
                {
                    Message m = new Message();
                    m.ShowMessage("Fill in Username.");
                }

                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    customer.Password = txtPassword.Text;
                }
                else
                {
                    Message m = new Message();
                    m.ShowMessage("Fill in Password.");
                }

                if (!String.IsNullOrEmpty(txtEmail.Text))
                {
                    customer.Email = txtEmail.Text;
                }
                else
                {
                    Message m = new Message();
                    m.ShowMessage("Fill in Email.");
                }
                if (!String.IsNullOrEmpty(txtStreet.Text))
                {
                    customer.Street = txtStreet.Text;
                }
                if (!String.IsNullOrEmpty(txtZipCode.Text))
                {
                    try
                    {
                        customer.ZipCode = int.Parse(txtZipCode.Text);
                    }
                    catch (Exception ex)
                    {
                        Message m = new Message();
                        m.ShowMessage("Please enter the ZipCode in numbers.");
                    }
                }
                
                await client.AddCustomerAsync(customer);
                Message mess = new Message();
                mess.ShowMessage("Customer added successfully!");

                //Clear text boxes after the creation of a new customer
                txtFirstname.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtStreet.Text = string.Empty;
                txtSurname.Text = string.Empty;
                txtZipCode.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtEmail.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Message mess = new Message();
                mess.ShowMessage("Unfortunately we are experiencing problems contacting the web service at the moment.\nPlease try again later.\nTechnical information: " + ex.Message);
            }
        }
    }
}
