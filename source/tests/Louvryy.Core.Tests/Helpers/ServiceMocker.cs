using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Helpers;

public static class ServiceMocker {
    public static (ServiceProvider, Mock<TService>) Mock<TService>(
        Action<IServiceCollection> services,
        Action<Mock<TService>> action
    ) where TService : class
    {
        // 1. Creates a new ServiceCollection
        var serviceCollection = new ServiceCollection();

        // 2. Set Services on the new ServiceCollection
        services(serviceCollection);

        // 3. Remove the current service registered for IZipCode
        ServiceDescriptor descriptor = serviceCollection.SingleOrDefault(d =>
            d.ServiceType == typeof(TService));
        serviceCollection.Remove(descriptor);

        // 4. Mock service
        var mockedService = new Mock<TService>();

        action(mockedService);

        // 5. Add service
        serviceCollection.AddScoped<TService>(x => mockedService.Object);

        // 6. Build service provider
        return (serviceCollection.BuildServiceProvider(), mockedService);
    }
}