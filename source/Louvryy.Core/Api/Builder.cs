using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Api;

public static class Builder
{
    public static void ConfigureLouvryy(this IServiceCollection services)
    {
        Console.WriteLine("Hello Louvryy!");
    }
}