namespace CrossCutting.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class ServerPropertyNameAttribute : Attribute
{
    // Private fields.
    private string name;

    public ServerPropertyNameAttribute(string name)
    {
        this.name = name;
    }

    public virtual string Name
    {
        get { return name; }
    }
}
