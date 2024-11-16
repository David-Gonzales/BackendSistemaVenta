namespace Application.Parameters
{
    public class RequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public RequestParameter(int pageNumber, int pageSize)
        {
            //Que el pagenumber no sea menor a 1 (validación) y el pagesize no sea mayor a 10 (para no desbordar la info)
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
