using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Services;

namespace ClothingStore.Infrastructure.Services
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<ProcessProductImageJob> _queue;

        private BackgroundTaskQueue()
        {
            _queue = Channel.CreateBounded<ProcessProductImageJob>(new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.Wait
            });
        }

        public async ValueTask QueueAsync(
        ProcessProductImageJob job)
        {
            if(job is null)
                throw new ArgumentNullException(nameof(job));

            await _queue.Writer.WriteAsync(job);
        }
            
        public async ValueTask<ProcessProductImageJob> DequeueAsync(CancellationToken cancellationToken)
        {
            var job = await _queue.Reader.ReadAsync(cancellationToken);

            return job;
        }

    }
}
