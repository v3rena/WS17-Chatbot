using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot.Models
{
	public class ArticleRoot
	{
		public string Status { get; set; }
		public List<Article> Articles { get; set; }
	}
}
