using Chatbot.Plugins.RPGPlugin.Enumerations;
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
        int depth;
        RPGRoom currentRoom;
        RPGPlayer player;

        public RPGGame()
        {
            depth = 0;
            player = new RPGPlayer();
            currentRoom = new RPGRoom();
            currentRoom.CreateRooms(depth);
        }

        public string RoomFeelings()
        {
            string result = string.Format("You are {0} meters inside the temple.", depth * 20);

            result += RoomItems();

            result += string.Format("<br/>The room in the north: {0}", currentRoom.r1.GetPlayerFeeling(5));
            result += string.Format("<br/>The room in the west: {0}", currentRoom.r2.GetPlayerFeeling(5));
            result += string.Format("<br/>The room in the east: {0}", currentRoom.r3.GetPlayerFeeling(5));

            return result;
        }

        public string PlayerAction(List<string> tokens)
        {
            switch (tokens[1])
            {
                case "n":
                case "north":
                case "1":
                    return RoomCleared(1);
                case "2":
                case "w":
                case "west":
                    return RoomCleared(2);
                case "3":
                case "e":
                case "east":
                    return RoomCleared(3);
                case "look":
                case "inspect":
                    return RoomFeelings();
                case "take":
                case "grab":
                case "get":
                case "t":
                    if (tokens.Count > 2)
                        return TakeRoomItemByToken(tokens[2]);
                    else
                        return "What do you want to take?"+RoomItems();
                case "takeall":
                    return TakeAllRoomItems();
                case "i":
                case "inventory":
                    return player.PlayerInventoryInfo();
                case "help":
                default:
                    return "You can control your character through the following commands:<br/>" +
                        "Walk north: n, north, 1<br/>" +
                        "Walk west: w, west, 2<br/>"+
                        "Walk east: e, east, 3<br/>" +
                        "Inspect the room: look, inspect<br/>" +
                        "Take an item X: take X, grab X, get X, t X<br/>" +
                        "Take all items: takeAll<br/>" +
                        "Check inventory: i, inventory<br/>" +
                        "Open help: help";
            }
        }

        public string TakeRoomItemByToken(string item)
        {
            var items = currentRoom.items.Where(i => i.GetName().ToLower().Contains(item.ToLower())).ToList();
            if (items.Count > 0)
            {
                player.inventory.Add(new RPGGameObject(items.FirstOrDefault()));
                currentRoom.items.Remove(items.FirstOrDefault());
                return "You take the " + items.FirstOrDefault().GetName()+ ".";
            }
            return "You cannot take the " + item + "!";
        }

        public string TakeAllRoomItems()
        {
            var items = currentRoom.items;
            if(items.Count == 0)
            {
                return "You don't see any items...";
            }
            string result = "";
            for(int i = items.Count-1; i >= 0; i--)
            {
                IRPGItem item = items[i];
                result += "<br/>1 " + item.GetName();
                player.inventory.Add(new RPGGameObject(item));
                currentRoom.items.Remove(item);
            }
            return "You take the following items:" + result;
            
        }

        public string RoomCleared(int walkTo)
        {
            depth++;
            switch (walkTo)
            {
                case 1:
                    currentRoom = currentRoom.r1;
                    break;
                case 2:
                    currentRoom = currentRoom.r2;
                    break;
                case 3:
                    currentRoom = currentRoom.r3;
                    break;
            }
            currentRoom.CreateRooms(depth);

            return RoomFeelings();
        }

        public string RoomItems()
        {
            string result = "";
            if (currentRoom.items.Count > 0)
            {
                result += "<br/>On the floor, you see the following objects: ";

                for (int i = 0; i < currentRoom.items.Count; i++)
                {
                    var item = currentRoom.items[i];
                    result += "<u>"+item.GetName()+"</u>";
                    if (i < currentRoom.items.Count - 1)
                    {
                        result += ", ";
                    }
                }
            }
            return result;
        }
    }

    public class RPGPlayer
    {
        public IList<IRPGItem> inventory = new List<IRPGItem>();

        public string PlayerInventoryInfo()
        {
            string result = "";
            if (inventory.Count == 0)
            {
                result += "You don't have any items";
            }
            else if (inventory.Count == 1)
            {
                result += "You have 1 " + inventory[0].GetName();
            }
            else
            {
                result += "You carry " + inventory.Count + " items: ";
                foreach (IRPGItem item in inventory)
                {
                    result += "<br/>" + item.GetName();
                }
            }
            return result;
        }
    }

    public class RPGRoom
    {
        public IList<IRPGTrap> traps = new List<IRPGTrap>();
        public IList<IRPGMonster> monsters = new List<IRPGMonster>();
        public IList<IRPGItem> items = new List<IRPGItem>();

        public IList<IRPGObject> objects = new List<IRPGObject>();

        public RPGRoom r1, r2, r3;

        public RPGRoom()
        {
        }

        public void CreateRooms(int depth)
        {
            r1 = RPGRoomFactory.CreateRoom(depth);
            r2 = RPGRoomFactory.CreateRoom(depth);
            r3 = RPGRoomFactory.CreateRoom(depth);
        }

        public string GetPlayerFeeling(int perception)
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

            int linesRemaining = perception;
            string result = "";

            if (linesRemaining > 0)
            {
                result += string.Format("{0} {1} tint. ", "You notice a", GetRoomColor());
                linesRemaining--;
            }

            var noise = GetRoomSense(PlayerSenseType.NOISE);
            if (linesRemaining > 0 && noise.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("{0} {1} noise. ", "You hear a", noise.Description);
            }

            var smell = GetRoomSense(PlayerSenseType.SMELL);
            if (linesRemaining > 0 && smell.Potency > 0)
            {
                linesRemaining--;
                result += string.Format("{0} {1}. ", "The air smells", smell.Description);
            }

            var magic = GetRoomSense(PlayerSenseType.MAGIC);
            if (linesRemaining > 0 && magic.Potency > 0)
            {
                result += string.Format("You feel the {0} magical power inside. ", magic.Description);
            }

            if (linesRemaining > 0 && GetRoomTemp() != 0)
            {
                linesRemaining--;
                result += "The temperature is unsual. ";
            }

            /*foreach (RPGItem item in objects)
            {
                result += "<br/>" + item.GetName();
            }
            */
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
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (IRPGObject obj in objects)
            {
                PlayerSense sense = obj.GetPlayerSense(roomSense);
                if (dict.ContainsKey(sense.Description))
                {
                    dict[sense.Description] += sense.Potency;
                }
                else
                {
                    dict.Add(sense.Description, sense.Potency);
                }
            }

            string key = dict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            return new PlayerSense() { Description = key, Potency = dict[key] };
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
    }

    public class RPGRoomFactory
    {
        public static RPGRoom CreateRoom(int depth)
        {
            RPGRoom result = new RPGRoom();

            result.traps = CreateTraps(depth);
            result.monsters = CreateMonsters(depth);
            result.items = CreateItems(depth);

            return result;
        }

        private static IList<IRPGItem> CreateItems(int depth)
        {
            IList<IRPGItem> result = new List<IRPGItem>();

            result.Add(RPGItemFactory.GetItemForDepth(depth));
            result.Add(RPGItemFactory.GetItemForDepth(depth));
            result.Add(RPGItemFactory.GetItemForDepth(depth));
            result.Add(RPGItemFactory.GetItemForDepth(depth));
            result.Add(RPGItemFactory.GetItemForDepth(depth));
            result.Add(RPGItemFactory.GetItemForDepth(depth));

            return result;
        }

        private static IList<IRPGMonster> CreateMonsters(int level)
        {
            return new List<IRPGMonster>();
        }

        private static IList<IRPGTrap> CreateTraps(int level)
        {
            return new List<IRPGTrap>();
        }
    }
}
