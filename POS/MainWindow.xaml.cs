using POS.Core.Interfaces;
using POS.Core.Services;
using POS.Infrastructure.Services;
using POS.Models;
using System.Windows;
using System.Windows.Controls;

namespace POS;

public partial class MainWindow : Window
{
    private readonly IPaymentValidations _paymentValidations;
    private readonly IBankingService _bankingService;
    private Merchant merchant;

    public MainWindow()
    {
        _paymentValidations = new PaymentValidations();
        _bankingService = new BankingService();
        InitializeComponent();
    }

    private void SaveConfigurationbButton_Click(object sender, RoutedEventArgs e)
    {
        var merchantGuid = Guid.TryParse(MerchantIdTextBox.Text, out var merchantId)
            ? merchantId
            : Guid.Empty;

        var terminalGuid = Guid.TryParse(TerminalIdTextBox.Text, out var terminalId)
            ? terminalId
            : Guid.Empty;

        merchant = new Merchant
        {
            MerchantId = merchantGuid,
            TerminalId = terminalGuid,
            MerchantLocation = MerchantLocationTextBox.Text,
            MerchantName = MerchantNameTextBox.Text
        };

        var merchantErrorMessage = _paymentValidations.ProcessPaymentForMerchant(merchant);
        if (!string.IsNullOrEmpty(merchantErrorMessage))
        {
            ConfigurationTextBox.Text = merchantErrorMessage;
        }
    }

    private async void PayButton_Click(object sender, RoutedEventArgs e)
    {
        if (merchant == null)
        {
            ConfigurationTextBox.Text = "POS has not been configured";
            return;
        }

        var buyer = new Buyer
        {
            PAN = PanTextBox.Text,
            Name = NameTextBox.Text,
            PIN = PinTextBox.Password,
            ExpirationDate = $"{ExpMonthTextBox.Text}/{ExpYearTextBox.Text}",
        };

        var merchantErrorMessage = _paymentValidations.ProcessPaymentForMerchant(merchant);
        if (!string.IsNullOrEmpty(merchantErrorMessage))
        {
            ConfigurationTextBox.Text = merchantErrorMessage;
            return;
        }

        var buyerErrorMessage = _paymentValidations.ProcessPaymentForBuyer(buyer);
        if (!string.IsNullOrEmpty(buyerErrorMessage))
        {
            PaymentTextBox.Text = merchantErrorMessage;
            return;
        }

        var validatedData = new ValidatedData
        {
            Merchant = merchant,
            Buyer = buyer
        };

        var paymentProcessing = await _bankingService.ProcessPayment(validatedData);
    }
}