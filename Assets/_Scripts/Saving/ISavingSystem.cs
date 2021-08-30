namespace _Scripts.Saving
{
    public interface ISavingSystem
    {
        void Save(string saveFile);
        void Load(string saveFile);
    }
}