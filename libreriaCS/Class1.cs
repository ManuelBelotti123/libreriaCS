using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace libreriaCS
{
    public class LibreriaCS
    {
        //variabili globali
        string nomefile = "belotti.csv";
        //record
        public struct str
        {
            public string comune;
            public string prov;
            public string reg;
            public string tip;
            public string stelle;
            public string den;
            public string ind;
            public string cap;
            public string local;
            public string fraz;
            public string tel;
            public string fax;
            public string posel;
            public string web;
            public string ces;
            public string cam;
            public string pls;
            public string pla;
            public string mval;
            public string c;
        }

        public string Record(string[] div, string sp, int l, string random)
        {
            str r;
            r.comune = div[0];
            r.prov = div[1];
            r.reg = div[2];
            r.tip = div[3];
            r.stelle = div[4];
            r.den = div[5];
            r.ind = div[6];
            r.cap = div[7];
            r.local = div[8];
            r.fraz = div[9];
            r.tel = div[10];
            r.fax = div[11];
            r.posel = div[12];
            r.web = div[13];
            r.ces = div[14];
            r.cam = div[15];
            r.pls = div[16];
            r.pla = div[17];
            r.c = "1";
            //stabiliamo una lunghezza fissa per ogni record
            return (r.comune + sp + r.prov + sp + r.reg + sp + r.tip + sp + r.stelle + sp + r.den + sp + r.ind + sp + r.cap + sp + r.local + sp + r.fraz + sp + r.tel + sp + r.fax + sp + r.posel + sp + r.web + sp + r.ces + sp + r.cam + sp + r.pls + sp + r.pla + sp + random + sp + r.c).PadRight(l) + "##\r\n";

        }

        public void aggiusta()
        {
            Random rn = new Random();
            int a = 0, cont = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            var app = new FileStream("app.csv", FileMode.Append, FileAccess.Write, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            BinaryWriter wr = new BinaryWriter(app);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                string random = rn.Next(0, 21).ToString();
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 10)
                    {
                        a = file.ReadByte();
                        cont++;
                    }
                    else
                    {
                        break;
                    }
                }
                file.Seek(-cont, SeekOrigin.Current);
                byte[] br = rd.ReadBytes(cont - 2);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                string[] div = line.Split(';');
                string linea = Record(div, ";", 300, random);
                char[] array = linea.ToCharArray();
                wr.Write(array);
                file.Seek(2, SeekOrigin.Current);
            }
        }

        public void campi_prova(string nomefile)
        {
            string line = ";miovalore;cancellato\r\n";
            char[] linea = line.ToCharArray();
            //apertura del file e dello strumento di scrittura
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryWriter writer = new BinaryWriter(file);
            BinaryReader reader = new BinaryReader(file);
            int a = 0;
            while (a == 10)
            {
                a = file.ReadByte();
            }
            Console.WriteLine(a);
            //scrive sul file
            file.Seek(-2, SeekOrigin.Current);
            writer.Write(linea);
            writer.Close();
            file.Close();
        }
    }
}
