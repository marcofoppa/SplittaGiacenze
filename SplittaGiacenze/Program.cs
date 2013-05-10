using System;
using System.IO;
using System.Text;

namespace SplittaGiacenze
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Apro il file in lettura");

            string line;

            var file = new StreamReader("giacenze.txt");
            var contenutoSplittato = new StringBuilder();
            var codiceTargaturaGrossista = string.Empty;
            var codiceAsl = string.Empty;

            while ((line = file.ReadLine()) != null)
            {
                // Se la linea inizia per G, la linea appartiene al file RET, quindi la considero
                // Altrimenti, se lo StringBuilder contiene qualcosa, lo scarico su un file .RET
                if (line.StartsWith("G"))
                {
                    contenutoSplittato.AppendLine(line);
                    codiceTargaturaGrossista = line.Substring(5, 6);
                    codiceAsl = line.Substring(233, 6);
                    Console.WriteLine(line);
                }
                else
                {
                    if (contenutoSplittato.Length > 0)
                    {

                        if (!Directory.Exists(codiceTargaturaGrossista))
                        {
                            Directory.CreateDirectory(codiceTargaturaGrossista);
                        }

                        // Scrivo il file e svuoto lo stringbuilder
                        var nomefile = string.Format(@"{0}\{1}_{2}_{3}.RET", codiceTargaturaGrossista, codiceAsl, codiceTargaturaGrossista, DateTime.Now.ToString("yyyyddMMHHmmss_fffffff"));

                        File.WriteAllText(nomefile, contenutoSplittato.ToString());

                        contenutoSplittato.Length = 0;
                        contenutoSplittato.Capacity = 0;
                        codiceTargaturaGrossista = string.Empty;
                        codiceAsl = string.Empty;
                    }
                }
            }

            file.Close();

            Console.WriteLine("File terminato...");
            Console.ReadLine();
        }

    }

}
