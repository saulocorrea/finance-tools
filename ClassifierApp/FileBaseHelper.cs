namespace ClassifierApp
{
    public static class FileBaseHelper
    {
        public static void ClearFileDescriptions()
        {
            var fileLines = File.ReadAllLines($"{Environment.CurrentDirectory}\\dados-entrada-comclassificacao.tsv");

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

            File.WriteAllLines($"{Environment.CurrentDirectory}\\dados-entrada-comclassificacao.tsv", newFileLines);
        }
    }
}
