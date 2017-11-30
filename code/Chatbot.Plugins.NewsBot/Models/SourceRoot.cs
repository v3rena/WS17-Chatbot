using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.NewsBot.Models
{
	public class SourceRoot
	{
		public string Status { get; set; }
		public List<Source> Sources { get; set; }
	}
}
