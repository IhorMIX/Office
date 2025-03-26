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
public class SubdivisionController(ISubdivisionService subdivisionService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{subdivisionId:int}")]
    public async Task<IActionResult> GetById(int subdivisionId, CancellationToken cancellationToken = default)
    {
        var subdivision = await subdivisionService.GetByIdAsync(subdivisionId, cancellationToken);
        return Ok(mapper.Map<SubdivisionViewModel>(subdivision));
    }

    [HttpPost("create-subdivision")]
    public async Task<IActionResult> CreatePosition(SubdivisionCreateModel subdivisionCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await subdivisionService.CreateSubdivisionAsync(mapper.Map<SubdivisionModel>(subdivisionCreateModel), adminId, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{subdivisionId:int}")]
    public async Task<IActionResult> DeletePosition(int subdivisionId, CancellationToken cancellationToken = default)
    {
        await subdivisionService.DeleteSubdivisionAsync(subdivisionId, cancellationToken);
        return Ok();
    }
} 