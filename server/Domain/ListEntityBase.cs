namespace Domain;

public class ListEntityBase
{
    public ListEntityBase() {
        this.list = new List<Player>();
    }

    public List<Player> list {get; set;}

    public void addPlayer(Player player){
        list.Add(player);
    }

    public void removePlayer(Player player){
        list.Remove(player);
    }
}
