using BackEnd.Dtos.ViolationPenalty;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class Supervise_Service : ISupervise_Service
    {
        private readonly IAdministratorRepository _administratorRepository;

        public Supervise_Service(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public async Task<IEnumerable<GetViolationPenaltyInfo>> GetViolationPenaltiesForAdminAsync(int adminId)
        {
            var penaltiesFromDb = await _administratorRepository.GetViolationPenaltiesByAdminIdAsync(adminId);

            if (penaltiesFromDb == null || !penaltiesFromDb.Any())
            {
                return Enumerable.Empty<GetViolationPenaltyInfo>();
            }

            var penaltyDtos = penaltiesFromDb.Select(penalty => new GetViolationPenaltyInfo
            {
                PunishmentId = penalty.PenaltyID.ToString(),
                StoreName = penalty.Store.StoreName,
                Reason = penalty.PenaltyReason ?? "-",
                MerchantPunishment = penalty.SellerPenalty ?? "-",
                StorePunishment = penalty.StorePenalty ?? "-",
                PunishmentTime = penalty.PenaltyTime.ToString("yyyy-MM-dd HH:mm"),
                Status = penalty.ViolationPenaltyState == ViolationPenaltyState.Pending ? "待处理" : "已完成",
                ProcessingNote = penalty.PenaltyNote ?? "-"
            });

            return penaltyDtos;
        }

        public async Task<SetViolationPenaltyInfoResponse> UpdateViolationPenaltyAsync(SetViolationPenaltyInfo request)
        {
            try
            {
                if (request == null)
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "请求数据不能为空"
                    };
                }

                if (!int.TryParse(request.PunishmentId, out int punishmentId))
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "无效的处罚编号格式"
                    };
                }

                var existingPenalty = await _administratorRepository.GetViolationPenaltyByIdAsync(punishmentId);
                if (existingPenalty == null)
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "未找到指定的违规处罚"
                    };
                }

                var newState = request.Status switch
                {
                    "待处理" => ViolationPenaltyState.Pending,
                    "已完成" => ViolationPenaltyState.Completed,
                    _ => (ViolationPenaltyState?)null
                };

                if (newState == null)
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "无效的状态值，只能是：待处理、已完成"
                    };
                }

                // 检查是否已经处理完成
                if (existingPenalty.ViolationPenaltyState == ViolationPenaltyState.Completed)
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "该违规处罚已经处理完成，无法重复处理"
                    };
                }

                existingPenalty.ViolationPenaltyState = newState.Value;
                existingPenalty.PenaltyNote = request.ProcessingNote;
                existingPenalty.PenaltyReason = request.Reason;
                existingPenalty.SellerPenalty = request.MerchantPunishment == "-" ? null : request.MerchantPunishment;
                existingPenalty.StorePenalty = request.StorePunishment == "-" ? null : request.StorePunishment;
                existingPenalty.PenaltyNote = request.ProcessingNote == "-" ? null : request.ProcessingNote;

                if (DateTime.TryParse(request.PunishmentTime, out DateTime newPenaltyTime))
                {
                    existingPenalty.PenaltyTime = newPenaltyTime;
                }

                bool updateSuccess = await _administratorRepository.UpdateViolationPenaltyAsync(existingPenalty);
                if (!updateSuccess)
                {
                    return new SetViolationPenaltyInfoResponse
                    {
                        Success = false,
                        Message = "更新违规处罚失败，请稍后重试"
                    };
                }

                var updatedPenaltyDto = new GetViolationPenaltyInfo
                {
                    PunishmentId = existingPenalty.PenaltyID.ToString(),
                    StoreName = existingPenalty.Store.StoreName,
                    Reason = existingPenalty.PenaltyReason,
                    MerchantPunishment = existingPenalty.SellerPenalty ?? "-",
                    StorePunishment = existingPenalty.StorePenalty ?? "-",
                    PunishmentTime = existingPenalty.PenaltyTime.ToString("yyyy-MM-dd HH:mm"),
                    Status = request.Status,
                    ProcessingNote = existingPenalty.PenaltyNote ?? ""
                };

                return new SetViolationPenaltyInfoResponse
                {
                    Success = true,
                    Message = "违规处罚更新成功",
                    Data = updatedPenaltyDto
                };
            }
            catch (Exception ex)
            {
                return new SetViolationPenaltyInfoResponse
                {
                    Success = false,
                    Message = $"处理违规处罚时发生错误：{ex.Message}"
                };
            }
        }
    }
}
