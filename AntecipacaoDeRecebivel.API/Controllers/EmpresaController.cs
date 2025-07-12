using AntecipacaoDeRecebivel.Application.DTOs.Empresa;
using AntecipacaoDeRecebivel.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoDeRecebivel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaDto>> CreateEmpresa([FromBody] CreateEmpresaDto createDto)
        {
            try
            {
                var empresa = await _empresaService.CreateEmpresaAsync(createDto);
                return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaDto>> GetEmpresa([FromRoute] int id)
        {
            try
            {
                var empresa = await _empresaService.GetEmpresaAsync(id);
                return Ok(empresa);
            } catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetAllAsync()
        {
            try
            {
                var empresas = await _empresaService.GetAllEmpresasAsync();
                return Ok(empresas);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmpresaAsync([FromBody] EmpresaDto empresa)
        {
            try
            {
                await _empresaService.UpdateEmpresaAsync(empresa);
                return Ok();
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmpresaAsync([FromRoute] int id)
        {
            try
            {
                await _empresaService.DeleteEmpresaAsync(id);
                return Ok();
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
