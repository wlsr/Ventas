namespace Ventas.ViewModels
{
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Helpers;
    using Xamarin.Forms;
    using System;
    using Views;

    public class ProductItemViewModel : Product
    {
        #region Attributes
        private ApiService apiService;
        #endregion
        #region Constructs
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion
        #region Commands
        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await App.Navigator.PushAsync(new EditProductPage());
        }

        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(Languages.Confirm,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);
            if(!answer)
            {
                return;
            }
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, 
                    Languages.Accept);
                return;
            }
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller,this.ProductId, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
               
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            var productViewModel = ProductsViewModel.GetInstance();
            var deletedProduct = productViewModel.MyProducts.Where(p => p.ProductId == this.ProductId).FirstOrDefault();
            if (deletedProduct != null)
            {
                productViewModel.MyProducts.Remove(deletedProduct);
            }
	    productViewModel.RefreshList();

        }
        #endregion
    }
}
