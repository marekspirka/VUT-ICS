// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

using ICS_project.Common.Test.Seeds;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Factories;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace ICS_project.DAL.Test
{
    public class RepositoryTests
    {
        private IDbContextFactory<ICS_projectDbContext> DbContextFactory;
        private readonly ICS_projectDbContext _dbContextSUT;
        private Repository<ActivityEntity>? testedRepository;

        private readonly UserEntity _user;
        private readonly ProjectEntity _project;
        private readonly ActivityEntity _activity1;
        private readonly ActivityEntity _activity2;

        public RepositoryTests()
        {
            DbContextFactory = new DbContextSqLiteFactory("RepositoryTestDb.db", false);
            _dbContextSUT = DbContextFactory.CreateDbContext();

            _user = new UserEntity()
            {
                Id = Guid.Parse("1d4fa150-bd80-4d46-a511-4c666166ec5e"),
                Activities = new List<ActivityEntity>(),
                Name = "John",
                Surname = "Genericson",
                Projects = new List<ProjectUserEntity>(),
            };

            _project = new ProjectEntity()
            {
                Id = Guid.Parse(input: "97c7dab9-6e0a-4da8-8af7-986e15b2d7e7"),
                Name = "IPP",
            };

            _activity1 = new ActivityEntity()
            {
                Id = Guid.Parse("06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
                Name = "Football",
                Start = new DateTime(2023, 01, 25, 12, 34, 59),
                End = new DateTime(2023, 01, 25, 12, 54, 59),
                Description = "My first sport.",
                UserId = _user.Id,
                User = _user,
                ProjectId = Guid.Empty,
                Project = _project,
                Tags = new List<TagActivityEntity>(),
            };

            _activity2 = new ActivityEntity()
            {
                Id = Guid.Parse("17a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
                Name = "Football",
                Start = new DateTime(2023, 01, 25, 12, 34, 59),
                End = new DateTime(2023, 01, 25, 12, 54, 59),
                Description = "My first sport.",
                UserId = _user.Id,
                User = _user,
                ProjectId = Guid.Empty,
                Project = _project,
                Tags = new List<TagActivityEntity>(),
            };
        }

        [Fact]
        public async Task GetTest()
        {
            //Arrange
            await _dbContextSUT.Database.EnsureDeletedAsync();
            await _dbContextSUT.Database.EnsureCreatedAsync();

            //Act
            _dbContextSUT.Users.Add(_user);
            _dbContextSUT.Activities.Add(_activity1);
            _dbContextSUT.Projects.Add(_project);
            await _dbContextSUT.SaveChangesAsync();


            testedRepository = new Repository<ActivityEntity>(
                _dbContextSUT, new ActivityEntityMapper());

            //Assert
            var activities = testedRepository.Get();
            Assert.Equal(_activity1, await activities.SingleAsync(i => i.Id == _activity1.Id));
            Assert.NotEqual(_activity2, await activities.SingleAsync(i => i.Id == _activity1.Id));
            Assert.False(await activities.AnyAsync(i => i.Id == _activity2.Id));

            await _dbContextSUT.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task ExistsAsyncTest()
        {
            //Arrange
            await _dbContextSUT.Database.EnsureDeletedAsync();
            await _dbContextSUT.Database.EnsureCreatedAsync();

            //Act
            _dbContextSUT.Users.Add(_user);
            _dbContextSUT.Activities.Add(_activity1);
            await _dbContextSUT.SaveChangesAsync();

            testedRepository = new Repository<ActivityEntity>(
                _dbContextSUT, new ActivityEntityMapper());

            //Assert
            var activityExists = await testedRepository.ExistsAsync(_activity1);
            Assert.True(activityExists);
            activityExists = await testedRepository.ExistsAsync(_activity2);
            Assert.False(activityExists);

            await _dbContextSUT.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task InsertDeleteAsyncTest()
        {
            //Arrange
            await _dbContextSUT.Database.EnsureDeletedAsync();
            await _dbContextSUT.Database.EnsureCreatedAsync();

            //Act
            _dbContextSUT.Users.Add(_user);
            _dbContextSUT.Activities.Add(_activity1);
            await _dbContextSUT.SaveChangesAsync();

            testedRepository = new Repository<ActivityEntity>(
            _dbContextSUT, new ActivityEntityMapper());

            await testedRepository.InsertAsync(_activity2);
            await _dbContextSUT.SaveChangesAsync();

            //Assert
            Assert.True(await testedRepository.ExistsAsync(_activity2));
            testedRepository.Delete(_activity2.Id);
            await _dbContextSUT.SaveChangesAsync();
            Assert.False(await testedRepository.ExistsAsync(_activity2));

            await _dbContextSUT.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UpdateAsyncTest()
        {
            //Arrange
            await _dbContextSUT.Database.EnsureDeletedAsync();
            await _dbContextSUT.Database.EnsureCreatedAsync();

            //Act
            _dbContextSUT.Users.Add(_user);
            _dbContextSUT.Activities.Add(_activity1);
            await _dbContextSUT.SaveChangesAsync();

            testedRepository = new Repository<ActivityEntity>(
                _dbContextSUT, new ActivityEntityMapper());

            var newActivity = new ActivityEntity()
            {
                Id = Guid.Parse("06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
                Name = "Basketball",
                Start = new DateTime(2023, 01, 25, 12, 34, 59),
                End = new DateTime(2023, 01, 25, 12, 54, 59),
                Description = "My first sport.",
                UserId = _user.Id,
                User = _user,
                Project = null,
                Tags = new List<TagActivityEntity>(),
            };
            newActivity.Name = "Basketball";
            await testedRepository.UpdateAsync(newActivity);
            await _dbContextSUT.SaveChangesAsync();
            var activities = testedRepository.Get();
            var updated = await activities.SingleAsync(i => i.Id == _activity1.Id);

            //Assert
            Assert.NotEqual("Football", updated.Name);
            Assert.Equal(newActivity.Name, updated.Name);
            Assert.Equal(_activity1.Id, updated.Id);

            await _dbContextSUT.Database.EnsureDeletedAsync();
        }
    }
}
