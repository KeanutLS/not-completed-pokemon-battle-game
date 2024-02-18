using System;

namespace PRB99.ASN.PokemonBattleSim
{
    public class DamageItem : Items, IDamageable
    {
        public DamageItem(string name, int power, string description)
            : base(name, power, description)
        {

        }

        public string DoDamage(Pokemon user, Pokemon target)
        {
            // Set initial text for the damage
            string damageText = $"{target.Name} was damaged! ";

            // Subtract the power of the item from the target's HP
            target.HP -= Power;

            // Check if the target's HP is less than or equal to 0
            if (target.HP <= 0)
            {
                // The target fainted
                damageText += $"{target.Name} Fainted!";
            }

            return damageText;
        }
    }
}
