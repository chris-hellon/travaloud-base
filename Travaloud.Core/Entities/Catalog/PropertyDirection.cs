namespace Travaloud.Core.Entities.Catalog
{
    public class PropertyDirection : AuditableEntity, IAggregateRoot
    {
        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }

        public virtual IEnumerable<PropertyDirectionContent> Content { get; set; } = default!;

        public PropertyDirection()
        {

        }

        public PropertyDirection(Guid hostelId, string title)
        {
            PropertyId = hostelId;
            Title = title;
        }

        public PropertyDirection Update(string title)
        {
            if (title is not null && Title?.Equals(title) is not true) Title = title;
            return this;
        }
    }
}

