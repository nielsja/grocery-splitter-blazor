using grocery_splitter_blazor.Poco;
using grocery_splitter_blazor.Poco.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace grocery_splitter_blazor.Pages
{
    // needs to be a partial class because the .razor page because the compiler
    // sees the GrocerySplitter.razor as GrocerySplitter class already
    public partial class GrocerySplitter
    {
        private GrocerySplitterVM viewModel = new();
        private bool displayLineItems = true;
        private string testCalculateAllValuesString = "CalculateAllValues not yet called";
        private int testFunctionCallTimes = 0;

        // goes with the <EditForm>
        private EditContext? editContext;

        public GrocerySplitter()
        {
            editContext = new EditContext(viewModel);
        }
        // goes with the <EditForm>
        private void Submit()
        {
            CalculateTipAmounts();
            displayLineItems = true;
        }

        private void AddNewLineItem()
        {
            viewModel.LineItems.Add(new LineItemVM());
        }

        private void RemoveLineItem(LineItemVM lineItem)
        {
            viewModel.LineItems.Remove(lineItem);
            CalculateAllValues();
        }

        private void CalculateTipAmounts()
        {
            viewModel.Person1Amounts.Tip = Math.Round((viewModel.ReceiptTip ?? 0) / 2, 2);
            viewModel.Person2Amounts.Tip = (viewModel.ReceiptTip ?? 0) - viewModel.Person1Amounts.Tip;
        }

        private void CalculateAllValues()
        {
            testFunctionCallTimes++;
            testCalculateAllValuesString = $"CalculateAllValues called {testFunctionCallTimes} times!";

            if (viewModel.ReceiptSubtotal is null or 0)
            {
                return;
            }

            viewModel.ReceiptTax ??= 0;
            viewModel.ReceiptTip ??= 0;

            viewModel.Person1Amounts.SoloItems = viewModel.LineItems
                .Where((item) => item.Person == PersonEnum.Person1)
                .Sum((item) => item.ItemAmount ?? 0);

            viewModel.Person2Amounts.SoloItems = viewModel.LineItems
                .Where((item) => item.Person == PersonEnum.Person2)
                .Sum((item) => item.ItemAmount) ?? 0;

            var sharedItems = viewModel.LineItems
                .Where((item) => item.Person == PersonEnum.All)
                .Sum((item) => item.ItemAmount) ?? 0;

            viewModel.Person1Amounts.SharedItems = Math.Round(sharedItems / 2, 2);
            viewModel.Person2Amounts.SharedItems = sharedItems - viewModel.Person1Amounts.SharedItems;

            viewModel.Person1Amounts.Subtotal = viewModel.Person1Amounts.SoloItems + viewModel.Person1Amounts.SharedItems;
            viewModel.Person2Amounts.Subtotal = viewModel.Person2Amounts.SoloItems + viewModel.Person2Amounts.SharedItems;

            viewModel.Person1Amounts.Tax = Math.Round((viewModel.Person1Amounts.Subtotal / viewModel.ReceiptSubtotal.Value) * (viewModel.ReceiptTax ?? 0), 2);
            viewModel.Person2Amounts.Tax = viewModel.ReceiptTax!.Value - viewModel.Person1Amounts.Tax;

            viewModel.Person1Amounts.Tip = Math.Round((viewModel.ReceiptTip ?? 0) / 2, 2);
            viewModel.Person2Amounts.Tip = viewModel.ReceiptTip!.Value - viewModel.Person1Amounts.Tip;

            viewModel.Person1Amounts.TotalWithTax =
                viewModel.Person1Amounts.SoloItems +
                viewModel.Person1Amounts.SharedItems +
                viewModel.Person1Amounts.Tax;

            viewModel.Person1Amounts.TotalWithTaxAndTip = viewModel.Person1Amounts.TotalWithTax + viewModel.Person1Amounts.Tip;

            viewModel.Person2Amounts.TotalWithTax =
                viewModel.Person2Amounts.SoloItems +
                viewModel.Person2Amounts.SharedItems +
                viewModel.Person2Amounts.Tax;

            viewModel.Person2Amounts.TotalWithTaxAndTip = viewModel.Person2Amounts.TotalWithTax + viewModel.Person2Amounts.Tip;
        }
    }
}