using AntecipacaoDeRecebivel.Application.DTOs.Antecipacao;
using AntecipacaoDeRecebivel.Application.DTOs.NotaFiscal;
using AntecipacaoDeRecebivel.Application.Interfaces;
using AntecipacaoDeRecebivel.Domain.Entities;
using AntecipacaoDeRecebivel.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoDeRecebivel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _nfService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _nfService = notaFiscalService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotaFiscalDto?>> GetByIdAsync(int id)
        {
            try
            {
                var nf = await _nfService.GetByIdAsync(id);
                return Ok(nf);
            } catch (ArgumentException arg)
            {
                return NotFound(arg.Message);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<ActionResult<IEnumerable<NotaFiscalDto>>> GetByEmpresaIdAsync(int empresaId)
        {
            try
            {
                var nfs = await _nfService.GetByEmpresaIdAsync(empresaId);
                return Ok(nfs);
            } catch (ArgumentException arg)
            {
                return NotFound(arg.Message);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateNotaFiscalDto notaFiscal)
        {
            try
            {
                await _nfService.AddAsync(notaFiscal);
                return Created();
            } catch (ArgumentException arg)
            {
                return NotFound(arg.Message);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(NotaFiscalDto notaFiscal)
        {
            try
            {
                await _nfService.UpdateAsync(notaFiscal);
                return Ok();
            } catch (ArgumentException arg)
            {
                return NotFound(arg.Message);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _nfService.DeleteAsync(id);
                return Ok();
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("calcular")]
        public async Task<ActionResult<IEnumerable<EmpresaAntecipacaoDto>>> CalcularAntecipacao([FromBody] IEnumerable<int> notasFiscaisId)
        {
            try
            {
                var resp = await _nfService.CalcularAntecipacao(notasFiscaisId);
                return Ok(resp);
            } catch (ArgumentException arg)
            {
                return ValidationProblem(arg.Message);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
