// <copyright file="ResumeService.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Blazor.Services
{
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The ResumeService class provides methods to interact with the resume API.
    /// </summary>
    public class ResumeService
    {
        private readonly HttpClient http;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeService"/> class.
        /// </summary>
        /// <param name="http">The http client.</param>
        public ResumeService(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Gets the Resume.
        /// </summary>
        public Resume? Resume { get; private set; } = null;

        /// <summary>
        /// Gets the Error Message.
        /// </summary>
        public string? ErrorMessage { get; private set; } = string.Empty;

        /// <summary>
        /// The GetResumesAsync method retrieves resume by its Id.
        /// </summary>
        /// <param name="resumeId">The resume Id.</param>
        /// <returns>The resume.</returns>
        public async Task<Resume?> GetResumeAsync(Guid resumeId)
        {
            try
            {
                this.Resume = await this.http.GetFromJsonAsync<Resume>($"api/resume/{resumeId}");
            }
            catch
            {
                this.ErrorMessage = "⚠️ This is a static fallback because the API might be down or out of credits.";
            }
            return this.Resume;
        }

        /// <summary>
        /// The UpdateResumeAsync method updates an existing resume.
        /// </summary>
        /// <param name="resume">The resume.</param>
        /// <returns>The result.</returns>
        public async Task UpdateResumeAsync(Resume resume)
        {
            await this.http.PutAsJsonAsync($"api/resume/{resume.Id}", resume);
        }

        /// <summary>
        /// The CreateResumeAsync method creates a new resume.
        /// </summary>
        /// <param name="resume">The resume.</param>
        /// <returns>The result.</returns>
        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            var response = await this.http.PostAsJsonAsync("api/resume", resume);
            return await response.Content.ReadFromJsonAsync<Resume>() ?? new Resume();
        }

        /// <summary>
        /// The DeleteResumeAsync method deletes a resume by its Id.
        /// </summary>
        /// <param name="resumeId">The resume.</param>
        /// <returns>The result.</returns>
        public async Task DeleteResumeAsync(Guid resumeId)
        {
            await this.http.DeleteAsync($"api/resume/{resumeId}");
        }
    }
}
