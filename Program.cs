using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SpellViewer.Data;
using SpellViewer.Models;
using SpellViewer.Repositories;
using SpellViewer.Tools;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//------------Add services to the container------------\\
builder.Services.AddControllers();

// singleton's
builder.Services.AddSingleton<IJsonTools, JsonTools>();

//Transient's
builder.Services.AddTransient<IMasterSpellsRepo, MasterSpellsRepo>();
builder.Services.AddTransient<IUserMapper, UserMapper>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<ICharactersRepo,CharactersRepo>();
builder.Services.AddTransient<ITokenRepo, TokenRepo>();


// --- Data Base Stuff --- \\
var location = "localDefault";

var connString = builder.Configuration.GetConnectionString(location);

builder.Services.AddDbContext<SpellViewerContext>(options 
=> {
    options.EnableDetailedErrors(true);
    options.UseSqlServer(connString);
});

builder.Services.Configure<IdentityOptions>(options 
=> {
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Authentication & 
// Authorization 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))
    });

builder.Services.AddAuthorization(options 
=> {
    options.AddPolicy("GuestPolicy", policy =>
    {
        policy.RequireRole("Guest", "User", "Admin");
    });
    options.AddPolicy("UserPolicy", policy => 
    {
        policy.RequireRole("User","Admin");
    });
    options.AddPolicy("AdminPolicy", policy => 
    {
        policy.RequireRole("Admin");
    });
});

//--------------------

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Example Value: bearer {token}",
        In=ParameterLocation.Header,
        Name="Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
-------------------------
app.UseHttpsRedirection();
--------------------------
*/
app.UseStaticFiles("/client");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(
        endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client";
    spa.Options.DevServerPort = 4200;

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});
app.Run();
