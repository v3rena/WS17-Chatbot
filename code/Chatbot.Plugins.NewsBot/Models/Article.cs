using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot
{
	public class Article
	{
		public Source Source { get; set; }
		public string Author { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string UrlToImage { get; set; }
		public string PublishedAt { get; set; }
	}
}