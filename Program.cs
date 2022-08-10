using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;
using UserApi.Data;
using UserApi.Settings;
using UserApi.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configuration des Services

    //builder.Services.AddSingleton(builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>());
    builder.Services.AddSingleton(builder.Configuration.GetSection("JWTSettings").Get<JWTSettings>());
    builder.Services.AddSingleton(builder.Configuration.GetSection("ApiToBotSettings").Get<ApiToBotSettings>());
    builder.Services.AddSingleton(builder.Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>());
    builder.Services.AddControllers();

    AddCORS(builder); //Permet de d�finir les CallsOrigins 
    AddDatabase(builder);
    AddJWT(builder);
    AddServices(builder);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        var API_NAME = Assembly.GetExecutingAssembly().GetName().Name;
        var xmlPath = $"{AppContext.BaseDirectory}{API_NAME}.xml";

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = API_NAME,
            Description = "API for Guanajuato R�lePlay"
        });
        c.IncludeXmlComments(xmlPath);
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "bearer",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string [] {}
                    }
                });
    });
    

    AddItentity(builder);

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css"); //Get Swagger in dark mode
    });
}

ConfigureExceptionHandler(app);

app.UseAuthentication();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.Run();

void AddCORS(WebApplicationBuilder builder)
{
    List<string> originsAllowed = builder.Configuration.GetSection("CallsOrigins").Get<List<string>>();
    builder.Services.AddCors(o =>
    {
        o.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
            //builder.WithOrigins(originsAllowed.ToArray()).AllowAnyHeader().AllowAnyMethod().Build();
        });
    });
}

void AddDatabase(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<UserApiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserSQL")));
    Console.WriteLine(builder.Configuration.GetConnectionString("UserSQL"));
}

void AddJWT(WebApplicationBuilder builder)
{
    var jwtSettings = builder.Configuration.GetSection("JWTSettings").Get<JWTSettings>();
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateLifetime = true,
            RoleClaimType = "Roles",
            NameClaimType = "Name",
            
        };
    });
    builder.Services.AddAuthorization(options => 
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("Bearer")
            .Build();
    });


}

void AddServices(WebApplicationBuilder builder)
{
    //builder.Services.AddScoped<ServiceType>();
    //builder.Services.AddScoped<ServiceType>();
    //builder.Services.AddScoped<ServiceType>();
    //builder.Services.AddScoped<ServiceType>();
}

void AddItentity(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<ApiUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;

        options.ClaimsIdentity.RoleClaimType = "Roles";
        options.ClaimsIdentity.UserIdClaimType = "Username";
        options.ClaimsIdentity.EmailClaimType = "DiscordId";

        //Password requirement
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 10;
        options.Password.RequiredUniqueChars = 4; //Determine le nombre de caract�re unnique minimum requis


        //Lockout si mdp fail 5 fois alors compte bloquer 10 min
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
        options.Lockout.AllowedForNewUsers = true;

        //User
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    })
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserApiContext>();


    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

}

void ConfigureExceptionHandler(IApplicationBuilder app)
{
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeatures == null) return;

            context.Response.ContentType = "text/html; charset=utf-8";
            string message = string.Empty;
            var user = context?.User?.Identity?.Name ?? "Unknow User";
            if (contextFeatures.Error is ServiceException se)
            {
                context.Response.StatusCode = (int)se.StatusCode;
                message = se.Message;

            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = "Internal Server Error";
            }

            await context.Response.WriteAsync(message);
        });
    });
}