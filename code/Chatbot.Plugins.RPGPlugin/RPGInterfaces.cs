using Chatbot.Plugins.RPGPlugin.Enumerations;
using System.Collections.Generic;

namespace Chatbot.Plugins.RPGPlugin.Interfaces
{
    public interface IRPGObject
    {
        string GetName();
        Gender GetGender();

        string GetColor();
        int GetTemp();

        PlayerSense GetPlayerSense(PlayerSenseType sense);

        int GetDepth();
        float GetRarity();
    }

    public interface IRPGItem : IRPGObject
    {
        int GetBaseDmg();
        int GetBaseDef();
        int GetDurability();
        MagicEffect GetEnchantment();
    }

    public interface IRPGMonster : IRPGObject
    {
        List<MonsterType> GetTags();
    }

    public interface IRPGTrap : IRPGObject
    {
        MagicEffect GetEffect();
    }

    public interface IRPGRoom
    {
        void Initialize();
        string GetPlayerFeeling(int perception);
        void CreateFutureRooms(int depth, int walkDir);

        IList<IRPGItem> GetItems();
        IRPGRoom GetRoom(int id);
        bool IsCleared();
    }
}