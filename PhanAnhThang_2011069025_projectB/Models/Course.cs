using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhanAnhThang_2011069025_projectB.Models
{
    public class Course//khóa học
    {
        public int ID { get; set; }//có ID mặc định enity hiểu là Key
        public bool IsCanceled { get; set; }
        public ApplicationUser Lecturer { get; set; }
        [Required]//
        public string LecturerId { get; set; }//kh được phép null
        [Required]
        [StringLength(255)]//tối đa kí tự
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public Category Category { get; set; }//danh mục - loại khóa học
        [Required]
        public byte CategoryId { get; set; }
    }
    
}