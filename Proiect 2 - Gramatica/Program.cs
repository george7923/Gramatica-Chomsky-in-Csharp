using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Proiect_2___Gramatica
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            List<Productie> P = new List<Productie>();
            Gramatica G = new Gramatica(P);
            
            G = G.Lema1();
            if (G != null)
            {
                Console.WriteLine("Gramatica rezultata dupa lema 1: ");
                for (int i = 0; i < G.getP().Count; i++)
                {
                    Console.WriteLine(G.getP()[i].getNotem() + " " + G.getP()[i].getsir());
                }
                Console.WriteLine("-------------");
            }
            G = G.Lema2();
            if (G != null)
            {
                
                for (int i = 0; i < G.getP().Count; i++)
                {
                    Console.WriteLine(G.getP()[i].getNotem() + " " + G.getP()[i].getsir());
                }
                Console.WriteLine("-------------");

            }

            Console.WriteLine("Gramatica rezultata dupa lema 3:");
            Console.ResetColor();
            G = G.Lema3();


        }
    }
}