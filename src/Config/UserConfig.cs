﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace VirtualDesktopIndicator.Config;

public class UserConfig
{
    public static UserConfig Current { get; } = LoadFromFile(Constants.ConfigName);

    public bool NotificationsEnabled { get; set; } = true;

    public string FontName { get; set; } = "Tahoma";

    public FontStyle FontStyle { get; set; } = FontStyle.Regular;

    public int AdditionalXOffset { get; set; } = 0;

    public int AdditionalYOffset { get; set; } = 0;

    public Dictionary<uint, Color> IconColorForLightThemeDesktop { get; set; } = new Dictionary<uint, Color>();

    public Dictionary<uint, Color> IconColorForDarkThemeDesktop { get; set; } = new Dictionary<uint, Color>();

    [JsonIgnore]
    private string _path;
    
    public UserConfig()
    {
    }
    
    private static UserConfig LoadFromFile(string path)
    {
        var config = new UserConfig();

        if (!File.Exists(path))
        {
            config._path = path;
            config.Save();
        }
        else
        {
            config = JsonSerializer.Deserialize<UserConfig>(File.ReadAllText(path)) ?? config;
            config._path = path;
        }
        
        return config;
    }

    public void Save()
    {
        File.WriteAllText(_path, JsonSerializer.Serialize(this));
    }
}