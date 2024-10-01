using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class Connection
    {
        public string? ConnectionName { get; set; }
        private static Connection? connection = null;

        //You cannot instaiate teh object of this calls coz the constrictor is private
        //Used for singleton type where one object to be created from one class
        private Connection()
        {

        }
        public static Connection GetConnection()
        {
            if(connection == null)
            {
                connection = new Connection();
            }
            return connection;
        }
    }
}
