using System;

namespace PRB99.ASN.PokemonBattleSim
{
    public class HealingMove : Move, IHealable
    {
        public HealingMove(string name, Type type, int power, int accuracy, int powerPoints, bool makesContact)
            : base(name, type, power, accuracy, powerPoints, makesContact)
        {

        }

        public string DoHeal(Pokemon user)
        {
            // Set initial text for the heal
            string healText = $"{user.Name} used {this.Name}! ";

            // Check if the heal hits based on accuracy
            Random random = new Random();
            int randomValue = random.Next(1, 101); // Generate a random number between 1 and 100

            if (randomValue <= Accuracy)
            {
                // Heal user by the power of the move
                user.HP += Power;

                // Ensure user's HP doesn't exceed the maximum
                if (user.HP > user.HP)
                {
                    user.HP = user.HP;
                }
            }
            else
            {
                // The heal missed
                healText += $"{user.Name}'s heal missed!";
            }

            return healText;
        }
    }
}
