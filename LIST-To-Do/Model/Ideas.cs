using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.Model
{
    public class Ideas : INotifyPropertyChanged
    {
        private int idIdea;
        public int Id
        {
            get { return idIdea; }
            set { idIdea = value; OnPropertyChanged("Id"); }
        }


        private string idea;
        public string Idea
        {
            get { return idea; }
            set { idea = value; OnPropertyChanged("Idea"); }
        }

        private DateTime dateIdea;
        public DateTime DateIdea
        {
            get { return dateIdea; }
            set { dateIdea = value; OnPropertyChanged("DateIdea"); }
        }
        
        public Ideas()
        {
            DateIdea = DateTime.Now;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
