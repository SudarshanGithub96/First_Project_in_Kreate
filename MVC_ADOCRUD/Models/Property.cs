using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_ADOCRUD.Models
{
    public class Property
    {

        //Admin Login Properties
        [Key]
        public int Admin_Login_Id { get; set; }

        [Required(ErrorMessage = "Admin UserName is Required")]
        [Display(Name = "Admin UserName")]
        public string Admin_Login_Username { get; set; }

        [Required(ErrorMessage = "Admin Password is Required")]
        [Display(Name = "Admin Password")]
        [DataType(DataType.Password)]
        public string Admin_Password { get; set; }

        public string User{ get; set; }



        //Employee Registration Properties
        [Key]
        public int Employee_ID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }


        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password length should be between 6 to 10 characters")]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password should be combination of UpperCase, LowerCase, Numbers, Symbols")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Repeat Your Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Re-Enter the Password")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Mobile is Required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public long Mobile { get; set; }


        [Required(ErrorMessage = "Select the Country")]
        public int Country_Id { get; set; }


        [Required(ErrorMessage = "Select the State")]
        public int State_Id { get; set; }


        [Required(ErrorMessage = "Select the City")]
        public int City_Id { get; set; }

        //----------------------------------------------
        public string Country_Name { get; set; }
        public string State_Name { get; set; }
        public string City_Name { get; set; }




        //---------------Exam-------------------------------
        [Required(ErrorMessage = "Exam is Required")]
        public int Exam_Id { get; set; }

        [Required(ErrorMessage = "Exam is Required")]
        [Display(Name = "Exam Name")]
        public string Exam_Name { get; set; }



        //--------------------Paper--------------------------
        public int Paper_Id { get; set; }

        [Required(ErrorMessage = "Exam is Required")]
        public string Exam { get; set; }


        [Required(ErrorMessage = "Subject Name is Required")]
        [Display(Name = "Subject Name")]
        public string Subject_Name { get; set; }

        [Required(ErrorMessage = "Minimum Marks is Required")]
        [Display(Name = "Minimum Marks ")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100")]
        public int Min_Marks { get; set; }


        [Required(ErrorMessage = "Maximum Marks is Required")]
        [Display(Name = "Maximum Marks ")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100")]
        public int Max_Marks { get; set; }



        //-----------------------------------------

        public string Section { get; set; }

        //-----------------------------------------

        [Key]
        public int Section_Id { get; set; }


        [Required(ErrorMessage = "Minimum Marks is Required")]
        [Display(Name = "Minimum Marks ")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100")]
        public int Minimum_Marks { get; set; }


        [Required(ErrorMessage = "Maximum Marks is Required")]
        [Display(Name = "Maximum Marks ")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100")]
        public int Maximum_Marks { get; set; }

        [Required(ErrorMessage = "Section is Required")]
        [Display(Name = "Section")]
        public string Section_Name { get; set; }




        //Slot Management Properties

        public int Slot_Id { get; set; }


        [Required(ErrorMessage = "Slot_Paper_Name is Required")]
        public string Slot_Paper_Name { get; set; }


        [Required(ErrorMessage = "Slot_Start_Time is Required")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Slot_Start_time { get; set; }


        [Required(ErrorMessage = "Slot_End_Time is Required")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Slot_End_time { get; set; }


        [Required(ErrorMessage = "Slot_Day is Required")]
        public string Slot_Day { get; set; }



        [Display(Name = "Selected Days")]
        public List<string> selectedDays { get; set; }

        public List<DayOfWeek> Day { get; set; }




        //---------------------------------------------------------------
        //---------------------Exam_notification properties--------------

        public int Exam_Notification_Id { get; set; }

        public string Option { get; set; }


        [Required(ErrorMessage = "Published_Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Published_Date { get; set; }


        [Required(ErrorMessage = "Registration_Start_Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Registration_Start_Date { get; set; }



        [Required(ErrorMessage = "Registration_End_Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Registration_End_Date { get; set; }


        [Required(ErrorMessage = "Examination_Start_Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Examination_Start_Date { get; set; }



        [Required(ErrorMessage = "Examination_End_Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Examination_End_Date { get; set; }





        //---------------------Exam_notification1 properties--------------
        public int Exam_Notification_Id1 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Published_Date1 { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Registration_Start_Date1 { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Registration_End_Date1 { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Examination_Start_Date1 { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Examination_End_Date1 { get; set; }



        //------------------------------------------------------
        //-------------Exam_Description properties--------------

        public int Exam_Description_table_Id { get; set; }
        public string Paper_Name { get; set; }
        public string Description_Exam { get; set; }





        //----------------------------------------------------
        //---------------------Center Properties--------------

        public int Center_Id { get; set; }

        [Required(ErrorMessage = "Center Name is Required")]
        [Display(Name = "Center")]
        public string Center_Name { get; set; }

        [Required(ErrorMessage = "Location Name is Required")]
        [Display(Name = "Location")]
        public string Location_Name { get; set; }

        [Required(ErrorMessage = "Address of Center is Required")]
        [Display(Name = "Address")]
        public string Address_Of_Center_Name { get; set; }

        [Required(ErrorMessage = "Capacity is Required")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
        public int Capacity_Availibility { get; set; }

        [Required(ErrorMessage = "MAC is Required")]
        [Display(Name = "MAC")]
        public string Server_MAC { get; set; }





        //-----------------------------------------------------
        //------------Examination Group properties-------------

        public int Examination_Group_Id { get; set; }

        [Required(ErrorMessage = "Exam_group is Required")]
        public string Exam_Group { get; set; }




        //-------------------------------------------------
        //-----------------Scheule properties--------------

        public int Schedule_Partial_Id { get; set; }


        public string Exam_Date { get; set; }
        public string Slot_1_Data { get; set; }
        public string Slot_2_Data { get; set; }




        public int Schedule_Id { get; set; }
        public string ExamDate { get; set; }
        public string Slot1 { get; set; }
        public string Slot2 { get; set; }
        public string Center { get; set; }
        public string Capacityy { get; set; }
        public int Capacity_Availibilityy { get; set; }



    }







}

















