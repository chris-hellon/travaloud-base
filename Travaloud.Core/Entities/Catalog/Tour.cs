namespace Travaloud.Core.Entities.Catalog
{
    public class Tour : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public decimal Price { get; private set; }
        public string PriceLabel { get; private set; }
        public string ImagePath { get; private set; }
        public string ThumbnailImagePath { get; private set; }
        public int MaxCapacity { get; private set; }
        public int MinCapacity { get; private set; }
        public string DayDuration { get; private set; }
        public string NightDuration { get; private set; }
        public string HourDuration { get; private set; }
        public string Address { get; private set; }
        public string TelephoneNumber { get; private set; }
        public string WhatsIncluded { get; private set; }
        public string WhatsNotIncluded { get; private set; }
        public string AdditionalInformation { get; private set; }

        public string FriendlyUrl { get; set; }

        public virtual IEnumerable<TourDate> TourDates { get; private set; } = default!;
        public virtual IEnumerable<TourItinerary> TourItineraries { get; private set; } = default!;

        public Tour()
        {

        }

        public Tour(string name, string description, string shortDescription, decimal price, string priceLabel, string imagePath, string thumbnailImagePath, int maxCapacity, int minCapacity, string dayDuration, string nightDuration, string hourDuration, string address, string telephoneNumber, string whatsIncluded, string whatsNotIncluded, string additionalInformation)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            Price = price;
            PriceLabel = priceLabel;
            ImagePath = imagePath;
            ThumbnailImagePath = thumbnailImagePath;
            MaxCapacity = maxCapacity;
            MinCapacity = minCapacity;
            DayDuration = dayDuration;
            NightDuration = nightDuration;
            HourDuration = hourDuration;
            Address = address;
            TelephoneNumber = telephoneNumber;
            WhatsIncluded = whatsIncluded;
            WhatsNotIncluded = whatsNotIncluded;
            AdditionalInformation = additionalInformation;
        }

        public Tour Update(string name, string description, string shortDescription, decimal? price, string priceLabel, string imagePath, string thumbnailImagePath, int? maxCapacity, int? minCapacity, string dayDuration, string nightDuration, string hourDuration, string address, string telephoneNumber, string whatsIncluded, string whatsNotIncluded, string additionalInformation)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (shortDescription is not null && ShortDescription?.Equals(shortDescription) is not true) ShortDescription = shortDescription;
            if (price.HasValue && Price != price) Price = price.Value;
            if (priceLabel is not null && PriceLabel?.Equals(priceLabel) is not true) PriceLabel = priceLabel;
            if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
            if (thumbnailImagePath is not null && ThumbnailImagePath?.Equals(thumbnailImagePath) is not true) ThumbnailImagePath = thumbnailImagePath;
            if (maxCapacity.HasValue && MaxCapacity != maxCapacity) MaxCapacity = maxCapacity.Value;
            if (minCapacity.HasValue && MinCapacity != minCapacity) MinCapacity = minCapacity.Value;
            if (dayDuration is not null && DayDuration != dayDuration) DayDuration = dayDuration;
            if (nightDuration is not null && NightDuration != nightDuration) NightDuration = nightDuration;
            if (hourDuration is not null && HourDuration != hourDuration) HourDuration = hourDuration;
            if (address is not null && Address?.Equals(address) is not true) Address = address;
            if (telephoneNumber is not null && TelephoneNumber?.Equals(telephoneNumber) is not true) TelephoneNumber = telephoneNumber;
            if (whatsIncluded is not null && WhatsIncluded?.Equals(whatsIncluded) is not true) WhatsIncluded = whatsIncluded;
            if (whatsNotIncluded is not null && WhatsNotIncluded?.Equals(whatsNotIncluded) is not true) WhatsNotIncluded = whatsNotIncluded;
            if (additionalInformation is not null && AdditionalInformation?.Equals(additionalInformation) is not true) AdditionalInformation = additionalInformation;
            return this;
        }

        public Tour ClearImagePath()
        {
            ImagePath = string.Empty;
            return this;
        }
    }
}

