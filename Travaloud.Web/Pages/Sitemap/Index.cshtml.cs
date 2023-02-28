using System.Text;
using System.Xml.Serialization;
using AspNetCore.SEOHelper.Sitemap;

namespace Travaloud.Web.Pages.Sitemap
{
    public class IndexModel : TravaloudBasePageModel
    {
        public async Task<ContentResult> OnGetAsync()
        {
            await base.OnGetDataAsync();

            StringBuilder sb = new StringBuilder();
            sb.Append(GenerateSitemap());

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };
        }

        private string GenerateSitemap()
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";

            var sitemapNodes = new List<SitemapNode>()
            {
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = baseUrl, Frequency = SitemapFrequency.Monthly },
            };

            if (TenantId == "fuse")
            {
                sitemapNodes.AddRange(new List<SitemapNode>() {
                    new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/hostels", Frequency = SitemapFrequency.Yearly },
                    new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/tours", Frequency = SitemapFrequency.Yearly },
                    new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/our-story", Frequency = SitemapFrequency.Yearly },
                    new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/get-in-touch", Frequency = SitemapFrequency.Yearly },
                    new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/hostel-booking", Frequency = SitemapFrequency.Yearly }
                });

                foreach (var property in Properties)
                {
                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = $"{baseUrl}/hostels/{property.FriendlyUrl}", Frequency = SitemapFrequency.Monthly });
                }

                foreach (var tour in Tours)
                {
                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = $"{baseUrl}/tours/{tour.FriendlyUrl}", Frequency = SitemapFrequency.Monthly });
                }
            }

            sitemapNodes.AddRange(new List<SitemapNode>() {
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/terms-and-conditions", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/cookie-policy", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/privacy-policy", Frequency = SitemapFrequency.Yearly }
            });

            XmlSerializer serializer = new XmlSerializer(typeof(List<SitemapNode>));

            var stringwriter = new StringWriter();
            serializer.Serialize(stringwriter, sitemapNodes);

            return stringwriter.ToString();
        }
    }
}
