using System;
using System.Collections.Generic;

namespace pharma_izi.insertor.api.Database;

public partial class TiposPago
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
