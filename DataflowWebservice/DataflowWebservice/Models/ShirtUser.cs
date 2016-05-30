using DataflowWebservice.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowWebservice.Models
{
    public class ShirtUser
    {
        [PrimaryKey]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public ShirtUser()
        {

        }
        public ShirtUser(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}