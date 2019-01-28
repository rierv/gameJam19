using System;
using System.Collections.Generic;

public class Context
{
    private static List<Type> contextTypes;
    private static bool _initialized = false;

    private static void LoadTypes()
    {
        contextTypes = new List<Type>();
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        foreach (var type in assembly.GetTypes())
        {
            var attribs = type.GetCustomAttributes(typeof(ContextAttribute), false);
            if (attribs != null && attribs.Length > 0) contextTypes.Add(type);
        }
    }

    public static List<Type> Get<T>()
    {
        if (!_initialized)
        {
            LoadTypes();
            _initialized = true;
        }

        var ret = new List<Type>();
        foreach (var type in contextTypes)
        {
            var attribs = type.GetCustomAttributes(typeof(T), false);
            if (attribs.Length > 0) ret.Add(type);
        }

        return ret;
    }
}