using SocialApp.Domain.Exceptions.Common;

namespace SocialApp.Domain.Exceptions.RoleExceptions
{
    sealed public class RoleNotFoundException:NotFoundException

        {
            public RoleNotFoundException(string roleName):base(roleName+" not found ")
            {

            }
        }
}
