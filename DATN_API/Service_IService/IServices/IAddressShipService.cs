using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IAddressShipService
    {
        public Task<AddressShip> PostAddressShip(AddressShip addressShip);
        public Task<AddressShip> PutAddressShip(AddressShip addressShip);
        public Task<AddressShip> DeleteAddressShip(Guid Id);
        public Task<AddressShip> GetAddressShipById(Guid Id);
        public Task<IEnumerable<AddressShip>> GetAddressShipByUserId(Guid UsedId);
        public Task<IEnumerable<AddressShip>> GetAllAddressShip();
    }
}
