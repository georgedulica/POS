using POS.Models;

namespace POS.Core.Interfaces;

public interface IBankingService
{
    Task<string> ProcessPayment(ValidatedData validatedData);
}