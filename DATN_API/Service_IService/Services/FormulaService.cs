using DATN_API.Data;
using DATN_API.Models;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class FormulaService : IFormulaService
    {
        private readonly ApplicationDbContext _context;
        public FormulaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Formula> DeleteFormula(Guid Id)
        {
            var dte = await _context.Formulas.FindAsync(Id);
            if (dte == null) return dte;
            _context.Formulas.Remove(dte);
            await _context.SaveChangesAsync();
            return dte;
        }

        public async Task<IEnumerable<Formula>> GetAllFormula()
        {
            return await _context.Formulas.ToListAsync();
        }

        public async Task<Formula> GetFormulaById(Guid Id)
        {
            return await _context.Formulas.FindAsync(Id);
        }

        public async  Task<IEnumerable<Formula>> GetFormulaByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Formula> PostFormula(Formula formula)
        {
            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();
            return formula;
        }

        public async Task<Formula> PutFormula(Formula formula)
        {
            var a = await _context.Formulas.FindAsync(formula.Id);
            if (a == null) return formula;
            a = formula;
            //a.Coefficient = formula.Coefficient;
            //a.Status = formula.Status;
            _context.Update(formula);
            await _context.SaveChangesAsync();
            return formula;
        }
    }
}
