using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Structural
    {
        public interface IButton
        {
            void Render();
        }

        public class WindowsButton : IButton
        {
            public void Render()
            {
                Console.WriteLine("Render Windows button.");
            }
        }

        public class MacButton : IButton
        {
            public void Render()
            {
                Console.WriteLine("Render Mac button.");
            }
        }

        public interface IUIFactory
        {
            IButton CreateButton();
        }

        public class WindowsFactory : IUIFactory
        {
            public IButton CreateButton()
            {
                return new WindowsButton();
            }
        }

        public class MacFactory : IUIFactory
        {
            public IButton CreateButton()
            {
                return new MacButton();
            }
        }

        class Program
        {
            static void Main()
            {
                IUIFactory factory = new WindowsFactory();
                IButton button = factory.CreateButton();
                button.Render();
            }
        }

    }
}
