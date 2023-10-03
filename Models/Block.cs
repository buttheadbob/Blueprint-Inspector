using System.Collections.Generic;
using System.Linq;
using Blueprint_Inspector.Utils;

namespace Blueprint_Inspector.Models;

public class Block
{
    public string Type { get; set; }
    public string SubType { get; set; }
    public string PCU { get; set; }
    public SerializableDictionary<string,int> Components { get; set; }

    public string DisplayName => $"{Type}/{SubType} [PCU: {PCU}]";
    public IEnumerable<string> ComponentsList => Components.Select(c => $"{c.Key} [{c.Value}]");
}