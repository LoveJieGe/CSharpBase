using Chapter15_ReflectionLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: SupportsWhatsNew]
namespace Chapter15_VectorLib
{
    [LastModified("2017-10-14", "IEnumerable接口的实现",Issues = "处理IEnumerable一个集合")]
    [LastModified("2017-10-14", "IFormattable接口的实现",Issues = "处理IFormattable的一个集合")]
    public class Vector : IFormattable, IEnumerable
    {
        public double x, y, z;
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(Vector rhs)
        {
            x = rhs.x;
            y = rhs.y;
            z = rhs.z;
        }
        [LastModified("2017-10-14","方法的处理")]
        public IEnumerator GetEnumerator()
        {
            return new VectorEnumerator(this);
        }
        [LastModified("2017-10-14","集合的使用")]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return ToString();
            string formatUpper = format.ToUpper();
            switch (formatUpper)
            {
                case "N":
                    return "|| " + Norm().ToString() + " ||";
                case "VE":
                    return string.Format("{0:E},{1:E},{2:E}",x,y,z);
                case "IJK":
                    StringBuilder sb = new StringBuilder(x.ToString(),30);
                    sb.Append("i+");
                    sb.Append(y.ToString());
                    sb.Append("j+");
                    sb.Append(z.ToString());
                    sb.Append(" k");
                    return sb.ToString();
                default:
                    return ToString();
            }
        }
        public double Norm()
        {
            return x * x + y * y + z * z;
        }

        public double this[uint i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException(string.Format("尝试检索Vector元素[{0}]失败", i));
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException(string.Format("尝试检索Vector元素[{0}]失败", i));
                }
            }
        }
        public static bool operator ==(Vector lhs, Vector rhs)
        {
            if ((Math.Abs(lhs.x - rhs.x) < double.Epsilon) &&
                (Math.Abs(lhs.y - rhs.y) < double.Epsilon) && (Math.Abs(lhs.z - rhs.z) < double.Epsilon))
                return true;
            return false;
        }
        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !(lhs == rhs);
        }
        public static Vector operator +(Vector lhs, Vector rhs)
        {
            Vector v = new Vector(lhs);
            v.x = v.x + rhs.x;
            v.y = v.y + rhs.y;
            v.z = v.z + rhs.z;
            return v;
        }
        public static double operator *(Vector lhs, Vector rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }
        public static Vector operator *(Vector lhs, double rhs)
        {
            return lhs*rhs;
        }
        public static Vector operator *(double lhs, Vector rhs)
        {
            return new Vector(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
        }
        [LastModified("2017-10-14", "VectorEnumerator类的处理")]
        private class VectorEnumerator : IEnumerator
        {
            private readonly Vector _theVector;
            int _location;

            public VectorEnumerator(Vector vector)
            {
                this._theVector = vector;
                this._location = -1;
            }

            public object Current
            {
                get
                {
                    if (_location < 0 || _location > 2)
                        throw new InvalidOperationException("枚举器位于第一个元素之前或最后一个元素之后");
                    return _theVector[(uint)_location];
                }
            }

            public bool MoveNext()
            {
                ++_location;
                return this._location > 2 ? false : true;
            }

            public void Reset()
            {
                this._location = -1;
            }
        }
    }

   
}
