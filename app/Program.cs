using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;
using sunstealer.mvc.odata.Models;
using sunstealer.mvc.odata.Swagger;

var builder = WebApplication.CreateBuilder(args);

/* builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(options => options.UseHttps(
        new HttpsConnectionAdapterOptions()
        {
            SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
            ClientCertificateMode = ClientCertificateMode.AllowCertificate,
            ServerCertificate = new X509Certificate2("./sunstealer.pfx", "password")
        })
    );
});*/

// ajm: sandpit
var AppName = "sunstealer.mvc.odata";

builder.Services.AddHttpClient();

// ajm: identity
if (sunstealer.mvc.odata.Services.Application.Auth)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://ajmwin11-01.ajm.net:9001";
        options.ClientId = "sunstealer.code";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("offline_access");
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        // ajm: options.Scope.Add("sunstealer.read");
        // ajm: options.Scope.Add("sunstealer.write");

        options.UsePkce = true;
        options.SignedOutRedirectUri = "https://ajmwin11-01.ajm.net:5001/signout-callback-oidc";
    })

    /*
        {
            "keys": [
            {
                "kty": "RSA",
                "use": "sig",
                "kid": "07AE8B59E9FEDCBD43952B2F066DAD57",
                "e": "AQAB",
                "n": "u_aFyzgDAYBiOi99Ph57YgF4lkQYseatMx8YW2n4h6VmpDa3x71kPrB6JWqy4hgXsg4LZdhUD0UIBoYsuVHKUhV0D72fxLHQyVkAo10F_oP39EfDxpYpb054T7M4bN0NqotAo8P3k3pjdJVWkJ9jJQfSUJJryzABfutzsgikJlonbVXRD7aW2CNebdsvCEUc37t5dcdZk4cXiRkdfTAR789FnZVTi9vB5wUOPlBfsQU2FAaggB0wbXAlLtFRHOlohohXRticX0rXJPnJPo6lDZDwTKCziEqM1UKvFwC2Aa1cKELRetsbc7XCIrzQAVqsRNBTMQHKTzxEEnDO74LsHQ",
                "alg": "RS256"
            },
            {
                "kty": "RSA",
                "use": "sig",
                "kid": "9A8C898E12444F81C0F791BCA9224D38",
                "e": "AQAB",
                "n": "mnhS8f4hz-hqNdn08FOSEbl81mr94Y8wGfV1nzkAjI7oDh4LVnwv3Wl2kcElikJBdGMCm_pIpDGc7ZJqiIWBGqiUI3nd4vV7-N5uKflOAVEOmHlNLZO5yCY4zTCysDK1d2cUGMuXnSwMK3Mu4ce59rRofpZK98wAZ55RW-sftxwvzbpAzcN9XKBzkxuY04hiKkZ9h0BveWnFlrOaHDmgx9WZZAiZHvt086ajbu2bBJUm_-2T-mdvyfMz_CQddbCBMJ9cO1WODnJznp2nkgfJnrLH8en4Br72xUnYswMY_mmNsoMsDBjodIPE6dGC6tMTFHsR0gtUtiQqiBvd3gXtMQ",
                "alg": "RS256"
            },
            {
                "kty": "RSA",
                "use": "sig",
                "kid": "0391063D971A0A9066B0091924C0AB0D",
                "e": "AQAB",
                "n": "u9YcxixgmGxFxAx13yQ0ef_vkJzNaOkWVHqBviDWBzBbY4SACYF2HB8JscCcJXL0kAcZP5vgZOYSfMG-3l2IifWD-FeXRVskdX2cASUExnuNBoC2xL271mOajLoMVvJA1O_qXcujC5o1ysDm4GtRJErsZ_aXZCRH1Huz1UVZsCdkHtkVDLGRaOtm-npFd1nUSYIcPLEw44uzEGWqRouJeXHXLcjIABn-riQnb55GSlU9eFZ9xXYIfcVU4p8kOhic_wP2XhcNSa6KQHzkndpUd9ID3Hvp-3sF6eG-yelcdB1W_q6nvv_CEefUPoweMHRThorqDFeh3CCl46ipU2YItQ",
                "alg": "RS256"
            }]}*/

    .AddJwtBearer("Bearer", options =>
    {
        options.SaveToken = true;
        options.Authority = "https://ajmwin11-01.ajm.net:9001";
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("u9YcxixgmGxFxAx13yQ0ef_vkJzNaOkWVHqBviDWBzBbY4SACYF2HB8JscCcJXL0kAcZP5vgZOYSfMG-3l2IifWD-FeXRVskdX2cASUExnuNBoC2xL271mOajLoMVvJA1O_qXcujC5o1ysDm4GtRJErsZ_aXZCRH1Huz1UVZsCdkHtkVDLGRaOtm-npFd1nUSYIcPLEw44uzEGWqRouJeXHXLcjIABn-riQnb55GSlU9eFZ9xXYIfcVU4p8kOhic_wP2XhcNSa6KQHzkndpUd9ID3Hvp-3sF6eG-yelcdB1W_q6nvv_CEefUPoweMHRThorqDFeh3CCl46ipU2YItQ")),
            ValidateIssuer = false,
            ValidIssuer = "https://ajmwin11-01.ajm.net:9001",
            ValidateAudience = false,
            ValidAudience = "https://ajmwin11-01.ajm.net:9001/resources"
        };

        /* ajm: options.Events ??= new JwtBearerEvents();

        options.Events = new JwtBearerEvents()
        {        
            OnAuthenticationFailed = e =>
            {
                System.Diagnostics.Debug.WriteLine($"OnAuthenticationFailed: {e.Request.Protocol} {e.Request.Method} {e.Request.Host}{e.Request.Path}?{e.Request.QueryString}");
                return Task.CompletedTask;
            },

            OnChallenge = e =>
            {
                System.Diagnostics.Debug.WriteLine($"OnChallenge: {e.Request.Protocol} {e.Request.Method} {e.Request.Host}{e.Request.Path}?{e.Request.QueryString}");
                return Task.CompletedTask;
            },

            OnForbidden = e =>
            {
                return Task.CompletedTask;
            },

            OnMessageReceived = e =>
            {
                System.Diagnostics.Debug.WriteLine($"OnMessageReceived: {e.Request.Protocol} {e.Request.Method} {e.Request.Host}{e.Request.Path}?{e.Request.QueryString}");
                return Task.CompletedTask;
            },


            OnTokenValidated = e =>
            {
                return Task.CompletedTask;
            }
        };*/
    });

    // ajm: identity
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("sunstealer.read", policy =>
        {
            policy.RequireClaim("scope", "sunstealer.read");
        });

        options.AddPolicy("sunstealer.write", policy =>
        {
            policy.RequireClaim("scope", "sunstealer.write");
        });
    });

    // builder.Services.AddRequiredScopeAuthorization();

    // ajm: swagger, without token
    /* builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "Protected API",
        Version = "v1"
      });

      options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
        {
          AuthorizationCode = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
          {
            AuthorizationUrl = new Uri("https://ajmwin11-01.ajm.net:9001/connect/authorize"),
            Scopes = { { "openid", "openid" }, { "profile", "profile" }, { "sunstealer.read", "sunstealer.read" }, { "sunstealer.read", "sunstealer.read" }, { "sunstealer.write", "sunstealer.write" } },
            TokenUrl = new Uri("https://ajmwin11-01.ajm.net:9001/connect/token")
          }
        },

        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            In = ParameterLocation.Header,
            Name = "oauth2",
            Reference = new OpenApiReference {
              Id = "oauth2",
              Type = ReferenceType.SecurityScheme
            },
            Scheme = "oauth2"
          },
          new List<string> { }
        }
      });
    });*/
}

