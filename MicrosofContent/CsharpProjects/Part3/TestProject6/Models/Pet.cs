namespace TestProejct6.Models;
public class Pet
{
    public string PetId { get; set; }
    public string PetSpecies { get; set; }
    public string? PetAge { get; set; }
    public string? PetPhysicalDescription { get; set; }
    public string? PetPersonalityDescription { get; set; }
    public string? PetNickname { get; set; }

    public Pet() {}

    public Pet(string petId, string petSpecies, 
        string petAge, string petPhysicalDescription, 
        string petPersonalityDescription, string petNickname)
    {
        PetId = petId;
        PetSpecies = petSpecies;
        PetAge = petAge;
        PetPhysicalDescription = petPhysicalDescription;
        PetPersonalityDescription = petPersonalityDescription;
        PetNickname = petNickname;
    }
}
