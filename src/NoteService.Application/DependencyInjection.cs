using Microsoft.Extensions.DependencyInjection;

namespace NoteService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
