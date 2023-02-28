namespace Travaloud.Infrastructure.Repositories
{
    public class EventsRepository : BaseRepository, IEventsRepository
    {
        public EventsRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Event>> GetEvents(string tenantId, Guid? propertyId = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Event>("GetEvents", new
            {
                TenantId = tenantId,
                PropertyId = propertyId
            });
        }
    }
}


