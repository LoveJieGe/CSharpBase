using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCSharp.Messaging
{
    public class CourseOrder:BindableBase
    {
        public const string CourseOrderQueueName = @".\Private$\MyNewPublicQueue";

        public CourseOrder()
        {
            customer = new Customer();
            course = new Course();
        }
        private Customer customer;
        public Customer Customer
        {
            get { return this.customer; }
            set { SetProperty(ref this.customer, value, "Customer"); }
        }
        private Course course;
        public Course Course
        {
            get { return this.course; }
            set { SetProperty(ref course, value, "Course"); }
        }
    }
}
