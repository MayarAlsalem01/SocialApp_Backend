using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Exceptions.RoleExceptions;
using SocialApp.IServices;
/// <summary>
/// Service class for managing roles in the application.
/// </summary>
public class RoleServices : IRoleService
{
    private readonly RoleManager<Role> _roleMgr;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleServices"/> class.
    /// </summary>
    /// <param name="roleMgr">The role manager.</param>
    public RoleServices(RoleManager<Role> roleMgr)
    {
        _roleMgr = roleMgr;
    }

    /// <summary>
    /// Creates a new role with the specified name.
    /// </summary>
    /// <param name="roleName">The name of the role to create.</param>
    /// <returns>The newly created role.</returns>
    /// <exception cref="RoleAlreadyExistException">Thrown if the role already exists.</exception>
    public async Task<Role> CreateNewRole(string roleName)
    {
        //check if the role already exists
        if (await _roleMgr.RoleExistsAsync(roleName))
            throw new RoleAlreadyExistException("The role already exists");

        // create new role 
        Role role = new Role() { Name = roleName };

        // insert the role 
        var createdRole = await _roleMgr.CreateAsync(new Role() { Name = roleName });
        
        //to check if the role anded successfully or not 
        //and if not throw an error 
        if (!createdRole.Succeeded)
            throw new Exception("Failed to create new role");

        return role;
    }

    /// <summary>
    /// Deletes a role by its name.
    /// </summary>
    /// <param name="roleName">The name of the role to delete.</param>
    /// <returns>The deleted role.</returns>
    /// <exception cref="RoleNotFoundException">Thrown if the role is not found.</exception>
    public async Task<Role> Delete(string roleName)
    {
        //get the role 
        var role = await _roleMgr.FindByNameAsync(roleName);

        //check if it already exists
        if (role is null)
            throw new RoleNotFoundException("The role was not found");

        var result = await _roleMgr.DeleteAsync(role);

        if (!result.Succeeded)
            throw new Exception("An error occurred while deleting");

        return role;
    }

    /// <summary>
    /// Retrieves all roles in the system.
    /// </summary>
    /// <returns>A collection of all roles.</returns>
    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        //return all roles 
        return await _roleMgr.Roles.ToListAsync();
    }

    /// <summary>
    /// Retrieves a role by its name asynchronously.
    /// </summary>
    /// <param name="roleName">The name of the role to retrieve.</param>
    /// <returns>The role if found; otherwise, null.</returns>
    public async Task<Role> GetRoleByNameAsync(string roleName)
    {
        var role = await _roleMgr.FindByNameAsync(roleName);
        return role;
    }

    /// <summary>
    /// Updates the name of a role asynchronously.
    /// </summary>
    /// <param name="roleName">The current name of the role.</param>
    /// <param name="newRoleName">The new name for the role.</param>
    /// <returns>The updated role.</returns>
    /// <exception cref="RoleNotFoundException">Thrown if the specified role is not found.</exception>
    public async Task<Role> UpdateRoleAsync(string roleName, string newRoleName)
    {
        var role = await GetRoleByNameAsync(roleName);
        if (role == null)
            throw new RoleNotFoundException(roleName);

        role.Name = newRoleName;
        await _roleMgr.UpdateAsync(role);
        return role;
    }
}
