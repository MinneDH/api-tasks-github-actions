using TaskApi.Repositories;
using TaskApi.Models;
using FluentAssertions;

namespace TaskApi.Tests.Repositories;

public class InMemoryTaskRepositoryTests {

    private readonly InMemoryTaskRepository _repo;

    public InMemoryTaskRepositoryTests(){
        _repo = new();
    }
    

    // [Fact]
    // public void Add_TareaValida_AsignaIdYRetornaTarea(){
    //     //Arrange        
    //     var tarea = new TaskItem {
    //         Title = "Comprar Guitarra",
    //         Description= "Comprar Guitarra para ser Feliz"
    //     };

    //     //Act
    //     var resultado = _repo.Add(tarea);

    //     //Assert
    //     resultado.Id.Should().BeGreaterThan(0);
    //     resultado.Title.Should().Be("Comprar Guitarra");
    // }

    // [Fact]
    // public void Add_DosTareas_AsignaIdsSecuenciales(){
    //     //Arrange
    //     var tarea1 = new TaskItem{Title = "Comprar Guitarra",
    //         Description= "Comprar Guitarra para ser Feliz"};
    //     var tarea2 = new TaskItem{Title = "Comprar Laptop",
    //         Description= "Comprar Laptop"};

    //     //Act
    //     var r1 = _repo.Add(tarea1);
    //     var r2 = _repo.Add(tarea2);

    //     //Assert
    //     r2.Id.Should().Be(r1.Id + 1);
    // }
    
    // //GetAll
    // [Fact]
    // public void GetAll_RepositorioVacio_RetornaColeccionVacia(){
    //     //Arrange

    //     //Act
    //     var resultado = _repo.GetAll();

    //     //Assert
    //     resultado.Should().BeEmpty();

    // }

    // [Fact]
    // public void GetAll_ConDosTareas_RetornaDosTareas(){
    //     //Arrange
    //     var tarea1 = new TaskItem{Title = "Comprar Guitarra",
    //         Description= "Comprar Guitarra para ser Feliz"};
    //     var tarea2 = new TaskItem{Title = "Comprar Laptop",
    //         Description= "Comprar Laptop"};
    //     var r1 = _repo.Add(tarea1);
    //     var r2 = _repo.Add(tarea2);

    //     //Act        
    //     var resultado = _repo.GetAll();

    //     //Assert
    //     resultado.Should().HaveCount(2);        
    // }

    //GetById
    [Fact]
    public void GetById_TareaExiste_RetornaTarea(){
        //Arrange
        var tarea1 = new TaskItem{Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"};
        var tareaAgregada = _repo.Add(tarea1);

        //Act
        var resultado = _repo.GetById(tareaAgregada.Id);

        //Assert
        resultado.Should().NotBeNull();   //resultado.Should().BeNull();
        resultado!.Title.Should().Be("Comprar Guitarra");
    }

    [Fact]
    public void GetById_IdNoExiste_RetornaNull(){
        //Arrange

        //Act
        var resultado = _repo.GetById(1000);

        //Assert
        resultado.Should().BeNull();
    }

    [Fact]    
    public void Update_TareaExiste_ActualizaPropiedades(){
        //Arrange
        var tareaOriginal = _repo.Add(new TaskItem{Title="Tarea 1",Description="Tarea 1"});
        var cambiosTarea = new TaskItem{Title="Actualizada",Description="Tarea 1 Actualizada"};

        //Act
        var resultado = _repo.Update(tareaOriginal.Id,cambiosTarea);

        //Assert
        resultado.Should().NotBeNull();
        resultado!.Title.Should().Be("Actualizada");

    }

    [Fact]
    public void Update_IdNoExiste_RetornaNull(){

        //Act
        var resultado = _repo.Update(1000,new TaskItem{Title="XXX"});

        //Assert
        resultado.Should().BeNull();
    }

    //DELETE
    [Fact]
    public void Delete_TareaExiste_RetornaTrue(){
        //Arrange
        var tareaAgregada = _repo.Add(new TaskItem{Title="Tarea a eliminar"});

        //Act
        var resultado = _repo.Delete(tareaAgregada.Id);

        //Assert
        resultado.Should().BeTrue();
        _repo.GetById(tareaAgregada.Id).Should().BeNull();
    }

    [Fact]
    public void Delete_IdNoExiste_RetornaFalse(){

        //Act
        var resultado = _repo.Delete(1000);

        //Assert
        resultado.Should().BeFalse();
    }




}