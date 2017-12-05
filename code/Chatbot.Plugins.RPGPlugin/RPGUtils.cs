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
    public static class RPGUtils<TRPGObj> where TRPGObj : IRPGObject
    {
        /*//adapted from: https://stackoverflow.com/questions/196661/calling-a-static-method-on-a-generic-type-parameter
        public class RPGObjFactory<TRPGObj> where TRPGObj : new()
        {
            public delegate void ProductInitializationMethod(TRPGObj newProduct);

            private ProductInitializationMethod ProductInitMethod;

            public RPGObjFactory(ProductInitializationMethod productInitMethod)
            {
                ProductInitMethod = productInitMethod;
            }

            public TRPGObj CreateProduct()
            {
                var prod = new TRPGObj();
                ProductInitMethod(prod);
                return prod;
            }
        }*/

        public static float RarityByDepth(TRPGObj item, int depth)
        {
            int deltaDepth = Math.Abs(depth - item.GetDepth());
            if (deltaDepth > 5) return 0;
            return (float)item.GetRarity() / (float)Math.Pow(3, deltaDepth);
        }

        public static List<RPGMonster> MonsterCatalogue = new List<RPGMonster>()
        {
            new RPGMonster(){MonsterName = "spider", MonsterColor ="black", BaseDef=1, BaseDmg=1, Noise = new PlayerSense("scuttling",1), Depth=1, Rarity=0.2f, Effect=MagicEffect.POISON }
        };

        public static List<RPGGameObject> ItemCatalogue = new List<RPGGameObject>()
        {
            new RPGGameObject("iron dagger", 1, 1, 10, "light grey", 0, MagicEffect.NONE, new PlayerSense(), new PlayerSense(), new PlayerSense("rusty", 1), 1, 1.0f),
            new RPGGameObject("silver dagger", 1, 1, 10, "silver", 0, MagicEffect.SILVER, new PlayerSense("holy", 1), new PlayerSense("jingling", 1), new PlayerSense(), 1, 0.2f),
            new RPGGameObject("scroll of fire", 3, 0, 1, "papery", 1, MagicEffect.FIRE, new PlayerSense("burning", 1), new PlayerSense(), new PlayerSense("charred",1),1, 0.5f),
            new RPGGameObject("scroll of ice", 3, 0, 1, "papery", -1, MagicEffect.ICE, new PlayerSense("chilly", 1), new PlayerSense(), new PlayerSense("icy",1),1,0.2f),
            new RPGGameObject("small healing potion", -2,0,1,"red", 0, MagicEffect.HEAL, new PlayerSense(), new PlayerSense(), new PlayerSense("of rotten plants",1), 2, 0.5f)
        };

        public static IDictionary<Type, IList<IRPGObject>> RPGGameObjectDict = new Dictionary<Type, IList<IRPGObject>>() {
          //  {Type.GetType("RPGMonster"), MonsterCatalogue  }
        };

        private static Random rand = new Random();

        public static TRPGObj GetItemForDepth(int depth, Type type)
        {
            
            //adapted from: https://stackoverflow.com/questions/56692/random-weighted-choice
            IList<IRPGObject> items;
            RPGGameObjectDict.TryGetValue(type, out items);

            float totalWeight = 0.0f;
            foreach (TRPGObj item in items)
            {
                float rarity = RarityByDepth(item, depth);
                totalWeight += rarity;
            }

            var randomNumber = rand.NextDouble() * totalWeight;
            TRPGObj result = default(TRPGObj);
            foreach (TRPGObj item in items)
            {
                if (randomNumber < RarityByDepth(item, depth))
                {
                    result = item;
                    break;
                }

                randomNumber = randomNumber - RarityByDepth(item, depth);
            }

            return result;
        }
    }
}
