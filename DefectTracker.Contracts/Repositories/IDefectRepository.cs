using DefectTracker.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefectTracker.Contracts.Repositories
{
    public interface IDefectRepository
    {
        Task<Defects> CreateDefectAsync(Defects defect);

        Task<IEnumerable<DefectQualifierTypes>> GetDefectQualifiersAsync();

        Task<IEnumerable<DefectReportedByTypes>> GetDefectReportedByTypesAsync();

        Task<IEnumerable<Defects>> GetDefectsByProjectIdAsync(int projectId);

        Task<IEnumerable<DefectTypes>> GetDefectTypesAsync();
    }
}
