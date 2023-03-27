namespace domain;

public class Server
{
    public Server() {
        this.players = new List<Player>();
    }

    public List<Player> players {get; set;}

    public Whitelist whitelist {get; set;}

    public Blacklist blacklist {get; set;}

    public Adminlist adminlist {get; set;}

    public Serverproperties serverproperties {get; set;}
}
