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
    [Route("odata/Notification")]
    [ApiController]
    public class NotificationController : ODataController
    {
        private readonly INotificationRepository NotificationRepository;
        public NotificationController()
        {
            NotificationRepository = new NotificationRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications()
        {
            var list = await NotificationRepository.GetAllNotifications();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotificationById([FromODataUri] int id)
        {
            var notification = await NotificationRepository.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public async Task AddNotification([FromBody] Notification notification)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await NotificationRepository.AddNotification(notification);
                //return Created(notification);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Notification>> UpdateNotification([FromODataUri] int id, [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var notificationToUpdate = await NotificationRepository.GetNotificationById(id);
            if (notificationToUpdate == null)
            {
                return NotFound();
            }
            notification.NotificationId = notificationToUpdate.NotificationId;
            await NotificationRepository.UpdateNotification(notification);
            return Updated(notification);
        }
    }
}

