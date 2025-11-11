using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NOTE.Solutions.API.Health;

public class MailProviderHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {



        throw new NotImplementedException();
    }
}
