using System.Security.Claims;
using Office.Web.Options;

namespace Office.Web.Extensions;

public static class AuthorizeExtension
{
    public static int GetUserId(this ClaimsPrincipal employee)
    {
        var claim = employee.FindFirst(AuthOption.AuthOptions.EmployeeIdCalmName);
        
        if (claim == null)
        {
            throw new Exception("Member with id claim didn't exist on identity");
        }

        if (int.TryParse(claim.Value, out var memberId))
        {
            return memberId;
        }

        throw new Exception($"Member id was not an int. Id '{claim.Value}'");
    }
}