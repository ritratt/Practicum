using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace API.Areas.Buzzcoin.Providers.Implementations
{
    public class BuzzcoinProvider : IBuzzcoinProvider
    {
        private readonly String _url;

        public BuzzcoinProvider(String url)
        {
            _url = url;
        }

        public String Url
        {
            get { return _url; }
            set { }
        }

        public String GetEarned(String apiKey)
        {
            /*
             * Get all of todays swipes
             * Extract eligible swipes
             * Calculate coins earned
            */
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Add("apikey", apiKey);

            return "1";
        }
    }
}