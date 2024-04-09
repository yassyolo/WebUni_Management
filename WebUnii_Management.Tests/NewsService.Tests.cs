using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.News;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUnii_Management.Tests
{
    [TestFixture]
    public class NewsServiceTests   
    {
        private INewsService newsService;
        private IRepository repository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "NewsService_Tests")
                .Options;

            dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddNewsAsync_ShouldAddNewsToDb()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });

            await repository.SaveChangesAsync();
            var news = await repository.GetById<NewsArticle>(4);
            Assert.AreEqual(1, await dbContext.NewsArticles.CountAsync());
            Assert.AreEqual("Test", news.Title);
            Assert.AreEqual("Test", news.Content);
            Assert.AreEqual("Test", news.ImageUrl);
            Assert.AreEqual(true, news.IsApproved);
            Assert.AreEqual(4, news.Id);
            Assert.IsNotNull(news);

        }

        [Test]
        public async Task AddNewsAsync_ShouldAddNewsByModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await newsService.AddNewsAsync(new NewsFormViewModel
            {
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                PublishedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            var news = await repository.GetById<NewsArticle>(1);

            Assert.AreEqual(1, await dbContext.NewsArticles.CountAsync());
            Assert.AreEqual("Test", news.Title);
            Assert.AreEqual("Test", news.Content);
            Assert.AreEqual("Test", news.ImageUrl);
            Assert.AreEqual(true, news.IsApproved);
            Assert.AreEqual(1, news.Id);
            Assert.IsNotNull(news);
            Assert.IsInstanceOf<NewsArticle>(news);

        }
        public async Task ApproveNews_ShouldApproveNewsArticleInTheDb()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = false
            });

            await repository.SaveChangesAsync();
            await newsService.ApproveNewsArticleAsync(4);

            var news = await repository.GetById<NewsArticle>(4);

            Assert.AreEqual(true, news.IsApproved);
            Assert.IsNotNull(news);
            Assert.IsInstanceOf<NewsArticle>(news);
            Assert.AreEqual(4, news.Id);
        }

        [Test]
        public async Task DeleteNews_ShouldDeleteNewsArticleFromDb()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });

            await repository.SaveChangesAsync();
            await newsService.DeleteNewsArticleAsync(4);

            var news = await repository.GetById<NewsArticle>(4);

            Assert.IsNull(news);
            Assert.AreEqual(0, await dbContext.NewsArticles.CountAsync());
            
        }
        [Test]
        public async Task DiscardNews_ShouldDeleteNews()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = false
            });

            await repository.SaveChangesAsync();
            await newsService.DiscardNewsArticleAsync(4);

            var news = await repository.GetById<NewsArticle>(4);
            Assert.IsNull(news);
            Assert.AreEqual(0, await dbContext.NewsArticles.CountAsync());
            
        }

        [Test]
        public async Task EditNews_ShouldEditNews()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });

            await repository.SaveChangesAsync();
            await newsService.EditNewsAsync(4, new NewsFormViewModel
            {
                Title = "Test2",
                Content = "Test2",
                ImageUrl = "Test2",
                PublishedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            var news = await repository.GetById<NewsArticle>(4);
            Assert.IsNotNull(news);
            Assert.AreEqual("Test2", news.Title);
            Assert.AreEqual("Test2", news.Content);
            Assert.AreEqual("Test2", news.ImageUrl);
            Assert.AreEqual(4, news.Id);
            Assert.AreEqual(true, news.IsApproved);
            Assert.IsInstanceOf<NewsArticle>(news);
        }
        [Test]
        public async Task ExistsById_ShouldReturnTrue()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var result = await newsService.ExistByIdAsync(4);  

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
            
        }
        [Test]
        public async Task GetEditNewsFormAsync_ShouldReturnModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var result = await newsService.GetEditNewsFormAsync(4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NewsFormViewModel>(result);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual("Test", result.Content);
            Assert.AreEqual("Test", result.ImageUrl);
            Assert.AreEqual(4, result.Id);
 
        }
        [Test]
        public async Task GetNewsArticleDetailsById_ShouldReturnModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new NewsArticleReadStatus
            {
                NewsArticleId = 4,
                ReaderId = guid,
            });
            await repository.SaveChangesAsync();
            var result = await newsService.GetNewsArticleDetailsById(4, guid);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NewsDetailsViewModel>(result);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual("Test", result.Content);
            Assert.AreEqual("Test", result.ImageUrl);
            Assert.AreEqual(4, result.Id);
        }
        [Test]
        public async Task GetNewsArticleDetailsById_ShouldChangeReadStatusForuser()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new NewsArticleReadStatus
            {
                NewsArticleId = 4,
                ReaderId = guid,
            });
            await repository.SaveChangesAsync();
            await newsService.GetNewsArticleDetailsById(4, guid);
            var readStatus = await repository.All<NewsArticleReadStatus>().Where(x=> x.NewsArticleId == 4).FirstOrDefaultAsync();

            Assert.IsNotNull(readStatus);
            Assert.IsInstanceOf<NewsArticleReadStatus>(readStatus);
            Assert.AreEqual(4, readStatus.NewsArticleId);
            Assert.AreEqual(guid, readStatus.ReaderId);
        }
        /*[Test]
        public async Task WriteNewsAsync_ShouldAddnewsToDb()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            var guid = Guid.NewGuid().ToString();
            await newsService.WriteNewsAsync(guid, new NewsFormViewModel
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                PublishedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            /*await repository.AddAsync(new Student            
            {
                Id = 2,
                UserId = guid,
            });
            await repository.SaveChangesAsync();*/

            /*var news = await repository.GetById<NewsArticle>(4);
            Assert.IsNotNull(news);
            Assert.IsInstanceOf<NewsArticle>(news);
            Assert.AreEqual("Test", news.Title);
            Assert.AreEqual("Test", news.Content);
            Assert.AreEqual("Test", news.ImageUrl);
            Assert.AreEqual(4, news.Id);
            Assert.AreEqual(false, news.IsApproved);
            Assert.AreEqual(2, news.AuthorId);
        }*/
        [Test]
        public async Task GetNewsForApprovalAsync_ShouldReturnModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = false
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new NewsArticle
            {
                Id = 5,
                Title = "Test2",
                Content = "Test2",
                ImageUrl = "Test2",
                IsApproved = false
            });

            await repository.SaveChangesAsync();
            var result = await newsService.GetNewsForApprovalAsync(2, 1);
            var news = await repository.GetById<NewsArticle>(4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ApproveNewsViewModel>(result);
            Assert.AreEqual(2, result.News.Count());
            Assert.AreEqual(4, result.News.FirstOrDefault().Id);
            Assert.AreEqual("Test", news.Title);
        }
        [Test]
        public async Task GetLastThreeNewsArticlesAsync_ShouldReturnModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new NewsArticle
            {
                Id = 5,
                Title = "Test2",
                Content = "Test2",
                ImageUrl = "Test2",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new NewsArticle
            {
                Id = 6,
                Title = "Test3",
                Content = "Test3",
                ImageUrl = "Test3",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var result = await newsService.GetLastThreeNewsArticlesAsync();
            var news = await repository.GetById<NewsArticle>(4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<NewsArticleIndexViewModel>>(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(4, news.Id);
            Assert.AreEqual("Test", news.Title);
        }
        [Test]
        public async Task FilterNewsAsync_ShouldReturnAllNewsModel()
        {
            repository = new Repository(dbContext);
            newsService = new NewsService(repository);

            await repository.AddAsync(new NewsArticle
            {
                Id = 4,
                Title = "Test",
                Content = "Test",
                ImageUrl = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new NewsArticle
            {
                Id = 5,
                Title = "Test2",
                Content = "Test2",
                ImageUrl = "Test2",
                IsApproved = true
            });
            await repository.SaveChangesAsync();

           var result=  await newsService.FilterNewsAsync(null, null, null, 1, 2);

            var news = await repository.GetById<NewsArticle>(4);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NewsShowcaseViewModel>(result);
            Assert.AreEqual(2, result.News.Count());
            Assert.AreEqual(4, news.Id);
            Assert.AreEqual("Test", news.Title);
            


        }
    }
}
