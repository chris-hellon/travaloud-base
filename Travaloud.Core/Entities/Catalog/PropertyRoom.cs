namespace Travaloud.Core.Entities.Catalog
{
    public class PropertyRoom : AuditableEntity, IAggregateRoot
    {
        public Guid PropertyId { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string ImagePath { get; private set; }
        public string PageTitle { get; private set; }
        public string PageSubTitle { get; private set; }

        public virtual IEnumerable<Image> Images { get; private set; } = default!;

        public PropertyRoom()
        {

        }

        public PropertyRoom(string name, string description, string shortDescription, string imagePath, string pageTitle, string pageSubTitle)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            ImagePath = imagePath;
            PageTitle = pageTitle;
            PageSubTitle = pageSubTitle;
        }

        public PropertyRoom Update(string name, string description, string shortDescription, string imagePath, string pageTitle, string pageSubTitle)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (shortDescription is not null && ShortDescription?.Equals(shortDescription) is not true) ShortDescription = shortDescription;
            if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
            if (pageTitle is not null && PageTitle?.Equals(pageTitle) is not true) PageTitle = pageTitle;
            if (pageSubTitle is not null && PageSubTitle?.Equals(pageSubTitle) is not true) PageSubTitle = pageSubTitle;
            return this;
        }
    }
}

