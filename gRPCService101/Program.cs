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

        //https://github.com/npgsql/efcore.pg/issues/225
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                    webBuilder.ConfigureKestrel(kestrel =>
                    {
                        // 127.0.0.1:<port>
                        //kestrel.ListenLocalhost(6000, o => o.Protocols =  HttpProtocols.Http2);
                        kestrel.ConfigureHttpsDefaults(https =>
                        {
                            https.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.NoCertificate;
                            //https.ServerCertificate = new X509Certificate2();
                        });
                    });
                });
    }
}
