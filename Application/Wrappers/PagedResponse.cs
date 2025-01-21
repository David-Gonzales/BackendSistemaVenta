namespace Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // Agrega esta propiedad para el total de registros
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Calcula el total de páginas

        public PagedResponse(T data, int pageNumber, int pageSize, int totalCount) : base(data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            //this.Data = data;
            //this.Message = null;
            //this.Succeeded = true;
            //this.Errors = null;
        }
    }
}
