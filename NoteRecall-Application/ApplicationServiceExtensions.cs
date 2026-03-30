using Microsoft.Extensions.DependencyInjection;
using NoteRecall_Application.ServiceImplementation;
using NoteRecall_Application.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IReviewSessionService, ReviewSessionService>();


        }
    }
}
