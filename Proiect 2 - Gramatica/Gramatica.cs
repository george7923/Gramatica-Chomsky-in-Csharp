using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proiect_2___Gramatica
{
    public class Gramatica
    {
        private List<Productie> P = new List<Productie>();
        private String txt;
        private int[] binar;
        private List<char> N_nulabil = new List<char>();
        
        public Gramatica(List<Productie> p)
        {
            this.P = p;
            
            
        }
        
        private void IntializeazaP(List<Productie> p,String tx)
        {
            try
            {
                using (StreamReader sr = new StreamReader(tx))
                {
                    while (true)
                    {
                        String linie = sr.ReadLine();
                        if (linie != null)
                        {
                            
                            string[] elems = linie.Split("-");
                            Productie p1 = new Productie(elems[0][0], elems[1]);
                            p.Add(p1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }catch (NullReferenceException ex)
            {
                Console.WriteLine("In fisierul: " + tx + " avem o problema! " + ex.Message);
            }
        }
        public static void ScrieInFisier(Gramatica G, String txt)
        {
            try
            {
                // Utilizarea StreamWriter pentru a scrie în fișier
                using (StreamWriter sw = new StreamWriter(txt))
                {
                    foreach (Productie p in G.P)
                    {
                        sw.WriteLine(p.getNotem() + "-" + p.getsir());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A aparut o eroare: {ex.Message}");
            }
        }
        public List<char> ConstituieN(Gramatica G)
        {
            List <char> N = new List<char>();
            HashSet<char> setN = new HashSet<char>();
            foreach(Productie p in G.getP())
            {
                setN.Add(p.getNotem());
            }
            foreach (Productie p in G.getP())
            {
                
                char[] TEST = p.getsir().ToCharArray();

                foreach(char c in TEST)
                {
                    if (char.IsUpper(c))
                    {
                        setN.Add(c);
                    }
                }
            }
            N = setN.ToList();
            return N;
        }
        public List<char> ConstituieT(Gramatica G)
        {
            List<char> T = new List<char>();
            HashSet<char> setT = new HashSet<char>();
            foreach(Productie p in G.getP())
            {
                char[] TEST = p.getsir().ToCharArray() ;
                foreach (char c in TEST)
                {
                    if (char.IsLower(c)||c==' ')
                    {
                        setT.Add(c);
                    }
                }
            }
            T = setT.ToList();
            return T;
        }
        
        public Gramatica Lema1()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Lema 1:");
            Console.ResetColor();

            List<Productie> P1 = new List<Productie>();
            Gramatica G1 = new Gramatica(P1);
            IntializeazaP(P1, "Gramatica.txt");
            List<char> N1 = new List<char>();
            List<char> T1 = new List<char>();
            N1 = ConstituieN(G1);
            T1 = ConstituieT(G1);
            List<char> T1_star = new List<char>();
            T1_star = T1;
            T1_star.Add(' ');
            HashSet<char> n2 = new HashSet<char>();
            HashSet<char> t2 = new HashSet<char>();
            HashSet<Productie> p2 = new HashSet<Productie>();
            List<Productie> P2 = new List<Productie>();
            
            
            Console.Write("N1: ");
            foreach(char c in N1)
            {
                Console.Write(c+ " ");
            }
            Console.WriteLine();
            for (int i = 0; i < N1.Count; i++)
            {
                foreach (Productie p in P1)
                {
                    if (N1.Contains(p.getNotem()) && ContineDoarElementeDinLista(p.getsir(), T1_star))
                    {
                        n2.Add(p.getNotem());
                        p2.Add(p);
                        
                        

                    }
                    if (ContineDoarElementeDinLista(p.getsir(), T1))
                    {
                        t2.UnionWith(p.getsir());
                    }

                }
                
            }
            List<char> N_2temp = new List<char>();
            N_2temp = n2.ToList();
            foreach (Productie p in P1)
            {
                //if(ContineDoarElementeDinLista(p.getsir(), T1) || ContineDoarElementeDinLista(p.getsir(), N_2temp))
                if(DiferentaMultimi(p.getsir(),T1,N_2temp)) 
                {
                    n2.Add(p.getNotem());
                    p2.Add(p);
                    t2.UnionWith(term(p.getsir()));
                }
            }
            List<char> T2 = new List<char>();
            List<char> N2 = new List<char>();
            P2 = p2.ToList();
            T2 = t2.Distinct().ToList();
            N2 = n2.ToList();
            Console.Write("N2: ");
            foreach (char c in N2) { 
            Console.Write(c+ " ");
            
            }
            Console.WriteLine();
            Console.Write("T2: ");
            foreach(char c in T2)
            {
                Console.Write(c+ " ");
            }
            Console.WriteLine();
            MergeSortNotemi(P2, 0, P2.Count - 1);
            for(int i = 1; i < P2.Count; i++)
            {
                InterschimbareProductii(P2[i - 1], P2[i]);
            }
            Gramatica G2 = new Gramatica(P2);
            
            ScrieInFisier(G2, "Lema2.txt");
           
            
            
            return G2;


            
        }
        public Gramatica Lema2()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dupa Lema 2:");
            Console.ResetColor();
            List<Productie> P1 = new List<Productie>();
            List<Productie> P2 = new List<Productie>();
            List<char> N2 = new List<char>();
            List<char> T2 = new List<char>();
            HashSet<char> n2 = new HashSet<char>();
            HashSet<char> t2 = new HashSet<char>();
            HashSet<Productie> p2 = new HashSet<Productie>();
            
            Gramatica G1 = new Gramatica(P1);
            IntializeazaP(P1, "Lema2.txt");
            if (P1.Count > 0)
            {
                n2.Add(P1[0].getNotem());
                foreach (Productie p in P1)
                {
                    if (n2.Contains(p.getNotem()))
                    {

                        n2.UnionWith(nonterm(p.getsir()));
                        t2.UnionWith(term(p.getsir()));
                        p2.Add(p);
                    }
                }
                N2 = n2.ToList();
                T2 = t2.ToList();
                P2 = p2.ToList();
                Console.Write("N2: ");
                foreach (char c in N2)
                {
                    Console.Write(c + ", ");
                }
                Console.WriteLine();
                Console.Write("T2: ");
                foreach (char c in T2)
                {
                    Console.Write(c + ", ");
                }
                Console.WriteLine();
                Gramatica G2 = new Gramatica(P2);

                ScrieInFisier(G2, "Lema3.txt");

                return G2;
            }
            else
            {
                Console.WriteLine("Lema 1 a eliminat toate productiile!");
                return null;
            }

        }
        public Gramatica Lema3()
        {
            List<Productie> P1 = new List<Productie>();
            List<Productie> P2 = new List<Productie>();
            List<char> N1 = new List<char>();
            List<char> T1 = new List<char>();
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Dupa Lema 3:");
            Console.ResetColor();
            HashSet<char> n_nul = new HashSet<char>();
            
            List<char> N2 = new List<char>();
            List<char> T2 = new List<char>();
            HashSet<char> n2 = new HashSet<char>();
            HashSet<char> t2 = new HashSet<char>();
            HashSet<Productie> p2 = new HashSet<Productie>();
            List<String> SiruriBinare = new List<String>();
            IntializeazaP(P1, "Lema3.txt");
            Gramatica G1 = new Gramatica(P1);
            N1 = ConstituieN(G1);
            T1 = ConstituieT(G1);
            Regex VerificaDeSpatii = new Regex("^[a-zA-Z]+$");
            
            for(int i = 0; i<P1.Count;i++)

            {
                if (!VerificaDeSpatii.IsMatch(P1[i].getsir()))
                {
                   
                    n_nul.Add(P1[i].getNotem());
                    P1.Remove(P1[i]);
                    
                }
            }
           
            int contorSir = 0;
            
            HashSet<char> Temporar = new HashSet<char>();
            for(int i =  P1.Count - 1; i >= 0; i--)
            {
               for(int j = 0; j < P1[i].getsir().Length; j++)
               {
                    if (n_nul.Contains(P1[i].getsir()[j]))
                    {
                        contorSir++;
                        Temporar.Add(P1[i].getNotem());
                    }
               }
                if (contorSir == P1[i].getsir().Length && P1[i].getsir()!=" ")
                {
                    foreach(char c in Temporar)
                    {
                        n_nul.Add(c);
                    }
                }
                contorSir = 0;
            }
            N_nulabil = n_nul.ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("N1_nulabil = {");
            for (int i = 0; i < N_nulabil.Count; i++)
            {

                
                if(i==N_nulabil.Count - 1)
                {
                    Console.Write(N_nulabil[i]);
                }
                else
                {
                    Console.Write(N_nulabil[i] + ",");
                }
            }
            Console.Write("}");

            Console.WriteLine();
            
            Console.ResetColor();
            List<char> noulN = ScadereInMultimi(N1, N_nulabil);
            foreach (Productie p in P1)
            {
                binar = new int[p.getsir().Length];
                for (int i = 0; i < p.getsir().Length; i++)
                {
                    if (N_nulabil.Contains(p.getsir()[i]))
                    {
                        binar[i] = 0;
                    }
                    else if (noulN.Contains(p.getsir()[i]) || T1.Contains(p.getsir()[i]))
                    {
                        binar[i] = 1;
                    }
                }
                p2.Add(p);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < binar.Length; i++)
                {
                    if (binar[i] == 1)
                    {
                        sb.Append(p.getsir()[i]);
                    }
                }
                p2.Add(new Productie(p.getNotem(), sb.ToString()));
                if (p.getsir() == " ")
                {
                    string inlocuit = p.getsir();
                    inlocuit = inlocuit.Replace(" ", "#");
                    p.setsir(inlocuit);
                    continue;
                }
                Console.WriteLine();

                while (!binar.All(element => element == 1))
                {

                    String bruh = GenereazaSirUrmator(p.getsir(), binar);
                    Productie pp = new Productie(p.getNotem(), bruh);
                    p2.Add(pp);

                }

            }



            P2 = p2.Distinct().ToList();
            for(int i = 0; i < P2.Count; i++)
            {
                
                if (!VerificaDeSpatii.IsMatch(P2[i].getsir()))
                {
                    P2.Remove(P2[i]);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Productie p in P2)
            {
                Console.WriteLine(p.getNotem() + "-" + p.getsir());
            }
            Console.ResetColor();
            Gramatica G2 = new Gramatica(P2);
            ScrieInFisier(G2, "Gramatica.txt");
            return G2;
        }
        
        public String GenereazaSirUrmator(String sir, int[] binar)
        {
            
            for (int i = sir.Length - 1; i >= 0; i--)
            {

                if (binar[i] == 1)
                {
                    binar[i] = 0;

                }
                else
                {
                    binar[i] = 1;
                    sir = sir.Remove(i);
                    


                    break;
                }
                
            }
            
            return sir;
            
            

        }

        public List<char> ScadereInMultimi(List<char> N1, List<char> N1_nulabil)
        {
            List<char> Multimea = new List<char>();
            for(int i = 0; i < N1.Count; i++)
            {
                if (!N1_nulabil.Contains(N1[i]))
                {
                    Multimea.Add(N1[i]);
                }
            }
            
            return Multimea;
        }
        
        private char[] term(String alfa)
        {
            String temp = "";
            foreach(char c in alfa)
            {
                if (char.IsLower(c))
                {
                    temp = temp + c;
                }
            }
            return temp.ToCharArray();
        }
        private char[] nonterm(String alfa)
        {
            String temp = "";
            foreach (char c in alfa)
            {
                if (char.IsUpper(c))
                {
                    temp = temp + c;
                }
            }
            return temp.ToCharArray();
        }
        static bool ContineDoarElementeDinLista(String sir, List<char> lista)
        {
            
            foreach (char caracter in sir)
            {
                if (!lista.Contains(caracter))
                {
                    return false;
                }
            }

            return true;
        }
        
        static bool DiferentaMultimi(String sir, List<char> lista, List<char> lista1)
        {
            
            foreach (char caracter in sir)
            {
                if (!lista.Contains(caracter) && !lista1.Contains(caracter))
                {
                    return false;
                }
            }

            return true;
        }
        
        public List<Productie>  getP()
        {
            return P;
        }

        private bool Comparare(char a, char b)
        {
            
            if (a == 'S' && b == 'S')
            {
                return false;
            }
            
            if (a == 'S' && b != 'S')
            {
                return true;
            }
            
            if (a != 'S' && b == 'S')
            {
                return false;
            }
            
            return a < b;
        }

        
        private void MergeSortNotemi(List<Productie> A, int stanga, int dreapta)
        {
            if (stanga < dreapta)
            {
                int mij = stanga + (dreapta - stanga) / 2;

                MergeSortNotemi(A, stanga, mij);
                MergeSortNotemi(A, mij + 1, dreapta);
                InterclasareNotemi(A, stanga, mij, dreapta);
            }
        }

        
        private void InterclasareNotemi(List<Productie> Multime, int stanga, int mij, int dreapta)
        {
            int n1 = mij - stanga + 1;
            int n2 = dreapta - mij;

            List<Productie> st = Multime.GetRange(stanga, n1);
            List<Productie> dr = Multime.GetRange(mij + 1, n2);

            int i = 0, j = 0, k = stanga;
            while (i < n1 && j < n2)
            {
                
                if (Comparare(st[i].getNotem(), dr[j].getNotem()))
                {
                    Multime[k] = st[i];
                    i++;
                }
                else
                {
                    Multime[k] = dr[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                Multime[k] = st[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                Multime[k] = dr[j];
                j++;
                k++;
            }
        }
        private void InterschimbareProductii(Productie A1, Productie A2)
        {
            if (A1.getNotem() == A2.getNotem())
            {
                if (A1.getsir()[0] < A2.getsir()[0])
                {
                    Productie aux = A1;
                    A1 = A2;
                    A2 = aux;
                }
            }
        }

    }
}

