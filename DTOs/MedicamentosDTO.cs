namespace pharma_izi.insertor.api.DTOs
{
    public class MedicamentosDTO
    {
        public string nombre { get; set; }
        public string categoriaMedicamento { get; set; }
        public bool enStock { get; set; }
        public decimal tipoIva { get; set; }
        public string fotoMedicamento { get; set; }
        public bool requiereReceta { get; set; }
        public string marcaMedicamento { get; set; }

        public string zonaConsulta { get; set; }

        public List<PresentacionMedicamentoDTO> presentaciones { get; set; }
        
    }

    public class PresentacionMedicamentoDTO
    {
        public decimal valor { get; set; }
        public string descripcion { get; set; }

    }
}
