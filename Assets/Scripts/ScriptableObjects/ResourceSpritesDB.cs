using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSpriteDB", menuName = "Alchemy/Resource Sprite DB")]
public class ResourceSpriteDB: ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public string key; // resource name (case sensitive by default)
        public Sprite sprite;
    }

    public Entry[] entries;

    private Dictionary<string, Sprite> map;

    private void OnEnable()
    {
        BuildMap();
    }

    public void BuildMap()
    {
        map = new Dictionary<string, Sprite>(StringComparer.OrdinalIgnoreCase); // ignore case if you want
        if (entries == null) return;
        foreach (var e in entries)
        {
            if (string.IsNullOrEmpty(e.key) || e.sprite == null) continue;
            if (!map.ContainsKey(e.key))
                map.Add(e.key, e.sprite);
            else
                map[e.key] = e.sprite;
        }
    }

    public Sprite GetSprite(string key)
    {
        if (map == null) BuildMap();
        if (string.IsNullOrEmpty(key)) return null;
        map.TryGetValue(key, out var sprite);
        return sprite;
    }
}
