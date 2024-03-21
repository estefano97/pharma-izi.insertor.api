using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharma_izi.insertor.api.Database;
using pharma_izi.insertor.api.DTOs;

namespace pharma_izi.insertor.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly PharmaIziContext _context;
        public MedicamentosController()
        {
            _context = new PharmaIziContext();
        }

        [HttpPost("insertAll")]
        public async Task<IActionResult> InsertAll([FromBody] List<MedicamentosDTO> medicamentos)
        {
            try
            {
                List<Medicamento> elementos = new();

                foreach (var item in medicamentos)
                {
                    Guid categoriaMedicamento = await VerificarCategoriaMedicamento(item);
                    Guid marcaMedicamento = await VerificarMarcaMedicamento(item);
                    Guid tipoIva = await VerificarTipoIva(item);
                    Guid zonaConsulta = await VerificarZonaConsulta(item);

                    var medicamentoNuevo = new Medicamento
                    {
                        Id = Guid.NewGuid(),
                        EnStock = item.enStock,
                        FotoMedicamento = item.fotoMedicamento,
                        Nombre = item.nombre,
                        RequiereReceta = item.requiereReceta,
                        IdCategoriaMedicamento = categoriaMedicamento,
                        IdMarcaMedicamento = marcaMedicamento,
                        IdTipoIva = tipoIva,
                        IdZonaConsulta = zonaConsulta
                    };

                    var PresentacionesMedicamentos = new List<PresentacionesMedicamento>();

                    item.presentaciones.ForEach(el =>
                    {
                        PresentacionesMedicamentos.Add(new PresentacionesMedicamento
                        {
                            Id = Guid.NewGuid(),
                            Descripcion = el.descripcion,
                            IdMedicamento = medicamentoNuevo.Id,
                            Valor = el.valor
                        });
                    });

                    medicamentoNuevo.PresentacionesMedicamentos = PresentacionesMedicamentos;

                    elementos.Add(medicamentoNuevo);

                    
                }

                await _context.Medicamentos.AddRangeAsync(elementos);
                await _context.SaveChangesAsync();

                return Ok("Creados!!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<Guid> VerificarCategoriaMedicamento(MedicamentosDTO medicamentos)
        {
            var element = await _context.CategoriaMedicamentos.Where(el => el.Nombre == medicamentos.categoriaMedicamento).FirstOrDefaultAsync();

            if (element != null) return element.Id;

            var created = new CategoriaMedicamento { Id = Guid.NewGuid(), Nombre = medicamentos.categoriaMedicamento, TipoMedicamento = medicamentos.categoriaMedicamento };

            var addedItem = await _context.CategoriaMedicamentos.AddAsync(created);
            await _context.SaveChangesAsync();

            return created.Id;
        }

        private async Task<Guid> VerificarMarcaMedicamento(MedicamentosDTO medicamentos)
        {
            var element = await _context.MarcaMedicamentos.Where(el => el.Descripcion == medicamentos.marcaMedicamento).FirstOrDefaultAsync();

            if (element != null) return element.Id;

            var created = new MarcaMedicamento { Id = Guid.NewGuid(), Descripcion = medicamentos.marcaMedicamento };

            var addedItem = await _context.MarcaMedicamentos.AddAsync(created);
            await _context.SaveChangesAsync();

            return created.Id;
        }

        private async Task<Guid> VerificarTipoIva(MedicamentosDTO medicamentos)
        {
            var element = await _context.TipoIvas.Where(el => el.Valor == medicamentos.tipoIva).FirstOrDefaultAsync();

            if (element != null) return element.Id;

            var created = new TipoIva { Id = Guid.NewGuid(), Valor = medicamentos.tipoIva, Nombre = $"Tipo Iva {medicamentos.tipoIva}%" };

            var addedItem = await _context.TipoIvas.AddAsync(created);
            await _context.SaveChangesAsync();

            return created.Id;
        }

        private async Task<Guid> VerificarZonaConsulta(MedicamentosDTO medicamentos)
        {
            var element = await _context.ZonaConsulta.Where(el => el.Descripcion == medicamentos.zonaConsulta).FirstOrDefaultAsync();

            if (element != null) return element.Id;

            var created = new ZonaConsultum { Id = Guid.NewGuid(), Descripcion = medicamentos.zonaConsulta };

            var addedItem = await _context.ZonaConsulta.AddAsync(created);
            await _context.SaveChangesAsync();

            return created.Id;
        }


    }
}
