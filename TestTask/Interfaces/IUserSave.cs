using TestTask.Handlers;

namespace TestTask.Interfaces
{
    public interface IUserSave
    {
        void SaveUser(string path, TableResource user);
    }
}
