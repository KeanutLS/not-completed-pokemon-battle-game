using System;

namespace PRB99.ASN.PokemonBattleSim
{
    public class HealingItem : Items, IHealable
    {
        public HealingItem(string name, int power, string description)
            : base(name, power, description)
        {

        }

        public string DoHeal(Pokemon user)
        {
            // Set initial text for the heal
            string healText = $"{user.Name} was healed! ";

            // Add the power of the item to the user's HP
            user.HP += Power;

            return healText;
        }
    }
}
