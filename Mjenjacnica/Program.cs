using System;
using System.Text;
using System.IO;
using System.Security;
using Newtonsoft.Json;
using System.Collections.Generic;
using ConsoleTables;
using System.Linq;

namespace css_smrdi
{
    class Program
    {
        public struct Valute
        {
            public int id;
            public string naziv;
            public string isoCode;
            public string drzava;
            public float iznos;

            public Valute(int j, string i, string p, string g, float s)
            {
                id = j;
                naziv = i;
                isoCode = p;
                drzava = g;
                iznos = s;

            }
        }

        public struct Korisnik
        {
            public int id;
            public string username;
            public string lozinka;

            public Korisnik(int j, string s, string g)
            {
                id = j;
                username = s;
                lozinka = g;
            }
        }

        static void Main(string[] args)
        {
            logo();
            Console.WriteLine("1. za registraciju.");
            Console.WriteLine("2. za prijavu. ");
            Console.Write("Vaš odabir:");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            while (odabir != 49 && odabir != 50)
            // 49 - 1, 50 - 2
            {
                Console.WriteLine("Krivi odabir.");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            Console.Clear();
            logo();

            switch (odabir)
            {
                case 49:
                    Console.WriteLine("**REGISTRACIJA**");
                    registracijaKorsnika();
                    Console.Clear();
                    logo();
                    Console.WriteLine("Prijava:");
                    dohvatiIzbornikPrijava();
                    break;
                case 50:
                    Console.WriteLine("**PRIJAVA**");
                    dohvatiIzbornikPrijava();
                    Console.Clear();
                    break;
            }

            Console.ReadKey();

        }

        public static void dohvatiIzbornikPrijava()
        {

            string ki = "Korisničko ime:";
            Zeleno(ki);
            string user = Console.ReadLine();
            string passd = "Lozinka:";
            Zeleno(passd);
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            List<Korisnik> lKorisnik = ucitajKorisnika();
            for (int i = 0; i < lKorisnik.Count(); i++)
            {
                if (user == lKorisnik[i].username && pass == lKorisnik[i].lozinka)
                {
                    Console.WriteLine("Prijava uspješna!");
                    Console.WriteLine();
                    dohvatiIzbornik();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Korisničko ime ili lozinka nisu točni.");
            dohvatiIzbornikPrijava();
            Console.ReadKey();
            Console.Clear();

        }

        public static void registracijaKorsnika()
        {
            Console.Write("Unesite vaše korisničko ime: ");
            string username = Console.ReadLine();
            List<Korisnik> lKorisnik = ucitajKorisnika();

            for (int i = 0; i < lKorisnik.Count(); i++)
            {
                if (username == lKorisnik[i].username)
                {
                    Console.WriteLine("Korisničko ime je zauzeto. ");
                    registracijaKorsnika();
                }
            }

            Console.Write("Unesite vašu zaporku: ");
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            Console.WriteLine("");
            Console.Write("Ponovite zaporku.: ");
            string passCheck = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    passCheck += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && passCheck.Length > 0)
                    {
                        passCheck = passCheck.Substring(0, (passCheck.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            if (pass == passCheck)
            {
                int id = lKorisnik.Count() + 1;
                lKorisnik.Add(new Korisnik(id, username, pass));
                string sKorisnik = JsonConvert.SerializeObject(lKorisnik);
                spremiJson(@"korisnici.json", sKorisnik);
                Console.WriteLine("Korisnik uspješno dodan.");

            }

            else
            {
                Console.WriteLine("");
                Console.WriteLine("Zaporke se ne poklapaju.");
                registracijaKorsnika();
            }

        }

        public static void dohvatiIzbornik()
        {
            Console.Clear();
            logo();
            Console.WriteLine("Vaš odabir:");
            Console.WriteLine("1. Pregledaj sve valute.");
            Console.WriteLine("2. Dodaj valutu.");
            Console.WriteLine("3. Ažuriraj valutu.");
            Console.WriteLine("4. Obriši valutu.");
            Console.WriteLine("5. Pronađi valutu.");
            Console.WriteLine("6. Zamjena novca");
            int intTemp = Convert.ToInt32(Console.ReadKey().Key);

            int[] unosi = { 49, 50, 51, 52, 53, 54, 88 };
            while (!unosi.Contains(intTemp))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                intTemp = Convert.ToInt32(Console.ReadKey().Key);

            }

            switch (intTemp)
            {
                case 49:
                    ispisiValute();
                    prikaziKontrole();
                    break;
                case 50:
                    dodajValutu();
                    prikaziKontrole();
                    break;
                case 51:
                    urediValutu();
                    prikaziKontrole();
                    break;
                case 52:
                    obrisiValutu();
                    prikaziKontrole();
                    break;
                case 53:
                    pronadiValutuIzbornik();
                    prikaziKontrole();
                    break;
                case 54:
                    mjenjacnica();
                    prikaziKontrole();
                    break;
            }
        }


        public static void prikaziKontrole()
        {
            Console.WriteLine("\nPritisnite [Q] za povratak u prethodni izbornik");
            Console.WriteLine("Pritisnite [X] za izlaz iz programa");
            Console.WriteLine("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            /*
             88 - X
             81 - Q
            */
            while (odabir != 81 && odabir != 88)
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno\n");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            switch (odabir)
            {
                case 81:
                    dohvatiIzbornik();
                    break;

                case 88:
                    System.Environment.Exit(1);
                    break;
            }
        }

        public static void dodajValutu()
        {
            Console.Clear();
            logo();
            Console.WriteLine("**DODAVANJE VALUTE**");
            Console.WriteLine("Unesite naziv valute: ");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite ISO code: ");
            string bbb = "ISO 3166-1 alpha-2 standard.";
            Zeleno(bbb);
            string isoCode = Console.ReadLine();

            Console.WriteLine("Unesite državu: ");
            string drzava = Console.ReadLine();

            Console.WriteLine("Unesite vrijednost tečaj vaše valute u EUR.");
            float iznos = float.Parse(Console.ReadLine());

            List<Valute> lValute = ucitajValute();
            int id = lValute.Count() + 1;
            lValute.Add(new Valute(id, naziv, isoCode, drzava, iznos));
            string sValute = JsonConvert.SerializeObject(lValute);
            spremiJson("rates.json", sValute);
            Console.WriteLine("*** Valuta uspješno dodana! ***");

        }

        public static void pronadiValutuIzbornik()
        {
            Console.Clear();
            logo();
            Console.WriteLine("Odaberite željenu stavku pronalaska valute: ");
            Console.WriteLine("1: Redni broj.");
            Console.WriteLine("2: Naziv valute.");
            Console.WriteLine("3: ISO code.");
            Console.WriteLine("4: Država.");


            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            int[] unosi = { 49, 50, 51, 52, 53, 88 };
            while (!unosi.Contains(odabir))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            switch (odabir)
            {
                case 49:
                    pronadiValutuRbr();
                    prikaziKontrole();
                    break;
                case 50:
                    pronadiValutuNaziv();
                    prikaziKontrole();
                    break;
                case 51:
                    pronadiValutuISO();
                    prikaziKontrole();
                    break;
                case 52:
                    pronadiValutuDr();
                    prikaziKontrole();
                    break;

            }

        }
        public static void pronadiValutuISO()
        {
            List<Valute> lValute = ucitajValute();
            Console.WriteLine();
            Console.WriteLine("Unesite ISO code valute: ");
            string iso = Console.ReadLine();
            int counter = 0;
            foreach (Valute valute in lValute)
            {
                counter++;
                if (iso == valute.isoCode)
                {
                    Console.WriteLine("Valuta pronađena, vaša valuta je: ");
                    var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
                    odabrana_valuta.AddRow(lValute[counter - 1].id,
                        lValute[counter - 1].naziv,
                        lValute[counter - 1].isoCode,
                        lValute[counter - 1].drzava,
                        lValute[counter - 1].iznos);
                    odabrana_valuta.Write();
                    prikaziKontrole();

                }
            }
            Console.WriteLine("Valuta nije pronađena.");
            pronadiValutuISO();
        }

        public static void pronadiValutuNaziv()
        {
            List<Valute> lValute = ucitajValute();
            Console.WriteLine();
            Console.WriteLine("Unesite naziv valute: ");
            string naziv = Console.ReadLine();
            int counter = 0;
            foreach (Valute valute in lValute)
            {
                counter++;
                if (naziv == valute.naziv)
                {
                    Console.WriteLine("Valuta pronađena, vaša valuta je: ");
                    var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
                    odabrana_valuta.AddRow(lValute[counter - 1].id,
                        lValute[counter - 1].naziv,
                        lValute[counter - 1].isoCode,
                        lValute[counter - 1].drzava,
                        lValute[counter - 1].iznos);
                    odabrana_valuta.Write();
                    prikaziKontrole();
                }
            }
            Console.WriteLine("Valuta nije pronađena.");
            pronadiValutuNaziv();

        }

        public static void pronadiValutuRbr()
        {
            List<Valute> lValute = ucitajValute();
            Console.WriteLine();
            Console.WriteLine("Unesite redni broj države: ");
            int odabirValute = Convert.ToInt32(Console.ReadLine());
            int counter = 0;
            foreach (Valute valute in lValute)
            {
                counter++;
                if (odabirValute == valute.id)
                {
                    Console.WriteLine("Valuta pronađena, vaša valuta je: ");
                    var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
                    odabrana_valuta.AddRow(lValute[counter - 1].id,
                        lValute[counter - 1].naziv,
                        lValute[counter - 1].isoCode,
                        lValute[counter - 1].drzava,
                        lValute[counter - 1].iznos);
                    odabrana_valuta.Write();
                    prikaziKontrole();
                }
            }
            Console.WriteLine("Valuta nije pronađena.");
            pronadiValutuRbr();

        }
        public static void pronadiValutuDr()
        {
            int counter = 0;
            List<Valute> lValute = ucitajValute();
            Console.WriteLine();
            Console.WriteLine("Unesite naziv države: ");
            string drz = Console.ReadLine();
            foreach (Valute valute in lValute)
            {
                counter++;
                if (drz == valute.drzava)
                {
                    Console.WriteLine("Valuta pronađena, vaša valuta je: ");
                    var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
                    odabrana_valuta.AddRow(lValute[counter - 1].id,
                        lValute[counter - 1].naziv,
                        lValute[counter - 1].isoCode,
                        lValute[counter - 1].drzava,
                        lValute[counter - 1].iznos);
                    odabrana_valuta.Write();
                    prikaziKontrole();
                }
            }
            Console.WriteLine("Valuta nije pronađena.");
            pronadiValutuDr();

        }

        public static void urediValutu()
        {
            Console.Clear();
            logo();
            List<Valute> lValute = ucitajValute();
            List<int> unos = new List<int>();
            var table = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            int rbr = 1;
            foreach (Valute valute in lValute)
            {
                unos.Add(rbr);
                table.AddRow(rbr++ + ".", valute.naziv, valute.isoCode, valute.drzava, valute.iznos);
            }
            table.Write();
            Console.WriteLine("\nUnesite redni broj valute kojega želite urediti: ");
            Console.Write("\nRedni broj: ");
            int odabirValute = Convert.ToInt32(Console.ReadLine());
            while (!unos.Contains(odabirValute))
            {
                Console.Write("\nPokreška pri odabiru studenta.Pokušajte ponovno ");
                Console.Write("\nRedni broj: ");
                odabirValute = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Odabrali ste valutu: ");
            var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            odabrana_valuta.AddRow(lValute[odabirValute - 1].id,
                lValute[odabirValute - 1].naziv,
                lValute[odabirValute - 1].isoCode,
                lValute[odabirValute - 1].drzava,
                lValute[odabirValute - 1].iznos);
            odabrana_valuta.Write();

            Console.WriteLine("Odaberite stavku koju želite urediti: ");
            Console.WriteLine("//1. naziv");
            Console.WriteLine("//2. isoCode");
            Console.WriteLine("//3. drzava");
            Console.WriteLine("//4. iznos");
            int intTemp = Convert.ToInt32(Console.ReadKey().Key);
            int[] unosi = { 49, 50, 51, 52, 88 };
            while (!unosi.Contains(intTemp))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                intTemp = Convert.ToInt32(Console.ReadKey().Key);

            }
            switch (intTemp)
            {
                case 49:
                    Console.Write("Unesite novi naziv:");
                    string naziv = Console.ReadLine();
                    lValute[odabirValute - 1] = new Valute(lValute[odabirValute - 1].id,
                                                            naziv,
                                                           lValute[odabirValute - 1].isoCode,
                                                           lValute[odabirValute - 1].drzava,
                                                           lValute[odabirValute - 1].iznos);
                    string sValuta = JsonConvert.SerializeObject(lValute);
                    spremiJson(@"rates.json", sValuta);
                    Console.WriteLine("**Valuta uspješno ažurirana**");
                    break;
                case 50:
                    Console.Write("Unesite novi isoCode: ");
                    string isoCode = Console.ReadLine();
                    lValute[odabirValute - 1] = new Valute(lValute[odabirValute - 1].id,
                                                           lValute[odabirValute - 1].naziv,
                                                           isoCode,
                                                           lValute[odabirValute - 1].drzava,
                                                           lValute[odabirValute - 1].iznos);
                    string sValuta2 = JsonConvert.SerializeObject(lValute);
                    spremiJson(@"rates.json", sValuta2);
                    Console.WriteLine("**Valuta uspješno ažurirana**");
                    break;
                case 51:
                    Console.Write("Unesite novu državu: ");
                    string drzava = Console.ReadLine();
                    lValute[odabirValute - 1] = new Valute(lValute[odabirValute - 1].id,
                                                           lValute[odabirValute - 1].naziv,
                                                           lValute[odabirValute - 1].isoCode,
                                                           drzava,
                                                           lValute[odabirValute - 1].iznos);
                    string sValuta3 = JsonConvert.SerializeObject(lValute);
                    spremiJson(@"rates.json", sValuta3);
                    Console.WriteLine("**Valuta uspješno ažurirana**");
                    break;
                case 52:
                    Console.Write("Unesite novi iznos: ");
                    float iznos = float.Parse(Console.ReadLine());
                    lValute[odabirValute - 1] = new Valute(lValute[odabirValute - 1].id,
                                                           lValute[odabirValute - 1].naziv,
                                                           lValute[odabirValute - 1].isoCode,
                                                           lValute[odabirValute - 1].drzava,
                                                           iznos);
                    string sValuta4 = JsonConvert.SerializeObject(lValute);
                    spremiJson(@"rates.json", sValuta4);
                    Console.WriteLine("**Valuta uspješno ažurirana**");
                    break;

            }

        }

        public static void obrisiValutu()
        {
            Console.Clear();
            logo();
            Console.WriteLine("**OBRIŠI VALUTU**");
            List<Valute> lValute = ucitajValute();
            List<int> unos = new List<int>();
            var table = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            int rbr = 1;
            foreach (Valute valute in lValute)
            {
                unos.Add(rbr);
                table.AddRow(rbr++ + ".", valute.naziv, valute.isoCode, valute.drzava, valute.iznos);
            }
            table.Write();
            Console.WriteLine("Unesite redni broj valute koju želite obrisati: ");
            Console.Write("\nRedni broj: ");
            int odabirValute = Convert.ToInt32(Console.ReadLine());
            while (!unos.Contains(odabirValute))
            {
                Console.WriteLine("Krivo.");
                Console.Write("\nRedni broj: ");
                odabirValute = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Odabrali ste valutu: ");
            var odabrana_valuta = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            odabrana_valuta.AddRow(lValute[odabirValute - 1].id,
                lValute[odabirValute - 1].naziv,
                lValute[odabirValute - 1].isoCode,
                lValute[odabirValute - 1].drzava,
                lValute[odabirValute - 1].iznos);
            odabrana_valuta.Write();

            Console.WriteLine("Pritisnite F ako želite obrisati odabranu valutu.");
            Console.WriteLine("Pritisnite bilo koju tipku ako želite izaći na glavni glavni izbornik.");
            Console.Write(": ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            if (odabir != 70)
            {
                dohvatiIzbornik();
            }
            else
            {
                lValute.RemoveAt(odabirValute - 1);
                string sValute = JsonConvert.SerializeObject(lValute);
                spremiJson(@"rates.json", sValute);
                Console.WriteLine("**Valuta obrisana**");
            }

        }

        public static void mjenjacnica()
        {
            Console.Clear();
            logo();
            Console.WriteLine("**ZAMJENA NOVCA**");
            List<Valute> lValute = ucitajValute();
            List<int> unos = new List<int>();
            var table = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            int rbr = 1;
            foreach (Valute valute in lValute)
            {
                unos.Add(rbr);
                table.AddRow(rbr++ + ".", valute.naziv, valute.isoCode, valute.drzava, valute.iznos);
            }
            table.Write();
            Console.Write("Odaberite redni broj vase valute: ");
            Console.Write("\nRedni broj: ");
            int odabirValute = Convert.ToInt32(Console.ReadLine());
            while (!unos.Contains(odabirValute))
            {
                Console.WriteLine("Krivo.");
                Console.Write("\nRedni broj: ");
                odabirValute = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Odabrali ste valutu: {0}", lValute[odabirValute - 1].naziv);

            Console.Write("Odaberite redni broj druge valute: ");
            Console.Write("\nRedni broj: ");
            int odabirValute2 = Convert.ToInt32(Console.ReadLine());
            while (!unos.Contains(odabirValute2))
            {
                Console.WriteLine("Krivo.");
                Console.Write("\nRedni broj: ");
                odabirValute2 = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Odabrali ste valutu: {0}", lValute[odabirValute2 - 1].naziv);
            Console.WriteLine("Unesite iznos koji želite zamijeniti: ");
            int iznos = Convert.ToInt32(Console.ReadLine());
            float rate1 = 0;
            rate1 = lValute[odabirValute - 1].iznos;
            float rate2 = 0;
            rate2 = lValute[odabirValute2 - 1].iznos;
            float rez = iznos * (rate2 / rate1);
            Console.WriteLine("**{0} {1} = {2} {3}**", iznos, lValute[odabirValute - 1].naziv, rez, lValute[odabirValute2 - 1].naziv);

        }
        public static void ispisiValute()
        {
            List<Valute> lValute = ucitajValute();
            Console.WriteLine("PRIKAZ SVIH VALUTA");
            string aaaa = "The Currency Converter uses the European Central Bank foreign exchange rates to calculate the conversion of any amount of one currency into the equivalent amount of another currency.";
            Zeleno(aaaa);
            var table = new ConsoleTable("R.br. ", "Naziv", "ISO code", "Država", "Iznos");
            int rbr = 1;
            foreach (Valute valute in lValute)
            {
                table.AddRow(rbr++ + ".", valute.naziv, valute.isoCode, valute.drzava, valute.iznos);
            }
            table.Write();
            Console.WriteLine();
        }



        public static List<Valute> ucitajValute()
        {
            StreamReader oSr = new StreamReader(@"rates.json");
            List<Valute> lValute = new List<Valute>();
            string sJson = oSr.ReadToEnd();
            oSr.Close();
            lValute = JsonConvert.DeserializeObject<List<Valute>>(sJson);
            return lValute;
        }

        public static List<Korisnik> ucitajKorisnika()
        {
            StreamReader oSr = new StreamReader(@"korisnici.json");
            List<Korisnik> lKorisnik = new List<Korisnik>();
            string sJson = oSr.ReadToEnd();
            oSr.Close();
            lKorisnik = JsonConvert.DeserializeObject<List<Korisnik>>(sJson);
            return lKorisnik;
        }

        public static void spremiJson(string datoteka, string json)
        {
            System.IO.File.WriteAllText(datoteka, "");
            StreamWriter json_datoteka = new StreamWriter(datoteka, true);
            json_datoteka.WriteLine(json);
            json_datoteka.Flush();
            json_datoteka.Close();
        }

        static void Zeleno(string value)
        {

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(value);
            Console.ResetColor();
        }

        public static void logo()
        {
            //Console.OutputEncoding = Encoding.GetEncoding(866);
            Console.WriteLine("\t\t\t\t\t\t┌───────────┐");
            Console.WriteLine("\t\t\t\t\t\t│Mjenjačnica│");
            Console.WriteLine("\t\t\t\t\t\t└───────────┘");
        }
    }
}
