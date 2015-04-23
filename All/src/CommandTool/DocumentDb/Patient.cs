using System;
using System.Collections.Generic;

namespace CommandTool.DocumentDb
{
    public class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public IList<Medication> Medications { get; set; }
        public IList<Procedure> Procedures { get; set; }
    }

    public class Medication
    {
        public string Name { get; set; }
        public DateTime Ordered { get; set; }
        public double Dosage { get; set; }
    }

    public class Procedure
    {
        public int Ordinal { get; set; }
        public string Code { get; set; }
        public DateTime Performed { get; set; }
    }
}