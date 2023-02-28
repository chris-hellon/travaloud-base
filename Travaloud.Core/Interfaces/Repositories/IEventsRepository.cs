namespace Travaloud.Core.Interfaces.Repositories
{
	public interface IEventsRepository 
	{
        Task<IEnumerable<Travaloud.Core.Entities.Catalog.Event>> GetEvents(string tenantId, Guid? propertyId = null);
    }
}

