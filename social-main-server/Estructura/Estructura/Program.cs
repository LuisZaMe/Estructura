using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Redirigir la salida de la consola a un archivo
            var logFile = "output.log";
            var streamWriter = new StreamWriter(logFile);
            Console.SetOut(streamWriter);

            CreateHostBuilder(args).Build().Run(); //Solo tenia este

            streamWriter.Close(); // Cerrar el archivo de log
        }

        public static IHostBuilder Create;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Configure();
                        serverOptions.DisableStringReuse = true;
                    }).UseStartup<Startup>();

                });
    }
}
