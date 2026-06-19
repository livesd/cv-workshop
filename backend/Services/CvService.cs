using backend.Data;
using backend.Data.Mappers;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CvService(AppDbContext context) : ICvService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await context.Users.OrderBy(u => u.Name).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        return await context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync();
    }

    public async Task<Experience?> GetExperienceByIdAsync(Guid id)
    {
        return await context.Experiences.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetExperiencesByTypeAsync(string type)
    {
        return await context
            .Experiences.Where(e => e.Type == type)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersWithDesiredSkills(
        IEnumerable<string> desiredTechnologies
    )
    {
        var allUsers = await GetAllUsersAsync();
        var filteredUsers = allUsers.Where(user =>
            UserMapper
                .ParseUserSkills(user.Skills)
                .Any(skill =>
                    desiredTechnologies
                        .Select(tech => tech.ToLower())
                        .Contains(skill.Technology.ToLower())
                )
        );
        return filteredUsers;
    }
}