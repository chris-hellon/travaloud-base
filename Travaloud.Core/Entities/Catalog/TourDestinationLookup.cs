namespace Travaloud.Core.Entities.Catalog
{
    public class TourDestinationLookup : BaseEntity, IAggregateRoot
    {
        public Guid TourId { get; private set; }
        public Guid DestinationId { get; private set; }

        public virtual Tour Tour { get; private set; } = default!;
        public virtual Destination Destination { get; private set; } = default!;

        public TourDestinationLookup(Guid tourId, Guid destinationId)
        {
            TourId = tourId;
            DestinationId = destinationId;
        }
    }
}

