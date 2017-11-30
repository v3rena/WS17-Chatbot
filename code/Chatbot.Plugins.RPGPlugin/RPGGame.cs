using Chatbot.Plugins.RPGPlugin.Enumerations;
using Chatbot.Plugins.RPGPlugin.Interfaces;
using Chatbot.Plugins.RPGPlugin.RPGItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGGame
    {
        int level = 1;

        public string ShowPlayerActions()
        {
            /*string result = string.Format("You are standing {0}, the monster being {1} steps behind you.", "in a dark hallway", 500);
            result += string.Format("<br/>In the north, you see {0}. The room smells of {1}. It seems like there's {2} magic present.", "a faint shimmer of red", "rotten plants", "no");
            result += string.Format("<br/>In the west, you make out {0}. The room smells of {1}. The temperature seems to be {2}.", "a violet shimmer", "incense", "above average");
            result += string.Format("<br/>In the east, you notice {0}. The room smells of {1}. You can hear {2}.", "green shapes", "fish", "cackling voices");*/



            RPGRoom r1 = RPGRoomFactory.CreateRoom(level);
            RPGRoom r2 = RPGRoomFactory.CreateRoom(level);
            RPGRoom r3 = RPGRoomFactory.CreateRoom(level);

            string result = string.Format("You are {0} meters inside the temple.", level * 50);

            result += string.Format("<br/>The room in the north: {0}", r1.GetPlayerFeeling(5));
            result += string.Format("<br/>The room in the west: {0}", r2.GetPlayerFeeling(5));
            result += string.Format("<br/>The room in the east: {0}", r3.GetPlayerFeeling(5));

            level++;
            return result;


        }
    }

    public class RPGRoom
    {
        public IList<IRPGTrap> traps;
        public IList<IRPGMonster> monsters;
        public IList<IRPGItem> items;

        public IList<IRPGObject> objects;

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

            foreach (RPGItem item in objects)
            {
                result += "<br/>"+item.GetName();
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
        public static RPGRoom CreateRoom(int level)
        {
            RPGRoom result = new RPGRoom();

            result.traps = CreateTraps(level);
            result.monsters = CreateMonsters(level);
            result.items = CreateItems(level);

            return result;
        }

        private static IList<IRPGItem> CreateItems(int level)
        {
            IList<IRPGItem> result = new List<IRPGItem>();

            result.Add(RPGItemFactory.GetItemForDepth(level));
            result.Add(RPGItemFactory.GetItemForDepth(level));

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
