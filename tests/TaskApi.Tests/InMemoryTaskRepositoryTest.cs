using TaskApi.Repositories;
using TaskApi.Models;
using FluentAssertions;
namespace TaskApi.Tests.Repositories;
public class InMemoryTaskRepositoryTests {
    private readonly InMemoryTaskRepository _repo;
    public InMemoryTaskRepositoryTests(){
        _repo = new();
    }
    
    [Fact]
    public void Add_TareaValida_AsignaIdYRetornaTarea(){
        //Arrange        
        var tarea = new TaskItem {
            Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"
        };
        
        //Act
        var resultado = _repo.Add(tarea);
        
        //Arssert
        resultado.Id.Should().BeGreaterThan(0);
        resultado.Title.Should().Be("Comprar Guitarra");
    }
    [Fact]
    public void Add_DosTareas_IdsUnicos(){
        //Arrange        
        var tarea1 = new TaskItem {
            Title = "Comprar Juego",
            Description= "Comprar juego para ser Feliz"
        };
        var tarea2 = new TaskItem {
            Title = "Comprar control",
            Description= "Comprar control para ser Feliz"
        };
        //Act
        var resultado1 = _repo.Add(tarea1);
        var resultado2 = _repo.Add(tarea2);
        
        //Assert
        // resultado1.Id.Should().NotBe(resultado2.Id);
        resultado2.Id.Should().Be(resultado1.Id + 1);
    }


    //Get All

    [Fact]
    public void GetAll_RepositorioVacio_RetornaColeccionVacia()
    {
        //Act
        var resultado = _repo.GetAll();
        
        //Assert
        resultado.Should().BeEmpty();
    }

    [Fact]
    public void GetById_TareaExistente_RetornaTarea(){
        
        //Arrange        
        var tarea1 = new TaskItem {Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"};
        var tareaAgregada = _repo.Add(tarea1);
        
        //Act
        var resultado = _repo.GetById(tareaAgregada.Id);
        
        //Assert
        resultado.Should().NotBeNull();
        resultado!.Title.Should().Be("Comprar Guitarra");
    } 
    //Prueba unitaria para una tarea que no existe
    [Fact]
    public void GetById_TareaNoExistente_RetornaNull(){
        
        //Act
        var resultado = _repo.GetById(1000);
        
        //Assert
        resultado.Should().BeNull();
    }


    [Fact]
    public void Update_TareaExiste_ActualizaPropiedades(){
        //Arrange        
        var tareaOriginal = _repo.Add(new TaskItem {
            Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"
        });

        var cambiosTarea = new TaskItem {
            Title = "Comprar Guitarra Eléctrica",
            Description= "Comprar Guitarra Eléctrica para ser Más Feliz",
            
        };

        //Act
        var resultado = _repo.Update(tareaOriginal.Id, cambiosTarea);

        //Assert
        resultado.Should().NotBeNull();
        resultado!.Title.Should().Be("Comprar Guitarra Eléctrica");
        resultado.Description.Should().Be("Comprar Guitarra Eléctrica para ser Más Feliz");

    }

    [Fact]
    public void Update_TareaNoExiste_RetornaNull(){
        //Arrange        
        var cambiosTarea = new TaskItem {
            Title = "Comprar Guitarra Eléctrica",
            Description= "Comprar Guitarra Eléctrica para ser Más Feliz",
            
        };

        //Act
        var resultado = _repo.Update(1000, cambiosTarea);

        //Assert
        resultado.Should().BeNull();
    }

    

    //Delete

    [Fact]
    public void Delete_TareaExiste_RetornaTrue(){
        //Arrange 
        var tarea = _repo.Add(new TaskItem {
            Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"
        });

        //Act
        var resultado = _repo.Delete(tarea.Id);

        //Assert
        resultado.Should().BeTrue();
        _repo.GetById(tarea.Id).Should().BeNull();
    }


    [Fact]
    public void Delete_TareaNoExiste_RetornaFalse(){
        //Act
        var resultado = _repo.Delete(1000);

        //Assert
        resultado.Should().BeFalse();
    }



    
    
}
