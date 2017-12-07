using Chatbot.Plugins.RPGPlugin.Enumerations;
using Chatbot.Plugins.RPGPlugin.Factories;
using Chatbot.Plugins.RPGPlugin.Interfaces;
using Chatbot.Plugins.RPGPlugin.RPGObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGGame
    {
        public DateTime lastAction = DateTime.Now;
        public bool isPaused = false;

        private int _depth;
        private IRPGRoom _currentRoom;
        private RPGPlayer _player;

        public RPGGame()
        {
            _depth = 0;
            _player = new RPGPlayer();
            _currentRoom = RPGRoomFactory.CreateBasicRoom(0, 0);
            _currentRoom.CreateFutureRooms(_depth, 1);
            _currentRoom.Initialize();
        }

        public string RoomFeelings()
        {
            string result = "";

            if (_depth == 0)
            {
                result = "Mit keuchendem Atem beendet Ihr Eure Flucht an einer Felswand. Es gibt kein Entrinnen. Die schweren Schritte Eures Verfolgers kommen näher und näher...<br/><br/>Doch da: eine Menge an mystischen Glyphen und Gravuren deuten auf eine Spalte im Fels. Eine uralte Tempelanlage! Ob die alten Zauberer wohl wirklich so mächtig waren, wie die Legenden erzählen? Uralte Artefakte, Zaubertränke, Waffen... Wenn Ihr nur an den Fallen und Wächtern vorbeikommen könntet, so gäbe es vielleicht doch noch eine Chance zu überleben.<br/>Rasch zwängt Ihr Euch durch die Felsspalte und folgt dem dahinterliegenden Gang. Ihr wisst zwar nicht, was vor Euch liegt, doch eines steht fest - Umkehren steht außer Frage!<br/>";
            }
            else if (_currentRoom is RPGPuzzleRoom)
            {
                if (!_currentRoom.IsCleared())
                {
                    result += "Ihr steht plötzlich vor einem tiefen Abgrund. Es muss doch eine Möglichkeit geben, diesen zu passieren!<br/>";
                    result += RoomItems();
                    result += "<br/>An den Wänden sind einige Glyphen und <u title='Um Schalter A zu drücken, muss \"drücke A\" eingegeben werden. Das Rätsel ist gelöst, wenn das Ziel (Z) grün aufleuchtet.\n&, |, ^, ! stehen für die logischen Operatoren AND, OR, XOR, NOT.\nPfeile zeigen den Output der Operatoren an.'>bewegliche Steine</u> zu erkennen. Diese Glyphen haben Euch ja bereits zuvor geholfen. Vielleicht haben die Zauberer einen Hinweis hinterlassen...<br/>Ihr seht genauer hin:<br/><br/>";
                }
                else
                {
                    result += RoomItems();
                    result += "Der Weg scheint nun frei zu sein!<br/><br/>";
                }
                result += ((RPGPuzzleRoom)_currentRoom).puzzle.PuzzleDisplay();

            }
            else
            {
                result = string.Format("Ihr befindet euch {0} Meter innerhalb des Tempels. Das Gangsystem führt euch tiefer in die Dunkelheit.", (_depth) * 20);
                result += RoomItems();
                result += "<br/><br/>";
            }

            result += string.Format("<br/>{0}", _currentRoom.GetRoom(0).GetPlayerFeeling(5, _currentRoom.IsCleared()));

            if (_currentRoom.GetRoom(1) == null && _currentRoom.GetRoom(2) == null)
            {
                result += " Ihr seid sicher, dass es keine Alternativen gibt.";
            }
            else
            {
                if (_currentRoom.GetRoom(1) == null)
                    result += "<br/><br/>Ihr wollt sicherlich nicht nach Westen Eurem Verfolger in die Arme laufen.";
                else
                    result += string.Format("<br/><br/>{0}", _currentRoom.GetRoom(1).GetPlayerFeeling(5, _currentRoom.IsCleared()));

                if (_currentRoom.GetRoom(2) == null)
                    result += "<br/><br/>Ihr wollt sicherlich nicht nach Osten Eurem Verfolger in die Arme laufen.";
                else
                    result += string.Format("<br/><br/>{0}", _currentRoom.GetRoom(2).GetPlayerFeeling(5, _currentRoom.IsCleared()));
            }

            return result;
        }

        public string PlayerAction(List<string> tokens)
        {
            switch (tokens[0])
            {
                case "ende":
                case "quit":
                case "q":
                case "exit":
                    return "";
                case "n":
                case "north":
                case "norden":
                    return TryWalkDir(0);
                case "w":
                case "west":
                case "westen":
                    if (_currentRoom.GetRoom(1) != null)
                        return TryWalkDir(1);
                    else
                        return "Ihr schüttelt den Kopf. Hierhin wollt Ihr ganz sicher nicht gehen!";
                case "e":
                case "o":
                case "east":
                case "osten":
                    if (_currentRoom.GetRoom(2) != null)
                        return TryWalkDir(2);
                    else
                        return "Ihr schüttelt den Kopf. Hierhin wollt Ihr ganz sicher nicht gehen!";
                case "look":
                case "inspect":
                case "schau":
                    return RoomFeelings();
                case "nimm":
                case "hebe":
                case "take":
                case "grab":
                case "get":
                case "t":
                    if (_currentRoom.IsCleared())
                    {
                        if (tokens.Count > 1)
                            return TakeRoomItemByToken(tokens[1]);
                        else
                            return "Welche Gegenstände wollt ihr mitnehmen?" + RoomItems();
                    }
                    else
                    {
                        return "Ihr könnt die Gegenstände nicht erreichen!";
                    }
                case "takeall":
                case "nimmalles":
                    if (_currentRoom.IsCleared())
                        return TakeAllRoomItems();
                    else
                        return "Ihr könnt die Gegenstände nicht erreichen!";
                case "i":
                case "inventar":
                case "inventory":
                    return _player.PlayerInventoryInfo();
                case "drücke":
                case "aktiviere":
                case "schalte":
                    bool puzzleClear;
                    string result = _currentRoom.HandleCommand(tokens, out puzzleClear);
                    if (puzzleClear)
                        return result + RoomFeelings();
                    else
                        return result;
                case "pause":
                    return Pause();
                case "help":
                case "hilfe":
                default:
                    return "Es gibt folgende Kommandos (nicht case-sensitive), um den Verlauf des Spiels zu steuern:<br/>" +
                        "Gehe nach Norden: <u>n</u>, <u>norden</u>, <u>north</u><br/>" +
                        "Gehe nach Westen: <u>w</u>, <u>westen</u>, <u>west</u><br/>" +
                        "Gehe nach Osten: <u>o</u>, <u>osten</u>, <u>e</u>, <u>east</u><br/>" +
                        "Schau dich um: <u>schau</u>, <u>look</u>, <u>inspect</u><br/>" +
                        "Nimm Gegenstand X: <u>nimm X</u>, <u>hebe X</u>, <u>take X</u>, <u>grab X</u>, <u>get X</u>, <u>t X</u><br/>" +
                        "Nimm alles: <u>nimmAlles</u>, <u>takeAll</u><br/>" +
                        "Inventar: <u>inventar</u>, <u>i</u>, <u>inventory</u><br/>" +
                        "Knopf X drücken: <u>drücke X</u>, <u>aktiviere X</u>, <u>schalte X</u><br/>" +
                        "Zeige Hilfeseite: <u>hilfe</u>, <u>help</u><br/>" +
                        "Spiel pausieren: <u>pause</u><br/>" +
                        "Spiel beenden: <u>ende</u>, <u>quit</u>, <u>q</u>, <u>exit</u>";
            }
        }

        public string TakeRoomItemByToken(string item)
        {
            var items = _currentRoom.GetItems().Where(i => i.GetName().ToLower().Contains(item.ToLower())).ToList();
            if (items.Count > 0)
            {
                RPGItem it = (RPGItem)items.FirstOrDefault();
                _player.inventory.Add(it);
                _currentRoom.GetItems().Remove(it);
                return string.Format("Ihr nehmt {0} {1}.", German.GetArticle(it, 4), it.GetName());
            }
            return string.Format("Ihr könnt kein {0} sehen!", item);
        }

        public string TakeAllRoomItems()
        {
            var items = _currentRoom.GetItems();
            if (items.Count == 0)
            {
                return "Ihr könnt keine Gegenstände sehen.";
            }
            string result = "";
            for (int i = items.Count - 1; i >= 0; i--)
            {
                IRPGItem item = items[i];
                result += "<br/>1 " + item.GetName();
                _player.inventory.Add(new RPGItem(item));
                _currentRoom.GetItems().Remove(item);
            }
            return "Ihr nehmt die folgenden Gegenstände:<br/>" + result;
        }

        public string TryWalkDir(int walkTo)
        {
            if (_currentRoom.GetRoom(walkTo) != null)
            {
                if (_currentRoom.IsCleared())
                {
                    _currentRoom = _currentRoom.GetRoom(walkTo);

                    _depth++;
                    _currentRoom.CreateFutureRooms(_depth, walkTo);
                }
                else
                {
                    return "<br/>Der Weg scheint noch <u title='Ein Weg ist dann versperrt, wenn sich noch Monster oder ungelöste Rätsel im Raum befinden.'>versperrt</u> zu sein...";
                }
            }

            return RoomFeelings();
        }

        public string RoomItems()
        {
            string result = "";
            if (_currentRoom.GetItems().Count > 0)
            {
                result = "<br/>In der Nähe seht ihr folgende Gegenstände: ";

                for (int i = 0; i < _currentRoom.GetItems().Count; i++)
                {

                    var item = _currentRoom.GetItems()[i];
                    var title = "Verbleibende Anwendungen: " + item.GetDurability();
                    result += string.Format("<u title='{1}'>{0}</u>", item.GetName(), title);
                    if (i < _currentRoom.GetItems().Count - 1)
                    {
                        result += ", ";
                    }
                }
            }
            else if (_currentRoom.GetItems().Count == 1)
            {
                var item = _currentRoom.GetItems()[0];
                var title = "Verbleibende Anwendungen: " + item.GetDurability();
                var itemDesc = string.Format("<u title='{1}'>{0}</u>", item.GetName(), title);

                result = string.Format("<br/>Am Boden vor euch liegt ein{0} {1}.", item.GetGender() == Gender.FEMALE ? "e" : "", itemDesc);
            }
            else
            {
                result = "<br/>Es sind keine Gegenstände zu sehen.";
            }
            return result+"<br/>";
        }

        public void Unpause()
        {
            isPaused = false;
        }

        public string Pause()
        {
            isPaused = true;
            return "Spiel pausiert (-rp zum Fortfahren)";
        }
    }

    public class RPGPuzzleRoom : IRPGRoom
    {
        public int depth;
        public int id;
        public IList<IRPGTrap> traps = new List<IRPGTrap>();
        public IList<IRPGMonster> monsters = new List<IRPGMonster>();
        public IList<IRPGItem> items = new List<IRPGItem>();

        public IList<IRPGObject> objects = new List<IRPGObject>();

        public IList<IRPGRoom> rooms = new List<IRPGRoom>();

        public RPGPuzzle puzzle;


        public RPGPuzzleRoom() { }

        public void CreateFutureRooms(int depth, int walkDir)
        {
            rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 0));
            rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 1));
            rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 2));
        }

        public IList<IRPGItem> GetItems()
        {
            return items;
        }

        public string GetPlayerFeeling(int perception, bool isCleared)
        {
            var result = "Ein rätselhaftes Gebiet liegt vor Euch im <u title='Um in den Norden weiterzugehen, müsst Ihr \"Norden\" eingeben. Weitere Befehle findet Ihr mit \"Hilfe\".'>Norden</u>...";
            if (!isCleared)
                result += "<br/>Der Weg scheint noch <u title='Ein Weg ist dann versperrt, wenn sich noch Monster oder ungelöste Rätsel im Raum befinden.'>versperrt</u> zu sein...";

            return result;
        }

        public string ToggleButton(string buttonName)
        {
            switch (buttonName)
            {
                case "A":
                case "a":
                    return puzzle.Toggle(0);
                case "B":
                case "b":
                    return puzzle.Toggle(1);
                case "C":
                case "c":
                    return puzzle.Toggle(2);
                case "D":
                case "d":
                    return puzzle.Toggle(3);
                default:
                    return string.Format("Ihr könnt keinen Knopf mit der Bezeichnung '{0}' erkennen.<br/>", buttonName);
            }
        }

        public IRPGRoom GetRoom(int id)
        {
            if (rooms.Count <= id)
                return null;

            return rooms[id];
        }

        public void Initialize()
        {
            puzzle = new RPGPuzzle(depth);

            objects = new List<IRPGObject>();
            foreach (IRPGObject obj in traps)
            {
                objects.Add(obj);
            }
            foreach (IRPGObject obj in monsters)
            {
                objects.Add(obj);
            }
            foreach (IRPGObject obj in items)
            {
                objects.Add(obj);
            }
        }

        public bool IsCleared()
        {
            return puzzle.IsSolved();
        }

        public string HandleCommand(IList<string> tokens, out bool solved)
        {
            string result = "";
            for (int i = 1; i < tokens.Count; i++)
            {
                result += ToggleButton(tokens[i]);
            }

            if (!IsCleared())
                result += "<br/>" + puzzle.PuzzleDisplay();

            solved = IsCleared();

            return result;
        }
    }


    public class RPGBasicRoom : IRPGRoom
    {
        public int depth;
        public IList<IRPGTrap> traps = new List<IRPGTrap>();
        public IList<IRPGMonster> monsters = new List<IRPGMonster>();
        public IList<IRPGItem> items = new List<IRPGItem>();

        public IList<IRPGObject> objects = new List<IRPGObject>();

        public IList<IRPGRoom> rooms = new List<IRPGRoom>();

        public int id;

        public RPGBasicRoom() { }

        public void Initialize()
        {
            objects = new List<IRPGObject>();
            foreach (IRPGObject obj in traps)
            {
                objects.Add(obj);
            }
            foreach (IRPGObject obj in monsters)
            {
                objects.Add(obj);
            }
            foreach (IRPGObject obj in items)
            {
                objects.Add(obj);
            }
        }

        public void CreateFutureRooms(int depth, int walkTo)
        {
            if (depth % 5 != 0)
            {
                rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 0));

                if (walkTo != 3)
                    rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 1));
                else
                    rooms.Add(null);

                if (walkTo != 2)
                    rooms.Add(RPGRoomFactory.CreateBasicRoom(depth, 2));
                else
                    rooms.Add(null);
            }
            else
            {
                rooms.Add(RPGRoomFactory.CreatePuzzleRoom(depth));
                rooms.Add(null);
                rooms.Add(null);
            }
        }

        public string GetPlayerFeeling(int perception, bool isCleared)
        {
            int linesRemaining = perception;
            string result = "";

            string direction = id == 0 ? "Norden" : id == 1 ? "Westen" : "Osten";

            var color = GetRoomSense(PlayerSenseType.COLOR);
            if (linesRemaining > 0 && color.Potency > 0)
            {

                result += string.Format("Der Raum in Richtung {0} ist in {1}es Licht getaucht. ", direction, GetRoomColor());

                if (!isCleared)
                {
                    result += "<br/>Der Weg scheint noch <u title='Ein Weg ist dann versperrt, wenn sich noch Monster oder ungelöste Rätsel im Raum befinden.'>versperrt</u> zu sein...";
                    return result;
                }

                linesRemaining--;
            }

            var noise = GetRoomSense(PlayerSenseType.NOISE);
            if (linesRemaining > 0 && noise.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("Ihr hört {0}e Geräusche. ", noise.Description);
            }

            var smell = GetRoomSense(PlayerSenseType.SMELL);
            if (linesRemaining > 0 && smell.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("Ein Luftzug mit {0}em Aroma zieht vorbei. ", smell.Description);
            }

            var magic = GetRoomSense(PlayerSenseType.MAGIC);
            if (linesRemaining > 0 && magic.Potency > 0)
            {
                result += string.Format("Ihr fühlt die Präsenz von {0}er Magie. ", magic.Description);
            }

            if (linesRemaining > 0 && GetRoomTemp() != 0)
            {
                linesRemaining--;
                result += string.Format("Es scheint dort {0} zu sein...", GetRoomTemp() > 0 ? "wärmer" : "kälter");
            }

            return result;
        }

        public string GetRoomColor()
        {
            int totalObjects = objects.Count;
            Random rand = new Random();

            return objects[rand.Next(0, totalObjects)].GetColor();
        }

        public PlayerSense GetRoomSense(PlayerSenseType roomSense)
        {
            IList<PlayerSense> senses = new List<PlayerSense>();
            foreach (IRPGObject obj in objects)
            {
                PlayerSense sense = obj.GetPlayerSense(roomSense);
                if (sense != null)
                    senses.Add(sense);
            }

            PlayerSense randomSense = RPGUtils.GetRandomWeightedPlayerSense(senses);
            if (randomSense != null)

                return new PlayerSense(randomSense.Description, randomSense.Potency);
            else
                return new PlayerSense();
        }

        public int GetRoomTemp()
        {
            int temp = 0;
            foreach (IRPGObject obj in objects)
            {
                temp += obj.GetTemp();
            }

            return temp;
        }

        public IList<IRPGItem> GetItems()
        {
            return items;
        }

        public IRPGRoom GetRoom(int id)
        {
            if (rooms.Count < id)
                return null;

            return rooms[id];
        }

        public bool IsCleared()
        {
            return (monsters.Count == 0 && traps.Count == 0);
        }

        public string HandleCommand(IList<string> tokens, out bool isClr)
        {
            isClr = IsCleared();
            return "Hier gibt es keine Schalter, welche man drücken könnte...";
        }
    }
}