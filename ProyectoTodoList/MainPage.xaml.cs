using System.Collections.ObjectModel;

namespace ProyectoTodoList
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Inicializa tareas de ejemplo
            Tasks = new ObservableCollection<TaskItem>
        {
            new TaskItem { Title = "Diseñar interfaz", Status = "En progreso" },
            new TaskItem { Title = "Revisar requerimientos", Status = "Por hacer" },
            new TaskItem { Title = "Enviar reporte", Status = "Finalizada" }
        };

            BindingContext = this;
        }

        private void OnAddTaskClicked(object sender, EventArgs e)
        {
            // Placeholder para lógica de agregar tarea
            DisplayAlert("Agregar Tarea", "Aquí se agregará una nueva tarea.", "OK");
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Status { get; set; }
    }

}
