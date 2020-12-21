using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperationUsingJqueryCodeFirst.Views
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Dob { get; set; }
        public string Image { get; set; }
        public int Contect { get; set; }
        public int StateId { get; set; }
        public string Statename { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

    }
}