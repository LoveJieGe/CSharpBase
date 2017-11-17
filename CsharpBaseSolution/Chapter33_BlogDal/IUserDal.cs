using Chapter33_BlogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogDal
{
    public interface IUserDal:IBaseDal<User>
    {
    }
    public interface IRoleDal:IBaseDal<Role>
    { }
}
