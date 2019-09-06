using System;
using System.Collections.Generic;
using System.Text;

namespace Gol.Entities
{
    public class Airplane
    {
        public int ID { get; set; }
        public int AirplaneCode { get; set; }
        public string AirplaneModel { get; set; }
        public int Passengers { get; set; }
        public DateTime RegistryCreationDate { get; set; }
    }
}
