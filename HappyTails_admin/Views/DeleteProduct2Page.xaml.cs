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
    public sealed partial class DeleteProduct2Page : Page
    {
        private ProductInformation product;
        private HappyTailsServiceClient client = new HappyTailsServiceClient();
        Message mess = new Message();

        public DeleteProduct2Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            product = (ProductInformation) e.Parameter;
            if (product != null)
            {
                txtId.Text = product.Id.ToString();
                txtBrand.Text = product.Brand == null ? "" : product.Brand;
                txtCategory.Text = product.AnimalCategory == null ? "" : product.AnimalCategory;
                txtHeight.Text = product.Height.ToString();
                txtColor.Text = product.Color == null ? "" : product.Color;
                txtDescription.Text = product.Description == null ? "" : product.Description;
                txtPrice.Text = product.Price.ToString();
                txtLength.Text = product.Length.ToString();
                txtName.Text = product.Name;
            }
        }

        private void btnDeleteProd_Click(object sender, RoutedEventArgs e)
        {
            DisplayDeleteProdDialog();
        }

        private async void DisplayDeleteProdDialog()
        {
            ContentDialog deleteProdDialog = new ContentDialog
            {
                Title = "Delete Product Permanently?",
                Content =
                    "If you delete this product, you will not be able to recover the information. Are you sure you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteProdDialog.ShowAsync();

            //If user clicked on "Delete", the method for deleting customers will be called
            if (result == ContentDialogResult.Primary)
            {
                DeleteProduct(product);
            }
            else
            {
                //If the user clicked on "Cancel", nothing will happen
            }
        }

        private async void DeleteProduct(ProductInformation prod)
        {
            var productId = prod.Id;
            await client.DeleteProductAsync(productId);

            mess.ShowMessage("Product with id " + productId + " has been deleted successfully.");

            Frame.Navigate(typeof(ProductsPage));
        }
    }
}
