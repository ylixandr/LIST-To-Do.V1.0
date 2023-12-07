using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIST_To_Do.Model
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("DbConnection")
        {

        }

        public DbSet<Model.Task> TasksDb { get; set;}
        public DbSet<Ideas> IdeasDb { get; set; }

        public DbSet <Book> BooksDb { get; set; }

       
    }
}
