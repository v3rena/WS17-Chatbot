namespace Chatbot.Plugins.RPGPlugin.Enumerations
{
    public enum MonsterType
    {
        FIRE,
        ICE,
        UNDEAD
    }

    public enum MagicEffect
    {
        NONE,
        SILVER,
        HEAL,
        DAMAGE,
        ICE,
        FIRE
    }

    public enum PlayerSenseType
    {
        MAGIC,
        NOISE,
        SMELL
    }

    public class PlayerSense
    {
        public string Description;
        public int Potency;

        public PlayerSense(string desc = "", int potency = 0)
        {
            Description = desc;
            Potency = potency;
        }
    }
}
