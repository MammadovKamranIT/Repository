namespace Shop.DTO.Order
{
    public class OrderQueryParams
    {

        public int Page { get; set; } = 1;
        //[Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
        public string? SortDirection { get; set; }
        //[MinLength(3)]
        //[EmailAddress]
        //[StatusValidation]
        public string? Status { get; set; }
        public string? Search { get; set; }
        public int? CustomerId { get; set; }

        public void Validate()
        {
            if (Page < 1)
                Page = 1;

            if (PageSize < 1)
                PageSize = 1;

            if (PageSize > 100)
                PageSize = 100;

            if (string.IsNullOrWhiteSpace(SortDirection))
                SortDirection = "asc";

            SortDirection = SortDirection.ToLower();

            if (SortDirection != "asc" && SortDirection != "desc")
                SortDirection = "asc";
        }


    }
}
