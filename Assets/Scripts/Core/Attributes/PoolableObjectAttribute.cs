public class PoolableObjectAttribute : ContextAttribute
{
    private readonly string prefLocation;

    public PoolableObjectAttribute(string prefLocation)
    {
        this.prefLocation = prefLocation;
    }

    public string GetPrefLocation()
    {
        return prefLocation;
    }
}