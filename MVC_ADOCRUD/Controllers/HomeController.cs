using MVC_ADOCRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace MVC_ADOCRUD.Controllers
{
    public class HomeController : Controller
    {
        Property property = new Property();
        Logic logic = new Logic();

        private SqlConnection con;


        public void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(Property property)
        {
            var logindata = await Task.Run(() => logic.Get_Data_Admin_Login().FirstOrDefault(model => model.Admin_Login_Username == property.Admin_Login_Username && model.Admin_Password == property.Admin_Password));

            if (logindata != null)
            {
                Session["Login_Session"] = property.Admin_Login_Username;
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult Reset()
        {
            ModelState.Clear();
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        //-------------------Insert Employee----------------------

        [HttpGet]
        public ActionResult Insert()
        {
            var value = logic.GetCountryList().ToList();
            ViewBag.CountryList = value.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Property prop)
        {
            var value = logic.GetCountryList().ToList();
            ViewBag.CountryList = value.ToList();
            if (logic.Add_Employee(prop))
            {
                ViewBag.DataInsert = "Data has been submitted Successfully";
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            return View(logic.GetAllUser_For_View().Find(m => m.Employee_ID == Id));
        }
        [HttpPost]
        public ActionResult Update(Property updt)
        {
            if (logic.Update_Employee(updt))
            {
                ViewBag.UpdateData = "Data has been Updated Successfully";
            }
            ModelState.Clear();
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (logic.Delete_Employee(id))
            {
                ViewBag.DeleteData = "Data has been Deleted Successfully";
            }
            return RedirectToAction("GetEmployeeList");
        }

        [HttpGet]
        public ActionResult GetEmployeeList()
        {
            return View(logic.GetAllUser_For_View().ToList());
        }




        //====================================================
        //----------------Get Employee List json--------------
        [HttpGet]
        public ActionResult Get_Employee_List()
        {
            var value = logic.GetCountryList().ToList();
            ViewBag.CountryList = value.ToList();
            return View();
        }

        public JsonResult Get_Employee_Json()
        {
            var Get_Employee_List_Data = logic.GetAllUser_For_View().ToList();
            return Json(Get_Employee_List_Data, JsonRequestBehavior.AllowGet);
        }



        //----------------Insert data json--------------------

        public JsonResult Insert_Employee_Json(Property property)
        {
            var value = logic.GetCountryList().ToList();
            ViewBag.CountryList = value.ToList();
            return Json(logic.Add_Employee(property), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete_Employee_Json(int id)
        {
            var Delete_Employe_Data = logic.Delete_Employee(id);
            return Json(Delete_Employe_Data, JsonRequestBehavior.AllowGet);
        }


        //json result of state and city
        public JsonResult BindState(int country_id)
        {
            var objstatelist = logic.GetStateList().Where(r => r.Country_Id == country_id).ToList();
            return Json(objstatelist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindCity(int state_id)
        {
            var objcitylist = logic.GetCityList().Where(r => r.State_Id == state_id).ToList();
            return Json(objcitylist, JsonRequestBehavior.AllowGet);
        }





        //-------------------------Exam---------------------------------
        [HttpGet]
        public ActionResult Exam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Exam(Property prop)
        {
            Logic logic = new Logic();

            if (logic.Add_Exam(prop))
            {
                ViewBag.DataInsertExam = "Exam Data has been submitted Successfully";

            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult ExamUpdate(int Id)
        {
            return View(logic.GetExamList().Find(m => m.Exam_Id == Id));
        }
        [HttpPost]
        public ActionResult ExamUpdate(Property updtproperty)
        {
            if (logic.Update_Exam(updtproperty))
            {
                ViewBag.ExamUpdateData = "Data has been Updated Successfully";
            }
            ModelState.Clear();
            return View();
            //return RedirectToAction("GetList");
        }

        [HttpGet]
        public ActionResult ExamList()
        {
            return View(logic.GetExamList().ToList());
        }
        public ActionResult ExamDelete(int Id)
        {
            if (logic.Delete_Exam(Id))
            {
                ViewBag.ExamDeleteData = "Data has been Deleted Successfully";
            }

            return RedirectToAction("ExamList");
        }





        //--------------------------Paper------------------------------
        [HttpGet]
        public ActionResult Paper()
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Paper(Property prop)
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            if (logic.Add_Paper(prop))
            {
                ViewBag.PaperInsert = "Paper data has been submitted Successfully";
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult GetPaperList()
        {
            return View(logic.GetPaperList_For_View().ToList());
        }

        public ActionResult PaperDelete(int Id)
        {
            Logic logic = new Logic();

            if (logic.Delete_Paper(Id))
            {
                ViewBag.PaperDeleteData = "Data has been Deleted Successfully";
            }
            return RedirectToAction("GetPaperList");
        }


        [HttpGet]
        public ActionResult PaperUpdate(int Id)
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            ViewBag.SubjectLIST = new SelectList(logic.GetPaperList(), "Subject_Name", "Subject_Name");
            return View(logic.GetPaperList().Find(m => m.Paper_Id == Id));
        }
        [HttpPost]
        public ActionResult PaperUpdate(Property updt)
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            ViewBag.SubjectLIST = new SelectList(logic.GetPaperList(), "Subject_Name", "Subject_Name");
            if (logic.Update_Paper(updt))
            {
                ViewBag.UpdatePaperData = "Paper Data has been Updated Successfully";
            }
            ModelState.Clear();
            return RedirectToAction("GetPaperList");
        }




        //-----------------------------------------------------
        //-----------------------Section-----------------------
        [HttpGet]
        public ActionResult Section(int Id)
        {
            ViewBag.SUBJECTLIST = new SelectList(logic.GetListSubject(), "Subject_Name", "Subject_Name");
            Session["compare"] = property.Maximum_Marks;

            return View(logic.GetPaperList().Find(m => m.Paper_Id == Id));
            //return View();
        }

        [HttpPost]
        public JsonResult Section_Json(List<Property> data)
        {
            try
            {
                foreach (Property item in data)
                {
                    Connection();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Section", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Subject_Name", item.Subject_Name);
                    cmd.Parameters.AddWithValue("@Minimum_Marks", Convert.ToInt32(item.Minimum_Marks));
                    cmd.Parameters.AddWithValue("@Maximum_Marks", Convert.ToInt32(item.Maximum_Marks));
                    cmd.Parameters.AddWithValue("@Section_Name", item.Section_Name);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        Console.WriteLine("Data save");
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }
                }
                return Json(new { success = true, message = "data saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, message = "failed" }, JsonRequestBehavior.AllowGet);
            }
            //    //ViewBag.SUBJECTLIST = new SelectList(logic.GetListSubject(), "Subject_Name", "Subject_Name");
            //    //if (logic.Add_Section(prop))
            //    //{
            //    //    ViewBag.Section_DataInsert = "Section Data has been submitted Successfully";
            //    //}
            //    //ModelState.Clear();
            //    //return View();
            //}
        }

        [HttpGet]
        public ActionResult SectionList()
        {
            return View(logic.GetSectionList().ToList());
        }

        public ActionResult SectionDelete(int Id)
        {
            if (logic.Delete_Section(Id))
            {
                ViewBag.SectionDeleteData = "Section Data has been Deleted Successfully";
            }
            return RedirectToAction("SectionList");
        }

        [HttpGet]
        public ActionResult SectionUpdate(int Id)
        {
            return View(logic.GetSectionList().Find(m => m.Section_Id == Id));
        }
        [HttpPost]
        public ActionResult SectionUpdate(Property updt)
        {
            if (logic.Update_Section(updt))
            {
                ViewBag.UpdateSectionData = "Paper Data has been Updated Successfully";
            }
            ModelState.Clear();
            return View();
            //return RedirectToAction("GetList");
        }




        //-----------------------------------------------------
        //-----------------------Slot--------------------------

        [HttpGet]
        public ActionResult Slot()
        {
            var value1 = logic.GetExamList().ToList();
            ViewBag.EXAMLIST = value1.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Slot(Property prop, string[] selectedDays)
        {
            var value1 = logic.GetExamList().ToList();
            ViewBag.EXAMLIST = value1.ToList();

            var daysData = selectedDays != null ? string.Join(",", selectedDays.Where(Day => !string.IsNullOrEmpty(Day)).Distinct()) : "";
            var daysName = daysData.Replace("false,", "");
            prop.Slot_Day = daysName;
            if (logic.Add_Slot(prop))
            {
                ViewBag.AddSlot = "Slot has been submitted Successfully";
            }
            //ViewBag.EXAMLIST = new SelectList(logic.GetListExam(), "Exam_Id","Exam_Name"); 
            ModelState.Clear();
            return RedirectToAction("GetSlotList");
        }

        public ActionResult SlotDelete(int Id)
        {
            Logic logic = new Logic();

            if (logic.Delete_Slot_Logic(Id))
            {
                ViewBag.SlotDeleteData = "Slot Data has been Deleted Successfully";
            }
            return RedirectToAction("GetSlotList");
        }

        [HttpGet]
        public ActionResult GetSlotList()
        {
            Logic logic = new Logic();
            return View(logic.GetSlotListLogic());
        }
        public JsonResult BindPaper(int exam_id)
        {
            var objpaperlist = logic.GetPaperList1().Where(r => r.Exam_Id == exam_id).ToList();
            return Json(objpaperlist, JsonRequestBehavior.AllowGet);
        }



        //------------------------------------------------------------
        //----------------------Exam Notification1--------------------
        [HttpGet]
        public ActionResult Exam_Notification1()
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            return View();
        }
        [HttpPost]
        public ActionResult Exam_Notification1(Property prop)
        {
            ViewBag.EXAMLIST = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");

            DateTime published_Date1 = DateTime.Parse(prop.Published_Date1);
            DateTime registration_Start_Date1 = DateTime.Parse(prop.Registration_Start_Date1);
            DateTime registration_End_Date1 = DateTime.Parse(prop.Registration_End_Date1);
            DateTime examination_Start_Date1 = DateTime.Parse(prop.Examination_Start_Date1);
            DateTime examination_End_Date1 = DateTime.Parse(prop.Examination_End_Date1);

            if (published_Date1 < registration_Start_Date1 && registration_Start_Date1 < registration_End_Date1 && registration_End_Date1 < examination_Start_Date1 && examination_Start_Date1 < examination_End_Date1)
            {
                if (logic.Add_Exam_Notification_Logic1(prop))
                {
                    ViewBag.ADDExamNotification = "Exam Notification data has been submitted Successfully";
                    ModelState.Clear();
                }
            }
            else
            {
                ViewBag.ADDExamNotification = "data is wrong";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Get_Exam_Notification_List_For_View()
        {
            return View(logic.Get_Exam_Notification_List_Logic());
        }

        public ActionResult Exam_Notification_Delete(int Id)
        {
            if (logic.Delete_Exam_Notification_Logic(Id))
            {
                ViewBag.Exam_Notification_DeleteData = "Exam Notification Data has been Deleted Successfully";
            }
            return RedirectToAction("Get_Exam_Notification_List_For_View");
        }



        //just like update in exam_notification
        [HttpGet]
        public ActionResult Schedule(int id, int Examid, DateTime Examination_Start_Date1, DateTime Examination_End_Date1)
        {
            TempData["Examination_Start_Date1"] = Convert.ToDateTime(Examination_Start_Date1);
            TempData["Examination_End_Date1"] = Convert.ToDateTime(Examination_End_Date1);

            //Partial 2
            var centerdata = logic.Get_Center_List().ToList();
            ViewBag.centerdata = centerdata.ToList();

            var groupdata = logic.Get_Exam_Group_List_Logic_For_Partial1().Where(m => m.Exam_Id == Examid).ToList();
            ViewBag.groupdata = groupdata;


            return View(logic.Get_Exam_Notification_List_Logic().Find(m => m.Exam_Notification_Id1 == id));
        }
        //public JsonResult Get_Center_List_For_Partial_Schedule2()
        //{
        //    var Get_Center_List_Data = logic.Get_Center_List_Logic_For_Partial_Schedule2().ToList();
        //    return Json(Get_Center_List_Data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult Schedule(Property prop)
        {
            if (logic.Add_Schedule1_Logic(prop))
            {
                ViewBag.ScheduleInsert = "Paper data has been submitted Successfully";
            }
            ModelState.Clear();
            return View();
        }



        //--------------------------------------------------------------
        //-------------------------Exam Description---------------------
        [HttpGet]
        public ActionResult Exam_Description(int Id)
        {
            ViewBag.examdata = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            ViewBag.paperdata = new SelectList(logic.GetPaperList1(), "Paper_Id", "Subject_Name");

            return View(logic.GetExamList().Find(m => m.Exam_Id == Id));
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Exam_Description(Property prop)
        {
            ViewBag.examdata = new SelectList(logic.GetExamList(), "Exam_Id", "Exam_Name");
            ViewBag.paperdata = new SelectList(logic.GetPaperList1(), "Paper_Id", "Subject_Name");

            if (logic.Add_Exam_Description_Logic(prop))
            {
                ViewBag.ADDExam_Description = "Exam Description data has been submitted Successfully";
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Get_Exam_Description_List()
        {
            return View(logic.Get_Exam_Description_List_Logic_For_View());
        }

        public ActionResult Exam_Description_Delete(int Id)
        {
            if (logic.Delete_Exam_Description_Logic(Id))
            {
                ViewBag.Exam_Description_DeleteData = "Exam Description Data has been Deleted Successfully";
            }
            return RedirectToAction("Get_Exam_Description_List");
        }





        //------------------------------------------------------------
        //-------------------------Center-----------------------------

        [HttpGet]
        public ActionResult Center()
        {
            ViewBag.CityName = new SelectList(logic.GetCityList(), "City_Id", "City_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Center(Property prop)
        {
            ViewBag.CityName = new SelectList(logic.GetCityList(), "City_Id", "City_Name");
            if (logic.Add_Center_Logic(prop))
            {
                ViewBag.CenterMessage = "Center data has been submitted Successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.CenterMessage = "data not saved";

            }
            return View();
        }

        [HttpGet]
        public ActionResult GetCenterList()
        {
            return View(logic.Get_Center_List_For_View());
        }

        public ActionResult Center_Delete(int Id)
        {
            if (logic.Delete_Center_Logic(Id))
            {
                ViewBag.Center_DeleteData = "Center has been Deleted Successfully";
            }
            return RedirectToAction("GetCenterList");
        }



        [HttpGet]
        public ActionResult Update_Center(int Id)
        {
            ViewBag.CityName = new SelectList(logic.GetCityList(), "City_Id", "City_Name");
            return View(logic.Get_Center_List().Find(m => m.Center_Id == Id));
        }

        [HttpPost]
        public ActionResult Update_Center(Property updtproperty)
        {
            ViewBag.CityName = new SelectList(logic.GetCityList(), "City_Id", "City_Name");
            var updatevar = logic.Update_Center_Logic(updtproperty);
            if (updatevar)
            {
                ViewBag.Update_Center_data = "Data has been Updated Successfully";
            }
            ModelState.Clear();
            return RedirectToAction("GetCenterList");
        }


        //-----------------------------------------------------
        //-----------------------------------------------------
        //-------------------Examination Gruop-----------------

        [HttpGet]
        public ActionResult Examination_Group()
        {
            var value1 = logic.GetExamList().ToList();
            ViewBag.EXAMLIST = value1.ToList();
            return View();
        }

        [HttpPost]
        public JsonResult Examination_Group_Json(Property property, string[] dynamicPaperGroups)
        {
            if (dynamicPaperGroups == null)
            {
                return Json(new { success = false, message = "No PaperGroup values provided" });
            }

            var value1 = logic.GetExamList().ToList();
            ViewBag.EXAMLIST = value1.ToList();

            HashSet<string> uniqueValues = new HashSet<string>(dynamicPaperGroups);

            if (uniqueValues.Count != dynamicPaperGroups.Length)
            {
                return Json(new { success = false, message = "The PaperGroup values must be different for each textbox" });
            }


            // Save data
            for (int i = 0; i < dynamicPaperGroups.Length; i++)
            {
                Property dynamicProp = new Property
                {
                    Exam_Id = property.Exam_Id,
                    Paper_Id = property.Paper_Id,
                    Exam_Group = dynamicPaperGroups[i]
                };
                logic.Add_Examination_Group(dynamicProp);

            }
            ModelState.Clear();
            return Json(new { success = true, message = "Exam Group Data has been inserted" });
        }


        [HttpGet]
        public ActionResult Get_Exam_Geoup_List()
        {
            return View(logic.Get_Exam_Group_List_Logic());
        }

        public ActionResult Exam_Group_Delete(int Id)
        {
            if (logic.Delete_Exam_Group_Logic(Id))
            {
                ViewBag.exam_group_DeleteData = "Exam group data has been Deleted Successfully";
            }
            return RedirectToAction("Get_Exam_Geoup_List");
        }

        //-------------------------------------------------------------------
        //-------------------------------------------------------------------


        [HttpGet]
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentLogin(Property property)
        {
            var Student_logindata = logic.GetAllUser().Where(model => model.Employee_ID == property.Employee_ID && model.Password == property.Password);

            if (Student_logindata != null)
            {
                return RedirectToAction("Registration_Student", "Home");
            }

            else
            {
                return RedirectToAction("StudentLogin", "Home");
            }
            //return View();
        }

        [HttpGet]
        public ActionResult Registration_Student()
        {
            return View(logic.Get_Exam_Notification_List_Logic().ToList());
        }

        [HttpGet]
        public ActionResult Registration()
        {
            var dataa = logic.GetPaperList().ToList();
            if (dataa != null)
            {
                return View(dataa);
            }
            else
            {
                return RedirectToAction("Registration_Student", "Home");
            }
        }
    }
}