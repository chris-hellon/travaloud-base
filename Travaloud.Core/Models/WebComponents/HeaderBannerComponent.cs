using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;

namespace Travaloud.Core.Models.WebComponents
{
	public class HeaderBannerComponent
	{
		public string Title { get; private set; }
		public string SubTitle { get; private set; }
		public string Body { get; private set; }
		public string ImageSrc { get; private set; }
        public IEnumerable<OvalContainerComponent> OvalContainers { get; private set; } = null;

        public HeaderBannerComponent()
        {

        }

        public HeaderBannerComponent(string title, string subTitle, string body, string imageSrc, IEnumerable<OvalContainerComponent> ovalContainers = null)
        {
            Title = title;
            SubTitle = subTitle;
            Body = body;
            ImageSrc = imageSrc;
            OvalContainers = ovalContainers;
        }

        public HeaderBannerComponent(string imageSrc)
        {
            Title = null;
            SubTitle = null;
            Body = null;
            ImageSrc = imageSrc;
        }
    }
}

