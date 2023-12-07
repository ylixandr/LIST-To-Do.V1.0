using LIST_To_Do.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.ViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private MainWindowViewModel windowViewModel;
        private TaskDbContext dbContext;

        private string editPanelVisability;
        public string EditPanelVisability
        {
            get { return editPanelVisability; }
            set { editPanelVisability = value; OnPropertyChanged("EditPanelVisability"); }
        }
        private string controlsPanelVisability;
        public string ControlsPanelVisability
        {
            get { return controlsPanelVisability; }
            set { controlsPanelVisability = value; OnPropertyChanged("ControlsPanelVisability"); }
        }
        private string dataGridIsEnabled;
        public string DataGridIsEnabled
        {
            get { return dataGridIsEnabled; }
            set { dataGridIsEnabled = value; OnPropertyChanged("DataGridIsEnabled"); }
        }

        public void EditPanelVisabilitySwapper()
        {
            if (EditPanelVisability == "Visible")
            {
                EditPanelVisability = "Collapsed";
                ControlsPanelVisability = "Visible";
                DataGridIsEnabled = "True";
            }
            else
            {
                EditPanelVisability = "Visible";
                ControlsPanelVisability = "Collapsed";
                DataGridIsEnabled = "False";
            }
        }

        private Model.Task selectedTask;
        public Model.Task SelectedTask
        {
            get { return selectedTask; }
            set { selectedTask = value; OnPropertyChanged("SelectedTask"); }

        }
        public ObservableCollection<Model.Task> Tasks
        {
            get { return new ObservableCollection<Model.Task>(dbContext.TasksDb.ToList()); }
            set { OnPropertyChanged("Tasks"); }
        }

        public TaskViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new TaskDbContext();
            windowViewModel = mainWindowViewModel;

            if (dbContext.TasksDb.Count() == 0)
            {
                SelectedTask = new Model.Task();
            }
            else
            {
                SelectedTask = Tasks.FirstOrDefault();
            }
            EditPanelVisability = "Collapsed";
            DataGridIsEnabled= "True";
            ControlsPanelVisability = "Visible";
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Model.Task task = new Model.Task();
                        SelectedTask  = task;
                        dbContext.TasksDb.Add(task);
                        EditPanelVisabilitySwapper();

                    }));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(obj =>
                    {
                        EditPanelVisabilitySwapper();

                    }, obj => SelectedTask != null));
            }
        }
        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new RelayCommand(obj =>
                    {
                        EditPanelVisabilitySwapper();
                        SelectedTask = null;

                    }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            { return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        dbContext.TasksDb.Remove(SelectedTask);
                        dbContext.SaveChanges();
                        Tasks = null;

                    },obj => SelectedTask != null));
            } 
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        dbContext.SaveChanges();
                        SelectedTask = null;
                        Tasks = null;

                    },obj => SelectedTask != null && SelectedTask.Description != null && SelectedTask.Description != ""));
            }
        }
    

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
