using System;

namespace Core.Entities
{
    public class Record
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime CreationDate { get; set; }
        public string StareaGenerala { get; set; }
        public int Talie { get; set; }
        public int Greutate { get; set; }
        public string Nutritie { get; set; }
        public string Constienta { get; set; }
        public string Facies { get; set; }
        public string Tegumente { get; set; }
        public string Mucoase { get; set; }
        public string Fanere { get; set; }
        public string TesutConjunctiv { get; set; }
        public string SistemGanglionar { get; set; }
        public string SistemMuscular { get; set; }
        public string SistemOsteoArticular { get; set; }
        public string AparatRespirator { get; set; }
        public string AparatCardiovascular { get; set; }
        public string AparatDigestiv { get; set; }
        public string FicatSplina { get; set; }
        public string AparatUroGeneral { get; set; }
        public string SistemEndocrin { get; set; }
        public string MotiveleInternarii { get; set; }
        public string Anamneza { get; set; }
        public string IstoriculBolii { get; set; }
        public string Diagnosis { get; set; }
    }
}
