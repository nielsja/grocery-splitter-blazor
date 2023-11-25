using System.ComponentModel.DataAnnotations;

namespace grocery_splitter_blazor.Poco
{
    public class GrocerySplitterVM
    {
        // Required decorator is specifying the value is required on SUBMIT
        [Required]
        public decimal? ReceiptSubtotal { get; set; }

        [Required]
        public decimal? ReceiptTax { get; set; }

        public decimal? ReceiptTip { get; set; }

        public CalculatedAmountsVM Person1Amounts { get; set; } = new();

        public CalculatedAmountsVM Person2Amounts { get; set; } = new();

        public List<LineItemVM> LineItems { get; set; } = new()
        {
            new LineItemVM(),
            new LineItemVM(),
            new LineItemVM()
        };
    }

}