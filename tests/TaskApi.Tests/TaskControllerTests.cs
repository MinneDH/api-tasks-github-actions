using TaskApi.Controllers;
using TaskApi.Models;
using TaskApi.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TaskApi.Tests.Controllers;
public class TaskControllerTests{

    private readonly TasksController _controller;
    private readonly Mock<ITaskRepository> _mockRepo;


    public TaskControllerTests()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _controller = new TasksController(_mockRepo.Object);
    }

    [Fact]
    public void GetAll_HayTarea_RetornaOkConListaDeTareas()
    {
        // Arrange
       _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<TaskItem>
        {
            new() { Id = 1, Title = "Tarea 1", Description = "Descripción de la tarea 1"},
            new() { Id = 2, Title = "Tarea 2", Description = "Descripción de la tarea 2"}
        });
        
        // Assert

        _controller.GetAll().Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<TaskItem>>()
            .Which.Should().HaveCount(2);



    }

    [Fact]
    public void GetById_TareaExuste_RetornaOkConTarea()
    {
        // Arrange
        var tarea = new TaskItem { Id = 1, Title = "Tarea 1", Description = "Descripción de la tarea 1" };
        _mockRepo.Setup(repo => repo.GetById(1)).Returns(tarea);
        
        
        // Assert
        _controller.GetById(1).Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<TaskItem>()
            .Which.Should().Be(tarea);
    } 


    [Fact]
    public void GetById_IdNoExiste_RetornaNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetById(1)).Returns((TaskItem?)null);

        // Assert
        _controller.GetById(1).Should().BeOfType<NotFoundResult>();
        
    }



    





}