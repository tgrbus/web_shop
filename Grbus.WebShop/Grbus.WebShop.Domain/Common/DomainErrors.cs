namespace Grbus.WebShop.Domain.Common
{
    public static class DomainErrors
    {
        public static readonly Error StockShortage = new Error(1, "There are not enough products in stock");
    }
}
