using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gRPCService101.DataAccessor;
using gRPCService101.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace gRPCService101
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddTransient<BaseDataAccessor, OracleDataAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// add the StaticFIles middleware before app.UseRouting() and configure it to only serve proto files located in the /protos directory.
            //// client can navigate to http://<server name> : <port>/proto/greet.proto
            //var provider = new FileExtensionContentTypeProvider();
            //provider.Mappings.Clear();
            //provider.Mappings[".proto"] = "text/plain";
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Protos")),
            //    RequestPath = "/proto",
            //    ContentTypeProvider = provider
            //});


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<OrderService>().EnableGrpcWeb();


                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
