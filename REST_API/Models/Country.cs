﻿using System;
using System.Collections.Generic;

namespace REST_API.Models;

public partial class Country
{
    public long CountryId { get; set; }

    public string? CountryIsoCode { get; set; }

    public string? CountryName { get; set; }
}
