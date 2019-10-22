using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class EditProduct2Page : Page
    {
        private ProductInformation product;
        private HappyTailsServiceClient client = new HappyTailsServiceClient();
        Message mess = new Message();

        public EditProduct2Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            product = (ProductInformation) e.Parameter;
            txtId.Text = product.Id.ToString();
            txtName.Text = product.Name;

            //Code could be written with if statements, or with ?? expression (which uses less code than an if statement)
            //if (product.Color == null)
            //{
            //    txtColor.Text = "";
            //}
            //else
            //{
            //    txtColor.Text = product.Color;
            //}

            txtColor.Text = product.Color == null ? "" : product.Color;
            txtPrice.Text = product.Price == null ? "" : product.Price.ToString();            
            txtLength.Text = product.Length == null ? "" : product.Length.ToString();
            txtHeight.Text = product.Height == null ? "" : product.Height.ToString();
            txtBrand.Text = product.Brand == null ? "" : product.Brand;
            txtCategory.Text = product.AnimalCategory == null ? "" : product.AnimalCategory;
            txtDescription.Text = product.Description == null ? "" : product.Description;

        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            UpdateProduct(product);
        }

        //PRODUCT DOES NOT UPDATE
        //WORKS IN WCF
        public async void UpdateProduct(ProductInformation prod)
        {
            try
            {
                //This field cannot be null
                if (!String.IsNullOrEmpty(txtName.Text))
                {
                    prod.Name = txtName.Text;
                }
                else
                {
                    mess.ShowMessage("Please fill in Product name.");
                }

                prod.AnimalCategory = txtCategory.Text;
                prod.Brand = txtBrand.Text;
                prod.Color = txtColor.Text;
                prod.Description = txtDescription.Text;

                if (!String.IsNullOrEmpty(txtPrice.Text))
                {
                    try
                    {
                        prod.Price = decimal.Parse(txtPrice.Text);
                    }
                    catch (Exception ex)
                    {
                        mess.ShowMessage("Please enter the Price in numbers.\nTechnical information: " + ex.Message);

                    }
                }

                if (!String.IsNullOrEmpty(txtHeight.Text))
                {
                    try
                    {
                        prod.Height = decimal.Parse(txtHeight.Text);
                    }
                    catch (Exception ex)
                    {
                        mess.ShowMessage("Please enter the Height in numbers.\nTechnical information: " + ex.Message);

                    }
                }

                if (!String.IsNullOrEmpty(txtLength.Text))
                {
                    try
                    {
                        prod.Length = decimal.Parse(txtLength.Text);
                    }
                    catch (Exception ex)
                    {
                        mess.ShowMessage("Please enter the Length in numbers.\nTechnical information: " + ex.Message);

                    }
                }

                await client.EditProductAsync(prod);
                mess.ShowMessage("Product with id " + prod.Id + " has been updated successfully!");
            }
            catch (Exception ex)
            {
                mess.ShowMessage("Unfortunately we are experiencing problems contacting the web service at the moment.\nPlease try again later.\nTechnical information: " + ex.Message);

            }

        }
    }
}
