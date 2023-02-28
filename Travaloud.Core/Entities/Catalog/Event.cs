namespace Travaloud.Core.Entities.Catalog
{
	public class Event : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string ImagePath { get; private set; }
        public string FriendlyUrl { get; private set; }
        public string PageTitle { get; private set; }
        public string FriendlyPageTitle { get; private set; }
        public string PageSubTitle { get; private set; }
        public string FriendlyPageSubTitle { get; private set; }
        public string BackgroundColor { get; private set; }
        public Guid? PropertyId { get; private set; }

        public Event()
		{
		}
	}
}

