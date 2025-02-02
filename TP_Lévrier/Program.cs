using System.Linq.Expressions;
using TP_Lévrier;

internal class Program
{

    

    static void Main(string[] args)
    {
        List<int> podium = new List<int>();

        

        AutoResetEvent end = new AutoResetEvent(false);
        
        ManualResetEvent start = new ManualResetEvent(false);

        Random r = new Random();

        Console.Write("Nombre de lévrier entre 1 et 15 :");
        int num = int.Parse(Console.ReadLine());

        ManualResetEvent[] evnt = new ManualResetEvent[num];

        Levrier levrier;
        Mutex m = new Mutex(true);
        
        
        

        for (int i = 0; i < num; i++)
        {
           
            levrier = new Levrier();
            levrier.mut = m;
            levrier.numero = i;
            levrier.R = r;
            levrier.end = new ManualResetEvent(false);
            levrier.start = start;
            evnt[i] = levrier.end;
            Thread t = new Thread(levrier.Run);
            t.Start();

        }
        start.Set();

        int position;
        int arrives = 0;

        m.ReleaseMutex();


        while (arrives < num)
        {
            position = WaitHandle.WaitAny(evnt);
            m.WaitOne();
            Console.WriteLine("Le n° " + position + " est arrivé !   <=======================================================================");
            m.ReleaseMutex();
            arrives++;
            podium.Add(position);
            evnt[position].Reset();
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("     -----===== Tableau des arrivées =====-----");
        foreach (int i in podium)
        {
            Console.WriteLine("Levrier " + i);
        }

        
    }



}