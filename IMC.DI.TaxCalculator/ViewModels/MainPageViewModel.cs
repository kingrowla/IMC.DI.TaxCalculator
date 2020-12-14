using IMC.DI.TaxCalculator.API;
using IMC.DI.TaxCalculator.API.Mocks;
using IMC.DI.TaxCalculator.API.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IMC.DI.TaxCalculator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand CalculateCommand { get; set; }
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isBusy = false;

        public string Zip
        {
            get { return _zip; }
            set { SetProperty(ref _zip, value); }
        }

        private string _zip;

        public string Rate
        {
            get { return _rate; }
            set { SetProperty(ref _rate, value); }
        }

        private string _rate;

        public CalculateTaxServiceRequest CartItem
        {
            get { return _cartItem; }
            set { SetProperty(ref _cartItem, value); }
        }

        private CalculateTaxServiceRequest _cartItem;

        public double Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private double _amount;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "IMC DI Check Out";
            _navigationService = navigationService;
            _dialogService = dialogService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            CalculateCommand = new DelegateCommand(GetRates);
            Prepare();
        }
        
        async void Navigate(string name)
        {
            var parameter = new NavigationParameters();
            parameter.Add("cartItem", _cartItem);
            IsBusy = true;
            await _navigationService.NavigateAsync(name, parameter);
            IsBusy = false;
        }

        async void GetRates()
        {
            if (String.IsNullOrEmpty(_zip))
                return;
            IsBusy = true;
            try
            {
                Rate = "";
                var taxJarToken = await SecureStorage.GetAsync("taxjar_token");
                var service = new TaxRatesService(new Uri("https://api.taxjar.com/v2"), taxJarToken);
                var result = await service.GetTaxRates(_zip);
                var formatResult = Double.Parse(result.Rate.CombinedRate);
                Rate = formatResult.ToString("P", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Service Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Prepare()
        {
            _cartItem = PopulateCart();
            Amount = _cartItem.Amount;
        }
        private CalculateTaxServiceRequest PopulateCart()
        {
            //ideally, forms assignments would be made here from bindings. For this test, just using prefilled repo values.
            CalculateTaxServiceRequestRepository mock = new CalculateTaxServiceRequestRepository();
            return mock.GetTaxRequestByZip("92093");
        }
    }
}
