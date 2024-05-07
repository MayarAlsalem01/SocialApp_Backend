using SocialApp.Domain.Entities;

namespace SocialApp.IServices
{
    public interface IRoleService
    {
        /// <summary>
        /// Creates a new role with the specified name.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>The newly created role.</returns>
        /// <exception cref="RoleAlreadyExistException">Thrown if the role already exists.</exception>
        Task<Role> CreateNewRole(string roleName);
        /// <summary>
        /// Retrieves all roles in the system.
        /// </summary>
        /// <returns>A collection of all roles.</returns>
        Task<IEnumerable<Role>> GetAllRoles();
        /// <summary>
        /// Deletes a role by its name.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <returns>The deleted role.</returns>
        /// <exception cref="RoleNotFoundException">Thrown if the role is not found.</exception>
        Task<Role>Delete(string roleName);
        /// <summary>
        /// Updates the name of a role asynchronously.
        /// </summary>
        /// <param name="roleName">The current name of the role.</param>
        /// <param name="newRoleName">The new name for the role.</param>
        /// <returns>The updated role.</returns>
        /// <exception cref="RoleNotFoundException">Thrown if the specified role is not found.</exception>
        Task<Role>UpdateRoleAsync(string roleName,string newRoleName);
        /// <summary>
        /// Retrieves a role by its name asynchronously.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve.</param>
        /// <returns>The role if found; otherwise, null.</returns>
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}
