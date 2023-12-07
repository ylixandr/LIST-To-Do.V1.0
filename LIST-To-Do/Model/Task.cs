using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.Model
{
    public class Task : INotifyPropertyChanged
    {
        private int id;
        private string description;
        private DateTime date;
        private string priority;

        public string Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged("Priority"); }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        public Task()
        {
           Date = DateTime.Now;
        }
        public string Description { get { return description; } set { description = value;OnPropertyChanged("Descriprion"); } }
        public int Id 
        {
            get { return id; } 
            set { id = value; OnPropertyChanged("Id"); } 
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
