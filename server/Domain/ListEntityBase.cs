namespace domain;

public class ListEntityBase
{
    public ListEntityBase() {
        this.list = new List<Player>();
    }

    public List<Player> list {get; set;}

    public addPlayer(Player player){
        list.add(player);
    }

    public removePlayer(Player player){
        list.remove(player);
    }
}
