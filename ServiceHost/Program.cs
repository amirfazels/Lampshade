using AccountManagement.Configuration;
using CommentManagement.Infrastructure.Configuration;
using ShopManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using BlogManagement.Configuration;
using _0_Framework.Application;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Authentication.Cookies;
using _0_Framework.Infrastructure;
using InventoryManagement.Presentation.Api;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ShopManagement.Presentation.Api;

namespace ServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddShopManagementServices(builder.Configuration);
            builder.Services.AddDiscountManagementServices(builder.Configuration);
            builder.Services.AddInventoryManagementServices(builder.Configuration);
            builder.Services.AddBlogManagementServices(builder.Configuration);
            builder.Services.AddCommentManagementServices(builder.Configuration);
            builder.Services.AddAccountManagementServices(builder.Configuration);

            builder.Services.AddTransient<IAuthHelper, AuthHelper>();
            builder.Services.AddTransient<IFileUploader, FileUploader>();

            builder.Services.AddSingleton<IZarinPalFactory, ZarinPalFactory>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Account");
                options.LogoutPath = new PathString("/Account/");
                options.AccessDeniedPath = new PathString("/AccessDenied");
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            builder.Services
                .AddAuthorization(Options =>
                {
                    Options.AddPolicy
                    (
                        "AdminArea",
                        policy => policy.RequireRole(new List<string> 
                            { 
                                Roles.Admin, 
                                Roles.ContentUploader 
                            }
                        )
                    );
                    Options.AddPolicy
                    (
                        "AdminOnly",
                        policy => policy.RequireRole(new List<string> { Roles.Admin })
                    );
                });

            builder.Services.AddCors(options => 
                options.AddPolicy("CorsPolicy", 
                    policyBuilder => 
                        policyBuilder.WithOrigins("https://localhost:5001/")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                )
            );

            // Add services to the container.
            builder.Services.AddRazorPages()
                .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(Options =>
                    {
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "AdminOnly");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Shop/ProductCategories", "AdminOnly");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Shop/ProductPictures", "AdminOnly");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Shop/Products", "AdminOnly");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Shop/Slides", "AdminOnly");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Shop/Comments", "AdminArea");
                        Options.Conventions.AuthorizeAreaFolder("Administration", "/Account", "AdminOnly");
                    }
                )
                .AddApplicationPart(typeof(InventoryController).Assembly)
                .AddApplicationPart(typeof(ProductController).Assembly);

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();
            
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
            app.MapControllers();
            app.Run();
        }
    }
}
