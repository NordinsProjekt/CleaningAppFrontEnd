using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

public class CleaningTaskService(IUnitOfWork unitOfWork)
{
    public async Task AddTaskTypeAsync(TaskTypeDto taskDto)
    {
        var newTask = taskDto.ToEntity();
        await unitOfWork.Repository<TaskType>().AddAsync(newTask);
        await unitOfWork.CompleteAsync();
    }

    public async Task UpdateTaskTypeAsync(TaskTypeDto taskTypeDto)
    {
        var existingTaskType = await unitOfWork.Repository<TaskType>().GetByIdAsync(taskTypeDto.Id);
        if (existingTaskType == null) return;

        existingTaskType.Name = taskTypeDto.Name;
        unitOfWork.Repository<TaskType>().Update(existingTaskType);
        await unitOfWork.CompleteAsync();
    }

    public async Task DeleteTaskTypeAsync(Guid id)
    {
        var taskType = await unitOfWork.Repository<TaskType>().GetByIdAsync(id);
        if (taskType == null) return;

        unitOfWork.Repository<TaskType>().Remove(taskType);
        await unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
    {
        var taskTypeList = await unitOfWork.Repository<TaskType>().GetAllAsync();
        return taskTypeList.Select(x => new TaskTypeDto { Id = x.Id, Name = x.Name });
    }
}