using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.PokemonBattleSim
{
    public class Pokemon
    {
        private int _HP;
        private int _MaxHP;
        private List<Move> _moves;
        public string Name { get; set; }
        public Type Type { get; set; }

        public int HP
        {
            get { return _HP; }
            set
            {
                if (value < 0)
                {
                    _HP = 0;
                }
                else if (value > MaxHP)
                {
                    _HP = MaxHP;
                }
                else
                {
                    _HP = value;
                }
            }
        }

        public int MaxHP { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public List<Move> Moves
        {
            get { return _moves; }
            set
            {

                if (value == null || value.Count < 1 || value.Count > 4)
                {
                    throw new ArgumentException("Number of moves should be between 1 and 4.");
                }

                _moves = value;
            }
        }

        public Pokemon(string name, Type type, int maxHP, int attack, int defense, int speed, List<Move> moves)
        {
            Name = name;
            Type = type;
            MaxHP = maxHP; 
            HP = maxHP; 
            Attack = attack;
            Defense = defense;
            Speed = speed;
            Moves = moves;
        }


        public string DoMove(int moveIndex, Pokemon targetPokemon)
        {
            if (moveIndex >= 0 && moveIndex < Moves.Count)
            {
                Move selectedMove = Moves[moveIndex];

                // Reduce PowerPoints of the move
                if (selectedMove.PowerPoints > 0)
                {
                    selectedMove.PowerPoints--;

                    if (selectedMove is DamageMove)
                    {
                        // Execute DoDamage for DamageMove
                        DamageMove damageMove = (DamageMove)selectedMove;
                        return damageMove.DoDamage(this, targetPokemon);
                    }
                    else if (selectedMove is HealingMove)
                    {
                        // Execute DoHeal for HealingMove
                        HealingMove healingMove = (HealingMove)selectedMove;
                        return healingMove.DoHeal(this);
                    }
                    else
                    {
                        throw new InvalidOperationException("Unsupported move type.");
                    }
                }
                else
                {
                    return $"{Name}'s {selectedMove.Name} has no remaining PowerPoints!";
                }
            }
            else
            {
                return "Invalid move index.";
            }
        }

        public string GetPokemonBattleInfo()
        {
            return $"Name: {Name}. HP: {HP} / {MaxHP} max hp.";
        }

        public string GetMoveBattleInfo()
        {
            string moveInfo = "";
            for (int i = 0; i < Moves.Count; i++)
            {
                moveInfo += $"{i + 1}. {Moves[i].GetMoveBattleInfo()}\n";
            }
            return moveInfo;
        }

        public string GetInfo()
        {
            return $"Name: {Name}. Type: {Type}. HP: {HP}. Attack: {Attack}. Defense: {Defense}. Speed: {Speed}.";
        }
    }

}
