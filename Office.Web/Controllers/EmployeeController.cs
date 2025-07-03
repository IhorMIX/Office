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
    
    [HttpDelete("{employeeId:int}")]
    public async Task<IActionResult> DeletePosition(int employeeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await employeeService.DeleteEmployeeAsync(employeeId,managerId, cancellationToken);
        return Ok();
    }
    
    [HttpGet("{employeeId:int}")]
    public async Task<IActionResult> GetEmployee(int employeeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await employeeService.GetByIdAsync(employeeId, cancellationToken);
        return Ok(mapper.Map<EmployeeFullViewModel>(employees));
    }
    
    [HttpGet("get-all-employees")]
    public async Task<IActionResult> GetAllEmployee(CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await employeeService.GetAllAsync(managerId, cancellationToken);
        return Ok(mapper.Map<List<EmployeeViewModel>>(employees));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateModel employeeUpdateModel, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await employeeService.UpdateEmployeeAsync(managerId, mapper.Map<EmployeeModel>(employeeUpdateModel), cancellationToken);
        return Ok();
    }
}