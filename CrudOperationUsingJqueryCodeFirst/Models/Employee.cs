using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudOperationUsingJqueryCodeFirst.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Dob { get; set; }
        public string Image { get; set; }
        public int Contect { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }






    }
}


    
    
    
    
    
    
    
     
