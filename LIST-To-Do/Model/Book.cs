using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.Model
{
    public class Book : INotifyPropertyChanged
    {
        private int id;
        private string title;
        private int price;
        
        public Book()
        {
            
        }
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }

        }

        public int Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
