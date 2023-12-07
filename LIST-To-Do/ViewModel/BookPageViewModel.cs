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
    public class BookPageViewModel : INotifyPropertyChanged
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
        public ObservableCollection<Book> Books
        {
            get { return new ObservableCollection<Book>(dbContext.BooksDb.ToList()); }
            set
            {
                OnPropertyChanged("Books");
            }
        }

        private Book selectedBook;
        public Book SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; OnPropertyChanged("SelectedBook"); }
        }

        public BookPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new TaskDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            
            

            if (dbContext.BooksDb.Count() == 0)
            {
                SelectedBook = new Book();
            }
            else
            {
                SelectedBook = Books.FirstOrDefault();
            }
            EditPanelVisability = "Collapsed";
            DataGridIsEnabled = "True";
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
                        Book book = new Book();
                        SelectedBook = book;
                        dbContext.BooksDb.Add(book);
                        EditPanelVisabilitySwapper();
                        //dbContext.SaveChanges();



                    })) ;
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

                    }, obj => SelectedBook != null));
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
                        SelectedBook = null;

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
                        dbContext.BooksDb.Remove(SelectedBook);
                        dbContext.SaveChanges();
                        Books = null;

                    }, obj => SelectedBook != null));
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
                        SelectedBook = null;
                        Books = null;

                    }, obj => SelectedBook != null  && SelectedBook.Title!= null && SelectedBook.Title!=""));
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
