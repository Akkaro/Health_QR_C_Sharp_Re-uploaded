using Microsoft.Build.Framework;

namespace Health_QR.Models
{

    public class Contact_Information
    {
        public string? Telephone_Number { get; set; }
        public string? Relative_Name { get; set; }
        public string? Relative_Phone_Number { get; set; }
        public int UniquePatientId { get; set; }

    }

    public class Personal_Information
    {
        public string? CNP { get; set; }
        public string? Seria_Nr { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string? Place_Of_Birth { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Language { get; set; }
        public Contact_Information Contact_Information { get; set; }
        public string? Occupation { get; set; }
        public int UniquePatientId { get; set; }
        

    }
    public class Patient

    {
        public string PatientId { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        public Personal_Information Personal_Information { get; set; }
        public string? Medical_History { get; set; }
        public string? Current_And_Past_Medications { get; set; }
        public string? Medication_Food_And_Other_Allergies { get; set; }
        public Active_Problems_Diagnoses Active_Problems_Diagnoses { get; set; }

        public string? Past_Medical_History { get; set; }
        public string? Past_Surgical_History { get; set; }
        public string? Family_History { get; set; }
        public string? Social_History { get; set; }

        public int UniquePatientId { get; set; }
        public string QR_File { get; set; }
        public string? AddedBy { get; set; }


    }
    public class Vital_Signs
    {
        public string? Blood_Pressure { get; set; }
        public string? Heart_Rate { get; set; }

        public string? Respiratory_Rate { get; set; }

        public int UniquePatientId { get; set; }
    }
    public class Physical_Examination
    {
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Pulse_Rate { get; set; }
        public string? Blood_Type { get; set; }
        public int UniquePatientId { get; set; }
    }
    public class Active_Problems_Diagnoses
    {
        public string? Chief_Complaint { get; set; }
        public string? History_Of_Present_Illness { get; set; }
        public string? Review_Of_Systems { get; set; }
        public Physical_Examination Physical_Examination { get; set; }

        public Vital_Signs Vital_Signs { get; set; }

        public string? Results { get; set; }
        public string? Orders { get; set; }
        public string? Assessment_And_Plan { get; set; }
        public int UniquePatientId { get; set; }

    }
}