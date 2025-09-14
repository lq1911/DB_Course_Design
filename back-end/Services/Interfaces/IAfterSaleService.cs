using System.Threading.Tasks;
using BackEnd.Dtos.AfterSale;

namespace BackEnd.Services.Interfaces
{
    public interface IAfterSaleService
    {
        Task<APageResultDto<AfterSaleApplicationDto>> GetAfterSalesAsync(int sellerId, int page, int pageSize, string? keyword);
        Task<AfterSaleApplicationDto?> GetAfterSaleByIdAsync(int id);
        Task<ProcessResponseDto> ProcessAfterSaleAsync(int id, ProcessAfterSaleDto processDto);
    }
}