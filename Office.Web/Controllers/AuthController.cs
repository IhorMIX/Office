using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.Web.Extensions;
using Office.Web.Helpers;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenHelper _tokenHelper;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public AuthController(TokenHelper tokenHelper, IAuthService authService, IMapper mapper)
    {
        _tokenHelper = tokenHelper;
        _authService = authService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> AuthorizeEmployee([FromBody] AuthorizeModel model, CancellationToken cancellationToken)
    {
        var employee = await _authService.GetByLoginAndPasswordAsync(model.Login, model.Password, cancellationToken);
        var token = _tokenHelper.GetToken(employee.Id);
        var refreshToken = TokenHelper.GenerateRefreshToken(token);
        DateTime? expiredDate = model.IsNeedToRemember ? null : DateTime.Now;

        await _authService.AddAuthorizationValueAsync(
            _mapper.Map<BaseEmployeeModel>(employee),
            refreshToken,
            expiredDate,
            cancellationToken);
        
        return Ok(new { accessKey = token, refresh_token = refreshToken, expiredDate = expiredDate, role = employee.GetType().Name });
    }
    
    [AllowAnonymous]
    [HttpPost("token/{refreshToken}")]
    public async Task<IActionResult> UpdateTokenAsync([FromQuery] string refreshToken, CancellationToken cancellationToken)
    {
        refreshToken = refreshToken.Replace(" ", "+"); 
        var user = await _authService.GetUserByRefreshTokenAsync(refreshToken, cancellationToken);
        var token = _tokenHelper.GetToken(user.Id);
        return Ok(new { accessKey = token, refresh_token = refreshToken, expiredDate = user.AuthorizationInfo!.ExpiredDate });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> LogOutAsync(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        await _authService.LogOutAsync(userId, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var userModel = await _authService.GetUserById(userId, cancellationToken);
        
        var currentUser = _mapper.Map<CurrentUserViewModel>(userModel);
        return Ok(currentUser);
    }

}