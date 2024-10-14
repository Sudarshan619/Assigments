using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25_DataBase
{
    public interface IAuthentication
    {
        public void register();

        public Boolean login();

        public void updatePassword();
    }
}
