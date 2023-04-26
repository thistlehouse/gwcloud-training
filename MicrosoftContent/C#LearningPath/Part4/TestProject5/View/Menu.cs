using TestProejct6.Models;

namespace TestProejct6.View;

public class Menu
{
    private List<Pet> Pets = new List<Pet>()
    {
        new Pet 
        { 
            PetId = "d1", 
            PetAge = "4", 
            PetSpecies = "Dog", 
            PetPhysicalDescription = "Great Dane - An extra-large brindle dog with a short and sleek coat, warm brown eyes, and medium-sized floppy ears.",
            PetPersonalityDescription = "Affectionate and playful",
            PetNickname = "Bow",
            SuggestedDonation = "85.00"
        },
        new Pet
        {
            PetId = "c2",
            PetAge = "2",
            PetSpecies = "Cat",
            PetPhysicalDescription = "Siamese - A medium-sized cat with a short and sleek seal point coat, almond-shaped blue eyes, and large pointed ears.",
            PetPersonalityDescription = "Bold and confident",
            PetNickname = "Charlie",
            SuggestedDonation = "49.99"
        },
        new Pet
        {
            PetId = "d3",
            PetAge = "1",
            PetSpecies = "Dog",
            PetPhysicalDescription = "Siberian Husky - A medium to large black and white dog with a thick and fluffy coat, blue eyes, and pointed erect ears.",
            PetPersonalityDescription = "Vocal and talkative",
            PetNickname = "Jack",
            SuggestedDonation = "40.00"
        },
        new Pet
        {
            PetId = "c4",
            PetAge = "",
            PetSpecies = "Cat",
            PetPhysicalDescription = "Unkown",
            PetPersonalityDescription = "Affectionate and playful",
            PetNickname = "Boomer",
            SuggestedDonation = ""
        },
        new Pet
        {
            PetId = "d5",
            PetAge = "1",
            PetSpecies = "Dog",
            PetPhysicalDescription = "Siamese - A medium-sized cat with a short and sleek seal point coat, almond-shaped blue eyes, and large pointed ears.",
            PetPersonalityDescription = "Affectionate and playful",
            PetNickname = "Charlie",
            SuggestedDonation = ""
        },
    };
    public Menu() {}

    public int MenuOption()
    {
        int inputRead = 0;

        Console.WriteLine("1. List all of our current pet information.");        
        Console.WriteLine("2. Display all dogs with a specified characteristic.");        
        inputRead = Convert.ToInt32(Console.ReadLine());

        return inputRead;
    }

