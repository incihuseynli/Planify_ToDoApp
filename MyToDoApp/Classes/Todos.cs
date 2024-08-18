using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Classes
{
    public class Todos
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
