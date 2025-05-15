using System.Windows;
using System.Windows.Controls;
using TodoApp1.Models;

namespace TodoApp1.Views
{
    public partial class AddTodoWindow : Window
    {
        public TodoItem Result { get; private set; }

        public AddTodoWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleBox.Text.Trim();
            var desc = DescriptionBox.Text.Trim();
            var status = (StatusBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Title cannot be empty.");
                return;
            }

            Result = new TodoItem
            {
                Title = title,
                Description = desc,
                Status = Enum.Parse<TodoStatus>(status!)
            };

            DialogResult = true;
        }
    }
}
