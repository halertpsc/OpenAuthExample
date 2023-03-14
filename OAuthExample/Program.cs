using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using OAuthExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<GitHubClient>();

var githubAuth = builder.Configuration.GetSection("Authentication:Github");
builder.Services.AddAuthentication(GitHubAuthenticationDefaults.AuthenticationScheme)
    .AddGitHub(options => {
        options.AccessDeniedPath = githubAuth["AccessDenied"];
        options.CallbackPath = githubAuth["Callback"];
        options.ClientId = githubAuth["ClientId"];
        options.ClientSecret = githubAuth["ClientSecret"];
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SaveTokens = true;
        options.Scope.Add("repo");
    })
.AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(1));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
