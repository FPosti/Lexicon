using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceExc
{
    public class DubbelRunner : IRunVoid, IRunInt
    {
        void IRunVoid.Run()
        {
            Console.WriteLine("run med void körs");
        }
        int IRunInt.Run()
        {
            Console.WriteLine("run med int körs");
            return 7;

        }
    }
}
