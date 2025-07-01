// <copyright file="Program.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Blazor
{
    using ResumeProject.Blazor.Components;
    using ResumeProject.Blazor.Services;

    /// <summary>
    /// The Program class is the entry point for the Blazor application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method is the entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(baseUrl!),
            });

            builder.Services.AddScoped<ResumeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}