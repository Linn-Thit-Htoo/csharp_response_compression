using Microsoft.AspNetCore.ResponseCompression;

namespace csharp_response_compression.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<GzipCompressionProviderOptions>(opt =>
            {
                opt.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

            builder.Services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<GzipCompressionProvider>();
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });

            return services;
        }
    }
}
