using System.Text;

namespace ClassifierApp
{
    public static class FileBaseHelper
    {
        public static List<string> ParseTextToTabularData(string[] lines)
        {
            var newLines = new List<string>();

            foreach (var line in lines)
            {
                var lineParts = line.Split(' ');

                var descr = new StringBuilder();

                var lastIndex = lineParts.Length - 1;

                /*
                "15/10/2024 COMPRAS NACIONAIS PETISKAO NOVO HAMBURGOBR VE0289549 -98,00"
                "02/09/2024 COMPRAS NACIONAIS ARMAZEM CAMILLO NOVO HAMBURGOB VE0823939 -87,50 -87,50"
                 */

                if (double.TryParse(lineParts[lastIndex], out double _) &&
                    double.TryParse(lineParts[lastIndex - 1], out double _))
                {
                    lastIndex--;
                }

                for (int i = 1; i < lastIndex; i++)
                {
                    descr.Append(" " + lineParts[i]);
                }

                var date = DateTime.Parse(lineParts[0]);
                var value = double.Parse(lineParts[lastIndex]);

                newLines.Add($"{date:dd/MM/yyyy}\t{descr}\t{value}");
            }

            return newLines;
        }

        public static List<string> ClearFileDescriptions(string[] fileLines)
        {
            var newFileLines = new List<string>();

            foreach (var line in fileLines)
            {
                var lineParts = line.Split('\t');

                var extratoItemDescription = lineParts[0];
                var extratoItemClassification = lineParts[1];

                extratoItemDescription = extratoItemDescription
                    .Replace("COMPRAS NACIONAIS ", "")
                    .Replace("PAGAMENTO PIX ", "")
                    .Replace("LIQUIDACAO BOLETO SICREDI ", "")
                    .Replace("LIQUIDACAO BOLETO ", "");

                var description = extratoItemDescription.ToString().Trim();

                newFileLines.Add($"{description}\t{extratoItemClassification}");
            }

            return newFileLines;
        }
    }
}
