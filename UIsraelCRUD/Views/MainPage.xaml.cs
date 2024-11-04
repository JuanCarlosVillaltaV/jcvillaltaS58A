using UIsraelCRUD.Models;
using UIsraelCRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
namespace UIsraelCRUD.Views;
using Microsoft.Maui.Controls;





public partial class MainPage : ContentPage
{
    public PersonRepository _repository; // Declaración de la variable
    public List<Person> personas = new List<Person>();


    public MainPage()
    {
        InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "persons.db");
        _repository = new PersonRepository(dbPath);
    }


    private async void OnAddClicked(object sender, EventArgs e)
    {
        var person = new Person
        {
            Nombre = NombreEntry.Text,
            Apellido = ApellidoEntry.Text,
            Edad = int.Parse(EdadEntry.Text)
        };

        await _repository.AddPersonAsync(person);

        // Mostrar los datos ingresados
        string mensaje = $"Nombre: {person.Nombre}\nApellido: {person.Apellido}\nEdad: {person.Edad}";
        await DisplayAlert("Datos Ingresados", mensaje, "OK");

        await DisplayAlert("Success", "Persona agregada", "OK");
    }




    private async void OnListClicked(object sender, EventArgs e)
    {
        var persons = await _repository.GetAllPersonsAsync();
        PersonsListView.ItemsSource = persons;


        
        


        // Construir un mensaje con los datos de cada persona
        string message = "Lista de Personas:\n";
        foreach (var person in persons)
        {
            message += $"Nombre: {person.Nombre}, Apellido: {person.Apellido}, Edad: {person.Edad}\n";
        }

        
       

        // Mostrar un alert con la información
        await DisplayAlert("Datos de Personas", message, "OK");

    }


    //Boton actualizar
    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        var selectedPerson = (Person)PersonsListView.SelectedItem;
        if (selectedPerson != null)
        {
            selectedPerson.Nombre = NombreEntry.Text;
            selectedPerson.Apellido = ApellidoEntry.Text;
            selectedPerson.Edad = int.Parse(EdadEntry.Text);

            await _repository.UpdatePersonAsync(selectedPerson);

            // Crear el mensaje con los datos actualizados
            string message = $"Nombre: {selectedPerson.Nombre}\n" +
                             $"Apellido: {selectedPerson.Apellido}\n" +
                             $"Edad: {selectedPerson.Edad}";

            // Mostrar un alert con los datos actualizados
            await DisplayAlert("Persona actualizada", message, "OK");
        }

    }




    private async void OnSearchClicked(object sender, EventArgs e)
    {
        var persons = await _repository.GetAllPersonsAsync();
        var result = persons.FirstOrDefault(p => p.Nombre == NombreEntry.Text);
        if (result != null)
        {
            ApellidoEntry.Text = result.Apellido;
            EdadEntry.Text = result.Edad.ToString();
        }
        else
        {
            await DisplayAlert("Not Found", "Persona no encontrada", "OK");
        }
    }



    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var selectedPerson = (Person)PersonsListView.SelectedItem;
        if (selectedPerson != null)
        {
            await _repository.DeletePersonAsync(selectedPerson);
            await DisplayAlert("Success", "Persona eliminada", "OK");
        }
    }
}

