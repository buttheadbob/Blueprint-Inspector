using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using Blueprint_Inspector.Models;
using Blueprint_Inspector.Settings;
using Serilog;
using Serilog.Core;

namespace Blueprint_Inspector;

public static class Session
{
    public static Config? Config { get; private set; }
    public const string configFileName = "config.json";
    public static readonly string configDirectory = Directory.GetCurrentDirectory();
    private static readonly string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
    public static Logger? LogIt;
    
    public static ObservableCollection<ServerProfile> ServerProfileList = new();
    
    public static async Task LoadConfig()
    {
        if (!File.Exists(configFileName))
        {
            Config = new Config();
            await SaveConfig();
            return;
        }
            
        string configText = await File.ReadAllTextAsync(Path.Combine( configDirectory, configFileName));
        XmlSerializer serializer = new (typeof(Config));
        using StringReader stringReader = new (configText);
        Config? config = (Config?)serializer.Deserialize(stringReader);
        if (config is null)
        {
            File.Move(Path.Combine( configDirectory, configFileName), Path.Combine( configDirectory, $"{configFileName}.invalid"));
            Config = new Config();
            return;
        }
        Config = config;
    }
        
    public static Task SaveConfig()
    {
        Config ??= new Config();

        XmlSerializer serializer = new (typeof(Config));
        using StringWriter stringWriter = new ();
        serializer.Serialize(stringWriter, Config);
        
        return File.WriteAllTextAsync(Path.Combine( configDirectory, configFileName), stringWriter.ToString());
    }
    
    public static Task SaveServerProfile(string serverName, List<Block> blocks)
    {
        XmlSerializer serializer = new (typeof(List<Block>));
        using StringWriter stringWriter = new ();
        serializer.Serialize(stringWriter, blocks);
        File.WriteAllText(Path.Combine(Config!.ServerProfileFolder, $"{serverName}.server"), stringWriter.ToString());
        return Task.CompletedTask;
    }
    
    public static void InitLogger(RichTextBox rtbLoggerView)
    {
        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);
            
        LogIt = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(logDirectory, "Log.txt"), rollingInterval: RollingInterval.Day)
            .WriteTo.RichTextBox(rtbLoggerView)
            .CreateLogger();
            
        Log.Information("Application started");
    }

    public static async Task LoadServerProfiles()
    {
        string[] serverFiles = Directory.GetFiles(Config!.ServerProfileFolder, "*.server");
        foreach (string serverFile in serverFiles)
        {
            try
            {
                string serverText = await File.ReadAllTextAsync(serverFile);
                XmlSerializer serializer = new (typeof(List<Block>));
                using StringReader stringReader = new (serverText);
                ServerProfile? profile = (ServerProfile?)serializer.Deserialize(stringReader);
                if (profile is null)
                {
                    File.Move(serverFile, $"{serverFile}.invalid");
                    continue;
                }
                ServerProfileList.Add(profile);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, $"Error loading server block file {serverFile}");
            }
        }
    }

    public static void Dispose()
    {
        LogIt?.Dispose();
    }
}