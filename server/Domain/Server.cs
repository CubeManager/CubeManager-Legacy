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

    public ServerType serverType { get; set; }

    public string exactVersion { get; set;}
}
