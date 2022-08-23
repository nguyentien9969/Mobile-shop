using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MyShop.Infrastructure
{
    public static class IdentityExtension
    {
        public static Customer Customer(this IIdentity identity)
        {
            if (identity == null)
                return null;

            return new Customer()
            {
                Id = (identity as ClaimsIdentity).FirstOrNull("Id"),
                FullName = (identity as ClaimsIdentity).FirstOrNull("FullName"),
            };
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }
}