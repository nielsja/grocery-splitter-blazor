namespace grocery_splitter_blazor.Poco
{
    public class CalculatedAmountsVM
    {
        public decimal SoloItems { get; set; }
        public decimal SharedItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Tip { get; set; }
        public decimal TotalWithTax { get; set; }
        public decimal TotalWithTaxAndTip { get; set; }
    }
}