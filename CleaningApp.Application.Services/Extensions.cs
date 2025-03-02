using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CleaningApp.Application.Services
{
    public static class TaskMappings
    {
        // Domain -> ViewModel (CleaningTask -> CleaningTaskViewModel)
        public static CleaningTaskViewModel ToViewModel(this CleaningTask entity)
        {
            return new CleaningTaskViewModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                UserName = entity.User?.Name ?? string.Empty,
                RoomName = entity.Room?.Name ?? string.Empty,
                TaskTypeName = entity.TaskType?.Name ?? string.Empty,
                TaskDate = entity.TaskDate
            };
        }

        // Domain -> DTO (CleaningTask -> CleaningTaskDto)
        public static CleaningTaskDto ToDto(this CleaningTask entity)
        {
            return new CleaningTaskDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                RoomId = entity.RoomId,
                TaskTypeId = entity.TaskTypeId,
                TaskDate = entity.TaskDate
            };
        }

        // DTO -> Domain (create new CleaningTask from CleaningTaskDto)
        public static CleaningTask ToEntity(this CleaningTaskDto dto)
        {
            return new CleaningTask
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                TaskTypeId = dto.TaskTypeId,
                TaskDate = dto.TaskDate
            };
        }

        // DTO -> Domain (apply DTO to an existing CleaningTask)
        public static void UpdateFromDto(this CleaningTask entity, CleaningTaskDto dto)
        {
            entity.UserId = dto.UserId;
            entity.RoomId = dto.RoomId;
            entity.TaskTypeId = dto.TaskTypeId;
            entity.TaskDate = dto.TaskDate;
        }

        // Domain -> DTO (User -> UserDto)
        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        // Domain -> DTO (Room -> RoomDto)
        public static RoomDto ToDto(this Room entity)
        {
            return new RoomDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        // Domain -> DTO (TaskType -> TaskTypeDto)
        public static TaskTypeDto ToDto(this TaskType entity)
        {
            return new TaskTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static TaskType ToEntity(this TaskTypeDto dto)
        {
            return new TaskType { Id = dto.Id, Name = dto.Name };
        }
    }
}