using IMC.DI.TaxCalculator.API;
using IMC.DI.TaxCalculator.API.Models;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IMC.DI.TaxCalculator.ViewModels
{
    public class CheckOutViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isBusy = false;

        public double Tax
        {
            get { return _tax; }
            set { SetProperty(ref _tax, value); }
        }

        private double _tax;

        public double Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private double _amount;

        public double Shipping
        {
            get { return _shipping; }
            set { SetProperty(ref _shipping, value); }
        }

        private double _shipping;

        public double AmountTotal
        {
            get { return _amountTotal; }
            set { SetProperty(ref _amountTotal, value); }
        }

        private double _amountTotal;

        public CalculateTaxServiceRequest CartItem
        {
            get { return _cartItem; }
            set { SetProperty(ref _cartItem, value); }
        }

        private CalculateTaxServiceRequest _cartItem;

        public CalculateTaxServiceResponse CartItemResponse
        {
            get { return _cartItemResponse; }
            set { SetProperty(ref _cartItemResponse, value); }
        }

        private CalculateTaxServiceResponse _cartItemResponse;

        public CategoriesServiceResponse CategoriesResponse
        {
            get { return _categoriesResponse; }
            set { SetProperty(ref _categoriesResponse, value); }
        }

        private CategoriesServiceResponse _categoriesResponse;
        public CheckOutViewModel(INavigationService navigationService, IPageDialogService dialogService)
             : base(navigationService)
        {
            Title = "IMC DI Calculate Tax";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("cartItem"))
            {
                CartItem = (CalculateTaxServiceRequest)parameters["cartItem"];
                CartItemResponse = await CalculateTax(CartItem);
                BuildCheckOut(CartItemResponse);
            }
        }

        async Task<CalculateTaxServiceResponse> CalculateTax(CalculateTaxServiceRequest cartItem)
        {
            try
            {
                var taxJarToken = await SecureStorage.GetAsync("taxjar_token");
                var service = new CalculateTaxService(new Uri("https://api.taxjar.com/v2"), taxJarToken);
                return _cartItemResponse = await service.GetTaxCalculation(cartItem);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Service Error", ex.Message, "OK");
            }
            return null;
        }
        private void BuildCheckOut(CalculateTaxServiceResponse cartObject)
        {
            Amount = cartObject.Tax.TaxableAmount;
            Shipping = cartObject.Tax.Shipping;
            Tax = cartObject.Tax.AmountToCollect;
            AmountTotal = cartObject.Tax.OrderTotalAmount + Tax;
        }
    }
}
