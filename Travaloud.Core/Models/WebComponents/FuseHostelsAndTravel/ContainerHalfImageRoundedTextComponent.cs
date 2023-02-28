namespace Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel
{
    public class ContainerHalfImageRoundedTextComponent : CardComponent
    {
        public IEnumerable<OvalContainerComponent> OvalContainers { get; private set; } = null;
        public IEnumerable<string> Titles { get; private set; }
        public bool SwapDirecion { get; private set; }

        public ContainerHalfImageRoundedTextComponent()
        {

        }

        public ContainerHalfImageRoundedTextComponent(IEnumerable<string> titles, string title, string body, string imageSrc, ButtonComponent buttonComponent, IEnumerable<OvalContainerComponent> ovalContainers = null, bool swapDirecion = false, int? mdClass = null, int? lgClass = null, int? xsClass = null, int? marginBottom = null, int? marginTop = null, int? marginLeft = null, int? marginRight = null, int? paddingBottom = null, int? paddingTop = null, int? paddingLeft = null, int? paddingRight = null) : base(title, body, imageSrc, mdClass, lgClass, xsClass, marginBottom, marginTop, marginLeft, marginRight, paddingBottom, paddingTop, paddingLeft, paddingRight, buttonComponent)
        {
            OvalContainers = ovalContainers;
            Titles = titles;
            SwapDirecion = swapDirecion;
        }
    }
}

