using Microsoft.Extensions.DependencyInjection;

namespace Chess.Presentation
{
    public static class Extensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            return services;
        }
    }
}
