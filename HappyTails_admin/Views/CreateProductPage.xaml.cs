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

namespace HappyTails_admin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateProductPage : Page
    {
        public CreateProductPage()
        {
            this.InitializeComponent();
        }

        private void btnAddProd_Click(object sender, RoutedEventArgs e)
        {
            AddNewProduct();
        }

        public async void AddNewProduct()
        {
            try
            {
                HappyTailsServiceRef.HappyTailsServiceClient
                    client = new HappyTailsServiceRef.HappyTailsServiceClient();
                HappyTailsServiceRef.ProductInformation product = new HappyTailsServiceRef.ProductInformation();

                Message m = new Message();

                //There is nothing for now that checks if the product is new or already exists
                //Balance and ImagePath properties are not able to fill in for now

                //Checks is the text box has been filled in, and if has has a value (not null) it will be added to the product
                if (!String.IsNullOrEmpty(txtName.Text))
                {
                    product.Name = txtName.Text;
                }
                else
                {
                    m.ShowMessage("Fill in Product name.");
                }

                if (!String.IsNullOrEmpty(txtBrand.Text))
                {
                    product.Brand = txtBrand.Text;
                }

                if (!String.IsNullOrEmpty(txtColor.Text))
                {
                    product.Color = txtColor.Text;
                }

                if (!String.IsNullOrEmpty(txtCategory.Text))
                {
                    product.AnimalCategory = txtCategory.Text;
                }

                if (!String.IsNullOrEmpty(txtDescription.Text))
                {
                    product.Description = txtDescription.Text;
                }

                if (!String.IsNullOrEmpty(txtPrice.Text))
                {
                    try
                    {
                        product.Price = decimal.Parse(txtPrice.Text);
                    }
                    catch (Exception ex)
                    {
                        m.ShowMessage("Please enter the Price in numbers.\nTechnical information: " + ex.Message);
                    }
                }

                if (!String.IsNullOrEmpty(txtLength.Text))
                {
                    try
                    {
                        product.Length = decimal.Parse(txtLength.Text);
                    }
                    catch (Exception ex)
                    {
                        m.ShowMessage("Please enter the Length in numbers.\nTechnical information: " + ex.Message);
                    }
                }

                if (!String.IsNullOrEmpty(txtHeight.Text))
                {
                    try
                    {
                        product.Height = decimal.Parse(txtHeight.Text);
                    }
                    catch (Exception ex)
                    {
                        m.ShowMessage("Please enter the Height in numbers.\nTechnical information: " + ex.Message);
                    }
                }

                await client.AddProductAsync(product);
                Message mess = new Message();
                mess.ShowMessage("Product added successfully!");

                //Clear text boxes after the creation of a new product
                txtHeight.Text = string.Empty;
                txtLength.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtColor.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtBrand.Text = string.Empty;
                txtName.Text = string.Empty;
                txtPrice.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Message mess = new Message();
                mess.ShowMessage("Unfortunately we are experiencing problems contacting the web service at the moment.\nPlease try again later.\nTechnical information: " + ex.Message);
            }
        }
    }
}
