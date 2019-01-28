using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigValuesReader
{
    private Dictionary<string, string> configurations;
    private string path;

    protected string GetString(string key)
    {
        string value = null;
        if (configurations.ContainsKey(key))
            value = configurations[key];
        else
            throw new Exception(key + " is not defined in: " + path);
        return value;
    }

    protected int GetIntValue(string key, int defaultValue)
    {
        var value = GetString(key);
        var intValue = defaultValue;
        if (value != null) int.TryParse(value, out intValue);

        return intValue;
    }

    protected float GetFloatValue(string key, float defaultValue)
    {
        var value = GetString(key);
        var floatValue = defaultValue;
        if (value != null) float.TryParse(value, out floatValue);

        return floatValue;
    }

    public Color GetColorValue(string key)
    {
        var value = GetString(key);
        var color = default(Color);
        if (!ColorUtility.TryParseHtmlString(value, out color))
            throw new Exception(value + " is not a color (" + path + "/" + key + ")");

        return color;
    }

    protected void Load(string filePath)
    {
        if (filePath == null) throw new Exception("filePath is not defined");

        var conf = Resources.Load<TextAsset>(filePath);
        path = filePath;
        if (conf != null)
        {
            configurations = new Dictionary<string, string>();

            var values = conf.text.Split(Environment.NewLine.ToCharArray());
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] == null || values[i].Length <= 0) continue;
                var value = values[i].Split(':');
                if (value.Length > 1) configurations.Add(value[0].Trim(), value[1].Trim());
            }
        }
    }
}