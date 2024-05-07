using SocialApp.Domain.Entities;

namespace SocialApp.Dtos.PaginationDtos
{
    public class PaginationResponseDto<T>
    {
        public int PageNumber { get; init; }


        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalRecords { get; init; }
        public IEnumerable<T> Data { get; set; }
        public PaginationResponseDto(int pageNumber, int pageSize, int totalRecords, IEnumerable<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
            TotalRecords = totalRecords;
            Data = data;
        }
     
       
    }
}
