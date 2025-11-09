using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Application.Common
{
    public static class ApplicationErrors
    {
        public static readonly Error BasketNotFound = new Error(1000, "Basket not found.");
        public static readonly Error DatabaseException = new Error(1001, "A database error occurred.");
        public static readonly Error RecordDoesNotExistForGivenKey = new Error(1002, "Record doesn't exist for given key.");
        public static readonly Error UnexpectedError = new Error(1003, "Unexpected error occured");
    }
}
