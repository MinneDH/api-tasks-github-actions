using TaskApi.Controllers;
using TaskApi.Models;
using TaskApi.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TaskApi.Tests.Controllers;

public class TaskControllerTests {

    private readonly TasksController _ctrl;
    private readonly Mock<ITaskRepository> _mockRepo;

    public TaskControllerTests(){
        _mockRepo = new Mock<ITaskRepository>();
        _ctrl = new TasksController(_mockRepo.Object);
    }

    //GetAll
    [Fact]
    public void GetAll_HayTareas_RetornaOkConListaDeTareas(){
        //Arrange
        _mockRepo.Setup(r => r.GetAll()).Returns(
            new List<TaskItem>{
                new (){ Id=1, Title="Tarea 1" },
                new (){ Id=2, Title="Tarea 2" }
            }
        );

        //Assert
        _ctrl.GetAll()
                .Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<TaskItem>>()
                .Which.Should().HaveCount(2);        
    }

    //GetById
    [Fact]
    public void GetById_TareaExiste_RetornaOkConTarea(){
        //Arrange
        _mockRepo.Setup(r=>r.GetById(1)).Returns(new TaskItem{Id=1,Title="Tarea"});

        //Assert
        _ctrl.GetById(1)
                .Should().BeOfType<OkObjectResult>()            
                .Which.Value.Should().BeOfType<TaskItem>()
                .Which.Title.Should().Be("Tarea");

    }

    //NotFoundResult
    [Fact]
    public void GetById_IdNoExiste_RetornaNotFound(){
        _mockRepo.Setup(r=>r.GetById(99)).Returns((TaskItem?)null);

        //Assert
        _ctrl.GetById(99).Should().BeOfType<NotFoundResult>();
    }


}
