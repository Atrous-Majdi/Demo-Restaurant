using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity
{
    public static class StaticDetails
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
            new ApiScope("mango", " Restaurant Administrator"),
            new ApiScope("read",  "Read Data"),
            new ApiScope("write", "Write Data"),
            new ApiScope("delete","Delete Data")
        };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                    new Client{
                    ClientId="mango",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code ,
                    RedirectUris= { "http://localhost:44317/signin-oidc" },
                    PostLogoutRedirectUris ={ "http://localhost:44317/signout-callback-oidc" },
                    AllowedScopes= new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                    }
            };
        
    }
}
