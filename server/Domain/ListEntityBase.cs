namespace Domain;

public class ListEntityBase
{
    public ListEntityBase() {
        this.list = new List<Player>();
    }

    public List<Player> list {get; set;}

    public void AddPlayer(Player player){
        list.Add(player);
    }

    public void RemovePlayer(Player player){
        list.Remove(player);
    }
}
