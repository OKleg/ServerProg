﻿using System;
using System.Collections.Generic;

namespace IndivServerProg.Models;

public partial class MovieKeyword
{
    public long? MovieId { get; set; }

    public long? KeywordId { get; set; }

    public virtual Keyword? Keyword { get; set; }

    public virtual Movie? Movie { get; set; }
}
