using System;

namespace PRB99.ASN.PokemonBattleSim
{
    public class Items
    {
        private int _power;

        public string Name { get; set; }
        public int Power
        {
            get { return _power; }
            set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("Power should be between 0 and 50.");
                }
                _power = value;
            }
        }
        public string Description { get; set; }

        public Items(string name, int power, string description)
        {
            Name = name;
            Power = power; 
            Description = description;
        }

        public string GetItemBattleInfo()
        {
            return $"Name: {Name}";
        }

        public string GetInfo()
        {
            return $"Name: {Name}. Power: {Power}. Description: {Description}";
        }
    }
}
