using ClothingStore.Application.Common.Events;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common.Interfaces;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventDispatcher _dispatcher;

    public UnitOfWork(ApplicationDbContext context, IDomainEventDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        //// Get All Entities that be changed in Tracker
        //var entities = _context.ChangeTracker
        //           .Entries<IHasDomainEvents>()
        //           .Where(e => e.Entity.DomainEvents.Any())
        //           .Select(e => e.Entity);


        //// Get just the domain events 
        //var domainEvents = entities
        //    .SelectMany(e => e.DomainEvents)
        //    .ToList();


        //// clear domain events to not send tows 
        //foreach (var entity in entities)
        //    entity.ClearDomainEvents();

        //// send all events  but here will happened Inconsistency Problem so we will solve it by Outbox pattern 
        //await _dispatcher.DispatchAsync(domainEvents);

        // save all entities that changed to database
        return  await _context.SaveChangesAsync(cancellationToken);
    }

}