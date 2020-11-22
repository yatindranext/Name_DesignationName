using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace DuckTaleITTest.Models
{
    [Table("tbl_student")]
    public class Student
    {
        [Key]
        public int Std_Id { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",ErrorMessage ="Invalid")]
        [Required(ErrorMessage = "FirstName is required")]
        public string Std_FirstName { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "LastName is required")]
        public string Std_LastName { get; set; }


        [Range(1, 12, ErrorMessage = "Marks should Between 0 To 12")]
        [Required(ErrorMessage = "Class is required")]
        public string Std_Class { get; set; }

        public List<Subject> Std_Subjects { get; set; }
    }

    [Table("tbl_subject")]
    public class Subject
    {
        [Key]
        public int Sub_Id { get; set; }

        [ForeignKey("studentPk_subject")]
        public int Std_Id { get; set; }

        public string Sub_Name { get; set; }

        [Range(0, 100, ErrorMessage = "Marks should Between 0 To 100")]
        [Required(ErrorMessage = "Marks is required")]
        public int Sub_Marks { get; set; }

    }


    public class DB:DbContext
    {
        public DB():base("cs")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> subjects { get; set; }
    }

}