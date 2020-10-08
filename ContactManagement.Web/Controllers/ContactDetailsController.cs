

namespace ContactManagement.Web.Controllers
{
    using AutoMapper;

    using ContactManagement.Data;
    using ContactManagement.Data.Abstraction;
    using ContactManagement.Data.Models;
    using ContactManagement.Models.Dto;

    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {        
        private IRepository<ContactDetail> _contactDetailRepo;

        public IMapper _mapper;

        public ContactDetailsController(IMapper mapper, IRepository<ContactDetail> contactDetailRepo)
        {
            _mapper = mapper;
            _contactDetailRepo = contactDetailRepo;
        }


        /// <summary>
        /// Update email id of existing contact by exisiting record id
        /// </summary>
        /// <param name="contactDetailId"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{contactDetailId}/Email")]
        public async Task<IActionResult> UpdateEmail([FromRoute] int contactDetailId, [FromBody] string email, CancellationToken token)
        {
            try
            {
                var contactDetail = await _contactDetailRepo.Get(contactDetailId, token).ConfigureAwait(false);

                // If doesn't exists
                if (contactDetail == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                contactDetail.Value = email;

                await _contactDetailRepo.Update(contactDetail, token).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }



    }
}
