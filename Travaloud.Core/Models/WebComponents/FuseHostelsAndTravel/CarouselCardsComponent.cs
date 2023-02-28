﻿namespace Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel
{
	public class CarouselCardsComponent : AlternatingCardHeightComponent
	{
        public string PartialView { get; private set; }

        public CarouselCardsComponent()
        {

        }

        public CarouselCardsComponent(GenericBannerComponent header, List<CardComponent> cards = null, string animationStart = "onScroll", string partialView = "_CardPartial") : base(header, cards, animationStart)
        {
            PartialView = partialView;
        }
    }
}

