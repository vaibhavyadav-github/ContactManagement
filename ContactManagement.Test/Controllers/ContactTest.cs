
using AutoMapper;

using ContactManagement.Data.Abstraction;
using ContactManagement.Data.Models;
using ContactManagement.Web.Controllers;

using Moq;

using System.Threading;

using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ContactManagement.Models.Dto;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.Data.SqlClient;

namespace ContactManagement.Test.Controllers
{
    public class ContactTest
    {
        public Mock<IRepository<Contact>> _contactRepo;

        public Mock<IMapper> _mapper;

        public ContactTest()
        {
            _contactRepo = new Mock<IRepository<Contact>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddContact_Success()
        {
            // Arrange
            var controler = new ContactsController(_mapper.Object,_contactRepo.Object);

            _mapper.Setup(service => service.Map<Contact>(It.IsAny<ContactRequest>())).Returns(SeedData.Contact());

            _contactRepo.Setup(c => c.Insert(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result  = await controler.Post(SeedData.ContactRequest(), It.IsAny<CancellationToken>()).ConfigureAwait(false);

            // Assert
            var status = result.Should().BeOfType<StatusCodeResult>().Subject;
            status.StatusCode.Equals(HttpStatusCode.Created);

            _contactRepo.Verify(v => v.Insert(It.IsAny<Contact>(), It.IsAny<CancellationToken>()));

        }


        [Fact]
        public async Task AddContact_Exception()
        {
            // Arrange
            var sqlException = FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;

            var controler = new ContactsController(_mapper.Object, _contactRepo.Object);

            _mapper.Setup(m => m.Map<Contact>(It.IsAny<ContactRequest>())).Returns(new Contact());

            _contactRepo.Setup(c => c.Insert(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(sqlException);

            // Act
            var result = await controler.Post(SeedData.ContactRequest(), It.IsAny<CancellationToken>()).ConfigureAwait(false);

            // Assert
            var status = result.Should().BeOfType<ObjectResult>().Subject;
            status.StatusCode.Equals(HttpStatusCode.InternalServerError);

            _contactRepo.Verify(v => v.Insert(It.IsAny<Contact>(), It.IsAny<CancellationToken>()));

        }

    }
}
