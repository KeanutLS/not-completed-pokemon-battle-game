//sources: chatgpt => refactoring, checking if i was on the right track with the code and to get some ideas on how to do certain things
// toledo => everything we learned so far

namespace PRB99.ASN.PokemonBattleSim
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------Pokemon battle simulator game-----------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            PlayGame(); // Play the game
        }

        private static Move GetMoveAtIndex(int index, string[] moveData)
        {
            string moveDataLine = moveData[index]; // get the line at the specified index

            string[] moveDataPart = moveDataLine.Split(','); // split the moveDataLine into an array of strings

            if (moveDataPart.Length == 7) // check if there are 7 properties in the moveDataPart array
            {
                // get the properties from the moveDataPart array
                string moveType = moveDataPart[0];
                string moveName = moveDataPart[1];
                Type type = (Type)Enum.Parse(typeof(Type), moveDataPart[2]); // convert the string to a Type enum
                int power = int.Parse(moveDataPart[3]);
                int accuracy = int.Parse(moveDataPart[4]);
                int powerPoints = int.Parse(moveDataPart[5]);
                bool makesContact = Convert.ToBoolean(moveDataPart[6]);

                if (index >= 0 && index < moveData.Length)
                {
                    if (moveDataPart[0] == "D") // check if the first property is a damage move
                    {
                        return new DamageMove(moveName, type, power, accuracy, powerPoints, makesContact);
                    }
                    else if (moveDataPart[0] == "H") // check if the first property is a healing move
                    {
                        return new HealingMove(moveName, type, power, accuracy, powerPoints, makesContact);
                    }
                }
            }
            return null;
        }

        private static Pokemon GetPokemonAtIndex(int index, string[] pokemonData, List<Move> moves)
        {
            if (index >= 0 && index < pokemonData.Length)
            {
                string pokemonDataLine = pokemonData[index];
                string[] pokemonDataPart = pokemonDataLine.Split(','); // split the pokemonDataLine into an array of strings

                if (pokemonDataPart.Length == 7) // check if there are 7 properties in the pokemonDataPart array
                {
                    // get the properties from the pokemonDataPart array
                    string pokemonName = pokemonDataPart[0];
                    Type pokemonType = (Type)Enum.Parse(typeof(Type), pokemonDataPart[1]); // convert the string to a Type enum
                    int healthPoints = int.Parse(pokemonDataPart[2]);
                    int attack = int.Parse(pokemonDataPart[3]);
                    int defense = int.Parse(pokemonDataPart[4]);
                    int speed = int.Parse(pokemonDataPart[5]);

                    string[] MoveData = pokemonDataPart[6].Split(' ');

                    List<Move> pokemonMoves = new List<Move>();
                    foreach (string moveIndex in MoveData) // loop through the MoveData array
                    {
                        if (int.TryParse(moveIndex, out int moveindex) && moveindex >= 0 && moveindex < moves.Count) // check if the moveIndex is a valid integer
                        {
                            pokemonMoves.Add(moves[moveindex]); // add the move to the pokemonMoves list
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid move index '{moveIndex}' for Pokemon '{pokemonName}'."); // throw an exception if the moveIndex is invalid
                        }
                    }

                    return new Pokemon(pokemonName, pokemonType, healthPoints, attack, defense, speed, pokemonMoves); // return a new Pokemon object

                }
            }

            return null; // return null if the index is invalid
        }

        private static Items GetItemAtIndex(int index, string[] itemsData)
        {
            if (index >= 0 && index < itemsData.Length) // check if the index is valid
            {
                string itemDataLine = itemsData[index]; // get the line at the specified index
                string[] itemDataPart = itemDataLine.Split(','); // split the itemDataLine into an array of strings

                if (itemDataPart.Length == 4) // check if there are 4 properties in the itemDataPart array
                {
                    // get the properties from the itemDataPart array
                    string itemType = itemDataPart[0];
                    string itemName = itemDataPart[1];
                    int power = int.Parse(itemDataPart[2]);
                    string description = itemDataPart[3];

                    if (itemType == "D") // check if the first property is a damage item
                    {
                        return new DamageItem(itemName, power, description);
                    }
                    else if (itemType == "H") //    check if the first property is a healing item
                    {
                        return new HealingItem(itemName, power, description);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid item type.");
                    }
                }
            }

            return null;
        }

        private static void DisplayPokemonInfo(int index, List<Pokemon> pokemons) // display the pokemon info
        {
            if (index >= 0 && index < pokemons.Count) // check if the index is valid
            {
                Pokemon selectedPokemon = pokemons[index]; // get the pokemon at the specified index
                // display the pokemon info
                Console.WriteLine();
                Console.WriteLine($"Details for {selectedPokemon.Name}:");
                Console.WriteLine($"Type: {selectedPokemon.Type}");
                Console.WriteLine($"HP: {selectedPokemon.HP}");
                Console.WriteLine($"Attack: {selectedPokemon.Attack}");
                Console.WriteLine($"Defense: {selectedPokemon.Defense}");
                Console.WriteLine($"Speed: {selectedPokemon.Speed}");

                Console.WriteLine("Moves:");
                foreach (Move move in selectedPokemon.Moves)
                {
                    Console.WriteLine($"  - {move.GetInfo()}"); // display the move info
                }

                Console.WriteLine();
            }
        }

        private static void DisplayItemInfo(int index, List<Items> items)
        {
            if (index >= 0 && index < items.Count)
            {
                Items selectedItem = items[index]; // get the item at the specified index

                Console.WriteLine(items[index].GetInfo()); // display the item info
            }
        }

        private static bool ConfirmPokemonSelection(int index, List<Pokemon> pokemons)
        {
            if (index >= 0 && index < pokemons.Count)
            {
                Pokemon selectedPokemon = pokemons[index];

                Console.WriteLine();
                Console.WriteLine($"Details for: {selectedPokemon.Name}");
                Console.WriteLine();

                Console.WriteLine($"Type: {selectedPokemon.Type}");
                Console.WriteLine($"HP: {selectedPokemon.HP}");
                Console.WriteLine($"Attack: {selectedPokemon.Attack}");
                Console.WriteLine($"Defense: {selectedPokemon.Defense}");
                Console.WriteLine($"Speed: {selectedPokemon.Speed}");

                Console.WriteLine("Moves:");
                foreach (Move move in selectedPokemon.Moves)
                {
                    Console.WriteLine($"  - {move.GetInfo()}");
                }

                Console.Write($"Do you want to select {selectedPokemon.Name}? (yes/no): ");

                string confirmation = Console.ReadLine().ToLower();

                while (true)
                {
                    if (confirmation == "yes")
                    {
                        // Positive confirmation, return true
                        return true;
                    }
                    else if (confirmation == "no")
                    {
                        // Negative confirmation, return false
                        Console.Clear();
                        return false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Invalid input. Please enter 'yes' or 'no': ");

                        confirmation = Console.ReadLine().ToLower();

                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid pokemon index.");
                return false;
            }
        }
        private static List<Pokemon> ChoosePokemon(string trainerName, List<Pokemon> pokemons)
        {
            List<Pokemon> selectedPokemon = new List<Pokemon>();

            Console.WriteLine($"Trainer {trainerName}, create your team and select items:");
            Console.WriteLine();

            for (int i = 0; i < 6 && pokemons.Count > 0; i++) // 6 is the maximum number of Pokemon a trainer can have
            {
                Console.WriteLine("Overview of pokemon:");
                for (int j = 0; j < pokemons.Count; j++)
                {
                    // Display Pokemon name along with stats
                    Console.WriteLine($"{j + 1}. {pokemons[j].GetInfo()}");
                }

                Console.Write($"Enter the number of pokemon {i + 1} you want to choose (Enter '0' to stop): "); // i + 1 to display the correct number of the pokemon
                string input = Console.ReadLine();

                if (input == "0")
                {
                    if (i > 0)
                    {
                        // User wants to stop choosing pokemon
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You must select at least one pokemon");
                        Console.WriteLine();
                        i--; // Decrement i to allow the user to retry selecting pokemon
                        continue;
                    }
                }

                if (int.TryParse(input, out int selectedPokemonIndex))
                {
                    if (selectedPokemonIndex >= 1 && selectedPokemonIndex <= pokemons.Count)
                    {
                        // Confirm the selection
                        if (ConfirmPokemonSelection(selectedPokemonIndex - 1, pokemons))
                        {
                            // Add the selected pokemon to the list
                            Pokemon selected = pokemons[selectedPokemonIndex - 1];
                            selectedPokemon.Add(selected);

                            // Remove the selected Pokemon from the list
                            pokemons.RemoveAt(selectedPokemonIndex - 1);

                            // Clear the console
                            Console.Clear();
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid pokemon number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter a valid number.");
                }
            }

            return selectedPokemon;
        }



        private static bool ConfirmItemSelection(int index, List<Items> items)
        {
            if (index >= 0 && index < items.Count)
            {
                Items selectedItem = items[index];

                Console.WriteLine();
                Console.WriteLine($"Details for: {selectedItem.Name}");
                Console.WriteLine();
                Console.WriteLine($"Power: {selectedItem.Power}");
                Console.WriteLine($"Description: {selectedItem.Description}");
                Console.WriteLine();

                Console.Write($"Do you want to select {selectedItem.Name}? (yes/no): ");
                Console.WriteLine();

                string confirmation = Console.ReadLine().ToLower();

                while (true)
                {
                    if (confirmation == "yes")
                    {
                        // Positive confirmation, return true
                        return true;
                    }
                    else if (confirmation == "no")
                    {
                        // Negative confirmation, return false
                        return false;
                    }
                    else
                    {
                        Console.Write("Invalid input. Please enter 'yes' or 'no': ");
                        confirmation = Console.ReadLine().ToLower();
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Item index.");
                return false;
            }
        }
        private static List<Items> ChooseItems(List<Items> allItems, string trainerName)
        {
            List<Items> selectedItems = new List<Items>();

            Console.Clear();
            Console.WriteLine($"Trainer {trainerName}, select your items (Enter '0' to stop):");

            while (selectedItems.Count < 10)
            {
                Console.WriteLine("Available Items:");
                for (int i = 0; i < allItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {allItems[i].Name} - Power: {allItems[i].Power}");
                }

                Console.Write("Enter the number of the item you want to choose (Enter '0' to stop): ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    // User wants to stop choosing items
                    break;
                }

                if (int.TryParse(input, out int selectedItemIndex))
                {
                    if (selectedItemIndex >= 1 && selectedItemIndex <= allItems.Count)
                    {
                        // Display the selected item before confirming
                        for (int i = 0; i < allItems.Count; i++)
                        {
                            if (selectedItemIndex == i + 1)
                            {
                                Console.WriteLine($"Selected item: {allItems[i].Name} - Power: {allItems[i].Power}");
                            }
                        }

                        // Confirm the selection
                        if (ConfirmItemSelection(selectedItemIndex - 1, allItems))
                        {
                            // Add the selected item to the list
                            Items selected = allItems[selectedItemIndex - 1];
                            selectedItems.Add(selected);

                            // Clear the console
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid item number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter a valid number.");
                }
            }

            return selectedItems;
        }

        private static string GetTrainerName()
        {
            Console.WriteLine("Enter your name, trainer: ");
            string trainerName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(trainerName) || trainerName.Length == 0)
            {
                Console.WriteLine("Please enter a valid name.");
                Console.WriteLine("Enter your name, trainer: ");
                trainerName = Console.ReadLine();
            }

            Console.WriteLine("Welcome, " + trainerName + "!");
            return trainerName;
        }


        private static Trainer CreateTrainer(List<Pokemon> allPokemon, List<Items> allItems)
        {
            Trainer trainer = new Trainer();
            string trainerName = GetTrainerName();

            trainer.Name = trainerName; // Set the trainer name

            Console.Clear();
            List<Pokemon> selectedPokemon = new List<Pokemon>();
            List<Items> selectedItems = new List<Items>();

            while (true)
            {
                try
                {
                    // Select Pokemon
                    selectedPokemon.AddRange(ChoosePokemon(trainerName, allPokemon)); // Add the selected Pokemon to the list of the trainer
                    Console.WriteLine($"Trainer {trainerName}, create your team and select items:");

                    // Select items
                    selectedItems.AddRange(ChooseItems(allItems, trainerName)); // Add the selected items to the list of the trainer

                    // Clear the console before displaying the selected Pokemon and Item info
                    Console.Clear();

                    Console.WriteLine();

                    // Display selected Pokemon
                    Console.WriteLine("Selected Pokemon:");
                    for (int i = 0; i < selectedPokemon.Count; i++)
                    {
                        DisplayPokemonInfo(i, selectedPokemon);
                    }

                    // Display selected Items
                    Console.WriteLine("Selected Items:");
                    for (int i = 0; i < selectedItems.Count; i++)
                    {
                        DisplayItemInfo(i, selectedItems);
                    }

                    // If all steps are successful, break out of the loop
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    Console.WriteLine("Press enter to try again.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            // Set the selected pokemon and items to the trainer
            trainer.Pokemons = selectedPokemon;
            trainer.Items = selectedItems;

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"Info of {trainer.GetInfo()}");
            Console.WriteLine();
            Console.WriteLine("Items:");
            Console.WriteLine(trainer.GetItemsBattleInfo());
            Console.WriteLine();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();

            return trainer;
        }

        private static int ChooseMoveIndex(Trainer trainer, Pokemon selectedPokemon) // choose the move index
        {
            while (true)
            {
                try
                {
                    Console.Write($"Trainer {trainer.Name}, what move would you like {selectedPokemon.Name} to do?"); // display the move that the pokemon will do
                    Console.WriteLine();

                    Console.WriteLine(selectedPokemon.GetMoveBattleInfo()); // display the move info

                    Console.Write("Enter the number of the move: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int selectedMoveIndex)) // check if the input is a valid integer
                    {
                        if (selectedMoveIndex >= 1 && selectedMoveIndex <= selectedPokemon.Moves.Count) // check if the input is a valid move index
                        {
                            if (selectedPokemon.Moves[selectedMoveIndex - 1].PowerPoints > 0) // check if the move has remaining PowerPoints
                            {
                                selectedPokemon.Moves[selectedMoveIndex - 1].PowerPoints--; // decrement the PowerPoints of the move

                                return selectedMoveIndex - 1;
                            }
                            else
                            {
                                throw new InvalidOperationException("This move has no remaining PowerPoints.");
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Invalid move index.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input. Enter a valid number.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
        private static int ChooseItemIndex(Trainer trainer)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Trainer {trainer.Name}, what item would you like to use?");
                    Console.WriteLine();

                    Console.WriteLine(trainer.GetItemsBattleInfo());

                    Console.Write("Enter the number of the item: ");
                    string input = Console.ReadLine();


                    if (int.TryParse(input, out int selectedItemIndex)) // check if the input is a valid integer
                    {
                        int selectedNumber = selectedItemIndex - 1;
                        if (selectedNumber >= 0 && selectedNumber < trainer.Items.Count) // check if the input is a valid item index
                        {
                            return selectedItemIndex;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid item index.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input. Enter a valid number.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
        private static int ChooseActionIndex(Trainer trainer)
        {
            Console.WriteLine($" {trainer.Name}, What would you like to do?");
            Console.WriteLine("1) Perform Move");
            Console.WriteLine("2) Use Item");

            Console.WriteLine();
            Console.Write("Enter the number of the action: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int selectedActionIndex)) // check if the input is a valid integer
            {
                if (selectedActionIndex == 1 || selectedActionIndex == 2) // check if the input is a valid action index
                {
                    return selectedActionIndex; // return the selected action index
                }
                else
                {
                    Console.WriteLine($"Invalid action index: {input}. Please enter 1 for Move or 2 for Item.");
                }
            }
            else
            {
                Console.WriteLine($"Invalid input: {input}. Please enter a valid number.");
            }

            // Return a default value if the input is not valid
            return 0;
        }

        public static Tuple<char, int> GetTrainerChoice(Trainer trainer, Pokemon pokemon)
        {
            int selectedActionIndex = ChooseActionIndex(trainer);

            if (selectedActionIndex == 1)
            {
                // => = lambda expression used to create anonymous methods which means that the method has no name.
                if (pokemon.Moves.Any(move => move.PowerPoints > 0)) //check if the pokemon has moves with remaining PowerPoints
                {
                    // The trainer chose to do a move
                    int selectedMoveIndex = ChooseMoveIndex(trainer, pokemon);
                    return Tuple.Create('M', selectedMoveIndex);
                }
                else
                {
                    throw new InvalidOperationException("No moves with remaining PowerPoints available for the Pokemon.");
                }
            }
            else if (selectedActionIndex == 2)
            {
                if (trainer.Items.Count > 0)
                {
                    // The trainer chose to use an item
                    int selectedItemIndex = ChooseItemIndex(trainer);
                    return Tuple.Create('I', selectedItemIndex);
                }
                else
                {
                    Console.WriteLine("Trainer has no items available. Choose a different action.");
                    return GetTrainerChoice(trainer, pokemon); // Retry getting the trainer's choice
                }
            }
            else
            {
                Console.WriteLine("Invalid action index. Choose a different action.");
                return GetTrainerChoice(trainer, pokemon); // Retry getting the trainer's choice
            }
        }


        private static void ExecuteMoves(Trainer trainer1, Trainer trainer2, Pokemon pokemon1, Pokemon pokemon2, int moveIndex1, int moveIndex2)
        {
            Console.WriteLine("Battle Start!");
            Console.WriteLine();

            // Initialize the count of fainted pokemon for each trainer this will be used to check if a trainer is defeated
            int faintedCountTrainer1 = 0;
            int faintedCountTrainer2 = 0;

            Random random = new Random();
            bool isFirstPokemonFirst = pokemon1.Speed >= pokemon2.Speed; // Check if Pokemon 1 is faster than Pokemon 2
            string goesFirst;

            if (random.Next(2) == 0)
            {
                isFirstPokemonFirst = !isFirstPokemonFirst;
            }

            if (isFirstPokemonFirst)
            {
                goesFirst = trainer1.Name;
            }
            else
            {
                goesFirst = trainer2.Name;
            }

            Console.WriteLine($"{goesFirst} goes first.");

            while (faintedCountTrainer1 < trainer1.Pokemons.Count && faintedCountTrainer2 < trainer2.Pokemons.Count) // Loop until one of the trainers is defeated
            {
                if (isFirstPokemonFirst)
                {
                    // Pokemon 1 goes first
                    string result1 = pokemon1.DoMove(moveIndex1, pokemon2);
                    Console.WriteLine(result1);

                    // Check if Pokemon 2 fainted
                    if (pokemon2.HP == 0)
                    {
                        faintedCountTrainer2++;
                    }

                    // Update battle information
                    UpdateBattleInfo(trainer1, trainer2, pokemon1, pokemon2);

                    // Pokemon 2 goes next
                    string result2 = pokemon2.DoMove(moveIndex2, pokemon1);
                    Console.WriteLine(result2);

                    // Check if Pokemon 1 fainted
                    if (pokemon1.HP == 0)
                    {
                        faintedCountTrainer1++;
                    }

                    // Update battle information
                    UpdateBattleInfo(trainer1, trainer2, pokemon1, pokemon2);
                }
                else
                {
                    // Pokemon 2 goes first
                    string result2 = pokemon2.DoMove(moveIndex2, pokemon1);
                    Console.WriteLine(result2);

                    // Check if Pokemon 1 fainted
                    if (pokemon1.HP == 0)
                    {
                        faintedCountTrainer1++;
                    }

                    // Update battle information
                    UpdateBattleInfo(trainer1, trainer2, pokemon1, pokemon2);

                    // Pokemon 1 goes next
                    string result1 = pokemon1.DoMove(moveIndex1, pokemon2);
                    Console.WriteLine(result1);

                    // Check if Pokemon 2 fainted
                    if (pokemon2.HP == 0)
                    {
                        faintedCountTrainer2++;
                    }

                    // Update battle information
                    UpdateBattleInfo(trainer1, trainer2, pokemon1, pokemon2);
                }

                // Toggle the order for the next turn
                isFirstPokemonFirst = !isFirstPokemonFirst;

                // Clear the console
                Console.Clear();
            }

        }

        private static void UseItem(Trainer trainer1, Trainer trainer2, Pokemon pokemon1, Pokemon pokemon2)
        {
            Tuple<char, int> trainer1Choice = GetTrainerChoice(trainer1, pokemon1); // Get the trainer's choice, char is the type of choice and int is the index of the choice

            string goesFirst;
            bool isFirstTrainerFirst = new Random().Next(2) == 0;

            if (isFirstTrainerFirst)
            {
                goesFirst = trainer1.Name; // Trainer 1 goes first
            }
            else
            {
                goesFirst = trainer2.Name; // Trainer 2 goes first
            }

            Console.WriteLine($"{goesFirst} goes first.");


            if (trainer1Choice.Item1 == 'I')
            {
                // Trainer 1 chose to use an item
                Items selectedItem = trainer1.Items[trainer1Choice.Item2];

                if (selectedItem is DamageItem) // check if the item is a damage item
                {
                    // Execute DoDamage for DamageItem
                    DamageItem damageItem = (DamageItem)selectedItem;
                    string result = damageItem.DoDamage(pokemon1, pokemon2);

                    // Remove the item from the list of items
                    trainer1.Items.RemoveAt(trainer1Choice.Item2);

                    Console.WriteLine(result);
                }
                else if (selectedItem is HealingItem) // check if the item is a healing item
                {
                    // Execute DoHeal for HealingItem
                    HealingItem healingItem = (HealingItem)selectedItem;
                    string result = healingItem.DoHeal(pokemon1);

                    // Remove the item from the list of items
                    trainer1.Items.RemoveAt(trainer1Choice.Item2);

                    Console.WriteLine(result);
                    UpdateBattleInfo(trainer1, trainer2, pokemon1, pokemon2); // Update the battle info
                }
                else
                {
                    throw new InvalidOperationException("Unsupported item type."); // throw an exception if the item type is not supported
                }
            }
            else
            {
                throw new InvalidOperationException("Trainer 1 did not choose to use an item."); // throw an exception if trainer 1 did not choose to use an item
            }
        }
        private static void SendOutPokemon(Trainer trainer, int activePokemonIndex)
        {
            Console.WriteLine($"Trainer {trainer.Name} sent out {trainer.Pokemons[activePokemonIndex].Name}!"); // display the pokemon that was sent out
        }


        private static void CheckForWinner(Trainer trainer1, Trainer trainer2)
        {
            bool trainer1Defeated = true; // Assume trainer 1 is defeated
            bool trainer2Defeated = true; // Assume trainer 2 is defeated

            for (int i = 0; i < trainer1.Pokemons.Count; i++) // Loop through trainer 1's pokemon
            {
                if (trainer1.Pokemons[i].HP > 0) // Check if the pokemon's HP is greater than 0
                {
                    trainer1Defeated = false; // Trainer 1 is not defeated
                    break;
                }
            }

            for (int i = 0; i < trainer2.Pokemons.Count; i++) // Loop through trainer 2's pokemon
            {
                if (trainer2.Pokemons[i].HP > 0) // Check if the pokemon's HP is greater than 0
                {
                    trainer2Defeated = false; // Trainer 2 is not defeated
                    break;
                }
            }

            if (trainer1Defeated)
            {
                Console.WriteLine($"Trainer {trainer2.Name} is the winner!");
                Console.WriteLine("Battle End!");

                Environment.Exit(0); // End the game
            }

            if (trainer2Defeated)
            {
                Console.WriteLine($"Trainer {trainer1.Name} is the winner!");
                Console.WriteLine("Battle End!");

                Environment.Exit(0); // End the game
            }
        }
        private static void ShowFaintedCount(Trainer trainer1, Trainer trainer2)
        {
            int totalPokemon1 = trainer1.Pokemons.Count;
            int faintedCountTrainer1 = totalPokemon1;

            int totalPokemon2 = trainer2.Pokemons.Count;

            int faintedCountTrainer2 = totalPokemon2;

            while(trainer1.Pokemons.Count > 0)
            {
                for (int i = 0; i < trainer1.Pokemons.Count; i++)
                {
                    if (trainer1.Pokemons[i].HP <= 0)
                    {
                        faintedCountTrainer1--;
                    }
                }
                Console.WriteLine(trainer1.Name + $" Pokemon not fainted {faintedCountTrainer1} / {totalPokemon1}");
            }
            Console.WriteLine();

            while (trainer2.Pokemons.Count > 0)
            {
                for (int i = 0; i < trainer2.Pokemons.Count; i++)
                {
                    if (trainer2.Pokemons[i].HP <= 0)
                    {
                        faintedCountTrainer2--;
                    }
                }
                Console.WriteLine(trainer2.Name + $" Pokemon not fainted {faintedCountTrainer2} / {totalPokemon2}");
            }

        }
        public static void DisplayBattleInfo(Trainer trainer1, Trainer trainer2, Pokemon pokemon1, Pokemon pokemon2) // Display the current status of the battle
        {
            Console.WriteLine($"Battle between Trainer {trainer1.Name} and Trainer {trainer2.Name}!");
            Console.WriteLine();
            Console.WriteLine($"Trainer {trainer1.Name}'s Pokemon:");
            Console.WriteLine(pokemon1.GetInfo());
            Console.WriteLine();

            Console.WriteLine($"Trainer {trainer2.Name}'s Pokemon:");
            Console.WriteLine(pokemon2.GetInfo());
            Console.WriteLine();
            Console.WriteLine($"Trainer {trainer1.Name}'s Items:");
            Console.WriteLine(trainer1.GetItemsBattleInfo());
            Console.WriteLine();
            Console.WriteLine($"Trainer {trainer2.Name}'s Items:");
            Console.WriteLine(trainer2.GetItemsBattleInfo());
            Console.WriteLine();
        }
        private static void UpdateBattleInfo(Trainer trainer1, Trainer trainer2, Pokemon pokemon1, Pokemon pokemon2)
        {
            Console.Clear(); // Clear the console

            Console.WriteLine($"Battle between Trainer {trainer1.Name} and Trainer {trainer2.Name}!");
            Console.WriteLine();

            Console.WriteLine($"Current Status:");
            Console.WriteLine($"Trainer {trainer1.Name}: {pokemon1.GetInfo()}"); // Display the info  of trainer 1's pokemon
            Console.WriteLine();
            Console.WriteLine($"Trainer {trainer2.Name}: {pokemon2.GetInfo()}"); // Display the info  of trainer 2's pokemon
            Console.WriteLine();

            Console.WriteLine($"Moves for {pokemon1.Name}:"); // Display the moves of trainer 1's pokemon
            Console.WriteLine(pokemon1.GetMoveBattleInfo());
            Console.WriteLine();

            Console.WriteLine($"Moves for {pokemon2.Name}:"); // Display the moves of trainer 2's pokemon
            Console.WriteLine(pokemon2.GetMoveBattleInfo());
            Console.WriteLine();
            if (pokemon1.HP <= 0)
            {
                Console.WriteLine($"Trainer {trainer1.Name}'s Pokemon {pokemon1.Name} fainted!");
                Console.WriteLine();
            }
            if (pokemon2.HP <= 0)
            {
                Console.WriteLine($"Trainer {trainer2.Name}'s Pokemon {pokemon2.Name} fainted!");
                Console.WriteLine();
            }

            CheckForWinner(trainer1, trainer2);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.Clear(); // Clear the console for the new round

            Battle(trainer1, trainer2); // Start the next round
        }

        private static void Battle(Trainer trainer1, Trainer trainer2)
        {
            Console.WriteLine($"Battle between Trainer {trainer1.Name} and Trainer {trainer2.Name}!");

            Pokemon selectedPokemonTrainer1 = trainer1.Pokemons[0]; // Get the first pokemon of trainer 1
            Pokemon selectedPokemonTrainer2 = trainer2.Pokemons[0]; // Get the first pokemon of trainer 2

            Tuple<char, int> trainer1Choice; // Create a tuple for trainer 1's choice (char for the type of choice and int for the index of the choice)
            Tuple<char, int> trainer2Choice; // Create a tuple for trainer 2's choice (char for the type of choice and int for the index of the choice)

            bool isFirstTrainerFirst = new Random().Next(2) == 0; // Randomly choose which trainer goes first

            ShowFaintedCount(trainer1, trainer2);

            while (selectedPokemonTrainer1.HP > 0 && selectedPokemonTrainer2.HP > 0) // Check if both pokemon are still alive
            {
                if (isFirstTrainerFirst) // Check if trainer 1 goes first
                {
                    // Trainer 1's turn
                    SendOutPokemon(trainer1, 0); // Send out the first pokemon of trainer 1

                    Console.WriteLine();

                    SendOutPokemon(trainer2, 0); // Send out the first pokemon of trainer 2

                    trainer1Choice = GetTrainerChoice(trainer1, selectedPokemonTrainer1); // Get the choice of trainer 1

                    if (trainer1Choice.Item1 == 'M') // Check if trainer 1 chose to do a move
                    {
                        // Perform Move
                        int moveIndex1 = trainer1Choice.Item2; // Get the move index of trainer 1
                        Console.WriteLine($"Trainer {trainer1.Name} commands {selectedPokemonTrainer1.Name} to use {selectedPokemonTrainer1.Moves[moveIndex1].Name}!"); // Display the move info of trainer 1
                        Console.WriteLine();
                        Console.WriteLine();

                        trainer2Choice = GetTrainerChoice(trainer2, selectedPokemonTrainer2); // Get the choice of trainer 2
                        int moveIndex2 = trainer2Choice.Item2; // Get the move index of trainer 2

                        ExecuteMoves(trainer1, trainer2, selectedPokemonTrainer1, selectedPokemonTrainer2, moveIndex1, moveIndex2); // Execute the moves
                        Console.WriteLine();
                    }
                    else if (trainer1Choice.Item1 == 'I') // Check if trainer 1 chose to use an item
                    {
                        // Use Item
                        Console.WriteLine($"Trainer {trainer1.Name} uses {trainer1.Items[0].Name} on {selectedPokemonTrainer1.Name}!");
                        Console.WriteLine();
                        UseItem(trainer1, trainer2, selectedPokemonTrainer1, selectedPokemonTrainer2); // Use the item
                        Console.WriteLine();
                    }
                }
                else
                {
                    // Trainer 2's turn
                    SendOutPokemon(trainer2, 0);
                    trainer2Choice = GetTrainerChoice(trainer2, selectedPokemonTrainer2); // Get the choice of trainer 2

                    if (trainer2Choice.Item1 == 'M')
                    {
                        // Perform Move
                        int moveIndex2 = trainer2Choice.Item2;
                        Console.WriteLine($"Trainer {trainer2.Name} commands {selectedPokemonTrainer2.Name} to use {selectedPokemonTrainer2.Moves[moveIndex2].Name}!"); // Display the move info of trainer 2
                        Console.WriteLine();
                        trainer1Choice = GetTrainerChoice(trainer1, selectedPokemonTrainer1); // Get the choice of trainer 1
                        int moveIndex1 = trainer1Choice.Item2; // Get the move index of trainer 1

                        Console.WriteLine();

                        ExecuteMoves(trainer1, trainer2, selectedPokemonTrainer1, selectedPokemonTrainer2, moveIndex1, moveIndex2); // Execute the moves

                        Console.WriteLine();
                    }
                    else if (trainer2Choice.Item1 == 'I')
                    {
                        // Use Item
                        Console.WriteLine($"Trainer {trainer2.Name} uses {trainer2.Items[0].Name} on {selectedPokemonTrainer2.Name}!"); // Display the item info of trainer 2
                        UseItem(trainer1, trainer2, selectedPokemonTrainer1, selectedPokemonTrainer2); // Use the item
                        Console.WriteLine();
                    }
                }

                // Update battle information
                UpdateBattleInfo(trainer1, trainer2, selectedPokemonTrainer1, selectedPokemonTrainer2); // Update the battle info

                // Toggle the order for the next turn
                isFirstTrainerFirst = !isFirstTrainerFirst; // Toggle the order for the next turn
            }


        }

        private static void PlayGame()
        {
            string[] moveData = File.ReadAllLines("moves.txt"); // read the moves.txt file
            List<Move> moves = new List<Move>();

            string[] pokemonData = File.ReadAllLines("pokemon.txt"); // read the pokemon.txt file
            List<Pokemon> pokemons = new List<Pokemon>();

            string[] itemData = File.ReadAllLines("items.txt"); // read the items.txt file
            List<Items> items = new List<Items>();

            Console.WriteLine();
            for (int i = 0; i < moveData.Length; i++) // loop through the moveData array
            {
                moves.Add(GetMoveAtIndex(i, moveData)); // add the move to the moves list
            }

            for (int i = 0; i < pokemonData.Length; i++) // loop through the pokemonData array
            {
                pokemons.Add(GetPokemonAtIndex(i, pokemonData, moves)); // add the pokemon to the pokemons list
            }

            for (int i = 0; i < itemData.Length; i++) // loop through the itemData array
            {
                items.Add(GetItemAtIndex(i, itemData)); // add the item to the items list
            }

            //make a copie of the first lists to use for the second trainer
            List<Pokemon> availablePokemonForTrainer2 = new List<Pokemon>(pokemons);
            List<Items> availableItemsForTrainer2 = new List<Items>(items);

            // use the CreateTrainer function to create the first trainer
            Trainer trainer1 = CreateTrainer(pokemons, items); // use the first lists to create the first trainer
            Console.WriteLine(trainer1.GetItemsBattleInfo());
            Console.WriteLine();

            // use the CreateTrainer function to create the second trainer
            Trainer trainer2 = CreateTrainer(availablePokemonForTrainer2, availableItemsForTrainer2); // use the available lists to create the second trainer
            Console.WriteLine(trainer2.GetItemsBattleInfo());
            Console.WriteLine();

            // Toon informatie van beide spelers
            Console.WriteLine("trainer 1 Info:");
            Console.WriteLine();

            Console.WriteLine(trainer1.GetInfo());
            Console.WriteLine(trainer1.GetItemsBattleInfo());

            Console.WriteLine();
            Console.WriteLine("press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("trainer 2 Info:");
            Console.WriteLine();
            Console.WriteLine(trainer2.GetInfo());
            Console.WriteLine(trainer2.GetItemsBattleInfo());

            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();

            Battle(trainer1, trainer2);
        }

    }
}
