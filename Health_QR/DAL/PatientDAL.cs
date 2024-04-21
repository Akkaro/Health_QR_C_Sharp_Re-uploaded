using Health_QR.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Numerics;


namespace Health_QR.DAL
{
    public class PatientDAL
    {

        private string _connectionString = "Server=DESKTOP-84OLRP2;Database=health_QR;User Id=sa;Password=Witcher2003";

        public List<Patient> ListPatients(string searchParameter = "")
        {
            string commandText = "SELECT *  FROM [health_QR].[dbo].[Patient]";

            commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Patient> patients = new List<Patient>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Patient patient = new Patient()
                    {
                        First_Name = row["First_Name"].ToString(),
                        Last_Name = row["Last_Name"].ToString(),
                        Current_And_Past_Medications = row["Current_And_Past_Medications"].ToString(),
                        Medication_Food_And_Other_Allergies = row["Medication_Food_And_Other_Allergies"].ToString(),
                        Past_Medical_History = row["Past_Medical_History"].ToString(),
                        Past_Surgical_History = row["Past_Surgical_History"].ToString(),
                        Family_History = row["Family_History"].ToString(),
                        Social_History = row["Social_History"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),
                        QR_File = row["QR_File"].ToString(),
                        AddedBy = row["AddedBy"].ToString(),


                    };
                    patients.Add(patient);
                }
            }
            return patients;
        }
        public List<Patient> Search(string searchParameter)
        {
            string commandText = $"SELECT *  FROM [health_QR].[dbo].[Patient] WHERE AddedBy LIKE '%{searchParameter}%'";


            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Patient> patients = new List<Patient>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Patient patient = new Patient()
                    {
                        First_Name = row["First_Name"].ToString(),
                        Last_Name = row["Last_Name"].ToString(),
                        Current_And_Past_Medications = row["Current_And_Past_Medications"].ToString(),
                        Medication_Food_And_Other_Allergies = row["Medication_Food_And_Other_Allergies"].ToString(),
                        Past_Medical_History = row["Past_Medical_History"].ToString(),
                        Past_Surgical_History = row["Past_Surgical_History"].ToString(),
                        Family_History = row["Family_History"].ToString(),
                        Social_History = row["Social_History"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),
                        QR_File = row["QR_File"].ToString(),
                        AddedBy = row["AddedBy"].ToString(),


                    };
                    patients.Add(patient);
                }
            }
            return patients;
        }
        public Patient GetPatient(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Patient] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Patient> patients = new List<Patient>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Patient patient = new Patient()
                    {
                        First_Name = row["First_Name"].ToString(),
                        Last_Name = row["Last_Name"].ToString(),
                        Current_And_Past_Medications = row["Current_And_Past_Medications"].ToString(),
                        Medication_Food_And_Other_Allergies = row["Medication_Food_And_Other_Allergies"].ToString(),
                        Past_Medical_History = row["Past_Medical_History"].ToString(),
                        Past_Surgical_History = row["Past_Surgical_History"].ToString(),
                        Family_History = row["Family_History"].ToString(),
                        Social_History = row["Social_History"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),


                    };
                    patients.Add(patient);
                }
            }
            var ret = patients.First();
            ret.Personal_Information = GetPatient_Personal_Information(ret.UniquePatientId);
            ret.Personal_Information.Contact_Information = GetPatient_Contact_Information(ret.UniquePatientId);
            ret.Active_Problems_Diagnoses = GetPatient_Active_Problems_Diagnoses(ret.UniquePatientId);
            ret.Active_Problems_Diagnoses.Physical_Examination = GetPatient_Physical_Examination(ret.UniquePatientId);
            ret.Active_Problems_Diagnoses.Vital_Signs = GetPatient_Vital_Signs(ret.UniquePatientId);
            return ret;
        }
        public bool Delete(Patient patient)
        {

            return DeleteById(patient.UniquePatientId);
        }
        public bool DeleteById(int UniquePatientId)
        {
            string commandText =
                               $" DELETE FROM [dbo].[Active_Problems_Diagnoses] WHERE UniquePatientId = {UniquePatientId}"
                              + $" DELETE FROM [dbo].[Contact_Information] WHERE UniquePatientId = {UniquePatientId}"
                              + $" DELETE FROM [dbo].[Patient] WHERE UniquePatientId = {UniquePatientId}"
                              + $" DELETE FROM [dbo].[Personal_Information] WHERE UniquePatientId = {UniquePatientId}"
                              + $" DELETE FROM [dbo].[Physical_Examination] WHERE UniquePatientId = {UniquePatientId}"
                              + $" DELETE FROM [dbo].[Vital_Signs] WHERE UniquePatientId = {UniquePatientId}";
            int result = runQuery(commandText);

            return result == 1
                ? true : false;
        }

        public Personal_Information GetPatient_Personal_Information(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Personal_Information] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Personal_Information> personal_Informations = new List<Personal_Information>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Personal_Information personal_Information = new Personal_Information()
                    {
                        CNP = row["CNP"].ToString(),
                        Seria_Nr = row["Seria_Nr"].ToString(),
                        Date_Of_Birth = DateTime.Parse(row["Date_Of_Birth"].ToString()),
                        Place_Of_Birth = row["Place_Of_Birth"].ToString(),
                        Gender = row["Gender"].ToString(),
                        Language = row["Language"].ToString(),
                        Occupation = row["Occupation"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),


                    };
                    personal_Informations.Add(personal_Information);
                }
            }

                return personal_Informations.First();

        }

        public Contact_Information GetPatient_Contact_Information(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Contact_Information] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Contact_Information> contact_Informations = new List<Contact_Information>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Contact_Information contact_Information = new Contact_Information()
                    {
                        Telephone_Number = row["Telephone_Number"].ToString(),
                        Relative_Name = row["Relative_Name"].ToString(),
                        Relative_Phone_Number = row["Relative_Phone_Number"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),

                    };
                    contact_Informations.Add(contact_Information);
                }
            }
            return contact_Informations.First();
        }

        public Active_Problems_Diagnoses GetPatient_Active_Problems_Diagnoses(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Active_Problems_Diagnoses] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Active_Problems_Diagnoses> active_Problems_Diagnoses = new List<Active_Problems_Diagnoses>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Active_Problems_Diagnoses active_Problems_DiagnosesCurrent = new Active_Problems_Diagnoses()
                    {
                        Chief_Complaint = row["Chief_Complaint"].ToString(),
                        History_Of_Present_Illness = row["History_Of_Present_Illness"].ToString(),
                        Review_Of_Systems = row["Review_Of_Systems"].ToString(),
                        Results = row["Results"].ToString(),
                        Orders = row["Orders"].ToString(),
                        Assessment_And_Plan = row["Assessment_And_Plan"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),


                    };
                    active_Problems_Diagnoses.Add(active_Problems_DiagnosesCurrent);
                }
            }
            return active_Problems_Diagnoses.First();
        }

        public Physical_Examination GetPatient_Physical_Examination(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Physical_Examination] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Physical_Examination> physical_Examinations = new List<Physical_Examination>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Physical_Examination physical_Examination = new Physical_Examination()
                    {
                        Height = row["Height"].ToString(),
                        Weight = row["Weight"].ToString(),
                        Pulse_Rate = row["Pulse_Rate"].ToString(),
                        Blood_Type = row["Blood_Type"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),

                    };
                    physical_Examinations.Add(physical_Examination);
                }
            }
            return physical_Examinations.First();
        }

        public Vital_Signs GetPatient_Vital_Signs(int searchParameter)
        {
            string commandText = $"SELECT * FROM [dbo].[Vital_Signs] WHERE UniquePatientId LIKE '%{searchParameter}%'";

            //commandText += searchParameter;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<Vital_Signs> vital_Signs = new List<Vital_Signs>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    Vital_Signs vital_SignsCurrent = new Vital_Signs()
                    {
                        Blood_Pressure = row["Blood_Pressure"].ToString(),
                        Heart_Rate = row["Heart_Rate"].ToString(),
                        Respiratory_Rate = row["Respiratory_Rate"].ToString(),
                        UniquePatientId = int.Parse(row["UniquePatientId"].ToString()),
                    };
                    vital_Signs.Add(vital_SignsCurrent);
                }
            }
            return vital_Signs.First();
        }

        public bool AddPatient(Patient patient)
        {
            string commandText =
                $"INSERT INTO [dbo].[Patient] ([First_Name],[Last_Name],[Current_And_Past_Medications],[Medication_Food_And_Other_Allergies],[Past_Medical_History],[Past_Surgical_History],[Family_History],[Social_History],[UniquePatientId],[QR_File], [AddedBy])" +
                $"VALUES (@First_Name, @Last_Name, @Current_And_Past_Medications, @Medication_Food_And_Other_Allergies, @Past_Medical_History, @Past_Surgical_History , @Family_History, @Social_History, @UniquePatientId, @QR_File, @AddedBy)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("First_Name",patient.First_Name),
                new SqlParameter("Last_Name",patient.Last_Name),
                new SqlParameter("Current_And_Past_Medications",patient.Current_And_Past_Medications ?? string.Empty),
                new SqlParameter("Medication_Food_And_Other_Allergies",patient.Medication_Food_And_Other_Allergies ?? string.Empty),
                new SqlParameter("Past_Medical_History",patient.Past_Medical_History ?? string.Empty),
                new SqlParameter("Past_Surgical_History",patient.Past_Surgical_History ?? string.Empty),
                new SqlParameter("Family_History",patient.Family_History ?? string.Empty ),
                new SqlParameter("Social_History",patient.Social_History ?? string.Empty),
                new SqlParameter("UniquePatientId",patient.UniquePatientId),
                new SqlParameter("QR_File",patient.QR_File),
                new SqlParameter("AddedBy",patient.AddedBy)

            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }

        public bool AddPatient_Personal_Information(Personal_Information personal_information)
        {
            string commandText =
                $"INSERT INTO [dbo].[Personal_Information] ([CNP],[Seria_Nr],[Date_Of_Birth],[Place_Of_Birth],[Gender],[Language],[Occupation],[UniquePatientId])" +
                $"VALUES (@CNP, @Seria_Nr, @Date_Of_Birth, @Place_Of_Birth, @Gender, @Language , @Occupation, @UniquePatientId)";
            var date1 = new DateTime(2000, 1, 1, 1, 1, 1);
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("CNP",personal_information.CNP ?? string.Empty),
                new SqlParameter("Seria_Nr",personal_information.Seria_Nr ?? string.Empty),
                new SqlParameter("Date_Of_Birth",personal_information.Date_Of_Birth ?? date1),
                new SqlParameter("Place_Of_Birth",personal_information.Place_Of_Birth ?? string.Empty),
                new SqlParameter("Gender",personal_information.Gender  ?? string.Empty),
                new SqlParameter("Language",personal_information.Language ?? string.Empty),
                new SqlParameter("Occupation",personal_information.Occupation ?? string.Empty),
                new SqlParameter("UniquePatientId",personal_information.UniquePatientId),

            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }
        public bool AddPatient_Contact_Information(Contact_Information contact_information)
        {
            string commandText =
                $"INSERT INTO [dbo].[Contact_Information] ([Telephone_Number],[Relative_Name],[Relative_Phone_Number],[UniquePatientId])" +
                $"VALUES (@Telephone_Number, @Relative_Name, @Relative_Phone_Number, @UniquePatientId)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("Telephone_Number",contact_information.Telephone_Number ?? string.Empty),
                new SqlParameter("Relative_Name",contact_information.Relative_Name ?? string.Empty),
                new SqlParameter("Relative_Phone_Number",contact_information.Relative_Phone_Number ?? string.Empty),
                new SqlParameter("UniquePatientId",contact_information.UniquePatientId ),


            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }
        public bool AddPatient_Active_Problems_Diagnoses(Active_Problems_Diagnoses active_problems_diagnoses)
        {
            string commandText =
                $"INSERT INTO [dbo].[Active_Problems_Diagnoses] ([Chief_Complaint],[History_Of_Present_Illness],[Review_Of_Systems],[Results],[Orders],[Assessment_And_Plan],[UniquePatientId])" +
                $"VALUES (@Chief_Complaint, @History_Of_Present_Illness, @Review_Of_Systems, @Results, @Orders, @Assessment_And_Plan , @UniquePatientId)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("Chief_Complaint",active_problems_diagnoses.Chief_Complaint ?? string.Empty),
                new SqlParameter("History_Of_Present_Illness",active_problems_diagnoses.History_Of_Present_Illness ?? string.Empty),
                new SqlParameter("Review_Of_Systems",active_problems_diagnoses.Review_Of_Systems ?? string.Empty),
                new SqlParameter("Results",active_problems_diagnoses.Results ?? string.Empty),
                new SqlParameter("Orders",active_problems_diagnoses.Orders ?? string.Empty),
                new SqlParameter("Assessment_And_Plan",active_problems_diagnoses.Assessment_And_Plan ?? string.Empty),
                new SqlParameter("UniquePatientId",active_problems_diagnoses.UniquePatientId),


            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }
        public bool AddPatient_Physical_Examination(Physical_Examination physical_examination)
        {
            string commandText =
                $"INSERT INTO [dbo].[Physical_Examination] ([Height],[Weight],[Pulse_Rate],[Blood_Type],[UniquePatientId])" +
                $"VALUES (@Height, @Weight, @Pulse_Rate, @Blood_Type, @UniquePatientId)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("Height",physical_examination.Height ?? string.Empty),
                new SqlParameter("Weight",physical_examination.Weight ?? string.Empty),
                new SqlParameter("Pulse_Rate",physical_examination.Pulse_Rate ?? string.Empty),
                new SqlParameter("Blood_Type",physical_examination.Blood_Type ?? string.Empty),
                new SqlParameter("UniquePatientId",physical_examination.UniquePatientId),

            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }
        public bool AddPatient_Vital_Signs(Vital_Signs vital_signs)
        {
            string commandText =
                $"INSERT INTO [dbo].[Vital_Signs] ([Blood_Pressure],[Heart_Rate],[Respiratory_Rate],[UniquePatientId])" +
                $"VALUES (@Blood_Pressure, @Heart_Rate, @Respiratory_Rate, @UniquePatientId)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("Blood_Pressure",vital_signs.Blood_Pressure ?? string.Empty),
                new SqlParameter("Heart_Rate",vital_signs.Heart_Rate ?? string.Empty),
                new SqlParameter("Respiratory_Rate",vital_signs.Respiratory_Rate ?? string.Empty),
                new SqlParameter("UniquePatientId",vital_signs.UniquePatientId),

            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }

        public bool UpdatePatient(Patient patient)
        {
            string commandText =
                   $" UPDATE[dbo].[patient] " +
                   $" SET [First_Name] = @First_Name " +
                   $"     ,[Last_Name] = @Last_Name " +
                   $"     ,[Current_And_Past_Medications] = @Current_And_Past_Medications " +
                   $"     ,[Medication_Food_And_Other_Allergies] = @Medication_Food_And_Other_Allergies  " +
                   $"     ,[Past_Medical_History] = @Past_Medical_History " +
                   $"     ,[Past_Surgical_History] = @Past_Surgical_History " +
                   $"     ,[Family_History] = @Family_History  " +
                   $"     ,[Social_History] = @Social_History " +
                   $" WHERE UniquePatientId = @UniquePatientId ";
          

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("First_Name",patient.First_Name),
                new SqlParameter("Last_Name",patient.Last_Name),
                new SqlParameter("Current_And_Past_Medications",patient.Current_And_Past_Medications ?? string.Empty),
                new SqlParameter("Medication_Food_And_Other_Allergies",patient.Medication_Food_And_Other_Allergies ?? string.Empty),
                new SqlParameter("Past_Medical_History",patient.Past_Medical_History ?? string.Empty),
                new SqlParameter("Past_Surgical_History",patient.Past_Surgical_History ?? string.Empty),
                new SqlParameter("Family_History",patient.Family_History ?? string.Empty),
                new SqlParameter("Social_History",patient.Social_History ?? string.Empty),
                new SqlParameter("UniquePatientId",patient.UniquePatientId)
            };

            int result = runQuery(commandText, parameters);

            return result == 1
                ? true : false;
        }

        private int runQuery(string commandText)
        {
            return runQuery(commandText, new List<SqlParameter>());
        }
        public bool DeletePlaceById(int id)
        {
            string commandText =
                               $" DELETE FROM [dbo].[place] WHERE ID = {id}";

            int result = runQuery(commandText);

            return result == 1
                ? true : false;
        }
        private int runQuery(string commandText, List<SqlParameter> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            parameters.ForEach(parameter => command.Parameters.Add(parameter));

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return result;

        }


        //  public List<Patient> Search(string searchParameter)
        //  {
        //      string commandText = $"SELECT [Name], [ID], [Type], [Description], [ImgPath], [ImgTitle] FROM [dbo].[place] WHERE Name LIKE '%{searchParameter}%'";
        //      SqlConnection connection = new SqlConnection(_connectionString);
        //      SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
        //
        //      List<Patient> patients = new List<Patient>();
        //
        //      using (var adapter = new SqlDataAdapter(command))
        //      {
        //          var resultTable = new DataTable();
        //          adapter.Fill(resultTable);
        //
        //          foreach (var row in resultTable.AsEnumerable())
        //          {
        //              Patient patient = new Patient()
        //              {
        //                  ID = int.Parse(row["ID"].ToString()),
        //                  Name = row["Name"].ToString(),
        //                  Description = row["Description"].ToString(),
        //                  Type = row["Type"].ToString(),
        //                  ImgPath = row["ImgPath"].ToString(),
        //                  ImgTitle = row["ImgTitle"].ToString(),
        //
        //              };
        //              patients.Add(patient);
        //          }
        //      }
        //      return patients;
        //  }
        //
        //  public bool UpdatePlace(Patient patient)
        //  {
        //      string commandText =
        //             $" UPDATE[dbo].[place] " +
        //             $" SET [Name] = @Name " +
        //             $"     ,[Type] = @Type " +
        //             $"     ,[Description] = @Description " +
        //             $"     ,[ImgTitle] = @ImgTitle  " +
        //             $"     ,[ImgPath] = @ImgPath " +
        //             $" WHERE ID = @ID ";
        //
        //      List<SqlParameter> parameters = new List<SqlParameter>()
        //      {
        //          new SqlParameter("Name",patient.Name),
        //          new SqlParameter("Type",patient.Type),
        //          new SqlParameter("Description",patient.Description),
        //          new SqlParameter("ImgPath",patient.ImgPath),
        //          new SqlParameter("ImgTitle",patient.ImgTitle),
        //          new SqlParameter("ID",patient.ID)
        //      };
        //
        //      int result = runQuery(commandText, parameters);
        //
        //      return result == 1
        //          ? true : false;
        //  }
    }

}


