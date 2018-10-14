using DefectTracker.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefectTracker.Contracts.Repositories
{
    public interface IProjectRepository
    {
        Task<Projects> CreateProjectAsync(Projects project);

        Task<Projects> GetProjectByIdAsync(int projectId);

        Task<IEnumerable<Projects>> GetProjectsByUserIdAsync(string currentUserId);
    }
}
