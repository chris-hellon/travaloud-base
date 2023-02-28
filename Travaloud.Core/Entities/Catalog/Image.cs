namespace Travaloud.Core.Entities.Catalog
{
    public class Image : AuditableEntity, IAggregateRoot
    {
        public string ImagePath { get; private set; }
        public Guid EntityKey { get; private set; }
        public string EntityName { get; private set; }
        public string Title { get; private set; }
        public string SubTitle1 { get; private set; }
        public string SubTitle2 { get; private set; }
        public string Href { get; private set; }

        public Image(string imagePath, Guid entityKey, string entityName, string title, string subTitle1, string subTitle2, string href)
        {
            ImagePath = imagePath;
            EntityKey = entityKey;
            EntityName = entityName;
            Title = title;
            SubTitle1 = subTitle1;
            SubTitle2 = subTitle2;
            Href = href;
        }

        public Image Update(string imagePath, string title, string subTitle1, string subTitle2, string href)
        {
            if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
            if (title is not null && Title?.Equals(title) is not true) Title = title;
            if (subTitle1 is not null && SubTitle1?.Equals(subTitle1) is not true) SubTitle1 = subTitle1;
            if (subTitle2 is not null && SubTitle2?.Equals(subTitle2) is not true) SubTitle2 = subTitle2;
            if (href is not null && Href?.Equals(href) is not true) Href = href;
            return this;
        }
    }
}

