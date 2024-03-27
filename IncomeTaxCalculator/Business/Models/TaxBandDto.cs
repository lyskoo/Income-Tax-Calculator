﻿using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class TaxBandDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int LowerLimit { get; set; }

    public int? UpperLimit { get; set; }

    public int TaxRate { get; set; }
}
