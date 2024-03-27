using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class TaxBand
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The lower limit is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Lower limit must be greater than or equal to 0")]
    public int LowerLimit { get; set; }

    public int? UpperLimit { get; set; }

    [Required(ErrorMessage = "The tax rate is required")]
    [Range(0, 100, ErrorMessage = "Tax rate must be between 0 and 100")]
    public int TaxRate { get; set; }
}
