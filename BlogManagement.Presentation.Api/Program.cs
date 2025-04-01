
using _0_Framework.Application;
using BlogManagement.Configuration;
using CommentManagement.Infrastructure.Configuration;
using ServiceHost;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace BlogManagement.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddBlogManagementServices(builder.Configuration);
            builder.Services.AddCommentManagementServices(builder.Configuration);
            builder.Services.AddTransient<IAuthHelper, AuthHelper>();
            builder.Services.AddTransient<IFileUploader, FileUploader>(); 
            builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
