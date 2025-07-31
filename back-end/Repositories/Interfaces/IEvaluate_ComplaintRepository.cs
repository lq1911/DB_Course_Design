using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IEvaluate_ComplaintRepository
    {
        Task<IEnumerable<Evaluate_Complaint>> GetAllAsync();
        Task<Evaluate_Complaint?> GetByIdAsync(int adminId, int complaintId);
        Task AddAsync(Evaluate_Complaint evaluateComplaint);
        Task UpdateAsync(Evaluate_Complaint evaluateComplaint);
        Task DeleteAsync(Evaluate_Complaint evaluateComplaint);
        Task SaveAsync();
    }
}