using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClockInClockOut_API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected long GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claim = identity.FindFirst("ID");
                if (claim != null)
                {
                    long userId = 0;
                    if (Int64.TryParse(claim.Value, out userId))
                        return userId;
                }
            }

            throw new Exception("Token invalido.");
        }
    }
}

