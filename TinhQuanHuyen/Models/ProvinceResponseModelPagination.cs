namespace TinhQuanHuyen.Models
{
    public class ProvinceResponseModelPagination
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int NumberOfRecords { get; set; }
        public int TotalRecords { get; set; }

        public List<ProvinceResponseModel> Content { get; set; }
    }
}
