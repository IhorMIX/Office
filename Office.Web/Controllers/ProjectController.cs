    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Office.BLL.Models;
    using Office.BLL.Services.Interfaces;
    using Office.Web.Extensions;
    using Office.Web.Models;

    namespace Office.Web.Controllers;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService, IMapper mapper)
        : ControllerBase
    {
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> GetById(int projectId, CancellationToken cancellationToken = default)
        {
            var project = await projectService.GetByIdAsync(projectId, cancellationToken);
            return Ok(mapper.Map<ProjectViewModel>(project));
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateModel projectCreateModel, CancellationToken cancellationToken = default)
        {
            var adminId = User.GetUserId();
            var result = await projectService.CreateProjectAsync(mapper.Map<ProjectModel>(projectCreateModel), adminId, cancellationToken);
            return Ok(mapper.Map<ProjectViewModel>(result));
        }
        
        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> DeleteProject(int projectId, CancellationToken cancellationToken = default)
        {
            var managerId = User.GetUserId();
            await projectService.DeleteProjectAsync(projectId,managerId,cancellationToken);
            return Ok();
        }
        
        [HttpPut("{projectId:int}")]
        public async Task<IActionResult> DeactivateProject(int projectId, CancellationToken cancellationToken = default)
        {
            var managerId = User.GetUserId();
            await projectService.DeactivateProjectAsync(projectId,managerId,cancellationToken);
            return Ok();
        }
        
        [HttpPut("employees")]
        public async Task<IActionResult> AddEmployeesProject([FromBody]AddEmployeesModel addEmployeesModel,
            CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId();
            await projectService.AddEmployeesInProjectAsync(userId,
                addEmployeesModel.ProjectId,
                addEmployeesModel.EmployeesIds,
                cancellationToken);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody]ProjectUpdateModel projectCreateModel, CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId();
            var project= await projectService.UpdateProjectAsync(userId,mapper.Map<ProjectModel>(projectCreateModel), cancellationToken);
            return Ok(mapper.Map<ProjectViewModel>(project));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var projects = await projectService.GetAllAsync(userId, cancellationToken);
            return Ok(mapper.Map<List<ProjectViewModel>>(projects));
        }
    }