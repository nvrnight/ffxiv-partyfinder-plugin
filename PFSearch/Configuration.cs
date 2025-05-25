using Dalamud.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace PartyFinderPlugin;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    [IgnoreDataMember]
    private Lock _encounterLock = new Lock();

    [IgnoreDataMember]
    public HashSet<string> EncounterIds { get; set; } = [];

    public string SearchExpression { get; set; } = "";
    public string ExcludeExpression { get; set; } = "";
    public bool M5S { get; set; } = true;
    public bool M6S { get; set; } = true;
    public bool M7S { get; set; } = true;
    public bool M8S { get; set; } = true;
    public bool HellsKierUnreal { get; set; } = true;
    public bool RecollectionExtreme { get; set; } = true;
    public bool Ucob { get; set; } = true;
    public bool Uwu { get; set; } = true;
    public bool Tea { get; set; } = true;
    public bool Dsr { get; set; } = true;
    public bool Top { get; set; } = true;
    public bool Fru { get; set; } = true;
    public bool Cod { get; set; } = true;
    public bool On { get; set; } = true;

    // the below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }

    public void LoadEncounterIds()
    {
        lock(_encounterLock)
        {
            EncounterIds.Clear();
            if (M5S) EncounterIds.Add("1020");
            if (M6S) EncounterIds.Add("1022");
            if (M7S) EncounterIds.Add("1024");
            if (M8S) EncounterIds.Add("1026");
            if (HellsKierUnreal) EncounterIds.Add("1029");
            if (RecollectionExtreme) EncounterIds.Add("1031");
            if (Ucob) EncounterIds.Add("280");
            if (Uwu) EncounterIds.Add("539");
            if (Tea) EncounterIds.Add("694");
            if (Dsr) EncounterIds.Add("788");
            if (Top) EncounterIds.Add("908");
            if (Fru) EncounterIds.Add("1006");
            if (Cod) EncounterIds.Add("1010");
        }
    }
}
