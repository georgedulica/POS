using POS.Core.Interfaces;
using POS.Models;

namespace POS.Infrastructure.Services;

public class PaymentValidations : IPaymentValidations
{
    public string ProcessPaymentForMerchant(Merchant merchant)
    {
        var merchantDataProblemMessage = ValidateMerchantData(merchant);
        if (!string.IsNullOrEmpty(merchantDataProblemMessage))
        {
            return merchantDataProblemMessage;
        }

        return string.Empty;
    }

    public string ProcessPaymentForBuyer(Buyer buyer)
    {
        var buyerDataProblemMessage = ValidateBuyerData(buyer);
        if (!string.IsNullOrEmpty(buyerDataProblemMessage))
        {
            return buyerDataProblemMessage;
        }

        return string.Empty;
    }

    private string ValidateBuyerData(Buyer buyer)
    {
        if (buyer == null)
        {
            return "Buyer settings has not been added";
        }

        if (string.IsNullOrEmpty(buyer?.PAN))
        {
            return "The Primary Account Number (PAN) has not been added";
        }

        if (string.IsNullOrEmpty(buyer?.Name))
        {
            return "The name has not been added";
        }

        if (buyer?.ExpirationDate == null)
        {
            return "The Expiration date has not been added";
        }

        if (string.IsNullOrEmpty(buyer?.PIN))
        {
            return "PIN has not been added";
        }

        return string.Empty;
    }

    private string ValidateMerchantData(Merchant merchant)
    {       
        if (merchant == null)
        {
            return "Merchant settings has not been added";
        }

        if (merchant?.MerchantId == null || merchant?.MerchantId == Guid.Empty)
        {
            return "Merchant Id is not valid";
        }

        if (merchant?.TerminalId == null || merchant?.TerminalId == Guid.Empty)
        {
            return "Terminal Id is not valid";
        }

        if (string.IsNullOrEmpty(merchant?.MerchantName))
        {
            return "The merchant name has not been added";
        }

        if (string.IsNullOrEmpty(merchant?.MerchantLocation))
        {
            return "The merchant location has not been added";
        }

        return string.Empty;
    }
}