using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
            return (r.comune + sp + r.prov + sp + r.reg + sp + r.tip + sp + r.stelle + sp + r.den + sp + r.ind + sp + r.cap + sp + r.local + sp + r.fraz + sp + r.tel + sp + r.fax + sp + r.posel + sp + r.web + sp + r.ces + sp + r.cam + sp + r.pls + sp + r.pla + sp + random + sp + r.c + ']').PadRight(l) + "##\r\n";

        }

        public bool ContrAgg()
        {
            int a;
            bool b = false;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            file.Seek(301, SeekOrigin.Begin);
            a = rd.Read();
            if (a == 35)
            {
                b = true;
            }
            rd.Close();
            file.Close();
            return b;
        }

        public void aggiusta()
        {
            Random rn = new Random();
            int a = 0, cont = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            var app = new FileStream("app.csv", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            BinaryWriter wr = new BinaryWriter(app);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                string random = rn.Next(10, 21).ToString();
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
            rd.Close();
            wr.Close();
            file.Close();
            app.Close();
            File.Replace("app.csv", "belotti.csv", "backup.csv");
        }

        public int ContaCampi()
        {
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            byte[] br = rd.ReadBytes(302);
            string line = Encoding.ASCII.GetString(br, 0, br.Length);
            //divisione dei vari campi della stringa
            string[] div = line.Split(';');
            return div.Length;
        }

        public int LungMaxRec()
        {
            int a = 0, cont = 0, max = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
                    {
                        a = file.ReadByte();
                        cont++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (cont > max)
                {
                    max = cont;
                }
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            file.Close();
            return max;
        }

        public void AggRec(string[] div)
        {
            Random rn = new Random();
            LibreriaCS l = new LibreriaCS();
            var file = new FileStream("belotti.csv", FileMode.Append, FileAccess.Write, FileShare.Read);
            BinaryWriter wr = new BinaryWriter(file);
            string linea = Record(div, ";", 300, rn.Next(10, 21).ToString());
            char[] array = linea.ToCharArray();
            wr.Write(array);
            wr.Close();
            file.Close();
        }

        public string[] EstrapolaCampi(int f, int b, int c, bool val)
        {
            string[] arr = new string[100000000];
            string[] div;
            int a = 0, cont = 0, i = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                div = line.Split(';');
                if (val)
                {
                    arr[i] = div[f] + ';' + div[b] + ';' + div[c];
                }
                else
                {
                    arr[i] = line;
                }
                i++;
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            file.Close();
            return arr;
        }

        public string[] Ricerca(int campo, string ricerca)
        {
            string[] arr = new string[1000000];
            string[] div;
            int a = 0, cont = 0, i = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                div = line.Split(';');
                if (div[campo] == ricerca)
                {
                    arr[i] = line;
                    i++;
                }
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            file.Close();
            return arr;
        }

        public void ModificaCampo(int campo, string ricerca, string modifica)
        {
            Random rn = new Random();
            int a = 0, cont = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            var app = new FileStream("app.csv", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            BinaryWriter wr = new BinaryWriter(app);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                string random = rn.Next(10, 21).ToString();
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                string[] div = line.Split(';');
                if (div[campo] == ricerca)
                {
                    div[campo] = modifica;
                }
                string linea = RecordMod(div, ";", 300);
                char[] array = linea.ToCharArray();
                wr.Write(array);
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            wr.Close();
            file.Close();
            app.Close();
            File.Replace("app.csv", "belotti.csv", "backup.csv");
        }

        public string RecordMod(string[] div, string sp, int l)
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
            r.mval = div[18];
            r.c = "1";
            return (r.comune + sp + r.prov + sp + r.reg + sp + r.tip + sp + r.stelle + sp + r.den + sp + r.ind + sp + r.cap + sp + r.local + sp + r.fraz + sp + r.tel + sp + r.fax + sp + r.posel + sp + r.web + sp + r.ces + sp + r.cam + sp + r.pls + sp + r.pla + sp + r.mval + sp + r.c + ']').PadRight(l) + "##\r\n";
        }

        public void CancRecLogica(int campo, string ricerca, bool cor)
        {
            string[] div;
            int a = 0, cont = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            BinaryWriter wr = new BinaryWriter(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                div = line.Split(';');
                if (div[campo] == ricerca)
                {
                    if (cor)
                    {
                        file.Seek(-2, SeekOrigin.Current);
                        char[] zero = { '1' };
                        wr.Write(zero[0]);
                        file.Seek(1, SeekOrigin.Current);
                        file.Seek(304 - cont, SeekOrigin.Current);
                    }
                    else
                    {
                        file.Seek(-2, SeekOrigin.Current);
                        char[] zero = { '0' };
                        wr.Write(zero[0]);
                        file.Seek(1, SeekOrigin.Current);
                        file.Seek(304 - cont, SeekOrigin.Current);
                    }
                }
                else
                {
                    file.Seek(304 - cont, SeekOrigin.Current);
                }
            }
            rd.Close();
            wr.Close();
            file.Close();
        }

        public void Ricompatta()
        {
            LibreriaCS l = new LibreriaCS();
            int a = 0, cont = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            var app = new FileStream("app.csv", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            BinaryWriter wr = new BinaryWriter(app);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                string[] div = line.Split(';');
                if (div[div.Length - 1] == "1]")
                {

                    string linea = l.RecordMod(div, ";", 300);
                    char[] array = linea.ToCharArray();
                    wr.Write(array);
                }
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            wr.Close();
            file.Close();
            app.Close();
            File.Replace("app.csv", "belotti.csv", "backup.csv");
        }

        public string[] Visualizza()
        {
            string[] arr = new string[1000000];
            int a = 0, cont = 0, i = 0;
            var file = new FileStream("belotti.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            BinaryReader rd = new BinaryReader(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                a = 0; cont = 0;
                while (true)
                {
                    if (a != 93)
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
                byte[] br = rd.ReadBytes(cont - 1);
                string line = Encoding.ASCII.GetString(br, 0, br.Length);
                //divisione dei vari campi della stringa
                string[] div = line.Split(';');
                if (div[div.Length - 1] == "1")
                {
                    arr[i] = line;
                    i++;
                }
                file.Seek(1, SeekOrigin.Current);
                file.Seek(304 - cont, SeekOrigin.Current);
            }
            rd.Close();
            file.Close();
            return arr;
        }
    }
}
