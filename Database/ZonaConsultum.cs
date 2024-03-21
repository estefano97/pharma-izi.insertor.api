using System;
using System.Collections.Generic;

namespace pharma_izi.insertor.api.Database;

public partial class ZonaConsultum
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
