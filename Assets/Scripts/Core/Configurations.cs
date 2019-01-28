public class Configurations : ConfigValuesReader
{
    protected string filePath = "game_configs/conf";

    #region Singleton

    private static Configurations instance;

    public static Configurations Instance
    {
        get
        {
            if (instance == null) CreateInstance();
            return instance;
        }
    }

    public static void CreateInstance()
    {
        instance = new Configurations();
        instance.Load(instance.filePath);
    }

    #endregion

    #region GetValue

    public static string GetValue(string key)
    {
        return GetValue(key, null);
    }

    public static string GetValue(string key, string defaultValue)
    {
        return Instance.GetString(key);
    }

    #endregion

    #region GetIntValue

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static int GetInt(string key, int defaultValue)
    {
        return Instance.GetIntValue(key, defaultValue);
    }

    #endregion

    #region GetFloatValue

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0f);
    }

    public static float GetFloat(string key, float defaultValue)
    {
        return Instance.GetFloatValue(key, defaultValue);
    }

    #endregion
}