using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.NewsBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chatbot.Plugins.NewsBot
{
    public class NewsBot : IPlugin
	{
		public string Name => "NewsBot";
		private static readonly string Keyword = "Keyword";
		private static readonly string Place = "Ort";

		/// <summary>
		/// Returns 1 if plugin can handle the request and 0 if it cannot
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public float CanHandle(Message message)
		{
			if (message.Content.ToLower().Contains("news") || (message.Content.ToLower().Contains("neuigkeiten")))
			{
				return 0.9f;
			}
			else if (message.Content.ToLower().Contains("neues"))
			{
				return 0.3f;
			}
			else
			{
				return 0.0f;
			}
		}

		public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
		{
			return configuration;
        }

        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            
        }

        /// <summary>
        /// Processes the request and returns a new message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Message Handle(Message message)
		{
			string answer = "Keine News verfügbar";
			MessageProcessor msgProcessor = new MessageProcessor();
			List<Entity> list = msgProcessor.GetEntityList(message.Content);
			if (list!=null)
			{
				Entity entity = GetEntity(list);
				if (entity!=null)
				{
					if (entity.Type==Keyword)
					{
						answer = QueryByKeyword(entity);
						if (string.IsNullOrWhiteSpace(answer))
						{
							answer = $"Es wurden keine News zu \"{entity.entity}\" gefunden. ";
							answer += QueryGeneralNews();
						}
					}
				}
				else
				{
					answer = QueryGeneralNews();
				}
			}
			else
			{
				answer = QueryGeneralNews();
			}
			return new Message(answer);
		}

		/// <summary>
		/// Returns a keyword - places are treated as keywords, so they can be queried as well
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private Entity GetEntity(List<Entity> list)
		{
			foreach (var item in list)
			{
				if (item.Type == Keyword)
				{
					return item;
				}
				else if (item.Type == Place)
				{
					item.Type = Keyword;
					return item;
				}
			}
			return null;
		}
	
		/// <summary>
		/// Searches and returns news by querying a specific keyword
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		private string QueryByKeyword(Entity entity)
		{
			string answer = String.Empty;
			string keyword = "";
			NewsSelector newsSelector = new NewsSelector();
			if (entity != null)
			{
				keyword = entity.entity;
			}
			List<Article> news = newsSelector.SelectNewsByKeyword(keyword);
			if (news != null)
			{
				answer = "Es werden Ihnen News über \"" + entity.entity + "\" angezeigt:";
				answer += BuildAnswerString(news);
			}
			return answer;
		}

		/// <summary>
		/// If no keyword is given, general news are returned
		/// </summary>
		/// <returns></returns>
		private string QueryGeneralNews()
		{
			string answer = String.Empty;
			SourceSelector srcSelector = new SourceSelector();
			Source src = srcSelector.SelectSource(String.Empty);
			if (src != null)
			{
				NewsSelector newsSelector = new NewsSelector();
				List<Article> news = newsSelector.SelectNewsBySource(src.Id);
				if (news != null)
				{
					answer = "Es werden Ihnen allgemeine News aus \"" + src.Name.ToString() + "\" angezeigt:";
					answer += BuildAnswerString(news);
				}
			}
			return answer;
		}

		/// <summary>
		/// Constructs an answer from a list of news articles
		/// </summary>
		/// <param name="news"></param>
		/// <returns></returns>
		private string BuildAnswerString(List<Article> news)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var article in news)
			{
				builder.Append("<br/>");
				builder.Append(article.Title + " ");
				builder.Append("<a href=\"" + article.Url + "\">Link</a>");
			}
			return builder.ToString();
		}
    }
}