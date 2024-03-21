﻿using System;
using System.Collections.Generic;

namespace pharma_izi.insertor.api.Database;

public partial class Doctore
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? NombreConsultorio { get; set; }

    public Guid IdTemplateReceta { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string Apellido { get; set; } = null!;

    public virtual TemplatesReceta IdTemplateRecetaNavigation { get; set; } = null!;

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
