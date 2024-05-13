using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;


namespace MVC_ADOCRUD.Models
{
    public class Logic
    {
        private SqlConnection con;
        public void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            con = new SqlConnection(constr);
        }



        //-----------------Admin Login Logic----------------
        public List<Property> Get_Data_Admin_Login()
        {
            Connection();
            List<Property> Admin_Data_List = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Admin_Login;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Admin_Data_List.Add(
                    new Property
                    {
                        Admin_Login_Id = Convert.ToInt32(dr["Admin_Login_Id"]),
                        Admin_Login_Username = dr["Admin_Login_Username"].ToString(),
                        Admin_Password = dr["Admin_Password"].ToString()
                    }
                    );
            }
            return Admin_Data_List;
        }

        //---------------Employee Properties----------------
        public bool Add_Employee(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Insert_ADOCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@First_Name", property.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", property.Last_Name);
            cmd.Parameters.AddWithValue("@Email", property.Email);
            cmd.Parameters.AddWithValue("@Username", property.Username);
            cmd.Parameters.AddWithValue("@Password", property.Password);
            cmd.Parameters.AddWithValue("@ConfirmPassword", property.ConfirmPassword);
            cmd.Parameters.AddWithValue("@Mobile", Convert.ToInt64(property.Mobile));
            cmd.Parameters.AddWithValue("@Country_Id", Convert.ToInt32(property.Country_Id));
            cmd.Parameters.AddWithValue("@State_Id", property.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", property.City_Id);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update_Employee(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Update_ADOCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employee_ID", Convert.ToInt32(property.Employee_ID));
            cmd.Parameters.AddWithValue("@First_Name", property.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", property.Last_Name);
            cmd.Parameters.AddWithValue("@Email", property.Email);
            cmd.Parameters.AddWithValue("@Username", property.Username);
            cmd.Parameters.AddWithValue("@Password", property.Password);
            cmd.Parameters.AddWithValue("@ConfirmPassword", property.ConfirmPassword);
            cmd.Parameters.AddWithValue("@Mobile", Convert.ToInt64(property.Mobile));
            cmd.Parameters.AddWithValue("@Country_Id", property.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", property.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", property.City_Id);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete_Employee(int Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_ADOCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employee_ID", Convert.ToInt32(Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> GetAllUser()
        {
            Connection();
            List<Property> EmpList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT * from Insert_ADOCRUD;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new Property
                    {
                        Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                        First_Name = dr["First_Name"].ToString(),
                        Last_Name = dr["Last_Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        ConfirmPassword = dr["ConfirmPassword"].ToString(),
                        Mobile = Convert.ToInt64(dr["Mobile"]),
                        Country_Id = Convert.ToInt32(dr["Country_Id"]),
                        State_Id = Convert.ToInt32(dr["State_Id"]),
                        City_Id = Convert.ToInt32(dr["City_Id"]),
                    }
                    );
            }
            return EmpList;
        }
        public List<Property> GetAllUser_For_View()
        {
            Connection();
            List<Property> EmpList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Employee_Id, First_Name, Last_Name,Email,Username,Password,ConfirmPassword,Mobile,Country_Name,State_Name,City_Name FROM Insert_ADOCRUD INNER JOIN Country ON Insert_ADOCRUD.Country_Id = Country.Country_Id INNER JOIN State ON Insert_ADOCRUD.State_Id = State.State_Id INNER JOIN City ON Insert_ADOCRUD.City_Id = City.City_Id order by Employee_Id desc;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new Property
                    {
                        Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                        First_Name = dr["First_Name"].ToString(),
                        Last_Name = dr["Last_Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        ConfirmPassword = dr["ConfirmPassword"].ToString(),
                        Mobile = Convert.ToInt64(dr["Mobile"]),
                        Country_Name = dr["Country_Name"].ToString(),
                        State_Name = dr["State_Name"].ToString(),
                        City_Name = dr["City_Name"].ToString()
                    }
                    );
            }
            return EmpList;
        }


        public List<Property> GetCountryList()
        {
            Connection();
            List<Property> CountryList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Country", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountryList.Add(
                    new Property
                    {
                        Country_Id = Convert.ToInt32(dr["Country_Id"]),
                        Country_Name = dr["Country_Name"].ToString(),
                    }
                    );
            }
            return CountryList;
        }
        public List<Property> GetStateList()
        {
            Connection();
            List<Property> StateList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select *from State", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StateList.Add(
                    new Property
                    {
                        State_Id = Convert.ToInt32(dr["State_Id"]),
                        Country_Id = Convert.ToInt32(dr["Country_Id"]),
                        State_Name = dr["State_Name"].ToString(),
                    }
                    );
            }
            return StateList;
        }
        public List<Property> GetCityList()
        {
            Connection();
            List<Property> CityList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from City", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CityList.Add(
                    new Property
                    {
                        City_Id = Convert.ToInt32(dr["City_Id"]),
                        State_Id = Convert.ToInt32(dr["State_Id"]),
                        City_Name = dr["City_Name"].ToString(),
                    }
                    );
            }
            return CityList;
        }

        
        //--------------------Exam-------------------------------
        public bool Add_Exam(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Exam_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Name", property.Exam_Name);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete_Exam(int Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Exam_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update_Exam(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Exam_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(property.Exam_Id));
            cmd.Parameters.AddWithValue("@Exam_Name", property.Exam_Name);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> GetExamList()
        {
            Connection();
            List<Property> ExamList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Exam ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ExamList.Add(
                    new Property
                    {
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                    }
                    );
            }
            return ExamList;
        }


        //-----------------------Paper-------------------------------------

        public bool Add_Paper(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Insert_Paper", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(property.Exam));
            cmd.Parameters.AddWithValue("@Subject_Name", property.Subject_Name);
            cmd.Parameters.AddWithValue("@Min_Marks", Convert.ToInt32(property.Min_Marks));
            cmd.Parameters.AddWithValue("@Max_Marks", Convert.ToInt32(property.Max_Marks));

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update_Paper(Property prop)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Update_Paper", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Paper_Id", Convert.ToInt32(prop.Paper_Id));
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(prop.Exam_Id));
            cmd.Parameters.AddWithValue("@Subject_Name", prop.Subject_Name);
            cmd.Parameters.AddWithValue("@Min_Marks", Convert.ToInt32(prop.Min_Marks));
            cmd.Parameters.AddWithValue("@Max_Marks", Convert.ToInt32(prop.Max_Marks));

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete_Paper(int paper_Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Paper", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Paper_Id", Convert.ToInt32(paper_Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> GetPaperList()
        {
            Connection();
            List<Property> PaperList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT * from Paper;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PaperList.Add(
                    new Property
                    {
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Min_Marks = Convert.ToInt32(dr["Min_Marks"]),
                        Max_Marks = Convert.ToInt32(dr["Max_Marks"]),
                    }
                    );
            }
            return PaperList;
        }
        public List<Property> GetPaperList_For_View()
        {
            Connection();
            List<Property> PaperList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Paper_id, Exam_Name, Subject_Name,Min_Marks,Max_Marks  FROM Paper INNER JOIN Exam ON Paper.Exam_Id = Exam.Exam_Id order by Paper_id desc;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PaperList.Add(
                    new Property
                    {
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Min_Marks = Convert.ToInt32(dr["Min_Marks"]),
                        Max_Marks = Convert.ToInt32(dr["Max_Marks"]),
                    }
                    );
            }
            return PaperList;
        }


        //for slot
        public List<Property> GetPaperList1()
        {
            Connection();
            List<Property> PaperList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from PAPER", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PaperList.Add(
                    new Property
                    {
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Min_Marks = Convert.ToInt32(dr["Min_Marks"]),
                        Max_Marks = Convert.ToInt32(dr["Max_Marks"]),
                    }
                    );
            }
            return PaperList;
        }



     
        //--------------------------------------------------------------------
        //------------------------------Section-------------------------------
        public bool Update_Section(Property prop)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Update_Section", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Section_Id", Convert.ToInt32(prop.Section_Id));
            cmd.Parameters.AddWithValue("@Subject_Name", prop.Subject_Name);
            cmd.Parameters.AddWithValue("@Minimum_Marks", Convert.ToInt32(prop.Minimum_Marks));
            cmd.Parameters.AddWithValue("@Maximum_Marks", Convert.ToInt32(prop.Maximum_Marks));
            cmd.Parameters.AddWithValue("@Section_Name", prop.Section_Name);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete_Section(int sectionId)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Section", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Section_Id", Convert.ToInt32(sectionId));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> GetSectionList()
        {
            Connection();
            List<Property> SectionList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Section order  by Section_Id desc", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SectionList.Add(
                    new Property
                    {
                        Section_Id = Convert.ToInt32(dr["Section_Id"]),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Minimum_Marks = Convert.ToInt32(dr["Minimum_Marks"]),
                        Maximum_Marks = Convert.ToInt32(dr["Maximum_Marks"]),
                        Section_Name = dr["Section_Name"].ToString(),
                    }
                    );
            }
            return SectionList;
        }
        public List<Property> GetListSubject()
        {
            Connection();
            List<Property> SubjectList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Paper", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SubjectList.Add(
                    new Property
                    {
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Subject_Name = dr["Subject_Name"].ToString()
                    }
                    );
            }
            return SubjectList;
        }



      
        //---------------------------------------------------------------------
        //-----------------------------Slot------------------------------------
        public bool Add_Slot(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Slot_Management_1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(property.Exam_Id));
            cmd.Parameters.AddWithValue("@Paper_Id", Convert.ToInt32(property.Paper_Id));
            cmd.Parameters.AddWithValue("@Slot_Start_time", Convert.ToDateTime(property.Slot_Start_time));
            cmd.Parameters.AddWithValue("@Slot_End_time", Convert.ToDateTime(property.Slot_End_time));
            cmd.Parameters.AddWithValue("@Slot_Day", property.Slot_Day);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> GetSlotListLogic()
        {
            Connection();
            List<Property> SlotList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Slot_Id, Exam_Name, Subject_Name,Slot_Start_Time,Slot_End_Time,Slot_Day FROM Slot_Management_1 INNER JOIN Exam ON Slot_Management_1.Exam_Id = Exam.Exam_Id  INNER JOIN Paper ON Slot_Management_1.Paper_Id = Paper.Paper_Id order by Slot_Id desc ;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SlotList.Add(
                    new Property
                    {
                        Slot_Id = Convert.ToInt32(dr["Slot_Id"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Slot_Start_time = DateTime.Parse(dr["Slot_Start_Time"].ToString()),
                        Slot_End_time = DateTime.Parse(dr["Slot_End_Time"].ToString()),
                        Slot_Day = dr["Slot_Day"].ToString(),
                    }
                    );
            }
            return SlotList;
        }
        public bool Delete_Slot_Logic(int slot_Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Slot", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Slot_Id", Convert.ToInt32(slot_Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //------------------------------------------------------------------
        //------------------------------------------------------------------
        //-------------------------Exam_Notification_Logic1------------------
        public bool Add_Exam_Notification_Logic1(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Exam_Notification_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(property.Exam_Id));
            cmd.Parameters.AddWithValue("@Published_Date1", property.Published_Date1);
            cmd.Parameters.AddWithValue("@Registration_Start_Date1", property.Registration_Start_Date1);
            cmd.Parameters.AddWithValue("@Registration_End_Date1", property.Registration_End_Date1);
            cmd.Parameters.AddWithValue("@Examination_Start_Date1", property.Examination_Start_Date1);
            cmd.Parameters.AddWithValue("@Examination_End_Date1", property.Examination_End_Date1);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> Get_Exam_Notification_List_Logic()
        {
            Connection();
            List<Property> SlotList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from  Exam_Notification", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SlotList.Add(
                    new Property
                    {
                        Exam_Notification_Id1 = Convert.ToInt32(dr["Exam_Notification_Id1"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Published_Date1 = dr["Published_Date1"].ToString(),
                        Registration_Start_Date1 = dr["Registration_Start_Date1"].ToString(),
                        Registration_End_Date1 = dr["Registration_End_Date1"].ToString(),
                        Examination_Start_Date1 = dr["Examination_Start_Date1"].ToString(),
                        Examination_End_Date1 = dr["Examination_End_Date1"].ToString(),
                    }
                    );
            }
            return SlotList;
        }
        public List<Property> Get_Exam_Notification_List_Logic_For_View_only()
        {
            Connection();
            List<Property> SlotList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Exam_Notification_Id1,Exam_Name,Published_Date1,  Registration_Start_Date1,  Registration_End_Date1,  Examination_Start_Date1,  Examination_End_Date1 FROM Exam_Notification INNER JOIN Exam ON Exam_Notification.Exam_Id = Exam.Exam_Id order by Exam_Notification_Id1 desc", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SlotList.Add(
                    new Property
                    {
                        Exam_Notification_Id1 = Convert.ToInt32(dr["Exam_Notification_Id1"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Published_Date1 = dr["Published_Date1"].ToString(),
                        Registration_Start_Date1 = dr["Registration_Start_Date1"].ToString(),
                        Registration_End_Date1 = dr["Registration_End_Date1"].ToString(),
                        Examination_Start_Date1 = dr["Examination_Start_Date1"].ToString(),
                        Examination_End_Date1 = dr["Examination_End_Date1"].ToString(),
                    }
                    );
            }
            return SlotList;
        }
        public bool Delete_Exam_Notification_Logic(int exam_notification_Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Exam_Notification", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Notification_Id1", Convert.ToInt32(exam_notification_Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //----------------------------------------------------------------
        //----------------------------------------------------------------
        //--------------------------Exam_Description_Logic-----------------

        public bool Add_Exam_Description_Logic(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Exam_Description_table", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(property.Exam_Id));
            cmd.Parameters.AddWithValue("@Paper_Id", Convert.ToInt32(property.Paper_Id));
            cmd.Parameters.AddWithValue("@Description_Exam", property.Description_Exam);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> Get_Exam_Description_List_Logic_For_View()
        {
            Connection();
            List<Property> Get_Exam_Des_List = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Exam_Description_table_Id, Exam_Name, Subject_Name,Description_Exam FROM Exam_Description_table INNER JOIN Exam ON Exam_Description_table.Exam_Id = Exam.Exam_Id INNER JOIN Paper ON Exam_Description_table.Paper_Id = Paper.Paper_Id order by Exam_Description_table_Id desc ;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Get_Exam_Des_List.Add(
                    new Property
                    {
                        Exam_Description_table_Id = Convert.ToInt32(dr["Exam_Description_table_Id"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Description_Exam = dr["Description_Exam"].ToString(),
                    }
                    );
            }
            return Get_Exam_Des_List;
        }
        public bool Delete_Exam_Description_Logic(int exam_des_Id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Exam_Description_table", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Description_table_Id", Convert.ToInt32(exam_des_Id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //-------------------------------------------------------------------
        //-------------------------------------------------------------------
        // -------------------------Center Logic-----------------------------

        public bool Add_Center_Logic(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Insert_Center", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Center_Name", property.Center_Name);
            cmd.Parameters.AddWithValue("@City_Id", Convert.ToInt32(property.City_Id));
            cmd.Parameters.AddWithValue("@Address_Of_Center_Name", property.Address_Of_Center_Name);
            cmd.Parameters.AddWithValue("@Capacity", Convert.ToInt32(property.Capacity));
            cmd.Parameters.AddWithValue("@Server_MAC", property.Server_MAC);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update_Center_Logic(Property prop)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Update_Center", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Center_Id", Convert.ToInt32(prop.Center_Id));
            cmd.Parameters.AddWithValue("@Center_Name", prop.Center_Name);
            cmd.Parameters.AddWithValue("@City_Id", Convert.ToInt32(prop.City_Id));
            cmd.Parameters.AddWithValue("@Address_Of_Center_Name", prop.Address_Of_Center_Name);
            cmd.Parameters.AddWithValue("@Capacity", Convert.ToInt32(prop.Capacity));
            cmd.Parameters.AddWithValue("@Server_MAC", prop.Server_MAC);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete_Center_Logic(int center_id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Center", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Center_Id", Convert.ToInt32(center_id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> Get_Center_List()
        {
            Connection();
            List<Property> CenterList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Center;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CenterList.Add(
                    new Property
                    {
                        Center_Id = Convert.ToInt32(dr["Center_Id"]),
                        Center_Name = dr["Center_Name"].ToString(),
                        City_Id = Convert.ToInt32(dr["City_Id"]),
                        Address_Of_Center_Name = dr["Address_Of_Center_Name"].ToString(),
                        Capacity = Convert.ToInt32(dr["Capacity"]),
                        Server_MAC = dr["Server_MAC"].ToString()
                    }
                    );
            }
            return CenterList;
        }
        public List<Property> Get_Center_List_For_View()
        {
            Connection();
            List<Property> CenterList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Center_Id, Center_Name, City_Name,Address_Of_Center_Name,Capacity,Server_MAC FROM Center INNER JOIN City ON Center.City_Id = City.City_Id order by Center_Id desc;;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CenterList.Add(
                    new Property
                    {
                        Center_Id = Convert.ToInt32(dr["Center_Id"]),
                        Center_Name = dr["Center_Name"].ToString(),
                        City_Name = dr["City_Name"].ToString(),
                        Address_Of_Center_Name = dr["Address_Of_Center_Name"].ToString(),
                        Capacity = Convert.ToInt32(dr["Capacity"]),
                        Server_MAC = dr["Server_MAC"].ToString()
                    }
                    );
            }
            return CenterList;
        }

        //------------------------------------------------------------------
        //------------------------------------------------------------------
        //------------------Examination Group Logic-------------------------

        public bool Add_Examination_Group(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Insert_Examination_Group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exam_Id", Convert.ToInt32(property.Exam_Id));
            cmd.Parameters.AddWithValue("@Paper_Id", Convert.ToInt32(property.Paper_Id));
            cmd.Parameters.AddWithValue("@Exam_Group", property.Exam_Group);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Property> Get_Exam_Group_List_Logic_for_View()
        {
            Connection();
            List<Property> Exam_GroupList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Examination_Group_Id, Exam_Name, Subject_Name,Exam_Group FROM Examination_Group INNER JOIN Exam ON Examination_Group.Exam_Id = Exam.Exam_Id INNER JOIN Paper ON Examination_Group.Paper_Id = Paper.Paper_Id order by Examination_Group_Id desc;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Exam_GroupList.Add(
                    new Property
                    {
                        Examination_Group_Id = Convert.ToInt32(dr["Examination_Group_Id"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Exam_Group = dr["Exam_Group"].ToString()
                    }
                    );
            }
            return Exam_GroupList;
        }
        public List<Property> Get_Exam_Group_List_Logic()
        {
            Connection();
            List<Property> Exam_GroupList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT * from Examination_Group;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Exam_GroupList.Add(
                    new Property
                    {
                        Examination_Group_Id = Convert.ToInt32(dr["Examination_Group_Id"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Exam_Group = dr["Exam_Group"].ToString()
                    }
                    );
            }
            return Exam_GroupList;
        }
        public bool Delete_Exam_Group_Logic(int examgroup_id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Delete_Examination_Group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Examination_Group_Id", Convert.ToInt32(examgroup_id));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //---------------------------------------------------------
        //---------------------------------------------------------
        //------------------Schedule Logic-------------------------


        //after complete whole task
        public List<Property> Get_Exam_Notification_List_Logic_For_Schedule_View_Page()
        {
            Connection();
            List<Property> SlotList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT Exam_Notification_Id1, Exam_Name, Published_Date1, Registration_Start_Date1, Registration_End_Date1,  Examination_Start_Date1,  Examination_End_Date1 FROM Exam_Notification INNER JOIN Exam ON Exam_Notification.Exam_Id = Exam.Exam_Id order by Exam_Notification_Id1 desc", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SlotList.Add(
                    new Property
                    {
                        Exam_Notification_Id1 = Convert.ToInt32(dr["Exam_Notification_Id1"]),
                        Exam_Name = dr["Exam_Name"].ToString(),
                        Published_Date1 = dr["Published_Date1"].ToString(),
                        Registration_Start_Date1 = dr["Registration_Start_Date1"].ToString(),
                        Registration_End_Date1 = dr["Registration_End_Date1"].ToString(),
                        Examination_Start_Date1 = dr["Examination_Start_Date1"].ToString(),
                        Examination_End_Date1 = dr["Examination_End_Date1"].ToString(),
                    }
                    );
            }
            return SlotList;
        }
        public List<Property> Get_Exam_Notification_List_Logic_For_Schedule()
        {
            Connection();
            List<Property> SlotList = new List<Property>();
            SqlCommand cmd = new SqlCommand("select * from Exam_Notification", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SlotList.Add(
                    new Property
                    {
                        Exam_Notification_Id1 = Convert.ToInt32(dr["Exam_Notification_Id1"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Published_Date1 = dr["Published_Date1"].ToString(),
                        Registration_Start_Date1 = dr["Registration_Start_Date1"].ToString(),
                        Registration_End_Date1 = dr["Registration_End_Date1"].ToString(),
                        Examination_Start_Date1 = dr["Examination_Start_Date1"].ToString(),
                        Examination_End_Date1 = dr["Examination_End_Date1"].ToString(),
                    }
                    );
            }
            return SlotList;
        }
        public List<Property> Get_Exam_Group_List_Logic_For_Partial1()
        {
            Connection();
            List<Property> Exam_GroupList = new List<Property>();
            SqlCommand cmd = new SqlCommand("SELECT * from Examination_Group;", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Exam_GroupList.Add(
                    new Property
                    {
                        Examination_Group_Id = Convert.ToInt32(dr["Examination_Group_Id"]),
                        Exam_Id = Convert.ToInt32(dr["Exam_Id"]),
                        Paper_Id = Convert.ToInt32(dr["Paper_Id"]),
                        Exam_Group = dr["Exam_Group"].ToString()
                    }
                    );
            }
            return Exam_GroupList;
        }


        //public bool Add_Schedule_Logic(Property property)
        //{
        //    Connection();
        //    SqlCommand cmd = new SqlCommand("SP_Schedule", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@ExamDate", property.ExamDate);
        //    cmd.Parameters.AddWithValue("@Slot1", property.Slot1);
        //    cmd.Parameters.AddWithValue("@Slot2", property.Slot2);
        //    cmd.Parameters.AddWithValue("@Center", property.Center);
        //    cmd.Parameters.AddWithValue("@Capacityy", property.Capacityy);
        //    cmd.Parameters.AddWithValue("@Capacity_Availibilityy", property.Capacity_Availibilityy);
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    int a = cmd.ExecuteNonQuery();
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    if (a > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool Add_Schedule1_Logic(Property property)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SP_Schedule", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamDate", property.ExamDate);
            cmd.Parameters.AddWithValue("@Slot1", property.Slot1);
            cmd.Parameters.AddWithValue("@Slot2", property.Slot2);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int a = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }












      


    }
}