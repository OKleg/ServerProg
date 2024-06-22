using System;
using System.Collections.Generic;

namespace IndivServerProg.Models;

public partial class User
{
    public string Login { get; set; } = null!;

    public string? Password { get; set; }

    public string? Role { get; set; }
}
