using System.Collections.Generic;
using System.Threading.Tasks;
using DefectTracker.Contracts.Repositories;
using DefectTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace DefectTracker.Repositories
{
    public class DefectRepository : IDefectRepository
    {
        private readonly DefectTrackerDbContext _dbContext;

        public DefectRepository(DefectTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Defects> CreateDefectAsync(Defects defect)
        {
            _dbContext.Defects.Add(defect);
            await _dbContext.SaveChangesAsync();

            return defect;
        }

        public async Task<IEnumerable<DefectQualifierTypes>> GetDefectQualifiersAsync()
        => await _dbContext.DefectQualifierTypes.ToListAsync();

        public async Task<IEnumerable<DefectReportedByTypes>> GetDefectReportedByTypesAsync()
        => await _dbContext.DefectReportedByTypes.ToListAsync();

        public async Task<IEnumerable<DefectTypes>> GetDefectTypesAsync()
        => await _dbContext.DefectTypes.ToListAsync();
    }
}
