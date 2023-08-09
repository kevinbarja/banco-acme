namespace AcmeBank.Api.Pagging
{
    public class PagedResult<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public string? Filter { get; set; }

        public PagedResult(List<T> data, int total, PagedRequest pagedRequest)
        {
            Page = pagedRequest.Page;
            PerPage = pagedRequest.PerPage;
            Data = data;
            Total = total;
        }
    }
}
