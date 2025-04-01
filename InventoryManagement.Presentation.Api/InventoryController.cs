using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        [HttpGet("{id}")]
        public ICollection<InventoryOperationViewModel> GetOperationsById(long id)
        {
            return _inventoryApplication.GetOperationLog(id);
        }
    }
}
