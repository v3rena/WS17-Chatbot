using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot
{
	public class MessageParser
	{
		string Category { get; set; }
		string Country { get; set; }
		Dictionary<string, string> Entities = new Dictionary<string, string>();

		public MessageParser() { }

		//TODO: Include all categories, Improve GetKeyword()
		public Dictionary<string, string> Parse(string msg)
		{
			Entities.Clear();
			if(!GetCategory(msg))
			{
				GetKeyword(msg);
			}
			return Entities;
		}

		private bool GetCategory(string msg)
		{
			if (msg.Contains("Technik"))
			{
				if (Entities.ContainsKey("Category"))
				{
					Entities["Category"] = "technology";
				}
				else
				{
					Entities.Add("Category", "technology");
				}
				return true;
			}
			else if (msg.Contains("Wirtschaft"))
			{
				if (Entities.ContainsKey("Category"))
				{
					Entities["Category"] = "business";
				}
				else
				{
					Entities.Add("Category", "business");
				}
				return true;
			}
			else return false;
		}

		private void GetKeyword(string msg)
		{
			string lastWord = msg.Split(' ').Last().ToLower();

			if (Entities.ContainsKey("Keyword"))
			{
				Entities["Keyword"] = lastWord;
			}
			else
			{
				Entities.Add("Keyword", lastWord);
			}
		}
	}
}
