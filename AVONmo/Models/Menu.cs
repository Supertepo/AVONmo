using System;
using System.Collections.Generic;

namespace AVONmo.Models;

public partial class Menu
{
    public string IdCategoria { get; set; } = null!;

    public int? CantInu { get; set; }

    public int IdMenu { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
