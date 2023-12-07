using LIST_To_Do.Model;
using LIST_To_Do.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LIST_To_Do.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private TaskDbContext dbContext;
        private Page taskPage;
        private TaskViewModel taskViewModel;
       
        private Page bookPage;
        private BooksDbContext booksDbContext;
        private BookPageViewModel bookPageViewModel;

        private Page ideaPage;
        private IdeaPageViewModel ideaPageViewModel;

        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

       

        public MainWindowViewModel()
        {
            dbContext = new TaskDbContext();
            booksDbContext = new BooksDbContext();
            LoadPages();
        }


        public void LoadPages()
        {
            taskViewModel = new TaskViewModel(this);
            taskPage = new TaskPage() { DataContext = taskViewModel };
            bookPageViewModel = new BookPageViewModel(this);
            bookPage = new BookPage() { DataContext = bookPageViewModel };
            ideaPageViewModel = new IdeaPageViewModel(this);
            ideaPage = new IdeaPage() {  DataContext = ideaPageViewModel };


        }

        private RelayCommand taskPageCommand;
        public RelayCommand TaskPageCommand
        {
            get
            {
                return taskPageCommand ??
                    (taskPageCommand = new RelayCommand(obj =>
                    {
                        TaskPage();

                    }));
            }
        }

        private RelayCommand bookPageCommand;
        public RelayCommand BookPageCommand
        {
            get
            {
                return bookPageCommand ??
                    (bookPageCommand = new RelayCommand(obj =>
                    {
                        BookPage();
                    }));
            }
        }

        private RelayCommand ideaPageCommand;
        public RelayCommand IdeaPageCommand
        {
            get
            {
                return ideaPageCommand ??
                    (ideaPageCommand = new RelayCommand(obj =>
                    {
                        IdeaPage();
                    }));
            }
        }
        public void TaskPage()
        {
            CurrentPage = taskPage;
        }

        public void BookPage()
        {
            CurrentPage = bookPage;
        }

        public void IdeaPage()
        {
            CurrentPage = ideaPage;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
