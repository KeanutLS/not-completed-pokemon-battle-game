using System;

namespace PRB99.ASN.PokemonBattleSim
{
    public class DamageMove : Move, IDamageable
    {
        public DamageMove(string name, Type type, int power, int accuracy, int powerPoints, bool makesContact)
            : base(name, type, power, accuracy, powerPoints, makesContact)
        {

        }

        public string DoDamage(Pokemon user, Pokemon target)
        {
            string attackText = $"{user.Name} used {this.Name}! ";

            // Check if the attack hits based on accuracy
            Random random = new Random();
            int randomValue = random.Next(1, 101); // Generate a random number between 1 and 100

            if (randomValue <= Accuracy)
            {
                // Calculate damage
                double damage = (0.5 * Power * (user.Attack / target.Defense)) + 1;

                // Subtract damage from target's HP
                target.HP -= (int)damage;

                // Check if target's HP is now 0 or below
                if (target.HP <= 0)
                {
                    attackText += $"{target.Name} Fainted!";
                }
            }
            else
            {
                // The attack missed
                attackText += $"{user.Name}'s attack missed!";
            }

            return attackText;
        }
    }
}
