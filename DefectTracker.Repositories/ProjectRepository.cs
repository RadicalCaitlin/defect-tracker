using DefectTracker.Contracts.Repositories;
using DefectTracker.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectTracker.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DefectTrackerDbContext _dbContext;

        public ProjectRepository(DefectTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Projects> CreateProjectAsync(Projects project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Projects> GetProjectByIdAsync(int projectId)
        => await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == projectId);

        public async Task<IEnumerable<Projects>> GetProjectsByUserIdAsync(string currentUserId)
            => await _dbContext.Projects.Where(p => p.CreatedByUserId == currentUserId).ToListAsync();
    }
}
