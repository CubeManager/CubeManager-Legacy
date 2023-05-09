namespace Domain;

using Domain.Enums;

public class Server
{
    public Server() {
        this.players = new List<Player>();
    }

    public List<Player> players {get; set;}

    public Whitelist whitelist {get; set;}

    public Blacklist blacklist {get; set;}

    public Adminlist adminlist {get; set;}


    public int ram {get; set;}

    public int cpu {get; set;}

    public int storage {get; set;}

    public string location {get; set;}

    public ServerProperties serverproperties {get; set;}

    public ServerType serverType {get; set;}
}
