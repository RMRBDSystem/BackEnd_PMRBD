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
    public class ServiceFeedbackController : ODataController
    {
        private readonly IServiceFeedBackRepository serviceFeedBackRepository;
        public ServiceFeedbackController()
        {
            serviceFeedBackRepository = new ServiceFeedBackRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceFeedback>>> GetAllServiceFeedbacks()
        {
            var list = await serviceFeedBackRepository.GetAllServiceFeedBacks();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceFeedback>> GetServiceFeedbackById([FromODataUri] int id)
        {
            var ServiceFeedback = await serviceFeedBackRepository.GetServiceFeedBackById(id);
            if (ServiceFeedback == null)
            {
                return NotFound();
            }
            return Ok(ServiceFeedback);
        }

        [HttpPost]
        public async Task AddServiceFeedback([FromBody] ServiceFeedback serviceFeedBack)
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
        public async Task<ActionResult<ServiceFeedback>> UpdateServiceFeedback([FromODataUri] int id, [FromBody] ServiceFeedback serviceFeedBack)
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
