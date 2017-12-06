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
            new RPGItem()
            {
                ItemName = "Eisendolch",
                Gender = Gender.MALE,
                BaseDmg = 1,
                BaseDef = 1,
                UsesRemaining = 10,
                Enchantment = MagicEffect.NONE,
                NoiseSns = new PlayerSense(),
                MagicSns = new PlayerSense(),
                SmellSns = new PlayerSense("rostig", 0.1f),
                ColorSns = new PlayerSense("hellgrau",0.1f),
                HeatEmission = 0,
                Depth = 1,
                Rarity = 1.0f
            },
            new RPGItem()
            {
                ItemName = "Silberdolch",
                Gender = Gender.MALE,
                BaseDmg = 2,
                BaseDef = 1,
                UsesRemaining = 10,
                Enchantment = MagicEffect.SILVER,
                NoiseSns = new PlayerSense("klingelnd",0.2f),
                MagicSns = new PlayerSense("heilig",0.1f),
                SmellSns = new PlayerSense(),
                ColorSns = new PlayerSense("silbern",0.1f),
                HeatEmission = 0,
                Depth = 1,
                Rarity = 0.2f
            },
            new RPGItem()
            {
                ItemName = "Wort des Feuers",
                Gender = Gender.NEUTRAL,
                BaseDmg = 3,
                BaseDef = 0,
                UsesRemaining = 1,
                Enchantment = MagicEffect.FIRE,
                MagicSns = new PlayerSense("feurig",1),
                SmellSns = new PlayerSense("verbrannt",0.5f),
                ColorSns = new PlayerSense("flackernd",0.3f),
                HeatEmission = 0,
                Depth = 2,
                Rarity = 0.5f
            },
            new RPGItem()
            {
                ItemName = "Wort des Eises",
                Gender = Gender.NEUTRAL,
                BaseDmg = 3,
                BaseDef = 0,
                UsesRemaining = 1,
                Enchantment = MagicEffect.ICE,
                MagicSns = new PlayerSense("zitternd",1),
                SmellSns = new PlayerSense("scharf",0.2f),
                ColorSns =  new PlayerSense("blau",0.3f),
                HeatEmission = 0,
                Depth = 3,
                Rarity = 0.5f
            },
            new RPGItem()
            {
                ItemName = "kl. Trank der Heilung",
                Gender = Gender.MALE,
                BaseDmg = -2,
                BaseDef = 0,
                UsesRemaining = 1,
                Enchantment = MagicEffect.HEAL,
                MagicSns = new PlayerSense(),
                SmellSns = new PlayerSense("schimmlig"),
                ColorSns =  new PlayerSense("rot",0.3f),
                HeatEmission = 0,
                Depth = 2,
                Rarity = 0.5f
            },
        };

        public static RPGItem GetItemForDepth(int depth)
        {
            //adapted from : https://stackoverflow.com/questions/56692/random-weighted-choice
            List<RPGItem> items = ItemCatalogue.ToList();

            double totalWeight = 0.0f;
            foreach (RPGItem item in items)
            {
                double rarity = RPGUtils.RarityByDepth(item, depth);
                totalWeight += rarity;
            }

            if (totalWeight == 0)
                return new RPGItem() { ItemName = "Staub", Gender = Gender.MALE, ColorSns = new PlayerSense("schwach",0.1f) };

            var randomNumber = rand.NextDouble() * totalWeight;
            RPGItem result = null;
            foreach (RPGItem item in items)
            {
                if (randomNumber < RPGUtils.RarityByDepth(item, depth))
                {
                    result = item;
                    break;
                }

                randomNumber = randomNumber - RPGUtils.RarityByDepth(item, depth);
            }

            return new RPGItem(result);
        }
    }

    public class RPGItem : IRPGItem
    {
        public string ItemName;
        public int BaseDmg;
        public int BaseDef;
        public int UsesRemaining;

        public int HeatEmission;
        public MagicEffect Enchantment = MagicEffect.NONE;
        public PlayerSense ColorSns = new PlayerSense();
        public PlayerSense MagicSns = new PlayerSense();
        public PlayerSense NoiseSns = new PlayerSense();
        public PlayerSense SmellSns = new PlayerSense();
        public Gender Gender;

        public int Depth;
        public float Rarity;

        public RPGItem() { }

        public RPGItem(IRPGItem source)
        {
            ItemName = source.GetName();
            BaseDmg = source.GetBaseDmg();
            BaseDef = source.GetBaseDef();
            UsesRemaining = source.GetDurability();
            ColorSns = source.GetPlayerSense(PlayerSenseType.COLOR);
            HeatEmission = source.GetTemp();
            Enchantment = source.GetEnchantment();
            MagicSns = source.GetPlayerSense(PlayerSenseType.MAGIC);
            NoiseSns = source.GetPlayerSense(PlayerSenseType.NOISE);
            SmellSns = source.GetPlayerSense(PlayerSenseType.SMELL);
            Depth = source.GetDepth();
            Rarity = source.GetRarity();
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
            return UsesRemaining;
        }

        public string GetColor()
        {
            return ColorSns.Description;
        }

        public MagicEffect GetEnchantment()
        {
            return Enchantment;
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
                    return MagicSns;
                case PlayerSenseType.NOISE:
                    return NoiseSns;
                case PlayerSenseType.SMELL:
                    return SmellSns;
                case PlayerSenseType.COLOR:
                    return ColorSns;
                default:
                    throw new NotImplementedException("PlayerSenseType " + sense + " undefiniert!");
            }
        }

        public int GetTemp()
        {
            return HeatEmission;
        }

        public int GetDepth()
        {
            return Depth;
        }

        public float GetRarity()
        {
            return Rarity;
        }

        public Gender GetGender()
        {
            return Gender;
        }
    }
}