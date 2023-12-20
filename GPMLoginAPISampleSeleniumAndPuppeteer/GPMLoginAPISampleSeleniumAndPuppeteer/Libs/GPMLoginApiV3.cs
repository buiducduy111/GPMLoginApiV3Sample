using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GPMLoginAPISampleSeleniumAndPuppeteer.Libs
{
    public class GPMLoginApiV3
    {
        private const string START_ENDPOINT = "/api/v3/profiles/start/{id}";

        private string _apiUrl;

        public GPMLoginApiV3(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        /// <summary>
        /// Start profile by id
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns>Return JObject or null</returns>
        public async Task<JObject> StartProfileAsync(string profileId)
        {
            string apiUrl = $"{_apiUrl}{START_ENDPOINT}".Replace("{id}", profileId);
            string resp = await httpGetAsync(apiUrl);

            if (resp == null)
                return null;

            try
            {
                return JObject.Parse(resp);
            }
            catch
            {
                return null;
            }
        }

        #region Helpers
        private async Task<string> httpGetAsync(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();

                    throw new Exception("Unknow error");
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
