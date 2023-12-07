using LIST_To_Do.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.ViewModel
{
    public class IdeaPageViewModel : INotifyPropertyChanged
    {

        private MainWindowViewModel mainWindowViewModel;
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


        public ObservableCollection<Ideas> Ideas
        {
            get
            {
                return new ObservableCollection<Ideas>(dbContext.IdeasDb.ToList()); 
            }

            set { OnPropertyChanged("Ideas"); }
        }

        private Ideas selectedIdea;
        public Ideas SelectedIdea
        {
            get { return selectedIdea; }
            set { selectedIdea = value; OnPropertyChanged("SelectedIdea"); }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Ideas idea = new Ideas();
                        SelectedIdea = idea;
                        dbContext.IdeasDb.Add(idea);
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

                    }, obj => SelectedIdea != null));
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
                        SelectedIdea = null;

                    }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        dbContext.IdeasDb.Remove(SelectedIdea); 
                        dbContext.SaveChanges();
                        Ideas = null;
                    }, obj => SelectedIdea != null));
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
                        SelectedIdea = null;
                        Ideas = null;

                    }, obj => SelectedIdea != null));
            }
        }

        public IdeaPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new TaskDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            if (dbContext.IdeasDb.Count() == 0)
            {
                SelectedIdea = new Ideas();

            }
            else
                SelectedIdea = Ideas.FirstOrDefault();

            EditPanelVisability = "Collapsed";
            DataGridIsEnabled = "True";
            ControlsPanelVisability = "Visible";

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
