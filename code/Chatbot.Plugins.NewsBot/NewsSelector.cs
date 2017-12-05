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
	public class NewsSelector
	{
		private static readonly string apiKey = "de01ffd9808244fcbbfa65d675ee6fd0";

		private HttpClient GetClient()
		{
			HttpClient client = new HttpClient()
			{
				BaseAddress = new Uri("https://newsapi.org")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		public List<Article> SelectNewsBySource(string source)
		{
			List<Article> articleList = null;

			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"/v2/top-headlines?sources={source}&apiKey={apiKey}").Result;
			if (response.IsSuccessStatusCode)
			{
				ArticleRoot root = response.Content.ReadAsAsync<ArticleRoot>().Result;
				articleList = root.Articles;
			}

			return articleList.Take(5).ToList();
		}

		public List<Article> SelectNewsByKeyword(string keyword)
		{
			List<Article> articleList = null;

			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"/v2/everything?q={keyword}&apiKey={apiKey}&language=de").Result;
			if (response.IsSuccessStatusCode)
			{
				ArticleRoot root = response.Content.ReadAsAsync<ArticleRoot>().Result;
				articleList = root.Articles;
			}
			return articleList.Take(5).ToList();
		}
	}
}
