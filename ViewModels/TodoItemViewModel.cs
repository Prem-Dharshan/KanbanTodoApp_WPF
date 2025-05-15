using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp1.Helpers;
using TodoApp1.Models;

namespace TodoApp1.ViewModels
{
    public class TodoItemViewModel : INotifyPropertyChanged
    {
        private TodoItem _item;
        public TodoItemViewModel(TodoItem item) => _item = item;

        public string Title => _item.Title;
        public string Description => _item.Description;

        public TodoStatus Status
        {
            get => _item.Status;
            set
            {
                _item.Status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(ShowLeft));
                OnPropertyChanged(nameof(ShowRight));
            }
        }

        public bool ShowLeft => Status != TodoStatus.Todo;
        public bool ShowRight => Status != TodoStatus.Done;

        public ICommand MoveLeftCommand => new RelayCommand(_ => MoveLeft(), _ => ShowLeft);
        public ICommand MoveRightCommand => new RelayCommand(_ => MoveRight(), _ => ShowRight);

        private void MoveLeft()
        {
            if (Status > TodoStatus.Todo)
                Status--;
        }

        private void MoveRight()
        {
            if (Status < TodoStatus.Done)
                Status++;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
