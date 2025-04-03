using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class ProjectRepository(OfficeDbContext officeDbContext) : IProjectRepository
{
    public IQueryable<Project> GetAll()
    {
        return officeDbContext.Projects.Include(i => i.Employees).Include(i => i.ProjectType).AsQueryable();
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.Projects
            .Include(i => i.Employees)
            .Include(i => i.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        var entity = await officeDbContext.Projects.AddAsync(project,cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        officeDbContext.Projects.Remove(project);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        officeDbContext.Projects.Update(project);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddEmployeesInProjectAsync(int projectId, List<Employee> employees, CancellationToken cancellationToken = default)
    {
        var project = await officeDbContext.Projects.Where(r => r.Id == projectId).SingleOrDefaultAsync(cancellationToken);
        project!.Employees = employees;
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}