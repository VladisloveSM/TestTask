using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Handlers;

namespace TestTask.Interfaces
{
    public interface IUserSave
    {
        void SaveUser(string path, TableResource user);
    }
}
