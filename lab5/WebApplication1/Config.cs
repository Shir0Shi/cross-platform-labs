using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace WebApplication1
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "mvc-client",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Code,

                // TODO настроить SSL потому что оно не работает и не работает логин

                RedirectUris = { "https://localhost:5116/signin-oidc" },

                PostLogoutRedirectUris = { "https://localhost:5116/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1" 
                },

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                RequireConsent = false
            }

        };
        }
    }
}
