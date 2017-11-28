using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chatbot.Interfaces;
using Chatbot.Models;
using System.Text;
using Chatbot.Plugins.NewsBot.Models;

namespace Chatbot.Plugins.NewsBot
{
	public class NewsBot : IPlugin
	{
		public string Name => "NewsBot";
		private static readonly string Category = "Category";
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

		public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
		{
			//TODO: Read Api key from config
			return configuration;
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
					if(entity.Type==Category)
					{
						answer = QueryByCategory(entity);
						if (string.IsNullOrWhiteSpace(answer))
						{
							answer = $"Es wurden keine News zu \"{entity.entity}\" gefunden. ";
							answer += QueryGeneralNews();
						}
					}
					else if (entity.Type==Keyword)
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
		/// Returns either a category or a keyword
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private Entity GetEntity(List<Entity> list)
		{
			foreach (var item in list)
			{
				if (item.Type == Category)
				{
					return item;
				}
				else if (item.Type == Keyword)
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
		/// Searches and returns news by category
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		private string QueryByCategory(Entity entity)
		{
			string answer = String.Empty;
			SourceSelector srcSelector = new SourceSelector();
			Source src = srcSelector.SelectSource(entity.entity);
			if (src != null)
			{
				NewsSelector newsSelector = new NewsSelector();
				List<Article> news = newsSelector.SelectNewsBySource(src.Name);
				if (news != null)
				{
					answer = "Es werden Ihnen News aus \"" + src.Name.ToString() + "\" angezeigt:";
					answer += BuildAnswerString(news);

				}
			}
			return answer;
		}

		/// <summary>
		/// Searches and returns news by querying a specific keyword
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		private string QueryByKeyword(Entity entity)
		{
			string answer = String.Empty;
			NewsSelector newsSelector = new NewsSelector();
			string keyword = "";
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
		/// If no category or keyword is given, general news are returned
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
				List<Article> news = newsSelector.SelectNewsBySource(src.Name);
				if (news != null)
				{
					answer = "Es werden Ihnen allgemeine News aus \"" + src.Name.ToString() + "\" angezeigt:";
					answer += BuildAnswerString(news);

				}
			}
			return answer;
		}

		/// <summary>
		/// Constructs an answer from a list of news
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