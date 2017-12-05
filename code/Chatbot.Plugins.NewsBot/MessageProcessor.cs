using Chatbot.Plugins.NewsBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot
{
	public class MessageProcessor
	{
		private HttpClient GetClient()
		{
			HttpClient client = new HttpClient()
			{
				BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		public List<Entity> GetEntityList(string msg)
		{
			LuisRoot root=null;
			HttpClient client = GetClient();

			HttpResponseMessage response = client.GetAsync($"/luis/v2.0/apps/3324bbfe-9dfd-4af7-b9a6-832884291cec?subscription-key=b045044c27c7436ea2f8f350a8632b74&verbose=true&timezoneOffset=0&q={msg}").Result;
			if (response.IsSuccessStatusCode)
			{
				root = response.Content.ReadAsAsync<LuisRoot>().Result;
			}
			return root.Entities;
		}
	}
}
