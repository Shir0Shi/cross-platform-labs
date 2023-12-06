using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace WebApplication1
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources =>
         new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        
        public static IEnumerable<ApiResource> GetApiResources =>
        new List<ApiResource>
        {
            new ApiResource("api1", "My API", new []
                { JwtClaimTypes.Name})
            {
                Scopes = { "api1" }
            }
        };

        public static IEnumerable<ApiScope> GetApiScopes => 
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };
        

        public static IEnumerable<Client> GetClients =>
         new List<Client>
        {
            new Client
            {
                ClientId = "mvc-client",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                // TODO настроить SSL потому что оно не работает и не работает логин

                RedirectUris = { "https://localhost:7205/signin-oidc" },

                PostLogoutRedirectUris = { "https://localhost:7205/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1" 
                },

                RequireConsent = false,
                AllowAccessTokensViaBrowser = true
            }

        };
     
    }
}
