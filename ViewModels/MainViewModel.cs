using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TodoApp1.Helpers;
using TodoApp1.Models;
using TodoApp1.Views;

namespace TodoApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItemViewModel> AllItems { get; set; }

        public ObservableCollection<TodoItemViewModel> TodoItems =>
            new ObservableCollection<TodoItemViewModel>(AllItems.Where(i => i.Status == TodoStatus.Todo));

        public ObservableCollection<TodoItemViewModel> InProgressItems =>
            new ObservableCollection<TodoItemViewModel>(AllItems.Where(i => i.Status == TodoStatus.InProgress));

        public ObservableCollection<TodoItemViewModel> DoneItems =>
            new ObservableCollection<TodoItemViewModel>(AllItems.Where(i => i.Status == TodoStatus.Done));

        public MainViewModel()
        {
            AllItems = new ObservableCollection<TodoItemViewModel>
            {
                new TodoItemViewModel(new TodoItem { Title = "Design UI", Description = "Sketch layout", Status = TodoStatus.Todo }),
                new TodoItemViewModel(new TodoItem { Title = "Write Code", Description = "Bind ViewModel", Status = TodoStatus.InProgress }),
                new TodoItemViewModel(new TodoItem { Title = "Test App", Description = "Test features", Status = TodoStatus.Done }),
            };

            foreach (var item in AllItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItemViewModel.Status))
            {
                // Notify that collections have changed
                OnPropertyChanged(nameof(TodoItems));
                OnPropertyChanged(nameof(InProgressItems));
                OnPropertyChanged(nameof(DoneItems));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public ICommand AddItemCommand => new RelayCommand(_ => AddNewItem());
        public ICommand DeleteItemCommand => new RelayCommand(param => DeleteItem(param as TodoItemViewModel));

        private void AddNewItem()
        {
            var dialog = new AddTodoWindow { Owner = Application.Current.MainWindow };
            if (dialog.ShowDialog() == true)
            {
                var newVM = new TodoItemViewModel(dialog.Result);
                newVM.PropertyChanged += Item_PropertyChanged;
                AllItems.Add(newVM);

                OnPropertyChanged(nameof(TodoItems));
                OnPropertyChanged(nameof(InProgressItems));
                OnPropertyChanged(nameof(DoneItems));
            }
        }

        private void DeleteItem(TodoItemViewModel? item)
        {
            if (item == null) return;

            AllItems.Remove(item);

            OnPropertyChanged(nameof(TodoItems));
            OnPropertyChanged(nameof(InProgressItems));
            OnPropertyChanged(nameof(DoneItems));
        }

    }
}
