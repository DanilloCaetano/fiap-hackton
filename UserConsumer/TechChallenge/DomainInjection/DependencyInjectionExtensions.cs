using AutoMapper;
using Common.MessagingService;
using Domain.Doctor.Repository;
using Domain.Doctor.Service;
using Domain.Patient.Repository;
using Domain.Patient.Service;
using Domain.Schedule.Repository;
using Domain.Schedule.Service;
using Infraestructure.Context;
using Infraestructure.Repository.Doctor;
using Infraestructure.Repository.Patient;
using Infraestructure.Repository.Schedule;
using Integration;
using Microsoft.EntityFrameworkCore;
using RegistrationService.DomainInjection;

namespace TechChallenge1.DomainInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureContext(services, configuration);
            AddMapper(services, configuration);
            ConfigureSchedule(services);
            ConfigureScheduleDoctor(services);
            ConfigureDoctor(services);
            ConfigurePatient(services);
            ConfigureRabbit(services);
            ConfigureIntegration(services);

            return services;
        }

        private static void ConfigureIntegration(IServiceCollection services)
        {
            services.AddScoped<IIntegrationService, IntegrationService>();
        }

        private static void ConfigureRabbit(IServiceCollection services)
        {
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }

        public static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TechChallengeContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        private static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            MapperConfiguration? mapperConfig = new(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static void ConfigureSchedule(this IServiceCollection services)
        {
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IScheduleService, ScheduleService>();
        }

        public static void ConfigureScheduleDoctor(this IServiceCollection services)
        {
            services.AddScoped<IScheduleDoctorRepository, ScheduleDoctorRepository>();
            services.AddScoped<IScheduleDoctorService, ScheduleDoctorService>();
        }

        public static void ConfigureDoctor(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
        }

        public static void ConfigurePatient(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
        }
    }
}
