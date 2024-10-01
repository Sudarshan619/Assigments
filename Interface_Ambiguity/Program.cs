namespace Interface_Ambiguity
{
    internal class Program
    {
        IPerson person = new ConcretePerson();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.person.Work();
        }
    }
}
