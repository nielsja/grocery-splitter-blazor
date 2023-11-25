using System.ComponentModel.DataAnnotations;
using grocery_splitter_blazor.Poco.Enums;

namespace grocery_splitter_blazor.Poco
{
    public class LineItemVM
    {
        [Required]
        public decimal? ItemAmount { get; set; }

        [Required]
        public PersonEnum? Person { get; set; }
    }

}