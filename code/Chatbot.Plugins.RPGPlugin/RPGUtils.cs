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
    public static class RPGUtils
    {
        public static double RarityByDepth(IRPGObject item, int depth)
        {
            int deltaDepth = Math.Abs(depth - item.GetDepth());
            if (deltaDepth > 5) return 0;
            return item.GetRarity() / Math.Pow(3, deltaDepth);
        }

        private static Random rand = new Random();

        public static PlayerSense GetRandomWeightedPlayerSense(IList<PlayerSense> playerSenseList)
        {
            float totalWeight = 0.0f;
            foreach(PlayerSense sense in playerSenseList)
            {
                totalWeight += sense.Potency;
            }

            var randomNumber = rand.NextDouble() * totalWeight;

            PlayerSense result = null;
            foreach (PlayerSense sense in playerSenseList)
            {
                if (randomNumber < sense.Potency)
                {
                    result = sense;
                    break;
                }

                randomNumber = randomNumber - sense.Potency;
            }
            return result;
        }
    }
}