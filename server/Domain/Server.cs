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

    public ServerProperties serverProperties {get; set;}
    public string name {get; set;}

    public double ram {get; set;}

    public double cpu {get; set;}

    public double storage {get; set;}

    public string location {get; set;}

    public Boolean running {get; set;}

    public ServerType serverType { get; set; }

    public string exactVersion { get; set;}
}
