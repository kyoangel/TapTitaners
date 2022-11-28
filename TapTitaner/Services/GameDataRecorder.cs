namespace TapTitaner.Services;

public class GameDataRecorder
{
    public void Save(string storedDataInJson)
    {
        var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        File.WriteAllText(Path.Combine(docPath, "TapTitaner.txt"), storedDataInJson);
    }
}