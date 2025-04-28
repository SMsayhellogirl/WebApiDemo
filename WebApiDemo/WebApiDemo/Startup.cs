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
        // 建構子，讀取設定檔 (如 appsettings.json)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 註冊服務到 DI 容器中
        public void ConfigureServices(IServiceCollection services)
        {
            // 註冊自訂的 Filter（必須先註冊才能使用 Add<T>()）
            services.AddScoped<LoginFilter>();
            services.AddScoped<ErrorHandleFilter>();

            // 註冊 MVC Controller 並加上全域 Filter
            services.AddControllers(options =>
            {
                //options.Filters.Add<LoginFilter>();         // 登入驗證過濾器
                options.Filters.Add<ErrorHandleFilter>();    // 全域錯誤處理過濾器
            });

            // 註冊 Swagger 服務，用來產生 API 文件
            services.AddSwaggerGen(c =>
            {
                // 這裡告訴 Swagger 讀取 XML 註解文件
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

        // 配置 HTTP 請求管道（Middleware）
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 如果是開發環境，顯示詳細錯誤頁
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // 非開發環境，啟用 HTTP Strict Transport Security (HSTS)
                app.UseHsts();
            }

            // 啟用提供靜態檔案（如：Swagger UI需要的css, js）
            app.UseStaticFiles();

            // 啟用 Swagger 中介軟體，產生 Swagger JSON
            app.UseSwagger();

            // 啟用 Swagger UI，提供互動式 API 說明頁
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
                c.RoutePrefix = string.Empty; // 設空代表打開網站首頁直接是Swagger
            });

            // 啟用路由功能
            app.UseRouting();

            // 配置終端點（Endpoints），將Controller對應到路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();   // 自動對應 Controller 路由
            });
        }
    }
}
