using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudOperationUsingJqueryCodeFirst.Models
{
    public class State
    {
        [Key]
        public int Stateid { get; set; }
        public string Statename { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
    

}
