using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceExc
{
    public class Program2 : IStartbar, IStopbar
    {
        /*
        public void Go()
        {
             Console.WriteLine("nu kör vi program 2");
        }
         */

        public void Start()
        {
            Console.WriteLine("Nu körs program2");
        }

        public void Stop()
        {
            Console.WriteLine("Nu stoppas program2");
        }
    }
}
