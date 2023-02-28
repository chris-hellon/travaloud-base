namespace Travaloud.Core.Interfaces
{
	public interface IPropertiesRepository
	{
        Task<IEnumerable<Property>> GetProperties(string tenantId);
        Task<Property> GetProperty(string tenantId, Guid propertyId);
        Task GetPropertyInformation(Property property);
        Task<IEnumerable<PropertyDirection>> GetPropertyDirections(Guid propertyId);
        Task CreatePropertyDirectionContents(Guid propertyDirectionId, PropertyDirectionContent propertyDirectionContent);
    }
}

