using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IUserRepository
   {
       User GetByID(Guid id);

       void Add(User user);

       void Update(User user);

       void Delete(Guid id);
   }
}
