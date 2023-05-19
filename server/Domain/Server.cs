namespace Domain;

using Domain.Enums;

public class Server
{
    public Server() {
        this.currentPlayers = new List<Player>();
    }

    public List<Player> currentPlayers {get; set;}

    public Whitelist whitelist {get; set;}

    public Blacklist blacklist {get; set;}

    public Adminlist adminlist {get; set;}

    public ServerProperties serverProperties {get; set;}
    public string name {get; set;}

    public double memory {get; set;}

    public double cpu {get; set;}

    public string state {get; set;}

    public ServerType serverType { get; set; }

    public string exactVersion { get; set;}
}
