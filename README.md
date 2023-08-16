# sunstealer.mvc.odata aspcore 7.0

Sandpit .net asp core 7.0 API server with Identity and Swagger.
An Identity Server 6 is required with:

```csharp
using Duende.IdentityServer.Models;

using Duende.IdentityServer.Models;/

namespace Sunstealer.IdentityServer.Models;

public class Configuration {
  public static IEnumerable<Duende.IdentityServer.Models.ApiResource> ApiResources =>
  new List<Duende.IdentityServer.Models.ApiResource> {
    new Duende.IdentityServer.Models.ApiResource("sunstealer.read", "sunstealer.read"),
    new Duende.IdentityServer.Models.ApiResource("sunstealer.write", "sunstealer.write")
  };

  public static IEnumerable<Duende.IdentityServer.Models.ApiScope> ApiScopes =>
  new List<Duende.IdentityServer.Models.ApiScope> {
    new Duende.IdentityServer.Models.ApiScope("sunstealer.read", "sunstealer.read"),
    new Duende.IdentityServer.Models.ApiScope("sunstealer.write", "sunstealer.write")
  };

  public static IEnumerable<IdentityResource> IdentityResources =>
    new List<IdentityResource> { new IdentityResources.OpenId(), new IdentityResources.Profile()
  };

  public static IEnumerable<Duende.IdentityServer.Models.Client> Clients => new List<Duende.IdentityServer.Models.Client> {
    new Duende.IdentityServer.Models.Client() {
      AllowAccessTokensViaBrowser = true,
      AllowedGrantTypes = Duende.IdentityServer.Models.GrantTypes.Code,
      AlwaysIncludeUserClaimsInIdToken = false,
      AllowOfflineAccess = true,
      AllowedScopes = {
        Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess,
        Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
        Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
        "sunstealer.read",
        "sunstealer.write"
      },
      ClientId = "sunstealer.code",
      ClientSecrets = { new Duende.IdentityServer.Models.Secret("secret".Sha256()) },
      PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
      RedirectUris = { "https://localhost:5001/signin-oidc", "https://localhost:5001/Swagger/oauth2-redirect.html" },
      RequirePkce = true,
    },

    new Duende.IdentityServer.Models.Client() {
      AccessTokenLifetime = 600,
      AllowedGrantTypes = Duende.IdentityServer.Models.GrantTypes.HybridAndClientCredentials,
      AllowedScopes = {
        Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess,
        Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId
      },
      AllowOfflineAccess = true,
      AllowAccessTokensViaBrowser = true, 
      AlwaysIncludeUserClaimsInIdToken = false,
      Claims = new System.Collections.Generic.List<ClientClaim> { new ClientClaim(IdentityModel.JwtClaimTypes.Role, "Administrator") },
      ClientId = "sunstealer.hybridclientcredentials",
      ClientSecrets = { new Duende.IdentityServer.Models.Secret("hybrid_and_client_credentials_secret".Sha256()) },
      IdentityTokenLifetime = 600,
      RedirectUris = new System.Collections.Generic.List<string> { "http://localhost:8010" },
      RequirePkce = true,
    }
  };

  public static System.Collections.Generic.List<Duende.IdentityServer.Test.TestUser> Users = new System.Collections.Generic.List<Duende.IdentityServer.Test.TestUser>() {
    new Duende.IdentityServer.Test.TestUser() {
      SubjectId = "AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE",
      Username = "adam",
      Password = "password",
      Claims = new[] {
        new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Email, "ajm@mail.com"),
        new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Name, "Adam"),
      }
    }
  };
}
```
