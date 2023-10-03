using System.Collections.Generic;

namespace Blueprint_Inspector.Models;

public class ServerProfile
{
    public string ServerName { get; set; }
    public HashSet<Block> Blocks { get; set; }
}