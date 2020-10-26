using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MeetApp.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetValueFromClaim(this IEnumerable<Claim> claims, string type)
        {
            Claim c;
            
            try
            {
                c = claims.First(a => a.Type == type);
            }
            catch
            {
                return null;
            }

            return c.Value;
        }
    }
}