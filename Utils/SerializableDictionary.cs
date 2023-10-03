using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Blueprint_Inspector.Utils;

[XmlRoot("Dictionary")]
public class SerializableDictionary<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
{
    public SerializableDictionary() { }
    public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
    {
        foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
        {
            Add(kvp);
        }
    }

    public IDictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
        foreach (KeyValuePair<TKey, TValue> kvp in this)
        {
            result.Add(kvp.Key, kvp.Value);
        }
        return result;
    }
}
/*
class Program
{
    static void Main()
    {
        Dictionary<string, int> dict = new Dictionary<string, int>
        {
            { "One", 1 },
            { "Two", 2 },
            { "Three", 3 }
        };

        var serializableDict = new SerializableDictionary<string, int>(dict);

        XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<string, int>));

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, serializableDict);
            string xml = writer.ToString();
            Console.WriteLine(xml);
        }

        using (StringReader reader = new StringReader(xml))
        {
            SerializableDictionary<string, int> deserializedDict = (SerializableDictionary<string, int>)serializer.Deserialize(reader);
            Dictionary<string, int> finalDict = (Dictionary<string, int>)deserializedDict.ToDictionary();
        }
    }
}
*/