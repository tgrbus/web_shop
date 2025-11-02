namespace Grbus.WebShop.Domain.Common
{
    public record Error(int Code, string Message)
    {
        public static readonly Error None = new Error(0, string.Empty);
    }
}
