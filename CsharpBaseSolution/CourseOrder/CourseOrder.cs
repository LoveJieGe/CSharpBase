using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseOrder
{
    public class CourseOrder:BindableBase
    {
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
