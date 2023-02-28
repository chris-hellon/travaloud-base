namespace Travaloud.Core.Entities.Catalog
{
    public class TourCategoryLookup : BaseEntity, IAggregateRoot
    {
        public Guid TourId { get; private set; }
        public Guid TourCategoryId { get; private set; }

        public virtual Tour Tour { get; private set; } = default!;
        public virtual TourCategory TourCategory { get; private set; } = default!;

        public TourCategoryLookup(Guid tourId, Guid tourCategoryId)
        {
            TourId = tourId;
            TourCategoryId = tourCategoryId;
        }
    }
}

