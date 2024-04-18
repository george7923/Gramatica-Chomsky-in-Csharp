using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_2___Gramatica
{
    public class Productie
    {
        private char notem;
        private String sir;
        public Productie(char notem, String sir)
        {
            this.notem = notem;
            this.sir = sir;
        }
        public char getNotem() {  return notem; }
        public String getsir() { return sir; }

        public void setNotem(char value)
        {
            notem = value;
        }
        public void setsir(string value)
        {
            sir = value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + notem.GetHashCode();
                hash = hash * 23 + (sir != null ? sir.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Productie other))
                return false;

            return notem == other.notem && string.Equals(sir, other.sir);
        }

    }
}
