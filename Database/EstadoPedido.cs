using System;
using System.Collections.Generic;

namespace pharma_izi.insertor.api.Database;

public partial class EstadoPedido
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
