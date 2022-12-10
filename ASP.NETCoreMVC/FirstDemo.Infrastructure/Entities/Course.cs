namespace FirstDemo.Infrastructure.Entities
{
    public class Course : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }
        public List<Topic> Topics { get; set; }
        public List<CourseRegistration> CourseStudents { get; set; }

    }
}
