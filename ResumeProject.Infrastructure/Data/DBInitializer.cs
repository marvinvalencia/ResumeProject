﻿// <copyright file="DBInitializer.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The DbInitializer class is responsible for seeding the database with initial data, such as roles.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// The SeedRolesAsync method seeds the database with predefined roles if they do not already exist.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>Task.</returns>
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine($"Error creating role {roleName}: {ex.Message}");
                }
            }
        }
    }
}
