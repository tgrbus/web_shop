using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Application.Common
{
    public static class ErrorLists
    {
        public static readonly Error BasketNotFound = new Error(1, "Basket not found.");
        public static readonly Error DatabaseException = new Error(2, "A database error occurred.");
        public static readonly Error RecordDoesNotExistForGivenKey = new Error(3, "Record doesn't exist for given key.");
        public static readonly Error UnexpectedError = new Error(4, "Unexpected error occured");
    }
}
