using System;
using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;

namespace Travaloud.Core.Utils
{
	public static class WebComponentsBuilder
	{
		public static class FuseHostelsAndTravel
		{
            public static List<ContainerHalfImageTextComponent> GetHostelsContainers(IEnumerable<Property> properties)
            {
                var hostelsContainers = new List<ContainerHalfImageTextComponent>();
                bool swapDirection = true;

                foreach (var item in properties)
                {
                    hostelsContainers.Add(new ContainerHalfImageTextComponent(
                    item.Name,
                    item.ShortDescription,
                    item.ImagePath,
                    item.ThumbnailImagePath,
                    swapDirection,
                    swapDirection ? new OvalContainerComponent(item.Name.ConvertToCamelCase("Ovals"), null, -30, null, -25) : null,
                    new ButtonComponent($"/Hostels/Index?propertyName={item.FriendlyUrl}", $"View {item.Name}"), null, null, null, null, null, null, null, item == properties.Last() ? 0 : 5)
                    {
                        PaddingBottomLg = 2
                    });

                    swapDirection = swapDirection ? false : true;
                }

                return hostelsContainers;
            }

            public static AlternatingCardHeightComponent GetToursCards(IEnumerable<Tour> tours, string animationStart = "onScroll", string title = "TAILORED TOURS", string body = "Explore the culture of hoi an with FUSE travel.")
            {
                var toursCards = new AlternatingCardHeightComponent(new GenericBannerComponent(title, body), null, animationStart);

                var animationDelay = 200M;
                foreach (var item in tours)
                {
                    toursCards.Cards.Add(new CardComponent(
                        item.Name,
                        item.ShortDescription,
                        item.ImagePath,
                        item.ThumbnailImagePath,
                        12, 6, 12,
                        animationDelay,
                        item != tours.Last() ? 5 : null,
                        item != tours.Last() ? 8 : null,
                        null,
                        new ButtonComponent("btn-outline-primary", $"/Tours/Index?tourName={item.FriendlyUrl}", "Find Out More"), animationStart));

                    animationDelay += 200M;
                }

                return toursCards;
            }

            public static CarouselCardsComponent GetToursCarouselCards(IEnumerable<Tour> tours, string animationStart = "onScroll", string title = "TAILORED TOURS", string body = "Explore the culture of hoi an with FUSE travel.")
            {
                var toursCards = new CarouselCardsComponent(new GenericBannerComponent(title, body), null, animationStart);

                var animationDelay = 200M;
                foreach (var item in tours)
                {
                    toursCards.Cards.Add(new CardComponent(
                        item.Name,
                        item.ShortDescription,
                        item.ImagePath,
                        item.ThumbnailImagePath,
                        null, null, null,
                        null,
                        null,
                        null,
                        null,
                        new ButtonComponent("btn-outline-primary align-bottom", $"/Tours/Index?tourName={item.FriendlyUrl}", "Find Out More")));

                    animationDelay += 200M;
                }

                return toursCards;
            }

            public static CarouselCardsComponent GetEventsCarouselCards(IEnumerable<Travaloud.Core.Entities.Catalog.Event> events)
            {
                var eventsCards = new CarouselCardsComponent(null, null, null, "_CardWithBackgroundPartial");

                var animationDelay = 200M;
                foreach (var item in events)
                {
                    eventsCards.Cards.Add(new CardWithBackgroundComponent(item.Name,
                        item.ShortDescription,
                        item.BackgroundColor,
                        item.ImagePath,
                        null, null, null, null, null, null, new List<OvalContainerComponent>()
                        {
                        new OvalContainerComponent($"eventsCards{item.Id}OvalsContainer1", -60, null, -110, null),
                        new OvalContainerComponent($"eventsCards{item.Id}OvalsContainer2", null, -60, null, -100)
                        }));

                    animationDelay += 200M;
                }

                return eventsCards;
            }

            public static FullImageCardComponent GetHostelAccommodationCards(IEnumerable<PropertyRoom> propertyRooms, string animationStart = "onScroll", string title = "ACCOMMODATION", string body = "All private & shared rooms at FUSE Beachside come with pool and ocean views as standard so there is no better place to stay if beach vibes is your thing.")
            {
                var accommodationCards = new FullImageCardComponent(new GenericBannerComponent(title, body, new List<OvalContainerComponent>()
            {
                new OvalContainerComponent("hostelAccommodationOvals1", -185, null, -40, null)
            }), null, animationStart);
                var animationDelay = 200M;

                for (int i = 0; i < propertyRooms.ToList().Count; i++)
                {
                    var item = propertyRooms.ToList()[i];
                    accommodationCards.Cards.Add(new CardComponent(
                        item.Name,
                        item.Description,
                        item.ImagePath,
                        new List<string>() { item.ImagePath },
                        12, 6, 12,
                        animationDelay, 8, 10,
                        animationStart));

                    animationDelay += 200M;
                }

                return accommodationCards;
            }

            public static NavPillsComponent GetHostelDirectionsNavPills(Property property)
            {
                return new NavPillsComponent($"GETTING TO {property.PageSubTitle.ToUpper()} {property.PageTitle.ToUpper()}", property.Directions.Select(x => new NavPill(x.Title, x.Content.Select(c => new NavPillContent(c.Body, c.Style)).ToList())).ToList());
            }

            public static FeaturesTableComponent GetHostelFacilities(Property property)
            {
                return new FeaturesTableComponent("Faciliities & Amenities", property.Facilities);
            }
        }
	}
}

