using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot.Models
{
	public class Entity
	{
		public string entity { get; set; }
		public string Type { get; set; } //Keyword, Category, Ort, Zeit
		public int StartIndex { get; set; }
		public int EndIndex { get; set; }
		public float Score { get; set; }
	}
}
