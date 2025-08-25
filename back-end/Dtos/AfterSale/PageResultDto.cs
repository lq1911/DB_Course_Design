using System.Collections.Generic;

namespace BackEnd.Dtos.AfterSale
{
    public class APageResultDto<T>
    {
        public List<T>? List { get; set; }
        public int Total { get; set; }
    }
}