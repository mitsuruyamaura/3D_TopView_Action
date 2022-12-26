[System.Serializable]
public class GemData
{
    public int id;
    public GemType gemType;
    public int point;
    public Gem gemPrefab;
}

public enum GemType {
    A,
    B,
    C
}