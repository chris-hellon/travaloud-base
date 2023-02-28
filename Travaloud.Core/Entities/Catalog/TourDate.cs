namespace Travaloud.Core.Entities.Catalog
{
    public class TourDate : AuditableEntity, IAggregateRoot
    {
        public DateTime StartDate { get; set; }
        public Guid TourId { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSpaces { get; set; }


        public TourDate(DateTime startDate, DateTime endDate, int availableSpaces, Guid tourId)
        {
            StartDate = startDate;
            EndDate = endDate;
            AvailableSpaces = availableSpaces;
            TourId = tourId;
        }

        public TourDate Update(DateTime? startDate, DateTime? endDate, int? availableSpaces, Guid? tourId)
        {
            if (startDate is not null && !StartDate.Equals(startDate)) StartDate = startDate.Value;
            if (endDate is not null && !EndDate.Equals(endDate)) EndDate = endDate.Value;
            if (availableSpaces is not null && !AvailableSpaces.Equals(availableSpaces)) AvailableSpaces = availableSpaces.Value;
            if (tourId.HasValue && tourId.Value != Guid.Empty && !TourId.Equals(tourId.Value)) TourId = tourId.Value;
            return this;
        }
    }
}

