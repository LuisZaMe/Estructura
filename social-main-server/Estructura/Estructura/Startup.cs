using Estructura.API.Utilities;
using Estructura.Core.ConfigurationReflection;
using Estructura.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura
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
            //Cors stuff
            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddMemoryCache();
            services.AddControllers();
            AddSwagger(services);
            services.AddHttpContextAccessor();
            services.Configure<APIConfig>(Configuration);

            

            //Tokenization sources
            var jwtSettings = Configuration.GetSection("TokenizationCredentials");
            var secretKey = jwtSettings.GetValue<string>("Key");
            var hash = jwtSettings.GetValue<string>("ApiHashKey");
            var minutes = jwtSettings.GetValue<int>("MinutesToLive");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var RequireIssuer = jwtSettings.GetValue<bool>("RequireHttpsMetadata");

            #region Utilities
            services.AddSingleton(s => s.GetRequiredService<IOptions<APIConfig>>().Value);
            services.AddSingleton<ITokenUtil, API.Utilities.TokenUtil>();
            services.AddSingleton<IAuthorizationHandler, HasRequiredRoleForActionHandler>();
            services.AddSingleton<Core.Utilities.ISendMail, DAL.Utilities.SendMail>();
            services.AddScoped<System.Security.Claims.ClaimsPrincipal>(a => a.GetService<IHttpContextAccessor>().HttpContext.User);//claims

            #endregion

            #region BLL
            services.AddScoped<Core.Services.IAuthService, BLL.Services.AuthService>();
            services.AddScoped<Core.Services.IUtilitiesService, BLL.Services.UtilitiesService>();
            services.AddScoped<Core.Services.IAccountService, BLL.Services.AccountService>();
            services.AddScoped<Core.Services.ICandidateService, BLL.Services.CandidateService>();
            services.AddScoped<Core.Services.IStudyService, BLL.Services.StudyService>();
            services.AddScoped<Core.Services.IVisitService, BLL.Services.VisitService>();
            services.AddScoped<Core.Services.IMediaService, BLL.Services.MediaService>();
            services.AddScoped<Core.Services.IFileService, BLL.Services.FileService>();
            services.AddScoped<Core.Services.IStudyFinalResultService, BLL.Services.StudyFinalResultService>();
            services.AddScoped<Core.Services.IStudyGeneralInformationService, BLL.Services.StudyGeneralInformationService>();
            services.AddScoped<Core.Services.IStudySchoolarityService, BLL.Services.StudySchoolarityService>();
            services.AddScoped<Core.Services.IStudyFamilyService, BLL.Services.StudyFamilyService>();
            services.AddScoped<Core.Services.IStudyEconomicSituationService, BLL.Services.StudyEconomicSituationService>();
            services.AddScoped<Core.Services.IStudySocialService, BLL.Services.StudySocialService>();
            services.AddScoped<Core.Services.IStudyLaboralTrayectoryService, BLL.Services.StudyLaboralTrayectoryService>();
            services.AddScoped<Core.Services.IStudyIMSSValidationService, BLL.Services.StudyIMSSValidationService>();
            services.AddScoped<Core.Services.IStudyPersonalReferenceService, BLL.Services.StudyPersonalReferenceService>();
            services.AddScoped<Core.Services.IStudyPicturesService, BLL.Services.StudyPicturesService>();
            #endregion

            #region DAL
            services.AddScoped<Core.Repositories.IAuthRepository, DAL.Repositories.AuthRepository>();
            services.AddScoped<Core.Repositories.IUtilitiesRepository, DAL.Repositories.UtilitiesRepository>();
            services.AddScoped<Core.Repositories.IAccountRepository, DAL.Repositories.AccountRepository>();
            services.AddScoped<Core.Repositories.ICompanyRepository, DAL.Repositories.CompanyRepository>();
            services.AddScoped<Core.Repositories.ICandidateRepository, DAL.Repositories.CandidateRepository>();
            services.AddScoped<Core.Repositories.IStudyRepository, DAL.Repositories.StudyRepository>();
            services.AddScoped<Core.Repositories.IVisitRepository, DAL.Repositories.VisitRepository>();
            services.AddScoped<Core.Repositories.IMediaRepository, DAL.Repositories.MediaRepository>();
            services.AddScoped<Core.Repositories.IFileRepository, DAL.Repositories.FileRepository>();
            services.AddScoped<Core.Repositories.IStudyFinalResultRepository, DAL.Repositories.StudyFinalResultRepository>();
            services.AddScoped<Core.Repositories.IStudyGeneralInformationRepository, DAL.Repositories.StudyGeneralInformationRepository>();
            services.AddScoped<Core.Repositories.IStudySchoolarityRepository, DAL.Repositories.StudySchoolarityRepository>();
            services.AddScoped<Core.Repositories.IStudyFamilyRepository, DAL.Repositories.StudyFamilyRepository>();
            services.AddScoped<Core.Repositories.IStudyEconomicSituationRepository, DAL.Repositories.StudyEconomicSituationRepository>();
            services.AddScoped<Core.Repositories.IStudySocialRepository, DAL.Repositories.StudySocialRepository>();
            services.AddScoped<Core.Repositories.IStudyLaboralTrayectoryRepository, DAL.Repositories.StudyLaboralTrayectoryRepository>();
            services.AddScoped<Core.Repositories.IStudyIMSSValidationRepository, DAL.Repositories.StudyIMSSValidationRepository>();
            services.AddScoped<Core.Repositories.IStudyPersonalReferenceRepository, DAL.Repositories.StudyPersonalReferenceRepository>();
            services.AddScoped<Core.Repositories.IStudyPicturesRepository, DAL.Repositories.StudyPicturesRepository>();
            #endregion

            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var hashBytes = Encoding.ASCII.GetBytes(hash);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(hashBytes),//secret key
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(config =>
            {
                // no es necesario que los roles coincidan con las politicas de acceso, cambiarlo a full acces para admin, partial access para admin y los demas internos y exteno para el rol externo
                // encapsular bien los roles con los permisos y demas no tal cual como el comentario superior
                //config.AddPolicy(Core.Policies.Policies.SuperAdministrador, new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Core.Policies.Policies.SuperAdministrador).Build());
                config.AddPolicy(Core.Policies.Policies.SuperAdministrador, policy =>
                {
                    policy.Requirements.Add(new HasRequiredRoleForAction(new List<Common.Enums.Role>()
                    { Common.Enums.Role.SUPER_ADMINISTRADOR
                    }));
                });
                //config.AddPolicy(Core.Policies.Policies.Administrador, new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Core.Policies.Policies.Administrador).Build());
                config.AddPolicy(Core.Policies.Policies.Administrador, policy =>
                {
                    policy.Requirements.Add(new HasRequiredRoleForAction(new List<Common.Enums.Role>()
                    {Common.Enums.Role.ADMINISTRADOR, Common.Enums.Role.SUPER_ADMINISTRADOR
                    }));
                });
                config.AddPolicy(Core.Policies.Policies.InternoPlataforma, policy =>
                {
                    policy.Requirements.Add(new HasRequiredRoleForAction(new List<Common.Enums.Role>()
                    {Common.Enums.Role.ADMINISTRADOR,Common.Enums.Role.INTERNO_ANALISTA,Common.Enums.Role.INTERNO_ENTREVISTADOR, Common.Enums.Role.SUPER_ADMINISTRADOR
                    }));
                });
                config.AddPolicy(Core.Policies.Policies.InternoGlobal, policy =>
                {
                    policy.Requirements.Add(new HasRequiredRoleForAction(new List<Common.Enums.Role>()
                    {Common.Enums.Role.ADMINISTRADOR,Common.Enums.Role.INTERNO_ANALISTA,Common.Enums.Role.INTERNO_ENTREVISTADOR, Common.Enums.Role.CLIENTES, Common.Enums.Role.SUPER_ADMINISTRADOR
                    }));
                    //If more roles are required as an AND, previous sentence is an OR
                    //policy.Requirements.Add(new HasRequiredRoleForAction(Common.Enums.Role.INTERNO_ANALISTA));
                    //policy.Requirements.Add(new HasRequiredRoleForAction(Common.Enums.Role.INTERNO_ENTREVISTADOR));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estructura V1");
            });

            app.UseRouting();
            app.UseCors("foo");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Estructura {groupName}",
                    Version = groupName,
                    Description = "Estructura API",
                    Contact = new OpenApiContact
                    {
                        Name = "Estructura",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Token ** ONLY **",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });
        }

    }
}
