using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MyBizLogic
    {
        private IUserRepository _userRepository;

        public MyBizLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User UpdateExisingUser(Guid id, string firstName, string lastName)
        {
            var user = _userRepository.GetByID(id);
            if (user != null)
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                _userRepository.Update(user);
            }
            return user;
        }

        public User CreateUser(string firstName, string lastName)
        {
            var user = new User()
            {
                ID = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName
            };
            _userRepository.Add(user);
            return user;
        }
    }


}
