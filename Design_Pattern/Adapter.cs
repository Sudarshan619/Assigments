using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Adapter
    {
        public interface ISocket
        {
            string GetPower();
        }

        public class USASocket : ISocket
        {
            public string GetPower()
            {
                return "110V from USA Socket";
            }
        }

        public class EuropeSocket
        {
            public string ProvidePower()
            {
                return "220V from Europe Socket";
            }
        }

        public class EuropeToUSAAdapter : ISocket
        {
            private EuropeSocket _europeSocket;

            public EuropeToUSAAdapter(EuropeSocket europeSocket)
            {
                _europeSocket = europeSocket;
            }

            public string GetPower()
            {
                return _europeSocket.ProvidePower();
            }
        }


    }
}
