using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/ServiceFeedBack")]
    [ApiController]
    public class ServiceFeedBackController : ODataController
    {
        private readonly IServiceFeedBackRepository serviceFeedBackRepository;
        public ServiceFeedBackController()
        {
            serviceFeedBackRepository = new ServiceFeedBackRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceFeedBack>>> GetAllServiceFeedBacks()
        {
            var list = await serviceFeedBackRepository.GetAllServiceFeedBacks();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceFeedBack>> GetServiceFeedBackById([FromODataUri] int id)
        {
            var ServiceFeedBack = await serviceFeedBackRepository.GetServiceFeedBackById(id);
            if (ServiceFeedBack == null)
            {
                return NotFound();
            }
            return Ok(ServiceFeedBack);
        }

        [HttpPost]
        public async Task AddServiceFeedBack([FromBody] ServiceFeedBack serviceFeedBack)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await serviceFeedBackRepository.AddServiceFeedBack(serviceFeedBack);
                //return Created(serviceFeedBack);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceFeedBack>> UpdateServiceFeedBack([FromODataUri] int id, [FromBody] ServiceFeedBack serviceFeedBack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serviceFeedBackToUpdate = await serviceFeedBackRepository.GetServiceFeedBackById(id);
            if (serviceFeedBackToUpdate == null)
            {
                return NotFound();
            }
            serviceFeedBack.FeedBackId = serviceFeedBackToUpdate.FeedBackId;
            await serviceFeedBackRepository.UpdateServiceFeedBack(serviceFeedBack);
            return Updated(serviceFeedBack);
        }
    }
}
