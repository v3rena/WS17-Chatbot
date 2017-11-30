using Chatbot.Models;
using Chatbot.Plugins.NewsBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot
{
	public class SourceSelector
	{
		private string apiKey = "de01ffd9808244fcbbfa65d675ee6fd0";

		private HttpClient GetClient()
		{
			HttpClient client = new HttpClient()
			{
				BaseAddress = new Uri("https://newsapi.org/v2/sources")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		public Source SelectSource(string cat)
		{
			string category = cat;
			string country = "";

			List<Source> sourceList = null;

			HttpClient client = GetClient();

			//If no category is specified, general news are returned
			HttpResponseMessage response = client.GetAsync($"?category={category}&country={country}&language=de&apiKey={apiKey}").Result;
			if (response.IsSuccessStatusCode)
			{
				SourceRoot root = response.Content.ReadAsAsync<SourceRoot>().Result;
				sourceList = root.Sources;
			}
			Source source = sourceList.FirstOrDefault();
			return source;
		}
	}
}
