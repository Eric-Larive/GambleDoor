using System;

// --- Enums ---
public enum DoorOutcome { Boon, Malus, Nothing }
public enum BoonType  { Gold, Heal, Shield }
public enum MalusType { Damage, LooseGold, Death }

// --- Weighted Entry Helpers ---
[Serializable]
public class WeightedBoon
{
    public BoonType boon;
    public float weight;
}
[Serializable]
public class WeightedMalus
{
    public MalusType malus;
    public float weight;
}
