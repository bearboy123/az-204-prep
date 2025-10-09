using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyFuncFolder;

public class MyTimerTrigger
{
    private readonly ILogger<MyTimerTrigger> _logger;

    public MyTimerTrigger(ILogger<MyTimerTrigger> logger)
    {
        _logger = logger;
    }

    [Function("MyTimerTrigger")] // triggered every 10 seconds
    public Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.UtcNow);

        if (myTimer?.ScheduleStatus?.Next is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next.ToString("o"));
        }

        return Task.CompletedTask;
    }
}