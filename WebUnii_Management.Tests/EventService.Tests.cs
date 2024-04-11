using Microsoft.EntityFrameworkCore;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Event;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Migrations;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Tests
{
	[TestFixture]
    public class EventServiceTests
    {
        private IRepository repository;
        private IEventService eventService;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

			repository = new Repository(dbContext);
			eventService = new EventService(repository);
		}

        [Test]
        public async Task AddEventAsync_ShouldAddEvent()
        { 
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });

            await repository.SaveChangesAsync();

            var eventFromDb = await repository.GetById<Event>(15);

            Assert.AreEqual("Test Event", eventFromDb.Name);
            Assert.AreEqual(100, eventFromDb.Capacity);
            Assert.AreEqual("Test", eventFromDb.GuestParticipant);
            Assert.AreEqual("https://www.test.com", eventFromDb.ImageUrl);
            Assert.AreEqual("Test Description", eventFromDb.Description);
            Assert.AreEqual(15, eventFromDb.Id);
            Assert.IsNotNull(eventFromDb);
            Assert.IsInstanceOf<Event>(eventFromDb);
        }

        [Test]
        public async Task AddEventAsync_ShouldAddEventToDb()
        {
            await eventService.AddEventAsync(new EventFormViewModel
            {
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });

            var eventFromDb = await repository.GetById<Event>(4);

            Assert.AreEqual("Test Event", eventFromDb.Name);
            Assert.AreEqual(100, eventFromDb.Capacity);
            Assert.AreEqual("Test", eventFromDb.GuestParticipant);
            Assert.AreEqual("https://www.test.com", eventFromDb.ImageUrl);
            Assert.AreEqual("Test Description", eventFromDb.Description);
            Assert.IsNotNull(eventFromDb);
            Assert.IsInstanceOf<Event>(eventFromDb);
        }

        [Test]
        public async Task DeleteEventByIdAsync_ShouldDeleteEvent()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            await eventService.DeleteEventByIdAsync(15);

            var eventFromDb = await repository.GetById<Event>(15);
            Assert.IsNull(eventFromDb);
            Assert.AreEqual(3, await repository.All<Event>().CountAsync());            
        }

        [Test]
        public async Task DeleteEventParticipantByEventIdAsync_ShoulDeleteEventParticipant()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new EventParticipant
            {
                EventId = 15,
                ParticipantId = Guid.NewGuid().ToString()
            });
            await repository.SaveChangesAsync();

            await eventService.DeleteEventByIdAsync(15);
            var eventParticipant = await repository.All<EventParticipant>().Where(x => x.EventId == 15).FirstOrDefaultAsync();
            Assert.IsNull(eventParticipant);
            Assert.AreEqual(1, await repository.All<EventParticipant>().CountAsync());
            Assert.AreEqual(3, await repository.All<Event>().CountAsync());           
        }

        [Test]
        public async Task EventParticipantByDeleteEventById_ShouldReturnNull()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new EventParticipant
            {
                EventId = 16,
                ParticipantId = Guid.NewGuid().ToString()
            });
            await repository.SaveChangesAsync();

            await eventService.DeleteEventByIdAsync(15);
            var eventParticipant = await repository.All<EventParticipant>().Where(x => x.EventId == 16).FirstOrDefaultAsync();

            Assert.IsNotNull(eventParticipant);
            Assert.AreEqual(2, await repository.All<EventParticipant>().CountAsync());
            Assert.AreEqual(3, await repository.All<Event>().CountAsync());
        }

        [Test]
        public async Task EditEventAsync_ShouldEditEvent()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();
            await eventService.EditEventAsync(15, new EventFormViewModel
            {
                Name = "Edited Event",
                Capacity = 200,
                GuestParticipant = "Edited",
                ImageUrl = "https://www.edited.com",
                Description = "Edited Description"
            });

            var eventFromDb = await repository.GetById<Event>(15);

            Assert.IsNotNull(eventFromDb);
            Assert.AreEqual("Edited Event", eventFromDb.Name);
            Assert.AreEqual(200, eventFromDb.Capacity);
            Assert.AreEqual("Edited", eventFromDb.GuestParticipant);
            Assert.AreEqual("Edited Description", eventFromDb.Description);
            Assert.AreEqual(15, eventFromDb.Id);
            Assert.IsInstanceOf<Event>(eventFromDb);
        }

        [Test]
        public async Task EventExistsByIdAsync_ShouldReturnTrue()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.EventExistsByIdAsync(15);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task EventExistsByIdAsync_ShouldReturnFalse()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.EventExistsByIdAsync(16);

            Assert.IsFalse(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task GetDetailsForEventById_ShouldReturnModel()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.GetDetailsForEventById(15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EventDetailsViewModel>(result);
            Assert.AreEqual("Test Event", result.Name);
            Assert.AreEqual(100, result.Capacity);
            Assert.AreEqual("Test", result.GuestParticipant);
            Assert.AreEqual("https://www.test.com", result.ImageUrl);
            Assert.AreEqual("Test Description", result.Description);
            Assert.AreEqual(15, result.Id);          
        }

        [Test]
        public async Task GetDetailsForEventById_ShouldReturnNull()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.GetDetailsForEventById(16);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetEditFormById_ShouldReturnEventFormViewModel()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.GetEditEventFormAsync(15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EventFormViewModel>(result);
            Assert.AreEqual("Test Event", result.Name);
            Assert.AreEqual(100, result.Capacity);
            Assert.AreEqual("Test", result.GuestParticipant);
            Assert.AreEqual("https://www.test.com", result.ImageUrl);
            Assert.AreEqual("Test Description", result.Description);
            Assert.AreEqual(15, result.Id);
        }

        [Test]
        public async Task GetEditFormById_ShouldReturnNull()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description"
            });
            await repository.SaveChangesAsync();

            var result = await eventService.GetEditEventFormAsync(16);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLastThreeEvents_ShouldReturnModel()
        {
            await repository.AddAsync(new Event
            {
                Id = 15,
                Name = "Test Event1",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Event
            {
                Id = 16,
                Name = "Test Event2",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Event
            {
                Id = 17,
                Name = "Test Event",
                Capacity = 100,
                GuestParticipant = "Test3",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();

            var result = await eventService.GetLastThreeEventsAsync();
            var ev = await repository.GetById<Event>(15);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<EventIndexViewModel>>(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Test Event1", ev.Name);           
            Assert.AreEqual(15, ev.Id);
        }

        [Test]
        public async Task JoinEvent_ShouldAddEventParticipant()
        {
            await repository.AddAsync(new Event
            {
                Id = 19,
                Name = "Test Event1",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new EventParticipant
            {
                EventId = 19,
                ParticipantId = guid
            });
            await repository.SaveChangesAsync();

            await eventService.JoinEventAsync(15, guid);
            var eventParticipant = await repository.All<EventParticipant>().Where(x => x.EventId == 19 && x.ParticipantId == guid).FirstOrDefaultAsync();

            Assert.IsNotNull(eventParticipant);
            Assert.AreEqual(19, eventParticipant.EventId);
            Assert.AreEqual(guid, eventParticipant.ParticipantId);
            Assert.IsInstanceOf<EventParticipant>(eventParticipant);          
        }

        [Test]
        public async Task UserHasAlreadyJoinedEvent_ShouldReturnTrue()
        {
            await repository.AddAsync(new Event
            {
                Id = 19,
                Name = "Test Event1",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new EventParticipant
            {
                EventId = 19,
                ParticipantId = guid
            });
            await repository.SaveChangesAsync();

            var result = await eventService.UserHasAlreadyJoinedEvent(19, guid);
            var eventParticipant = await repository.All<EventParticipant>().Where(x => x.EventId == 19 && x.ParticipantId == guid).FirstOrDefaultAsync();

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
            Assert.IsNotNull(eventParticipant);
        }

        [Test]
        public async Task FilterEventsAsync_ShouldReturnModel()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new Event
            {
                Id = 19,
                Name = "Test Event1",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Event
            {
                Id = 20,
                Name = "Test Event2",
                Capacity = 100,
                GuestParticipant = "Test",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Event
            {
                Id = 21,
                Name = "Test Event3",
                Capacity = 100,
                GuestParticipant = "Test3",
                ImageUrl = "https://www.test.com",
                Description = "Test Description",
            });
            await repository.SaveChangesAsync();

           var result = await eventService.FilterEventsAsunc(guid, "", 3, 1);

            Assert.AreEqual(6, await repository.All<Event>().CountAsync());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<AllEventsShowcaseViewModel>(result);
            Assert.AreEqual(3, result.Events.Count());
            Assert.AreEqual(6, result.TotalEvents);
            Assert.AreEqual(1, result.CurrentPage);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}
