using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueAsync(ProcessProductImageJob job);

        ValueTask<ProcessProductImageJob> DequeueAsync(CancellationToken cancellationToken);
    }
}
