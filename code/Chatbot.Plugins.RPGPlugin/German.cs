using Chatbot.Plugins.RPGPlugin.Enumerations;
using Chatbot.Plugins.RPGPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
    public static class German
    {

        public static string GetSuffix4th(IRPGObject obj)
        {
            return GetSuffix4th(obj.GetGender());
        }

        public static string GetSuffix4th(Gender gender)
        {
            switch (gender)
            {
                default:
                case Gender.FEMALE:
                    return "e";
                case Gender.MALE:
                    return "en";
                case Gender.NEUTRAL:
                    return "";
            }
        }

        public static string GetArticle(IRPGObject obj, int artCase)
        {
            return GetArticle(obj.GetGender(), artCase);
        }

        public static string GetArticle(Gender gender, int artCase)
        {
            switch (gender)
            {
                default:
                case Gender.FEMALE:
                    return GetFemaleArticle(artCase);
                case Gender.MALE:
                    return GetMaleArticle(artCase);
                case Gender.NEUTRAL:
                    return GetNeutralArticle(artCase);
            }
        }

        private static string GetMaleArticle(int artCase)
        {
            switch (artCase)
            {
                default:
                case 1:
                    return "der";
                case 2:
                    return "des";
                case 3:
                    return "dem";
                case 4:
                    return "den";
            }
        }

        private static string GetFemaleArticle(int artCase)
        {
            switch (artCase)
            {
                default:
                case 1:
                    return "die";
                case 2:
                    return "der";
                case 3:
                    return "der";
                case 4:
                    return "die";
            }
        }

        private static string GetNeutralArticle(int artCase)
        {
            switch (artCase)
            {
                default:
                case 1:
                    return "das";
                case 2:
                    return "des";
                case 3:
                    return "dem";
                case 4:
                    return "das";
            }
        }
    }
}
