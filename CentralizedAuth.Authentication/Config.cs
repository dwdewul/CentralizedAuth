using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace CentralizedAuth.Authentication
{
    public class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "ddewulf",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "jzoss",
                    Password = "password"
                }
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("WebApi", "Api for Auth"),
                new ApiResource("PermissionsApi", "Api for Permissions")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // CLient Credentials Grant
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("Secret".Sha256())
                    },
                    AllowedScopes = { "WebApi", "PermissionsApi" }
                },
                //Resource Owner Password Grant
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("Secret".Sha256())
                    },
                    AllowedScopes = { "WebApi", "PermissionsApi" }
                },

                //Implicit Grant
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris =
                    {
                        "https://localhost:44319/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:44319/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }

                }
            };
        }
    }
}
