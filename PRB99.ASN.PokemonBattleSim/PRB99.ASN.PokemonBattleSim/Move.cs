using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.PokemonBattleSim
{
    public class Move
    {
        private int _power;
        private int _accuracy;
        private int _powerPoints;
        private int _maxPowerPoints;

        public string Name
        {
            get; set;
        }

        public Type Type
        {
            get; set;
        }

        public int Accuracy
        {
            get { return _accuracy; }
            set
            {
                if (value < 30 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Accuracy should be between 30 and 100.");
                }
                _accuracy = value;
            }
        }

        public int Power
        {
            get { return _power; }
            set
            {
                if (value < 0 || value > 250)
                {
                    throw new ArgumentOutOfRangeException("Power should be between 0 and 250.");
                }
                _power = value;
            }
        }

        public int PowerPoints
        {
            get { return _powerPoints; }
            set
            {
                if (value <= 0)
                {
                    _powerPoints = 0;
                }
                else
                {
                    _powerPoints = value;
                }
            }
        }


        public int MaxPowerPoints
        {
            get; set;
        }

        public bool MakesContact
        {
            get; set;
        }

        public Move(string name, Type type, int power, int accuracy, int powerPoints, bool makesContact)
        {
            Name = name;
            Type = type;
            Accuracy = accuracy;
            Power = power;
            MaxPowerPoints = powerPoints;
            PowerPoints = MaxPowerPoints;
            MakesContact = makesContact;
        }
        public string GetMoveBattleInfo()
        {
            return $"Move: {Name}, Type: {Type}, PP: {PowerPoints}/{MaxPowerPoints}";
        }
        public string GetInfo()
        {
            return $"Move: {Name}, Type: {Type}, Power: {Power}, Accuracy: {Accuracy}, PP: {PowerPoints}, Makes Contact: {MakesContact}";
        }

    }
}
