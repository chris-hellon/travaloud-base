namespace Travaloud.Infrastructure.Repositories
{
    public class ToursRepository : BaseRepository, IToursRepository
    {
        public ToursRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Tour>> GetTours(string tenantId)
        {
            var tours = await DapperConnection.ExecuteGetStoredProcedureList<Tour>("GetTours", new
            {
                TenantId = tenantId
            });

            tours = tours.Select(x => {
                x.FriendlyUrl = x.Name.UrlFriendly();
                return x;
            });

            return tours;
        }
    }
}

