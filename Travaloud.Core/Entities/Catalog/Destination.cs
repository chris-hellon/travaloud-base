namespace Travaloud.Core.Entities.Catalog
{
    public class Destination : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string Directions { get; private set; }
        public string ImagePath { get; private set; }
        public string GoogleMapsKey { get; private set; }

        public Destination(string name, string description, string shortDescription, string directions, string imagePath, string googleMapsKey)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            Directions = directions;
            ImagePath = imagePath;
            GoogleMapsKey = googleMapsKey;
        }

        public Destination Update(string name, string description, string shortDescription, string directions, string imagePath, string googleMapsKey)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (shortDescription is not null && ShortDescription?.Equals(shortDescription) is not true) ShortDescription = shortDescription;
            if (directions is not null && Directions?.Equals(directions) is not true) Directions = directions;
            if (imagePath is not null && ImagePath?.Equals(description) is not true) ImagePath = imagePath;
            if (googleMapsKey is not null && GoogleMapsKey?.Equals(googleMapsKey) is not true) GoogleMapsKey = googleMapsKey;

            return this;
        }

        public Destination ClearImagePath()
        {
            ImagePath = string.Empty;
            return this;
        }
    }
}

