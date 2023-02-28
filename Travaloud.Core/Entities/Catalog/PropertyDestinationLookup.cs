namespace Travaloud.Core.Entities.Catalog
{
    public class PropertyDestinationLookup : BaseEntity, IAggregateRoot
    {
        public Guid PropertyId { get; private set; }
        public Guid DestinationId { get; private set; }

        public virtual Property Property { get; private set; } = default!;
        public virtual Destination Destination { get; private set; } = default!;

        public PropertyDestinationLookup(Guid propertyId, Guid destinationId)
        {
            PropertyId = propertyId;
            DestinationId = destinationId;
        }
    }
}

