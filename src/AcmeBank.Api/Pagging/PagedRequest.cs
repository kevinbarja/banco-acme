namespace AcmeBank.Api.Pagging
{
    public class PagedRequest
    {
        private int _page = 1;

        public int Page
        {
            get => _page <= 0 ? 1 : _page;
            set => _page = value;
        }

        private int _perPage = 50;

        public int PerPage
        {
            get => _perPage <= 0 ? 50 : _perPage;
            set => _perPage = value;
        }
    }
}
