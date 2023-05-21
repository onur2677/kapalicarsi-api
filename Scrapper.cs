using HtmlAgilityPack.CssSelectors.NetCore;

namespace kapalicarsi
{
    public class Scrapper
    {
        private const string URL = "https://kur.doviz.com/kapalicarsi";
        private static async Task<string> Fetch()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(URL);
            return response;
        }

        private static float Convert(string data)
        {
            return float.Parse(data);
        }

        public static List<KapalicarsiModel> Get()
        {
            string response = Fetch().Result;

            var page = new HtmlAgilityPack.HtmlDocument();
            page.LoadHtml(response);
            var document = page.DocumentNode;

            var table = document.QuerySelector("#indexes");
            table.QuerySelector("thead").Remove();
            var rows = table.QuerySelectorAll("tr");

            var data = new List<KapalicarsiModel>();

            foreach (var row in rows)
            {
                var cols = row.QuerySelectorAll("td");
                if (cols != null)
                {
                    var item = new KapalicarsiModel
                    {
                        Name = cols[0].QuerySelector(".currency-details").SelectSingleNode("div").InnerText.Trim(),
                        Description = cols[0].QuerySelector(".cname").InnerText,
                        Buy = Convert(cols[1].InnerText.Trim()),
                        Sell = Convert(cols[2].InnerText.Trim()),
                        ChangeRate = Convert(cols[3].InnerText.Trim().Replace('%', ' ')),
                        Time = cols[4].InnerText.Trim(),
                    };
                    data.Add(item);
                }
            }

            return data;
        }
    }

}
