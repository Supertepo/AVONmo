﻿using System;
using System.Collections.Generic;

namespace AVONmo.Models;

public partial class Perfume
{
    public string? IdCategoria { get; set; }

    public string IdProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? IdPrecio { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Precio? IdPrecioNavigation { get; set; }
}