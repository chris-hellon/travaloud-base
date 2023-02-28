namespace Travaloud.Core.Entities.Catalog
{
    public class PropertyDirectionContent : AuditableEntity, IAggregateRoot
    {
        public Guid PropertyDirectionId { get; private set; }
        public string Body { get; private set; }
        public string Style { get; private set; }

        public PropertyDirectionContent()
        {

        }

        public PropertyDirectionContent(Guid propertyDirectionId, string body, string style)
        {
            PropertyDirectionId = propertyDirectionId;
            Body = body;
            Style = style;
        }

        public PropertyDirectionContent Update(string body, string style)
        {
            if (body is not null && Body?.Equals(body) is not true) Body = body;
            if (style is not null && Style?.Equals(style) is not true) Style = style;
            return this;
        }
    }
}

