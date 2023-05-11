namespace domain;

public class Player : NamedEntityBase
{
    public Player(string name, Guid id) : base(string name) {
        this.id = id;
    }

    public Guid id {get; set;}
}
