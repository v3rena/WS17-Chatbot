using Chatbot.Plugins.RPGPlugin.Enumerations;
using System.Collections.Generic;

namespace Chatbot.Plugins.RPGPlugin.Interfaces
{
    public interface IRPGObject
    {
        string GetName();

        string GetColor();
        int GetTemp();

        PlayerSense GetPlayerSense(PlayerSenseType sense);

        //PlayerSense GetSmell();
        //PlayerSense GetNoise();
        //PlayerSense GetMagic();

        int GetDepth();
        float GetRarity();
    }

    public interface IRPGItem : IRPGObject
    {
        int GetBaseDmg();
        int GetBaseDef();
        int GetDurability();
        MagicEffect GetEffect();
    }

    public interface IRPGMonster : IRPGObject
    {
        List<MonsterType> GetTags();

    }

    public interface IRPGTrap : IRPGObject
    {
        MagicEffect GetEffect();
    }
}