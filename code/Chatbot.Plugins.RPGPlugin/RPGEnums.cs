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
        FIRE,
        POISON
    }

    public enum PlayerSenseType
    {
        MAGIC,
        NOISE,
        SMELL,
        COLOR
    }

    public enum Gender
    {
        MALE,
        FEMALE,
        NEUTRAL
    }

    public class PlayerSense
    {
        public string Description;
        public float Potency;

        public PlayerSense()
        {
            Description = "";
            Potency = 0;
        }

        public PlayerSense(string desc, float potency = 0.1f)
        {
            Description = desc;
            Potency = potency;
        }
    }

 
}
