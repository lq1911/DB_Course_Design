using System.Collections.Generic;

namespace BackEnd.Dtos.Review
{
    public class RPageResultDto<T>
    {
        public List<T>? List { get; set; }
        public int Total { get; set; }
    }
}