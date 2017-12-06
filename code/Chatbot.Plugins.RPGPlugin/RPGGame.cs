﻿using Chatbot.Plugins.RPGPlugin.Enumerations;
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
            _currentRoom = new RPGBasicRoom();
            _currentRoom.CreateFutureRooms(_depth, 1);
            _currentRoom.Initialize();
        }

        public string RoomFeelings()
        {
            string result = "";
            if (_depth == 0)
            {
                result = "Mit keuchendem Atem beendet Ihr Eure Flucht an einer Felswand. Es gibt kein Entrinnen. Die schweren Schritte Eures Verfolgers kommen näher und näher...<br/><br/>Doch da: eine Menge an mystischen Glyphen und Gravuren deuten auf eine Spalte im Fels. Eine uralte Tempelanlage! Ob die alten Zauberer wohl wirklich so mächtig waren, wie die Legenden erzählen? Uralte Artefakte, Zaubertränke, Waffen... Wenn Ihr nur an den Fallen und Wächtern vorbeikommen könntet, so gäbe es vielleicht doch noch eine Chance zu überleben.<br/>Rasch zwängt Ihr Euch durch die Felsspalte und folgt dem dahinterliegenden Gang, doch schon nach ein paar Schritten teilt sich der Weg. Ihr wisst zwar nicht, was vor Euch liegt, doch eines steht fest - Umkehren steht außer Frage!<br/>";
            }
            else
            {
                result = string.Format("Ihr befindet euch {0} Meter innerhalb des Tempels. Aus dem Raum führen weitere Gänge in die Dunkelheit.", (_depth) * 20);
                result += RoomItems();
                result += "<br/>";
            }

            result += string.Format("<br/><br/>Der Gang in Richtung Norden {0}", _currentRoom.GetRoom(0).GetPlayerFeeling(5));

            if (_currentRoom.GetRoom(1) == null)
                result += "<br/><br/>Ihr wollt sicherlich nicht nach Westen eurem Verfolger in die Arme laufen.";
            else
                result += string.Format("<br/><br/>Der Gang in Richtung Westen {0}", _currentRoom.GetRoom(1).GetPlayerFeeling(5));

            if (_currentRoom.GetRoom(2) == null)
                result += "<br/><br/>Ihr wollt sicherlich nicht nach Osten eurem Verfolger in die Arme laufen.";
            else
                result += string.Format("<br/><br/>Der Gang in Richtung Osten {0}", _currentRoom.GetRoom(2).GetPlayerFeeling(5));


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
                case "1":
                case "n":
                case "north":
                case "norden":
                    return RoomCleared(1);
                case "2":
                case "w":
                case "west":
                case "westen":
                    if (_currentRoom.GetRoom(1) != null)
                        return RoomCleared(2);
                    else
                        return "Ihr schüttelt den Kopf. Umkehren steht außer Frage!";
                case "3":
                case "e":
                case "o":
                case "east":
                case "osten":
                    if (_currentRoom.GetRoom(2) != null)
                        return RoomCleared(3);
                    else
                        return "Ihr schüttelt den Kopf. Umkehren steht außer Frage!";
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
                    if (tokens.Count > 1)
                        return TakeRoomItemByToken(tokens[1]);
                    else
                        return "Welche Gegenstände wollt ihr mitnehmen?" + RoomItems();
                case "takeall":
                case "nimmalles":
                    return TakeAllRoomItems();
                case "i":
                case "inventar":
                case "inventory":
                    return _player.PlayerInventoryInfo();
                case "pause":
                    return Pause();
                case "help":
                case "hilfe":
                default:
                    return "Es gibt folgende Kommandos (nicht case-sensitive), um den Verlauf des Spiels zu steuern:<br/>" +
                        "Gehe nach Norden: <u>n</u>, <u>norden</u>, <u>north</u>, <u>1</u><br/>" +
                        "Gehe nach Westen: <u>w</u>, <u>westen</u>, <u>west</u>, <u>2</u><br/>" +
                        "Gehe nach Osten: <u>o</u>, <u>osten</u>, <u>e</u>, <u>east</u>, <u>3</u><br/>" +
                        "Schau dich um: <u>schau</u>, <u>look</u>, <u>inspect</u><br/>" +
                        "Nimm Gegenstand X: <u>nimm X</u>, <u>hebe X</u>, <u>take X</u>, <u>grab X</u>, <u>get X</u>, <u>t X</u><br/>" +
                        "Nimm alles: <u>nimmAlles</u>, <u>takeAll</u><br/>" +
                        "Inventar: <u>inventar</u>, <u>i</u>, <u>inventory</u><br/>" +
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

        public string RoomCleared(int walkTo)
        {
            _depth++;
            switch (walkTo)
            {
                case 1:
                    _currentRoom = _currentRoom.GetRoom(0);
                    break;
                case 2:
                    if (_currentRoom.GetRoom(1) != null)
                        _currentRoom = _currentRoom.GetRoom(1);
                    break;
                case 3:
                    if (_currentRoom.GetRoom(2) != null)
                        _currentRoom = _currentRoom.GetRoom(2);
                    break;
            }
            _currentRoom.CreateFutureRooms(_depth, walkTo);

            return RoomFeelings();
        }

        public string RoomItems()
        {
            string result = "";
            if (_currentRoom.GetItems().Count > 0)
            {
                result = "<br/>Am Boden vor euch seht ihr folgende Gegenstände: ";

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
            return result;
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

    public class RPGBasicRoom : IRPGRoom
    {
        public IList<IRPGTrap> traps = new List<IRPGTrap>();
        public IList<IRPGMonster> monsters = new List<IRPGMonster>();
        public IList<IRPGItem> items = new List<IRPGItem>();

        public IList<IRPGObject> objects = new List<IRPGObject>();

        public IList<IRPGRoom> rooms = new List<IRPGRoom>();

        public RPGBasicRoom()
        {
        }

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
            if ((depth-1) % 5 != 4)
            {
                rooms.Add(RPGRoomFactory.CreateBasicRoom(depth));

                if (walkTo != 3)
                    rooms.Add(RPGRoomFactory.CreateBasicRoom(depth));
                else
                    rooms.Add(null);

                if (walkTo != 2)
                    rooms.Add(RPGRoomFactory.CreateBasicRoom(depth));
                else
                    rooms.Add(null);
            }
            else
            {
                rooms.Add(RPGRoomFactory.CreateBasicRoom(depth));
                rooms.Add(null);
                rooms.Add(null);
            }
        }

        public string GetPlayerFeeling(int perception)
        {
            int linesRemaining = perception;
            string result = "";

            var color = GetRoomSense(PlayerSenseType.COLOR);
            if (linesRemaining > 0 && color.Potency > 0)
            {
                result += string.Format("{0} {1}es Licht getaucht. ", "ist in", GetRoomColor());
                linesRemaining--;
            }

            var noise = GetRoomSense(PlayerSenseType.NOISE);
            if (linesRemaining > 0 && noise.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("{0} {1}e Geräusche. ", "Ihr hört", noise.Description);
            }

            var smell = GetRoomSense(PlayerSenseType.SMELL);
            if (linesRemaining > 0 && smell.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("{0} {1}em Aroma zieht vorbei. ", "Ein Luftzug mit", smell.Description);
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
    }
}