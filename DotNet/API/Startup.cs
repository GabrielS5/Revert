using API.Services;
using Application.Authentication;
using Application.Profiles;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
           .AddApiKeySupport(options => { });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IRecordsRepository, RecordsRepository>();
            services.AddScoped<IRecordsService, RecordsService>();
            services.AddTransient<ITranslateService, TranslateService>();
            services.AddTransient<IKeywordsService, KeywordsService>();
            services.AddTransient<IClusteringService, ClusteringService>();
            services.AddTransient<IGetApiKeyQuery, InMemoryGetApiKeyMainQuery>();

            services.Configure<ExternalApisSettings>(
                options => Configuration.GetSection("ExternalApisSettings").Bind(options));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .UseLoggerFactory(new LoggerFactory()
                .AddFile("Logs/ts-{Date}.txt")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
             builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
           );
            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMvc();
        }
    }
}
