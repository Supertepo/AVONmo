using System;
using System.Collections.Generic;

namespace AVONmo.Models;

public partial class Precio
{
    public int IdPrecio { get; set; }

    public double? Cantidad { get; set; }

    public virtual ICollection<Crema> Cremas { get; set; } = new List<Crema>();

    public virtual ICollection<Electrodomestico> Electrodomesticos { get; set; } = new List<Electrodomestico>();

    public virtual ICollection<Maquillaje> Maquillajes { get; set; } = new List<Maquillaje>();

    public virtual ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();

    public virtual ICollection<Tupper> Tuppers { get; set; } = new List<Tupper>();
}
