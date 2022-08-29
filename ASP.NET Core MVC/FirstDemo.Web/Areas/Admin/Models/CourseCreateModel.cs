using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        [Required]
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        internal async Task CreateCourse()        //// we use async method here for, program wait here for successfully create Course instance
        {
            throw new NotImplementedException();
        }
    }
}
