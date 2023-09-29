using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IFormulaService
    {
        public Task<Formula> PostFormula (Formula formula);
        public Task<Formula> PutFormula (Formula formula);   
        public Task<Formula> DeleteFormula (Guid Id);
        public Task<Formula> GetFormulaById(Guid Id);
        public Task<IEnumerable<Formula>> GetFormulaByName(string name);
        public Task<IEnumerable<Formula>> GetAllFormula();


    }
}