// ajm: swagger, with token
builder.Services.AddSwaggerGen(options =>
{
    if (sunstealer.mvc.odata.Services.Application.Auth)
    {
        options.AddSecurityDefinition("bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Scheme = "bearer",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Id = "bearer",
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }
    options.OperationFilter<OperationFilter>();
});

builder.Services.AddHostedService<sunstealer.mvc.odata.Services.Application>();
builder.Services.AddHostedService<sunstealer.mvc.odata.Services.Background>();
builder.Services.AddControllersWithViews();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Model1>("Edm");
builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata", modelBuilder.GetEdmModel()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

// ajm: identity
if (sunstealer.mvc.odata.Services.Application.Auth)
{
    app.UseAuthentication();
}

app.UseRouting();

if (sunstealer.mvc.odata.Services.Application.Auth)
{
    app.UseAuthorization();
}

// ajm: swagger
app.UseSwagger(options => { });
app.UseSwaggerUI(options => {
    if (sunstealer.mvc.odata.Services.Application.Auth)
    {
        options.EnablePersistAuthorization();
        options.OAuthAppName(AppName);
        options.OAuthClientId("sunstealer.code");
        options.OAuthUsePkce();
    }
    options.RoutePrefix = "Swagger";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

if (sunstealer.mvc.odata.Services.Application.Auth)
{
    app.MapControllers().RequireAuthorization();
    app.MapControllerRoute(
        name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"
    ).RequireAuthorization();
} 
else {
    app.MapControllers();
    app.MapControllerRoute(
        name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"
    );
}

app.Run();

