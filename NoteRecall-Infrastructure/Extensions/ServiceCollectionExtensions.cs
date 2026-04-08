using Microsoft.Extensions.DependencyInjection;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Add repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IReviewSessionRepository, ReviewSessionRepository>();
            services.AddScoped<IReviewResultRepository, ReviewResultRepository>();

            // Add other infrastructure services as needed
        }
    }
}
