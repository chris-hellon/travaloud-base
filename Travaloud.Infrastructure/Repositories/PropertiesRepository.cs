using Microsoft.AspNetCore.Components.Web;

namespace Travaloud.Infrastructure.Repositories
{
    public class PropertiesRepository : BaseRepository, IPropertiesRepository
    {
        public PropertiesRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Property>> GetProperties(string tenantId)
        {
            var properties = await DapperConnection.ExecuteGetStoredProcedureList<Property>("GetProperties", new { TenantId = tenantId });

            properties = properties.Select(x => {
                x.FriendlyUrl = x.Name.UrlFriendly();
                x.FriendlyPageTitle = x.PageTitle.UrlFriendly();
                x.FriendlyPageSubTitle = x.PageSubTitle.UrlFriendly();
                return x; });

            return properties;
        }

        public async Task<Property> GetProperty(string tenantId, Guid propertyId)
        {
            var property = await DapperConnection.ExecuteGetStoredProcedureSingle<Property>("GetProperties", new { TenantId = tenantId, Id = propertyId });

            property.FriendlyUrl = property.Name.UrlFriendly();
            property.FriendlyPageTitle = property.PageTitle.UrlFriendly();
            property.FriendlyPageSubTitle = property.PageSubTitle.UrlFriendly();

            return property;
        }

        public async Task GetPropertyInformation(Property property)
        {
            var propertyId = property.Id;

            property.Rooms = await DapperConnection.ExecuteGetStoredProcedureList<PropertyRoom>("GetPropertyRooms", new { PropertyId = propertyId });
            property.Facilities = await DapperConnection.ExecuteGetStoredProcedureList<PropertyFacility>("GetPropertyFacilities", new { PropertyId = propertyId });
            property.Directions = await GetPropertyDirections(propertyId);
        }

        public async Task<IEnumerable<PropertyDirection>> GetPropertyDirections(Guid propertyId)
        {
            var directions = await DapperConnection.ExecuteGetStoredProcedureList<PropertyDirection>("GetPropertyDirections", new
            {
                PropertyId = propertyId
            });

            foreach (var direction in directions)
            {
                direction.Content = await DapperConnection.ExecuteGetStoredProcedureList<PropertyDirectionContent>("GetPropertyDirectionContents", new
                {
                    PropertyDirectionId = direction.Id
                });
            }

            return directions;
        }

        public async Task CreatePropertyDirectionContents(Guid propertyDirectionId, PropertyDirectionContent propertyDirectionContent)
        {
            await DapperConnection.ExecuteGetStoredProcedureSingle<PropertyDirectionContent>("CreateOrUpdatePropertyDirectionContents", new
            {
                PropertyDirectionId = propertyDirectionId,
                Body = propertyDirectionContent.Body,
                Style = propertyDirectionContent.Style,
                CreateId = "3B697EAC-A557-41A5-849B-A6E6839FCB8F"
            });
        }
    }
}

