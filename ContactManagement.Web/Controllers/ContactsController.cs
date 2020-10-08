

namespace ContactManagement.Web.Controllers
{
    using AutoMapper;

    using ContactManagement.Data;
    using ContactManagement.Data.Abstraction;
    using ContactManagement.Data.Models;
    using ContactManagement.Models.Dto;

    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {        
        private IRepository<Contact> _contactRepo;

        public IMapper _mapper;

        public ContactsController(IMapper mapper, IRepository<Contact> contactRepo)
        {
            _mapper = mapper;
            _contactRepo = contactRepo;
        }
        

        [HttpGet("{city}")]
        public async Task<IActionResult> Get([FromRoute] string city, CancellationToken token)
        {
            try
            {
                var result = await _contactRepo.FindByExpression(c =>
                                        c.Address.City == city ,token).ConfigureAwait(false);

                if (result == null)
                    return StatusCode((int)HttpStatusCode.Gone);

                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }           

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactRequest contactRequest, CancellationToken token)
        {
            try
            {
                var contact = _mapper.Map<Contact>(contactRequest);
                contact.CreatedDate = DateTime.Now;
                contact.ModifiedDate = DateTime.Now;
                
                await _contactRepo.Insert(contact, token).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }           

        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> Delete([FromRoute] int contactId, CancellationToken token)
        {
            try
            {
                var contact = await _contactRepo.FirstOrDefault(c => c.UniqueId == contactId, token).ConfigureAwait(false);


                // If doesn't exists
                if (contact == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                await _contactRepo.Delete(contact, token).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }            

        }


        [HttpPut("{contactId}/Phone")]
        public async Task<IActionResult> AddPhone([FromRoute] int contactId, [FromBody] ContactDetailRequest contactDetailRequest, CancellationToken token)
        {
            try
            {
                var contact = await _contactRepo.FirstOrDefault(token, c => c.UniqueId == contactId,
                    include => include.Address,
                    include => include.ContactDetails
                ).ConfigureAwait(false);
                               

                // If doesn't exists
                if (contact == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                var newContactdetail = _mapper.Map<ContactDetail>(contactDetailRequest);
                contact.ContactDetails.Add(newContactdetail);

                await _contactRepo.Update(contact, token).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }




        //[HttpPut("{contactId}")]
        //public async Task<IActionResult> Put([FromRoute] int contactId, [FromBody] ContactRequest contactRequest, CancellationToken token)
        //{
        //    try
        //    {
        //        var existingContact = await _contactRepo.FirstOrDefault(c => c.UniqueId == contactId, token).ConfigureAwait(false);

        //        // If doesn't exists
        //        if (existingContact == null)
        //            return StatusCode((int)HttpStatusCode.NotFound);

        //        existingContact.FirstName = contactRequest.FirstName;

        //        existingContact.ModifiedDate = DateTime.Now;

        //        await _contactRepo.Update(existingContact, token).ConfigureAwait(false);

        //        return StatusCode((int)HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //       return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        //    }            

        //}

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAll(CancellationToken token)
        //{
        //    try
        //    {
        //        var result = await _contactRepo.FindByExpression(token,
        //            include => include.Address,
        //            include => include.ContactDetails
        //        ).ConfigureAwait(false);

        //        if (result == null)
        //            return StatusCode((int)HttpStatusCode.NotFound);

        //        return StatusCode((int)HttpStatusCode.OK, result);
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        //    }           

        //}


    }
}
