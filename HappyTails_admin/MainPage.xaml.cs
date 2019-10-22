using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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
using HappyTails_admin.Views;
using Microsoft.Toolkit.Uwp;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HappyTails_admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        #region NavigationView event handlers
        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Main_Page")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }

            contentFrame.Navigate(typeof(ProductsPage));
        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock itemContent = args.InvokedItem as TextBlock;

            if (itemContent != null)
            {
                switch (itemContent.Tag)
                {
                    case "Nav_Home":
                        contentFrame.Navigate(typeof(ProductsPage));
                        break;

                    case "Nav_CreateProduct":
                        contentFrame.Navigate(typeof(CreateProductPage));
                        break;

                    case "Nav_EditProduct":
                        contentFrame.Navigate(typeof(EditProductPage));
                        break;

                    case "Nav_DeleteProduct":
                        contentFrame.Navigate(typeof(DeleteProductPage));
                        break;

                    case "Nav_Customers":
                        contentFrame.Navigate(typeof(CustomersPage));
                        break;

                    case "Nav_CreateCustomer":
                        contentFrame.Navigate(typeof(CreateCustomerPage));
                        break;

                    case "Nav_EditCustomer":
                        contentFrame.Navigate(typeof(EditCustomerPage));
                        break;

                    case "Nav_DeleteCustomer":
                        contentFrame.Navigate(typeof(DeleteCustomerPage));
                        break;

                    case "Nav_Purchases":
                        contentFrame.Navigate(typeof(PurchasesPage));
                        break;
                }
            }
        }
        #endregion
    }
}
