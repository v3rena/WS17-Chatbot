using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGPlugin : IPlugin
    {
        private static string wordToInit;
        private static string apiKey;

        private static Dictionary<Guid, RPGGame> games;

        public RPGPlugin()
        {
            apiKey = "b013336be8ae44ccb59fa486e6f7e93e";
            wordToInit = "-rp";
            games = new Dictionary<Guid, RPGGame>();
        }

        public string Name => "RPGPlugin";

        public float CanHandle(Message message)
        {
            if (games.ContainsKey(message.SessionKey.Key))
                return 1f;
            if (message.Content.Split(' ')[0].Equals(wordToInit.ToLower()))
                return 1f;
            else
                return 0f;
        }

        public IEnumerable<PluginConfiguration> EnsureDefaultConfiguration(IList<PluginConfiguration> configuration)
        {
            //currently nothing to do
            return configuration;
        }

        public Message Handle(Message message)
        {

            List<string> tokens = message.Content.ToLower().Split(' ').Where(t => t.Length > 0).ToList();

            if (message.Content.StartsWith("-rp"))
            {
                tokens.RemoveAt(0);
                return HandleTokens(message.SessionKey, tokens);
            }
            else
            {
                return HandleTokens(message.SessionKey, tokens);
            }
        }

        public class RPGMessage : Message
        {
            public RPGMessage(string content) : base(MonospacedHTML(content)) { }

            public static string MonospacedHTML(string content)
            {
                content = "<p style=\"font-family: Courier, Consolas, Monospace\">" + content + "</p>";
                return content;
            }
        }

        private Message HandleTokens(SessionKey session, List<string> tokens)
        {
            RPGGame game;
            string message = "";
            if (games.TryGetValue(session.Key, out game))
            {
                //message = string.Format("Game loaded for Session {0}", session.Key);
                if (tokens.Count == 0)
                {
                    message = game.RoomFeelings();
                }
                else
                {
                    if (game.PlayerAction(tokens) == "")
                    {
                        games.Remove(session.Key);
                        message = "Das Spiel wurde beendet!";
                    }
                }
            }
            else
            {
                game = new RPGGame();
                games.Add(session.Key, game);
                message = string.Format("Starte ein neues Spiel für Session '{0}'. Schreibe 'Hilfe', um verfügbare Befehle zu sehen. Mit 'Ende' wird das Spiel beendet.", session.Key);
            }

            return new RPGMessage(message);
        }
    }
}
