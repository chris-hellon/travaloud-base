namespace Travaloud.Core.Entities.Catalog
{
    public class PropertyFacility : AuditableEntity, IAggregateRoot
    {
        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }

        public PropertyFacility()
        {

        }

        public PropertyFacility(Guid propertyId, string title)
        {
            PropertyId = propertyId;
            Title = title;
        }

        public PropertyFacility Update(string title)
        {
            if (title is not null && Title?.Equals(title) is not true) Title = title;
            return this;
        }
    }
}

