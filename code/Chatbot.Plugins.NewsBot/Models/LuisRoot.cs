using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot.Models
{
	public class LuisRoot
	{
		public string Query { get; set; }
		public Intent TopScoringIntent { get; set; }
		public List<Intent> Intents { get; set; }
		public List<Entity> Entities { get; set; }
	}
}
