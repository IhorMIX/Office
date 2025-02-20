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
public class EmployeeController(IEmployeeService employeeService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateModel employeeCreateModel,
        CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employee = await employeeService.CreateEmployeeAsync(managerId,
            mapper.Map<EmployeeModel>(employeeCreateModel), cancellationToken);
        return Ok(mapper.Map<EmployeeViewModel>(employee));
    }
}