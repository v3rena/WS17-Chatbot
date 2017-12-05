using Chatbot.Plugins.RPGPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.RPGPlugin.Enumerations;

namespace Chatbot.Plugins.RPGPlugin.RPGObjects
{

    public static class RPGItemFactory
    {
        public static Random rand = new Random();
        public static List<RPGItem> ItemCatalogue = new List<RPGItem>()
        {
            new RPGItem("iron dagger", 1, 1, 10, "light grey", 0, MagicEffect.NONE, new PlayerSense(), new PlayerSense(), new PlayerSense("rusty", 1), 1, 1.0f),
            new RPGItem("silver dagger", 1, 1, 10, "silver", 0, MagicEffect.SILVER, new PlayerSense("holy", 1), new PlayerSense("jingling", 1), new PlayerSense(), 1, 0.2f),
            new RPGItem("scroll of fire", 3, 0, 1, "papery", 1, MagicEffect.FIRE, new PlayerSense("burning", 1), new PlayerSense(), new PlayerSense("charred",1),1, 0.5f),
            new RPGItem("scroll of ice", 3, 0, 1, "papery", -1, MagicEffect.ICE, new PlayerSense("chilly", 1), new PlayerSense(), new PlayerSense("icy",1),1,0.2f),
            /*new RPGItem("super debug thingy", 100, 100, 100, "bright", 3, MagicEffect.FIRE, new PlayerSense("blazing", 3), new PlayerSense("energetic crackling",3), new PlayerSense("like victory", 3),3,0.3f),*/
            new RPGItem("small healing potion", -2,0,1,"red", 0, MagicEffect.HEAL, new PlayerSense(), new PlayerSense(), new PlayerSense("of rotten plants",1), 2, 0.5f)
        };

        public static RPGItem GetItemForDepth(int depth)
        {
            //adapted from : https://stackoverflow.com/questions/56692/random-weighted-choice
            List<RPGItem> items = ItemCatalogue.ToList();

            float totalWeight = 0.0f;
            foreach (RPGItem item in items)
            {
                float rarity = RPGUtils<RPGItem>.RarityByDepth(item, depth);
                totalWeight += rarity;
            }

            var randomNumber = rand.NextDouble() * totalWeight;
            RPGItem result = null;
            foreach (RPGItem item in items)
            {
                if (randomNumber < RPGUtils<RPGItem>.RarityByDepth(item, depth))
                {
                    result = item;
                    break;
                }

                randomNumber = randomNumber - RPGUtils<RPGItem>.RarityByDepth(item, depth);
            }

            return new RPGItem(result);
        }
    }

    public class RPGItem : IRPGItem
    {
        public string ItemName;
        public int BaseDmg;
        public int BaseDef;
        public int Dur;
        public string ItemColor;
        public int Temperature;
        public MagicEffect Effect;
        public PlayerSense Magic;
        public PlayerSense Noise;
        public PlayerSense Smell;

        public int Depth;
        public float Rarity;

        public RPGItem(IRPGItem source)
        {
            ItemName = source.GetName();
            BaseDmg = source.GetBaseDmg();
            BaseDef = source.GetBaseDef();
            Dur = source.GetDurability();
            ItemColor = source.GetColor();
            Temperature = source.GetTemp();
            Effect = source.GetEffect();
            Magic = source.GetPlayerSense(PlayerSenseType.MAGIC);
            Noise = source.GetPlayerSense(PlayerSenseType.NOISE);
            Smell = source.GetPlayerSense(PlayerSenseType.SMELL);
            Depth = source.GetDepth();
            Rarity = source.GetRarity();
        }

        public RPGItem(string itemName, int baseDmg, int baseDef, int dur,
           string itemColor, int temperature, MagicEffect effect,
            PlayerSense magic, PlayerSense noise, PlayerSense smell,
            int depth, float rarity)
        {
            ItemName = itemName;
            BaseDmg = baseDmg;
            BaseDef = baseDef;
            Dur = dur;
            ItemColor = itemColor;
            Temperature = temperature;
            Effect = effect;
            Magic = magic;
            Noise = noise;
            Smell = smell;
            Depth = depth;
            Rarity = rarity;
        }

        public int GetBaseDmg()
        {
            return BaseDmg;
        }

        public int GetBaseDef()
        {
            return BaseDef;
        }

        public int GetDurability()
        {
            return Dur;
        }

        public string GetColor()
        {
            return ItemColor;
        }

        public MagicEffect GetEffect()
        {
            return Effect;
        }

        public string GetName()
        {
            return ItemName;
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
                    throw new NotImplementedException("RoomSense " + sense + " not found!");
            }
        }

        public int GetTemp()
        {
            return Temperature;
        }

        public int GetDepth()
        {
            return Depth;
        }

        public float GetRarity()
        {
            return Rarity;
        }
    }
}