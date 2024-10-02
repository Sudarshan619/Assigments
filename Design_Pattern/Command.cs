using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Command
    {
        public interface ICommand
        {
            void Execute();
        }

        public class Light
        {
            public void On()
            {
                Console.WriteLine("Light is On");
            }

            public void Off()
            {
                Console.WriteLine("Light is Off");
            }
        }

        public class LightOnCommand : ICommand
        {
            private Light _light;

            public LightOnCommand(Light light)
            {
                _light = light;
            }

            public void Execute()
            {
                _light.On();
            }
        }

        public class RemoteControl
        {
            private ICommand _command;

            public void SetCommand(ICommand command)
            {
                _command = command;
            }

            public void PressButton()
            {
                _command.Execute();
            }
        }
    }
}
