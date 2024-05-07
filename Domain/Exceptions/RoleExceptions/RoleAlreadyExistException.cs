using SocialApp.Domain.Entities;
using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.RoleExceptions
{
    sealed public class RoleAlreadyExistException:AlreadyExistException
    {
        public RoleAlreadyExistException(string roleName):base(roleName+" is already exist")
        {

        }
    }
}
