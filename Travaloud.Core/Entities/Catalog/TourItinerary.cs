namespace Travaloud.Core.Entities.Catalog
{
    public class TourItinerary : AuditableEntity, IAggregateRoot
    {
        public string Header { get; set; }
        public Guid TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Image> Images { get; private set; } = default!;

        public TourItinerary(string header, string title, string description, Guid tourId)
        {
            Header = header;
            Title = title;
            Description = description;
            TourId = tourId;
        }

        public TourItinerary Update(string header, string title, string description, Guid? tourId)
        {
            if (header is not null && Header?.Equals(header) is not true) Header = header;
            if (title is not null && Title?.Equals(title) is not true) Title = title;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (tourId.HasValue && tourId.Value != Guid.Empty && !TourId.Equals(tourId.Value)) TourId = tourId.Value;
            return this;
        }
    }
}

