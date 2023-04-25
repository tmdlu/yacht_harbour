using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using yacht_harbour.Models;
namespace yacht_harbour.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorageService = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var name = await _sessionStorageService.GetItemAsync<string>("name");
            var email = await _sessionStorageService.GetItemAsync<string>("email");
          
            var surname = await _sessionStorageService.GetItemAsync<string>("surname");
            var role = await _sessionStorageService.GetItemAsync<string>("role");

            ClaimsIdentity identity;

            if (email != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Surname, surname),
                    new Claim(ClaimTypes.Role, role)
                }, "apiauth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(AccountFront user)
        {
            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void MarkUserAsLoggedOut()
        {
            _sessionStorageService.RemoveItemAsync("account_id");
            _sessionStorageService.RemoveItemAsync("email");
            _sessionStorageService.RemoveItemAsync("name");
            _sessionStorageService.RemoveItemAsync("surname");
            
            _sessionStorageService.RemoveItemAsync("password");
            _sessionStorageService.RemoveItemAsync("phone");
            _sessionStorageService.RemoveItemAsync("status");
            _sessionStorageService.RemoveItemAsync("role");



            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        //tu naprawic
        private ClaimsIdentity GetClaimsIdentity(AccountFront user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
{
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                
          
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Role, user.Role)



        }, "apiauth_type");

            return claimsIdentity;
        }
    }
}

