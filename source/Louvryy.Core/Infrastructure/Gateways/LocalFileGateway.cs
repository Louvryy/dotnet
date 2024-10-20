using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using Louvryy.Core.Domain.Interfaces;

namespace Louvryy.Core.Infrastructure.Gateways;

public class LocalFileGateway(IOptions<LocalFileGatewayConfiguration> configuration) : IFileGateway
{
    private readonly IOptions<LocalFileGatewayConfiguration> Configuration = configuration;

    public string MakeUrl(string filename)
    {
        return $"{Configuration.Value.BasePath}/{filename}";
    }
}

public record LocalFileGatewayConfiguration
{
    [Required]
    public string BasePath { get; set; }
}