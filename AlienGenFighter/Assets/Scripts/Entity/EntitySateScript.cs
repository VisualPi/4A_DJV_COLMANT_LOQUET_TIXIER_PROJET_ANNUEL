public class EntityStateScript
{
    private int _food = 50;
    private int _water = 50;

    public int Food {
        get { return _food; }
        set { _food = value; }
    }

    public int Water {
        get { return _water; }
        set { _water = value; }
    }

    public EdibleInformations TargetedFood { get; set; }

    public EdibleInformations TargetedWater { get; set; }
}
