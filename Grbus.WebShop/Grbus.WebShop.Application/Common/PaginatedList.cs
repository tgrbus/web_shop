namespace Grbus.WebShop.Application.Common
{
    public record PaginatedList<T>
    {
        public List<T> Items { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }

        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }

    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            return PaginatedList<T>.Create(source, pageNumber, pageSize);
        }
    }
}
