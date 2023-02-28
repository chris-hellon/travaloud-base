namespace Travaloud.Core.Models.WebComponents
{
	public class FullImageCarouselComponent
	{
		public BookNowComponent BookNowComponent { get; private set; } = null;

		public List<Image> CarouselImages { get; private set; }

        public FullImageCarouselComponent()
        {

        }

        public FullImageCarouselComponent(List<Image> carouselImages, BookNowComponent bookNowComponent = null)
        {
            BookNowComponent = bookNowComponent;
            CarouselImages = carouselImages;
        }
    }
}

