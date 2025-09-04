using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        // 管理员
        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _context.Administrators
                                 .Include(a => a.User)
                                 .Include(a => a.ReviewComments)
                                    .ThenInclude(rc => rc.Comment)
                                 .Include(a => a.Supervise_s)
                                      .ThenInclude(s => s.Penalty)
                                 .Include(a => a.EvaluateAfterSales)
                                      .ThenInclude(eas => eas.Application)
                                 .Include(a => a.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Complaint)
                                 .ToListAsync();
        }

        public async Task<Administrator?> GetByIdAsync(int id)
        {
            return await _context.Administrators
                                 .Include(a => a.User)
                                 .Include(a => a.ReviewComments)
                                    .ThenInclude(rc => rc.Comment)
                                 .Include(a => a.Supervise_s)
                                      .ThenInclude(s => s.Penalty)
                                 .Include(a => a.EvaluateAfterSales)
                                      .ThenInclude(eas => eas.Application)
                                 .Include(a => a.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Complaint)
                                 .FirstOrDefaultAsync(a => a.UserID == id);
        }

        public async Task<bool> UpdateAdministratorAsync(Administrator administrator)
        {
            try
            {
                _context.Administrators.Update(administrator);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 售后申请
        public async Task<IEnumerable<AfterSaleApplication>> GetAfterSaleApplicationsByAdminIdAsync(int adminId)
        {
            var applications = await _context.Evaluate_AfterSales
                                             .Where(eas => eas.AdminID == adminId)
                                             .Select(eas => eas.Application)
                                             .ToListAsync();

            return applications;
        }

        public async Task<AfterSaleApplication?> GetAfterSaleApplicationByIdAsync(int applicationId)
        {
            return await _context.AfterSaleApplications
                .FirstOrDefaultAsync(app => app.ApplicationID == applicationId);
        }

        public async Task<bool> UpdateAfterSaleApplicationAsync(AfterSaleApplication application)
        {
            try
            {
                _context.AfterSaleApplications.Update(application);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 配送投诉
        public async Task<IEnumerable<DeliveryComplaint>> GetDeliveryComplaintsByAdminIdAsync(int adminId)
        {
            var complaints = await _context.Evaluate_Complaints
                                           .Where(ec => ec.AdminID == adminId)
                                           .Select(ec => ec.Complaint)
                                           .ToListAsync();

            return complaints;
        }

        public async Task<DeliveryComplaint?> GetDeliveryComplaintByIdAsync(int complaintId)
        {
            return await _context.DeliveryComplaints
               .FirstOrDefaultAsync(dc => dc.ComplaintID == complaintId);
        }

        public async Task<bool> UpdateDeliveryComplaintAsync(DeliveryComplaint complaint)
        {
            try
            {
                _context.DeliveryComplaints.Update(complaint);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 违规处罚
        public async Task<IEnumerable<StoreViolationPenalty>> GetViolationPenaltiesByAdminIdAsync(int adminId)
        {
            var penalties = await _context.Supervise_s
                                          .Where(s => s.AdminID == adminId)
                                          .Select(s => s.Penalty)
                                          .Include(p => p.Store)
                                          .Include(p => p.Store.Seller)
                                          .Include(p => p.Store.Seller.User)
                                          .ToListAsync();

            return penalties;
        }

        public async Task<StoreViolationPenalty?> GetViolationPenaltyByIdAsync(int penaltyId)
        {
            return await _context.StoreViolationPenalties
                .Include(p => p.Store)
                .Include(p => p.Store.Seller)
                .Include(p => p.Store.Seller.User)
                .FirstOrDefaultAsync(p => p.PenaltyID == penaltyId);
        }

        public async Task<bool> UpdateViolationPenaltyAsync(StoreViolationPenalty penalty)
        {
            try
            {
                _context.StoreViolationPenalties.Update(penalty);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 评论审核
        public async Task<IEnumerable<Comment>> GetReviewCommentsByAdminIdAsync(int adminId)
        {
            var comments = await _context.Review_Comments
                                         .Where(rc => rc.AdminID == adminId)
                                         .Select(rc => rc.Comment)
                                         .Include(c => c.Commenter)
                                         .Include(c => c.Commenter.User)
                                         .Include(c => c.Store)
                                         .Include(c => c.FoodOrder)
                                         .ToListAsync();

            return comments;
        }

        public async Task<Comment?> GetReviewCommentByIdAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.Commenter)
                .Include(c => c.Commenter.User)
                .Include(c => c.Store)
                .Include(c => c.FoodOrder)
                .FirstOrDefaultAsync(c => c.CommentID == commentId);
        }

        public async Task<bool> UpdateReviewCommentAsync(Comment comment)
        {
            try
            {
                _context.Comments.Update(comment);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _context.Administrators.AddAsync(administrator);
            await SaveAsync();
        }

        public async Task UpdateAsync(Administrator administrator)
        {
            _context.Administrators.Update(administrator);
            await SaveAsync();
        }

        public async Task DeleteAsync(Administrator administrator)
        {
            _context.Administrators.Remove(administrator);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}