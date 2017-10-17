using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            var redirectUrl = Url.Page("/Index");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                // challenge the user by logging in with OIDC server
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }

        public IActionResult Logout()
        {
            var callbackUrl = Url.Page("/Account/SignedOut");
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                // remove cookie that authenticates user
                CookieAuthenticationDefaults.AuthenticationScheme, 
                // also logout of OIDC identity provider
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }
    }
}
