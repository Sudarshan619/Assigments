using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Ambiguity
{
    internal class ConcretePerson : IEmployee,IPerson, IEquatable<ConcretePerson>
    {
        void IEmployee.AttendMeeting()
        {
            Console.WriteLine("Attending meeting as employee");
        }

        void IPerson.DoExercise()
        {
            Console.WriteLine("Doing exercise as person");
        }

        void IPerson.EatFood()
        {
            Console.WriteLine("Having food as person");
        }

        public bool Equals(ConcretePerson? other)
        {
            throw new NotImplementedException();
        }

        void IEmployee.GoTOWork()
        {
            Console.WriteLine("Going to work as employee");
        }
        void IEmployee.Work()
        {
            Console.WriteLine("Working as employee");
        }
        void IPerson.Work()
        {
            Console.WriteLine("Working as person");
        }

        //public void Work() {
        //    Console.WriteLine("working");
        //}

    }
}
