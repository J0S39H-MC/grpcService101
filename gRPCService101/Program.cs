using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace gRPCService101
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    ////webBuilder.UseKestrel(options =>
                    ////{
                    ////    options.ListenLocalhost(9046, o => o.Protocols = HttpProtocols.Http2);
                    ////});

                    ////webBuilder.UseKestrel(options =>
                    ////{
                    ////    options.ListenLocalhost(5001, o => o.Protocols = HttpProtocols.Http2);                        
                    ////});

                    ////var urls = hostBuilder.GetSetting("urls");
                    ////urls = urls.Replace("localhost", "127.0.0.1");
                    ////hostBuilder.UseSetting("urls", urls);

                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(kestrel =>
                    {
                        kestrel.ConfigureHttpsDefaults(https =>
                        {
                            https.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.NoCertificate;
                            //https.ServerCertificate = new X509Certificate2();
                        });
                    });
                });
    }
}
