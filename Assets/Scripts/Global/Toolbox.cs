public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    public Language language = new Language();

    void Awake()
    {
        // Your initialization code here
    }
}

[System.Serializable]
public class Language
{
    public string current;
    public string lastLang;
}