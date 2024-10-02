using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Proxy
    {
        public interface IBookParser
        {
            string Parse();
        }

        public class BookParser : IBookParser
        {
            private string _bookContent;

            public BookParser(string bookContent)
            {
                _bookContent = bookContent;
            }

            public string Parse()
            {
                return $"Parsing book content: {_bookContent}";
            }
        }

        public class BookParserProxy : IBookParser
        {
            private string _bookContent;
            private BookParser _bookParser;

            public BookParserProxy(string bookContent)
            {
                _bookContent = bookContent;
            }

            public string Parse()
            {
                if (_bookParser == null)
                {
                    _bookParser = new BookParser(_bookContent);
                }
                return _bookParser.Parse();
            }
        }
    }
}
