using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.PokemonBattleSim
{
    public class Trainer
    {
        private List<Pokemon> _pokemons;
        private List<Items> _items;

        public string Name { get; set; }
        public List<Pokemon> Pokemons
        {
            get { return _pokemons; }
            set
            {
                if (value == null || value.Count < 1 || value.Count > 6)
                {
                    throw new ArgumentException("Number of Pokémon should be between 1 and 6.");
                }

                _pokemons = value;
            }
        }

        public List<Items> Items
        {
            get { return _items; }
            set
            {
                if (value != null && value.Count > 10)
                {
                    throw new ArgumentException("Number of items should not exceed 10.");
                }

                _items = value;
            }
        }

        public Trainer() { }
        public Trainer(string name, List<Pokemon> pokemons, List<Items> items)
        {
            Name = name;
            Pokemons = pokemons;
            Items = items;
        }
        public string UseItem(int itemIndex, Pokemon user, Pokemon target)
        {
            if (itemIndex >= 0 && itemIndex < Items.Count)
            {
                Items selectedItem = Items[itemIndex];

                if (selectedItem is DamageItem)
                {
                    // Execute DoDamage for DamageItem
                    DamageItem damageItem = (DamageItem)selectedItem;
                    string result = damageItem.DoDamage(user, target);

                    // Remove the item from the list of items
                    Items.RemoveAt(itemIndex);

                    return result;
                }
                else if (selectedItem is HealingItem)
                {
                    // Execute DoHeal for HealingItem
                    HealingItem healingItem = (HealingItem)selectedItem;
                    string result = healingItem.DoHeal(user);

                    // Remove the item from the list of items
                    Items.RemoveAt(itemIndex);

                    return result;
                }
                else
                {
                    // Handle other types of items if needed
                    throw new InvalidOperationException("Unsupported item type.");
                }
            }
            else
            {
                return "Invalid item index.";
            }
        }
        public string GetItemsBattleInfo()
        {
            string info = "";
            for (int i = 0; i < Items.Count; i++)
            {
                info += $"{i + 1}. {Items[i].GetItemBattleInfo()} \n";
            }

            return info;
        }

        public string GetInfo()
        {
            string trainerInfo = $"Trainer: {Name}\nNumber of Pokémon: {Pokemons.Count}\n";

            foreach (Pokemon pokemon in Pokemons)
            {
                trainerInfo += $"{pokemon.GetInfo()}\nMoves:\n{pokemon.GetMoveBattleInfo()}";
            }

            return trainerInfo;
        }

    }

}
