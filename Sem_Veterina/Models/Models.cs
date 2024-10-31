namespace Sem_Veterina.Models
{
    
        public class Clinic
        {
            public int ClinicID { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
        }

        public class Staff
        {
            public int StaffID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Specialization { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
        }

        public class Owner
        {
            public int OwnerID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        public class Pet
        {
            public int PetID { get; set; }
            public string Name { get; set; }
            public string Species { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public decimal Weight { get; set; }
            public string HealthStatus { get; set; }
            public int OwnerID { get; set; }
        }

        public class Procedure
        {
            public int ProcedureID { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public int StaffID { get; set; }
            public int DeviceID { get; set; }
            public string Result { get; set; }
        }

        public class Diagnosis
        {
            public int DiagnosisID { get; set; }
            public string Description { get; set; }
            public string RecommendedTreatment { get; set; }
            public int StaffID { get; set; }
        }

        public class Device
        {
            public int DeviceID { get; set; }
            public string Name { get; set; }
            public string Function { get; set; }
        }

        public class Treatment
        {
            public int TreatmentID { get; set; }
            public string Description { get; set; }
            public int MedicationID { get; set; }
        }

        public class Medication
        {
            public int MedicationID { get; set; }
            public string Name { get; set; }
            public string Dosage { get; set; }
            public string Instructions { get; set; }
            public string PossibleSideEffects { get; set; }
        }
    
}
