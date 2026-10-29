using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceExc
{
    public class Program1 : IStartbar, IStopbar
    {
        /*
        public void run()
        {
             Console.WriteLine("Nu kör vi program 1");
        }
        */
        public void Start()
        {
            Console.WriteLine("Nu körs program1");
        }
        public void Stop()
        {
            Console.WriteLine("Nu stoppas program1");
        }   
    }
}
