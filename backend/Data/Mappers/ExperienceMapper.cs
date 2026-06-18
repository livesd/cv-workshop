using backend.Data.Models;
using backend.DTOs;

namespace backend.Data.Mappers;

public static class ExperienceMapper
{
    public static ExperienceDto ToDto(this Experience experience) =>
        new(
            Id: experience.Id,
            Title: experience.Title,
            Type: experience.Type,
            Description: experience.Description,
            StartDate: experience.StartDate,
            EndDate: experience.EndDate
        );
    
}