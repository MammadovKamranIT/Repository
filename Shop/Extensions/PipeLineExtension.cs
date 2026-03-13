using Shop.Api.Middleware;
using Shop.Data;


namespace Shop.Api.Extensions
{
    public static class PipeLineExtension
    {

        public static WebApplication UseOrderPipeLine(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API v1");
                    options.RoutePrefix = string.Empty;
                    options.DisplayRequestDuration();
                    options.EnableFilter();
                    options.EnableTryItOutByDefault();
                });
                app.MapOpenApi();
            }
            app.UseMiddleware<GlobalExceptionMiddleware>();
          


            app.MapControllers();
            return app;
        }

    

    }
}
