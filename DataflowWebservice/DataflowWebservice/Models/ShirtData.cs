using DataflowWebservice.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowWebservice.Models
{
    public class ShirtData
    {
        [PrimaryKey]
        public int timestamp { get; set; }
        public int id { get; set; } // FK
        public int heartRate { get; set; }
        public int temperature { get; set; }

        public ShirtData()
        {

        }

        public ShirtData(int timestamp, int id, int heartRate, int temperature)
        {
            this.timestamp = timestamp;
            this.id = id;
            this.heartRate = heartRate;
            this.temperature = temperature;
        }
    }
}