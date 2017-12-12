using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGPlugin : IPlugin
    {
        private const int _maxGames = 5;
        private TimeSpan _timeToExpire = new TimeSpan(0, 5, 0);

        private string _wordToInit;
        private string _apiKey;

        private ConcurrentDictionary<Guid, RPGGame> _games;

        public RPGPlugin()
        {
            _apiKey = "b013336be8ae44ccb59fa486e6f7e93e";
            _wordToInit = "-rp";
            _games = new ConcurrentDictionary<Guid, RPGGame>();
        }

        public string Name => "RPGPlugin";

        public float CanHandle(Message message)
        {
            _games.TryGetValue(message.SessionKey.Key, out RPGGame game);
            if (game != null && !game.isPaused)
                return 1f;
            if (message.Content.Split(' ')[0].Equals(_wordToInit.ToLower()))
            {
                if (game != null)
                    game.Unpause();
                return 1f;
            }
            else
                return 0f;
        }

        public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
        {
            //currently nothing to do
            return configuration;
        }

        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            //currently nothing to do
        }


        public Message Handle(Message message)
        {
            List<string> tokens = message.Content.ToLower().Split(' ').Where(t => t.Length > 0).ToList();

            if (message.Content.StartsWith(_wordToInit))
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
            RPGGame newGame;
            string message = "";

            if (_games.TryGetValue(session.Key, out newGame))
            {
                if (tokens.Count == 0)
                {
                    message = newGame.RoomFeelings();
                }
                else
                {
                    var result = newGame.PlayerAction(tokens);
                    if (result == "")
                    {
                        _games.TryRemove(session.Key, out newGame);
                        message = "Das Spiel wurde beendet!";
                    }
                    else
                    {
                        message = result;
                    }
                }
            }
            else
            {
                var expiredGames = _games.Where(g => g.Value.lastAction.Add(_timeToExpire) < DateTime.Now).OrderBy(g => g.Value.lastAction).ToList();
                bool hasFreeSlot = true;
                if (_games.Count >= _maxGames)
                {
                    if (expiredGames.Count == 0)
                    {
                        message = "Alle Spielräume sind derzeit belegt. Bitte später nochmals versuchen!";
                        hasFreeSlot = false;
                    }
                    else
                    {
                        foreach (var i in expiredGames)
                        {
                            _games.TryRemove(i.Key, out RPGGame game);
                        }
                        hasFreeSlot = true;
                    }
                }

                if (hasFreeSlot)
                {
                    newGame = new RPGGame();
                    _games.TryAdd(session.Key, newGame);
                    message = string.Format("Willkommen im RPGPlugin! Schreibe 'Hilfe', um verfügbare Befehle zu sehen. Mit 'Ende' wird das Spiel beendet. Mit 'Pause' wird das Spiel pausiert.", session.Key);
                    message += "<br/><br/>-----------------------<br/><br/>";
                    message += newGame.RoomFeelings();
                }
            }
            return new RPGMessage(message);
        }
    }
}