namespace Domain.Response
{
    public class PagedResponse<T>
    {
        public List<T> PagedData { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
