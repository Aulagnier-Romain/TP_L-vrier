using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Lévrier
{
    internal class Levrier
    {


        public Random R { get; internal set; }
        public Mutex mut { get; internal set; }

        public int numero;

        public ManualResetEvent start;

        public ManualResetEvent end;


        public void Run()
        {

            start.WaitOne();

            R = new Random();

            
            for (int i = 0; i < 250; i++) 
            {
                mut.WaitOne();
                int randomRange = R.Next(0, 10);
                Console.ForegroundColor = (ConsoleColor)(numero + 1);
                Console.WriteLine("Levrier " + numero + " : " + i);
                mut.ReleaseMutex();
                Thread.Sleep(randomRange);

            }

            end.Set();
        }
    }
}
