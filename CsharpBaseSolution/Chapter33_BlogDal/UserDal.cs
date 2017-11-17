using Chapter33_BlogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Chapter33_BlogDal
{
    public class UserDal : BaseDal<User>, IUserDal
    {
        public UserDal() : base(new BlogContext())
        {
        }
    }
    public class RoleDal : BaseDal<Role>, IRoleDal
    {
        public RoleDal() : base(new BlogContext())
        {
        }
    }
}
