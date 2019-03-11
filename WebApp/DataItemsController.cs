using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    [Route("data-items")]
    [ApiController]
    public class DataItemsController : ControllerBase
    {
        private readonly DataItemService _service;

        public DataItemsController(DataItemService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           _service.Delete(id);
        }

        [HttpPost]
        public DataItem Create(DataItem item)
        {
            return _service.Create(item.Size);
        }

        [HttpGet]
        public IReadOnlyList<DataItem> GetAll()
        {
            return _service.GetAll();
        }
    }
}