using System.Text;

namespace Travaloud.Web.Pages.RobotsTxt
{
	public class IndexModel : PageModel
    {
        public ContentResult OnGet()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Disallow:")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap.xml");

            return this.Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}
