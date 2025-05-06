using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface IOvertimeRecordService
    {
        Task<List<OvertimeRecord>> GetOvertimeByEmployeeIdAsync(int employeeId);
        Task<OvertimeRecord> AddOvertimeRecordAsync(OvertimeRecord overtimeRecord);

        public class OvertimeRecordService : IOvertimeRecordService
        {
            private readonly IGenericRepository<Users> _repository;
            private readonly LogisticContext _context;
            private readonly IContextHelper _contextHelper;
            public OvertimeRecordService(IGenericRepository<Users> repository, LogisticContext dbcontext, IContextHelper contextHelper)
            {
                _repository = repository;
                _context = dbcontext;
                _contextHelper = contextHelper;
            }

            public async Task<List<OvertimeRecord>> GetOvertimeByEmployeeIdAsync(int employeeId)
            {
                return await _context.OvertimeRecords
                                     .Where(o => o.EmployeeId == employeeId && o.isActive == true)
                                     .ToListAsync();
            }

            public async Task<OvertimeRecord> AddOvertimeRecordAsync(OvertimeRecord overtimeRecord)
            {
                overtimeRecord.AddedOn = DateTime.UtcNow;
                overtimeRecord.AddedBy = "Admin"; // Replace with actual user
                _context.OvertimeRecords.Add(overtimeRecord);
                await _context.SaveChangesAsync();
                return overtimeRecord;
            }
        }

    }
}
