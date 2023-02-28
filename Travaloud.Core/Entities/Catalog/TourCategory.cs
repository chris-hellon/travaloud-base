namespace Travaloud.Core.Entities.Catalog
{
    public class TourCategory : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string IconClass { get; private set; }
        public string ShortDescription { get; private set; }
        public string ImagePath { get; private set; }

        public TourCategory(string name, string description, string iconClass, string shortDescription, string imagePath)
        {
            Name = name;
            Description = description;
            IconClass = iconClass;
            ShortDescription = shortDescription;
            ImagePath = imagePath;
        }

        public TourCategory Update(string name, string description, string iconClass, string shortDescription, string imagePath)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (iconClass is not null && IconClass?.Equals(iconClass) is not true) IconClass = iconClass;
            if (shortDescription is not null && ShortDescription?.Equals(shortDescription) is not true) ShortDescription = shortDescription;
            if (imagePath is not null && ImagePath?.Equals(description) is not true) ImagePath = imagePath;
            return this;
        }

        public TourCategory ClearImagePath()
        {
            ImagePath = string.Empty;
            return this;
        }
    }
}

