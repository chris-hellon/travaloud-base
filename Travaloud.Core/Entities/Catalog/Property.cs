namespace Travaloud.Core.Entities.Catalog
{
    public class Property : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string ImagePath { get; private set; }
        public string ThumbnailImagePath { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string TelephoneNumber { get; private set; }
        public string GoogleMapsPlaceId { get; private set; }
        public string PageTitle { get; private set; }
        public string PageSubTitle { get; private set; }
        public string CloudbedsKey { get; set; }

        public string FriendlyUrl { get; set; }
        public string FriendlyPageTitle { get; set; }
        public string FriendlyPageSubTitle { get; set; }

        public virtual IEnumerable<Tour> Tours { get; set; } = default!;
        public virtual IEnumerable<PropertyDirection> Directions { get; set; } = default!;
        public virtual IEnumerable<PropertyRoom> Rooms { get; set; } = default!;
        public virtual IEnumerable<PropertyFacility> Facilities { get; set; } = default!;

        public Property()
        {

        }

        public Property(string name, string description, string shortDescription, string imagePath, string thumbnailImagePath, string addressLine1, string addressLine2, string telephoneNumber, string googleMapsPlaceId, string pageTitle, string pageSubTitle, string cloudbedsKey)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            ImagePath = imagePath;
            ThumbnailImagePath = thumbnailImagePath;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            TelephoneNumber = telephoneNumber;
            GoogleMapsPlaceId = googleMapsPlaceId;
            PageTitle = pageTitle;
            PageSubTitle = pageSubTitle;
            CloudbedsKey = cloudbedsKey;
            FriendlyUrl = name.UrlFriendly();
            FriendlyPageTitle = pageTitle.UrlFriendly();
            FriendlyPageSubTitle = pageSubTitle.UrlFriendly();
        }

        public Property Update(string name, string description, string shortDescription, string imagePath, string addressLine1, string addressLine2, string telephoneNumber, string googleMapsPlaceId, string pageTitle, string pageSubTitle, string cloudbedsKey)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (shortDescription is not null && ShortDescription?.Equals(shortDescription) is not true) ShortDescription = shortDescription;
            if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
            if (addressLine1 is not null && AddressLine1?.Equals(addressLine1) is not true) AddressLine1 = addressLine1;
            if (addressLine2 is not null && AddressLine2?.Equals(addressLine2) is not true) AddressLine2 = addressLine2;
            if (addressLine1 is not null && AddressLine1?.Equals(addressLine1) is not true) AddressLine1 = addressLine1;
            if (telephoneNumber is not null && TelephoneNumber?.Equals(telephoneNumber) is not true) TelephoneNumber = telephoneNumber;
            if (googleMapsPlaceId is not null && GoogleMapsPlaceId?.Equals(googleMapsPlaceId) is not true) GoogleMapsPlaceId = googleMapsPlaceId;
            if (pageTitle is not null && PageTitle?.Equals(pageTitle) is not true) PageTitle = pageTitle;
            if (pageSubTitle is not null && PageSubTitle?.Equals(pageSubTitle) is not true) PageSubTitle = pageSubTitle;
            if (cloudbedsKey is not null && CloudbedsKey?.Equals(cloudbedsKey) is not true) CloudbedsKey = cloudbedsKey;
            return this;
        }

        public Property ClearImagePath()
        {
            ImagePath = string.Empty;
            return this;
        }
    }
}