    public void Render()
    {   
        bool exit = false;

        while (!exit)
        {
            int inputRead = MenuOption();

            switch(inputRead)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    ListAllPets();                    
                    break;
                case 2:
                    DisplayDogsByCharacteristics();
                    break;
            }
        }
    }

    public void ListAllPets()
    {   
        List<string> ids = new List<string>();
        decimal decimalDonation = 0.00m;

        foreach (Pet pet in Pets)
        {
            if (pet.PetAge == "Unkown" || pet.PetPhysicalDescription == "Unkown" ||
                pet.PetPersonalityDescription == "Unkown" || pet.PetNickname == "Unkown")
                ids.Add(pet.PetId);

            Console.WriteLine("\n"+ "Id #: " + pet.PetId);
            Console.WriteLine("Age: " + pet.PetAge);
            Console.WriteLine("Physical Description: " + pet.PetPhysicalDescription);
            Console.WriteLine("Personality: " + pet.PetPersonalityDescription);
            Console.WriteLine("Nickname: " + pet.PetNickname);

            decimalDonation = SuggestedDonationToDecimal(pet);

            Console.WriteLine("Suggested Donation: " + pet.SuggestedDonation);
            Console.WriteLine("=========");
        }   

        string petSingularOrPlural = (ids.Count > 1) ? "pets" : "pet";
        string needFirstOrThird = (ids.Count > 1) ? "need" : "needs";

        Console.WriteLine($"There\'s {ids.Count} {petSingularOrPlural} " + 
            $"that {needFirstOrThird} info to be inserted.");

        Console.WriteLine();
    }

    public void AddNewPet()
    {
        int lastIndex = Pets.Count;
        
        string petId = "";
        string petSpecies = "";
        string petAge = "";
        string petPhysicalDescription = "";
        string petPersonalityDescription = "";
        string petNickname = "";

        do
        {
            Console.WriteLine("Type Pet\'s Species: ");
            petSpecies = Console.ReadLine().ToLower();
        } while (petSpecies == "");
        
        petId = petSpecies.Substring(1).ToLower() + (lastIndex + 1).ToString();        

        Console.WriteLine("\nLeave it blank if info is unknown.");
        Console.WriteLine("Type Pet\'s Age: ");
        petAge = Console.ReadLine();

        Console.WriteLine("Type Pet\'s Physical Description: ");
        petPhysicalDescription = Console.ReadLine();

        Console.WriteLine("Type Pet\'s Personality: ");
        petPersonalityDescription = Console.ReadLine();

        Console.WriteLine("Type Pet\'s Nickname: ");
        petNickname = Console.ReadLine();

        if (petAge == "" && petPhysicalDescription == ""
            && petPersonalityDescription == "" && petNickname == "")
        {
            petAge = "Unknown";
            petPhysicalDescription = "Unknown";
            petPersonalityDescription = "Unknown";
            petNickname = "Unknown";
        }

        Pet pet = new Pet();

        pet.PetId = petId;
        pet.PetAge = petAge;
        pet.PetSpecies = petSpecies;
        pet.PetPhysicalDescription = petPhysicalDescription;
        pet.PetPersonalityDescription = petPersonalityDescription;
        pet.PetNickname = petNickname;

        Pets.Add(pet);

        ListAllPets();        
    }

    public void EnsureAgePhysicalDescription()
    {
        string petId = "";
        string petAge = "";
        string petPhysicalDescription = "";

        do
        {
            Console.WriteLine("Type Pet\'s Id: ");
            petId = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Age: ");
            petAge = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Physical Description: ");
            petPhysicalDescription = Console.ReadLine();
        } while (petId == "" && petAge == "" && petPhysicalDescription == "");

        Pet pet = Pets.FirstOrDefault(p => p.PetId == petId);

        pet.PetAge = petAge;
        pet.PetPhysicalDescription = petPhysicalDescription;

        ListAllPets();
    }

    public void EnsureNicknamePersonality()
    {
        string petId = "";
        string petNickname = "";
        string petPersonality = "";

        do
        {
            Console.WriteLine("Type Pet\'s Id: ");
            petId = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Nickname: ");
            petNickname = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Personality: ");
            petPersonality = Console.ReadLine();
        } while (petId == "" && petNickname == "" && petPersonality == "");

        Pet pet = Pets.FirstOrDefault(p => p.PetId == petId);

        pet.PetNickname = petNickname;
        pet.PetPersonalityDescription = petPersonality;

        ListAllPets();
    }

    public void EditPetAge()
    {
        string petId = "";
        string petAge = "";

        do 
        {
            Console.WriteLine("Type Pet\'s Id");
            petId = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Age and birthday (e.g 3 - 02/28/2000): ");
            petAge = Console.ReadLine();
        } while (petAge == "" && petId == "");

        Pet pet = Pets.FirstOrDefault(p => p.PetId == petId);
        pet.PetAge = petAge;
    }

    public void EditPetPersonality()
    {
        string petId = "";
        string petPersonalty = "";

        do 
        {
            Console.WriteLine("Type Pet\'s Id");
            petId = Console.ReadLine();

            Console.WriteLine("Type Pet\'s Personality: ");
            petPersonalty = Console.ReadLine();
        } while (petPersonalty == "" && petId == "");

        Pet pet = Pets.FirstOrDefault(p => p.PetId == petId);
        pet.PetPersonalityDescription = petPersonalty;
    }

    public void DisplayCatsByCharacteristics()
    {
        string characteristic = "";

        List<Pet> cats = Pets.Where(p => p.PetSpecies == "Cat").ToList();
        List<Pet> catsFound = new List<Pet>();

        do
        {
            Console.WriteLine("Type Cat\'s Characteristic: ");
            characteristic = Console.ReadLine(); 
            // characteristic = "Affectionate";                                           
        } while (characteristic == "");

        bool containsInPersonality = false;
        bool containsInPhysical = false;

        foreach (Pet pet in cats)
        {
            containsInPersonality = pet.PetPersonalityDescription.Contains(characteristic);
            containsInPhysical = pet.PetPhysicalDescription.Contains(characteristic);

            if (containsInPersonality || containsInPhysical)
                catsFound.Add(pet);
        }

        foreach (Pet cat in catsFound)
        {
            Console.WriteLine("\n"+ "Id #: " + cat.PetId);
            Console.WriteLine("Age: " + cat.PetAge);
            Console.WriteLine("Physical Description: " + cat.PetPhysicalDescription);
            Console.WriteLine("Personality: " + cat.PetPersonalityDescription);
            Console.WriteLine("Nickname: " + cat.PetNickname+"\n");
        }
    }

    public void DisplayDogsByCharacteristics()
    {
        string characteristic = "";

        List<Pet> dogs = Pets.Where(p => p.PetSpecies == "Dog").ToList();
        List<Pet> dogsFound = new List<Pet>();

        do
        {
            Console.WriteLine("Type Dog\'s Characteristic: ");
            characteristic = Console.ReadLine();                                                     
        } while (characteristic == "");

        bool containsInPersonality = false;
        bool containsInPhysical = false;

        foreach (Pet pet in dogs)
        {
            containsInPersonality = pet.PetPersonalityDescription.Contains(characteristic);
            containsInPhysical = pet.PetPhysicalDescription.Contains(characteristic);

            if (containsInPersonality || containsInPhysical)
                dogsFound.Add(pet);
            else
                Console.WriteLine($"None of our dogs are a match found for: {characteristic}");
        }

        foreach (Pet dog in dogsFound)
        {
            Console.WriteLine("\n"+ "Id #: " + dog.PetId);
            Console.WriteLine("Age: " + dog.PetAge);
            Console.WriteLine("Physical Description: " + dog.PetPhysicalDescription);
            Console.WriteLine("Personality: " + dog.PetPersonalityDescription);
            Console.WriteLine("Nickname: " + dog.PetNickname+"\n");
        }
    }

    public decimal SuggestedDonationToDecimal(Pet pet)
    {
        decimal decimalDonation = 0m;

        if (!decimal.TryParse(pet.SuggestedDonation, out decimalDonation))
            decimalDonation = 45.00m;

        return decimalDonation;        
    }
}
