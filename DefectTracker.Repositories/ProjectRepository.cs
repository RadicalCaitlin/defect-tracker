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

        public async Task<ProjectAreas> CreateProjectAreasAsync(ProjectAreas projectArea)
        {
            _dbContext.ProjectAreas.Add(projectArea);
            await _dbContext.SaveChangesAsync();

            return projectArea;
        }

        public async Task<Projects> CreateProjectAsync(Projects project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<ProjectBugs> CreateProjectBugAsync(ProjectBugs projectBugs)
        {
            _dbContext.ProjectBugs.Add(projectBugs);
            await _dbContext.SaveChangesAsync();

            return projectBugs;
        }

        public async Task<ProjectUsers> CreateProjectUserAsync(ProjectUsers projectUser)
        {
            _dbContext.ProjectUsers.Add(projectUser);
            await _dbContext.SaveChangesAsync();

            return projectUser;
        }

        public async Task<IEnumerable<Activities>> GetActivitiesByProjectIdAsync(int projectId)
        => await _dbContext.Activities.Where(a => a.ProjectId == projectId).ToListAsync();

        public async Task<IEnumerable<ProjectAreas>> GetAreasByProjectIdAsync(int projectId)
        => await _dbContext.ProjectAreas.Where(a => a.ProjectId == projectId).ToListAsync();

        public async Task<IEnumerable<ProjectBugs>> GetBugsByProjectIdAsync(int projectId)
        => await _dbContext.ProjectBugs.Where(b => b.ProjectId == projectId).ToListAsync();

        public async Task<ProjectBugs> GetProjectBugByIdAsync(int bugId)
        => await _dbContext.ProjectBugs.SingleOrDefaultAsync(b => b.Id == bugId);

        public async Task<Projects> GetProjectByIdAsync(int projectId)
        => await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == projectId);

        public async Task<IEnumerable<Projects>> GetProjectsByUserIdAsync(string currentUserId)
            => await _dbContext.Projects.Where(p => p.CreatedByUserId == currentUserId).ToListAsync();

        public async Task<IEnumerable<ProjectUsers>> GetProjectUsersByProjectIdAsync(int projectId)
        => await _dbContext.ProjectUsers.Where(u => u.ProjectId == projectId).ToListAsync();

        public async Task<IEnumerable<Tasks>> GetTasksByProjectIdAsync(int projectId)
        => await _dbContext.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
    }
}
