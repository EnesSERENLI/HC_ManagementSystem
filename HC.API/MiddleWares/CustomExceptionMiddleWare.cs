using HC.API.Utils.Interface;
using Newtonsoft.Json;
using System.Diagnostics; //For Stopwatch
using System.Net;

namespace HC.API.MiddleWares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public CustomExceptionMiddleWare(RequestDelegate next,ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew(); // Burada işlem başladığı için süreyi başlat!
            try
            {
                string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
                _logger.Log(message);
                //Console.WriteLine(message);

                await _next(context); //Bir sonraki middleware'e işlemi paslar! Kendinsen sonraki middleware'lerin çalışması bitince alttaki işlemler devam eder!

                watch.Stop();//Request cevaplandığı için burada süreyi bitir!

                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms.";
                _logger.Log(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context,ex,watch);
            }
        }

        private Task HandleException(HttpContext context,Exception ex,Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //Hata durumunda 500 dönsün dedim ancak faklı middleware'ler yazılarak durumlara göre uygun kodlar döndürelebilir!

            string errorMessage = "[Error]    HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _logger.Log(errorMessage);
            //Console.WriteLine(errorMessage);

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message,
                Formatting.None
            });

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddleWareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}
