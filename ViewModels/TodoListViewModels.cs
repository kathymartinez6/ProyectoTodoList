using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _3_Estados.TodolistViewModels
{
    public class TodoListViewModels 
    {
        private DatabaseService _databaseService;
        private Tarea _nuevaTarea = new Tarea();

        public ObservableCollection<Tarea> Tareas { get; set; }
        public Tarea NuevaTarea
        {
            get => _nuevaTarea;
            set
            {
                _nuevaTarea = value;
                OnPropertyChanged();
            }
        }

        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TodoListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Tareas = new ObservableCollection<Tarea>();
            AgregarTareaCommand = new Command(async () => await AgregarTarea());
            EliminarTareaCommand = new Command<Tarea>(async (tarea) => await EliminarTarea(tarea));
            CargarTareas();
        }

        private async void CargarTareas()
        {
            var lista = await _databaseService.ObtenerTareasAsync();
            foreach (var tarea in lista)
                Tareas.Add(tarea);
        }

        private async Task AgregarTarea()
        {
            if (string.IsNullOrWhiteSpace(NuevaTarea.Descripcion))
            {
                // Agregar validación para que los campos no vallan vacíos.
                return;
            }

            NuevaTarea.Estado = "Por hacer"; // se crea el estado inicial.
            await _databaseService.GuardarTareaAsync(NuevaTarea);
            Tareas.Add(NuevaTarea);
            NuevaTarea = new Tarea(); // aqui se einicia el formulario.
        }

        private async Task EliminarTarea(Tarea tarea)
        {
            await _databaseService.EliminarTareaAsync(tarea);
            Tareas.Remove(tarea);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

