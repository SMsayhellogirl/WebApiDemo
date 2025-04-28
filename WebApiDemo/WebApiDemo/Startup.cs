using System.IO;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiDemo.Filters;

namespace WebApiDemo
{
    public class Startup
    {
        // �غc�l�AŪ���]�w�� (�p appsettings.json)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ���U�A�Ȩ� DI �e����
        public void ConfigureServices(IServiceCollection services)
        {
            // ���U�ۭq�� Filter�]���������U�~��ϥ� Add<T>()�^
            services.AddScoped<LoginFilter>();
            services.AddScoped<ErrorHandleFilter>();

            // ���U MVC Controller �å[�W���� Filter
            services.AddControllers(options =>
            {
                //options.Filters.Add<LoginFilter>();         // �n�J���ҹL�o��
                options.Filters.Add<ErrorHandleFilter>();    // ������~�B�z�L�o��
            });

            // ���U Swagger �A�ȡA�ΨӲ��� API ���
            services.AddSwaggerGen(c =>
            {
                // �o�̧i�D Swagger Ū�� XML ���Ѥ��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Demo API",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API"
                });

            });
        }

        // �t�m HTTP �ШD�޹D�]Middleware�^
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // �p�G�O�}�o���ҡA��ܸԲӿ��~��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // �D�}�o���ҡA�ҥ� HTTP Strict Transport Security (HSTS)
                app.UseHsts();
            }

            // �ҥδ����R�A�ɮס]�p�GSwagger UI�ݭn��css, js�^
            app.UseStaticFiles();

            // �ҥ� Swagger �����n��A���� Swagger JSON
            app.UseSwagger();

            // �ҥ� Swagger UI�A���Ѥ��ʦ� API ������
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
                c.RoutePrefix = string.Empty; // �]�ťN���}�������������OSwagger
            });

            // �ҥθ��ѥ\��
            app.UseRouting();

            // �t�m�׺��I�]Endpoints�^�A�NController���������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();   // �۰ʹ��� Controller ����
            });
        }
    }
}
