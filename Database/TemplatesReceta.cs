using System;
using System.Collections.Generic;

namespace pharma_izi.insertor.api.Database;

public partial class TemplatesReceta
{
    public Guid Id { get; set; }

    public string Ruta { get; set; } = null!;

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();
}
