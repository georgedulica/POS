using POS.Models;

namespace POS.Core.Interfaces;

public interface IPaymentValidations
{
    string ProcessPaymentForMerchant(Merchant merchant);

    string ProcessPaymentForBuyer(Buyer buyer);
}