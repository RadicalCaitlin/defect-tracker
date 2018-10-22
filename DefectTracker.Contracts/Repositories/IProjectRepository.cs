using DefectTracker.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefectTracker.Contracts.Repositories
{
    public interface IProjectRepository
    {
        Task<ProjectAreas> CreateProjectAreasAsync(ProjectAreas projectArea);

        Task<Projects> CreateProjectAsync(Projects project);

        Task<ProjectUsers> CreateProjectUserAsync(ProjectUsers projectUser);

        Task<IEnumerable<Activities>> GetActivitiesByProjectIdAsync(int projectId);

        Task<IEnumerable<ProjectAreas>> GetAreasByProjectIdAsync(int projectId);

        Task<IEnumerable<ProjectBugs>> GetBugsByProjectIdAsync(int projectId);

        Task<Projects> GetProjectByIdAsync(int projectId);

        Task<IEnumerable<Projects>> GetProjectsByUserIdAsync(string currentUserId);

        Task<IEnumerable<ProjectUsers>> GetProjectUsersByProjectIdAsync(int projectId);

        Task<IEnumerable<Tasks>> GetTasksByProjectIdAsync(int projectId);
    }
}
