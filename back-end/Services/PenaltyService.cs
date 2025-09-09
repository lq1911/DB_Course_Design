using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.Penalty;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IStoreViolationPenaltyRepository _penaltyRepository;

        public PenaltyService(IStoreViolationPenaltyRepository penaltyRepository)
        {
            _penaltyRepository = penaltyRepository;
        }

        public async Task<List<PenaltyRecordDto>> GetPenaltiesAsync(string? keyword)
        {
            var penalties = await _penaltyRepository.GetAllAsync();
            
            if (!string.IsNullOrEmpty(keyword))
            {
                penalties = penalties.Where(p => 
                    p.PenaltyID.ToString().Contains(keyword) || 
                    p.PenaltyReason.Contains(keyword))
                    .ToList();
            }

            return penalties.Select(p => new PenaltyRecordDto
            {
                Id = $"PEN{p.PenaltyID}",
                Reason = p.PenaltyReason,
                Time = p.PenaltyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                MerchantAction = p.SellerPenalty ?? "",
                PlatformAction = p.StorePenalty ?? ""
            }).ToList();
        }

        public async Task<PenaltyRecordDto?> GetPenaltyByIdAsync(string id)
        {
            // 从ID中提取数字部分
            if (!int.TryParse(id.Replace("PEN", ""), out int penaltyId))
            {
                return null;
            }

            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);
            if (penalty == null)
            {
                return null;
            }

            return new PenaltyRecordDto
            {
                Id = $"PEN{penalty.PenaltyID}",
                Reason = penalty.PenaltyReason,
                Time = penalty.PenaltyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                MerchantAction = penalty.SellerPenalty ?? "",
                PlatformAction = penalty.StorePenalty ?? ""
            };
        }

        public async Task<AppealResponseDto?> AppealPenaltyAsync(string id, AppealDto appealDto)
        {
            // 从ID中提取数字部分
            if (!int.TryParse(id.Replace("PEN", ""), out int penaltyId))
            {
                return null;
            }
            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);
            if (penalty == null)
            {
                return null;
            }
            
            return new AppealResponseDto
            {
                Success = true,
                Message = "申诉提交成功"
            };
        }
    }
}