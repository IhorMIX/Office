using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity.Selections;
using Office.Web.Extensions;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SubdivisionController(ISubdivisionService subdivisionService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{subdivisionId:int}")]
    public async Task<IActionResult> GetById(int subdivisionId, CancellationToken cancellationToken = default)
    {
        var subdivision = await subdivisionService.GetByIdAsync(subdivisionId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(subdivision));
    }

    [HttpPost("create-subdivision")]
    public async Task<IActionResult> CreatePosition(SelectionCreateModel subdivisionCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await subdivisionService.CreateSubdivisionAsync(mapper.Map<Subdivision>(subdivisionCreateModel), adminId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(result));
    }
    
    [HttpDelete("{subdivisionId:int}")]
    public async Task<IActionResult> DeletePosition(int subdivisionId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await subdivisionService.DeleteSubdivisionAsync(subdivisionId,managerId,cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel subdivision, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await subdivisionService.UpdateSubdivisionAsync(userId, mapper.Map<Subdivision>(subdivision), cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSubdivisions(CancellationToken cancellationToken = default)
    {
        var subdivisions = await subdivisionService.GetAllAsync(cancellationToken);
        return Ok(mapper.Map<List<SelectionViewModel>>(subdivisions));
    }
} 