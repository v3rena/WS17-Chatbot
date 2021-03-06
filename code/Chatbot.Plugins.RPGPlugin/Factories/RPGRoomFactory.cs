﻿using Chatbot.Plugins.RPGPlugin.Interfaces;
using Chatbot.Plugins.RPGPlugin.RPGObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin.Factories
{
    public class RPGRoomFactory
    {
        public static RPGBasicRoom CreateBasicRoom(int depth, int roomId)
        {
            RPGBasicRoom result = new RPGBasicRoom
            {
                depth = depth,
                traps = CreateTraps(depth),
                monsters = CreateMonsters(depth),
                items = CreateItems(depth),
                id = roomId,
            };

            result.Initialize();
            return result;
        }

        public static RPGPuzzleRoom CreatePuzzleRoom(int depth)
        {
            RPGPuzzleRoom result = new RPGPuzzleRoom
            {
                depth = depth,
                traps = CreateTraps(depth),
                monsters = CreateMonsters(depth),
                items = CreateItems(depth),
            };

            result.Initialize();
            return result;
        }

        private static IList<IRPGItem> CreateItems(int depth)
        {
            IList<IRPGItem> result = new List<IRPGItem>
            {
                RPGItemFactory.GetItemForDepth(depth),
                RPGItemFactory.GetItemForDepth(depth),
            };

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