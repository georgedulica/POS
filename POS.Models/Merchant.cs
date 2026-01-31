namespace POS.Models;

public class Merchant
{
    public Guid MerchantId { get; set; }       

    public Guid TerminalId { get; set; }       

    public string MerchantName { get; set; }         

    public string MerchantLocation { get; set; } 
}