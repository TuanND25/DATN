
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
    [Route("api/Formula")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private readonly IFormulaService _formulaService;
        public FormulaController(IFormulaService formulaService)
        {
            _formulaService = formulaService;
        }
        [HttpGet]
        public async Task<ActionResult<Formula>> GetAllFormula()
        {
            try
            {
                var lst = await _formulaService.GetAllFormula();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        [HttpGet("ById")]
        public async Task<ActionResult<Formula>> GetByIdFormula(Guid Id)
        {
            try
            {
                var lstId = await _formulaService.GetFormulaById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        [HttpPost("PostFormula")]
        public async Task<ActionResult<Formula>> PostFormula(Formula_VM b)
        {
            try
            {
                Formula formula = new Formula();
                formula.Id = Guid.NewGuid();
                formula.Coefficient = b.Coefficient;
                formula.Status = b.Status;
                await _formulaService.PostFormula(formula);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        // PUT api/<ColorController>/5
        [HttpPut("PutFormula")]
        public async Task<ActionResult<Formula>> PutFormula(Formula_VM b)
        {
            try
            {
                Formula formula = await _formulaService.GetFormulaById(b.Id);
                formula.Coefficient = b.Coefficient;
                formula.Status = b.Status;
                await _formulaService.PutFormula(formula);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("DeleteFormula")]
        public async Task<ActionResult<Formula>> DeleteFormula(Guid Id)
        {
            try
            {
                await _formulaService.DeleteFormula(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");
            }

        }
    }
}
