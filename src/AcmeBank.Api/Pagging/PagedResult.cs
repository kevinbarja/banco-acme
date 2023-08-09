namespace AcmeBank.Api.Pagging
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public List<T> Data { get; set; } = new List<T>();

        public PagedResult(List<T> data, PagedRequest pagedRequest)
        {
            Page = pagedRequest.Page;
            PerPage = pagedRequest.PerPage;
            Data = data;
        }
    }
}
