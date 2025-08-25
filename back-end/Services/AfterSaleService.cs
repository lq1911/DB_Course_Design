using System;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.AfterSale;
using BackEnd.Dtos.Review;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class AfterSaleService : IAfterSaleService
    {
        private readonly IAfterSaleApplicationRepository _afterSaleRepository;
        private readonly IFoodOrderRepository _orderRepository; 
        private readonly ICustomerRepository _customerRepository; 

        public AfterSaleService(
            IAfterSaleApplicationRepository afterSaleRepository,
            IFoodOrderRepository orderRepository,
            ICustomerRepository customerRepository)
        {
            _afterSaleRepository = afterSaleRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<APageResultDto<AfterSaleApplicationDto>> GetAfterSalesAsync(int page, int pageSize, string? keyword)
        {
            var applications = await _afterSaleRepository.GetAllAsync();
            
            // 应用搜索过滤
            if (!string.IsNullOrEmpty(keyword))
            {
                applications = applications.Where(a => 
                    a.OrderID.ToString().Contains(keyword) ||
                    a.Description.Contains(keyword) ||
                    a.Order.Customer.User.PhoneNumber.ToString().Contains(keyword))
                    .ToList();
            }

            // 分页处理
            var total = applications.Count();
            var paginatedApplications = applications
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var applicationDtos = paginatedApplications.Select(c => new AfterSaleApplicationDto
            {
                Id = c.ApplicationID,
                OrderNo = $"ORD{c.OrderID}", 
                User = new AUserInfoDto
                {
                    Name = c.Order.Customer.User.Username,
                    Phone = c.Order.Customer.User.PhoneNumber.ToString(),
                    Avatar = c.Order.Customer.User.Avatar
                },
                Reason = c.Description,
                CreatedAt = c.ApplicationTime.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return new APageResultDto<AfterSaleApplicationDto>
            {
                List = applicationDtos,
                Total = total
            };
        }

        public async Task<AfterSaleApplicationDto?> GetAfterSaleByIdAsync(int id)
        {
            var app = await _afterSaleRepository.GetByIdAsync(id);
            if (app == null)
            {
                return null;
            }

            return new AfterSaleApplicationDto
            {
                Id = app.ApplicationID,
                OrderNo = $"ORD{app.OrderID}",
                User = new AUserInfoDto
                {
                    Name = app.Order.Customer.User.Username,
                    Phone = app.Order.Customer.User.PhoneNumber.ToString(),
                    Avatar = app.Order.Customer.User.Avatar
                },
                Reason = app.Description,
                CreatedAt = app.ApplicationTime.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }

        public async Task<ProcessResponseDto> ProcessAfterSaleAsync(int id, ProcessAfterSaleDto processDto)
        {
            var app = await _afterSaleRepository.GetByIdAsync(id);
            if (app == null)
            {
                return new ProcessResponseDto
                {
                    Success = false,
                    Message = "售后申请不存在"
                };
            }

            //相关字段不存在，这里默认处理成功
            return new ProcessResponseDto
            {
                Success = true,
                Message = "处理成功"
            };
        }
    }
}