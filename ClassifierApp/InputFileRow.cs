namespace ClassifierApp
{
    public class InputFileRow
    {
        private string _original = string.Empty;
        private string _cleanned = string.Empty;

        public string Original
        {
            get => _original;
            set
            {
                _original = value;
                _cleanned = value
                    .Replace("COMPRAS NACIONAIS ", "")
                    .Replace("PAGAMENTO PIX ", "")
                    .Replace("LIQUIDACAO BOLETO SICREDI ", "")
                    .Replace("LIQUIDACAO BOLETO ", "")
                    .Trim();
            }
        }

        public string Cleanned { get => _cleanned; }
    }
}
