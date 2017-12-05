using Chatbot.Plugins.RPGPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.RPGPlugin.Enumerations;

namespace Chatbot.Plugins.RPGPlugin.RPGObjects
{

    public class RPGMonsterFactory
    {
        public static Random rand = new Random();
        public static List<RPGMonster> ItemCatalogue = new List<RPGMonster>()
        {
            new RPGMonster(){MonsterName = "Spinne", MonsterColor ="schwarz", BaseDef=1, BaseDmg=1, Noise = new PlayerSense("krabbelnd",1), Depth=1, Rarity=0.2f, Effect=MagicEffect.POISON }
        };
    }
    public class RPGMonster : IRPGMonster
    {
        public string MonsterName;
        public int BaseDmg;
        public int BaseDef;
        public string MonsterColor;
        public int Temperature;
        public MagicEffect Effect;
        public PlayerSense Magic;
        public PlayerSense Noise;
        public PlayerSense Smell;

        public int Depth;
        public float Rarity;

        public IList<MonsterType> Tags;

        public string GetColor()
        {
            return MonsterColor;
        }

        public int GetDepth()
        {
            return Depth;
        }

        public string GetName()
        {
            return MonsterName;
        }

        public PlayerSense GetPlayerSense(PlayerSenseType sense)
        {
            switch (sense)
            {
                case PlayerSenseType.MAGIC:
                    return Magic;
                case PlayerSenseType.NOISE:
                    return Noise;
                case PlayerSenseType.SMELL:
                    return Smell;
                default:
                    throw new NotImplementedException("PlayerSenseType " + sense + " undefiniert!");
            }
        }

        public float GetRarity()
        {
            return Rarity;
        }

        public int GetTemp()
        {
            return Temperature;
        }

        public List<MonsterType> GetTags()
        {
            return Tags.ToList();
        }
    }
}
