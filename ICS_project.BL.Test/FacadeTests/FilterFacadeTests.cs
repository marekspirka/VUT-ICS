using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ICS_project.BL.Tests.FacadeTests;

public class FilterFacadeTests : FacadeTestsBase
{
    private readonly IFilterFacade _filterFacadeSUT;
    private readonly IActivityFacade _activityFacadeSUT;
    private readonly ActivityDetailModel _activity1;
    private readonly ActivityDetailModel _activity2;
    private readonly ActivityDetailModel _activity3;


    public FilterFacadeTests(ITestOutputHelper output) : base(output)
    {
        _filterFacadeSUT = new FilterFacade(UnitOfWorkFactory, FilterModelMapper);
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityDetailModelMapper);
        _activity1 = new ActivityDetailModel()
        {
            Id = Guid.Parse("0c0381dd-0ee7-4dac-8767-45f5b1cc693e"),
            Name = "Programming",
            Start = new DateTime(2021, 01, 01, 03, 34, 50),
            End = new DateTime(2021, 01, 01, 04, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Zbynek",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        _activity2 = new ActivityDetailModel()
        {
            Id = Guid.Parse("67bb8e81-617e-4782-81f9-436e4020f228"),
            Name = "Coding",
            Start = new DateTime(2021, 01, 01, 04, 34, 50),
            End = new DateTime(2021, 01, 01, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Zbynek",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        _activity3 = new ActivityDetailModel()
        {
            Id = Guid.Parse("d0c0381c-c5e5-4460-965c-a93c87565684"),
            Name = "Writing",
            Start = new DateTime(2021, 01, 02, 03, 34, 50),
            End = new DateTime(2021, 01, 02, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Zbynek",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
    }

    [Fact]
    public async Task FilterActivityByDate()
    {
        //Arrange
        await _activityFacadeSUT.SaveAsync(_activity1, _activity1.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity2, _activity2.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity3, _activity3.User.Id);

        var dateStart = new DateTime(2021, 01, 01, 00, 00, 00);
        var dateEnd = new DateTime(2021, 01, 02, 20, 50, 50);

        //Act
        var filtered = _filterFacadeSUT.GetByEverything(_activity1.User.Id, dateStart, dateEnd, null, null);

        var filteredStarts = new List<DateTime>();
        var filteredEnds = new List<DateTime>();

        foreach (var filterModel in filtered)
        {
            filteredStarts.Add(filterModel.Start);
            filteredEnds.Add(filterModel.End);
        }

        //Assert
        Assert.True(filteredStarts.All(start => start >= dateStart));
        Assert.True(filteredEnds.All(end => end <= dateEnd));
    }

    [Fact]
    public async Task FilterActivityFromDate()
    {
        //Arrange
        await _activityFacadeSUT.SaveAsync(_activity1, _activity1.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity2, _activity2.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity3, _activity3.User.Id);

        var dateStart = new DateTime(2021, 01, 01, 00, 00, 00);

        //Act
        var filtered = _filterFacadeSUT.GetByEverything(_activity1.User.Id, dateStart, null, null, null);

        var filteredStarts = filtered.Select(filterModel => filterModel.Start).ToList();

        //Assert
        Assert.True(filteredStarts.All(start => start >= dateStart));
    }

    [Fact]
    public async Task FilterActivityToDate()
    {
        //Arrange
        await _activityFacadeSUT.SaveAsync(_activity1, _activity1.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity2, _activity2.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity3, _activity3.User.Id);

        var dateEnd = new DateTime(2021, 01, 02, 20, 50, 50);

        //Act
        var filtered = _filterFacadeSUT.GetByEverything(_activity1.User.Id, null, dateEnd, null, null);

        var filteredEnds = filtered.Select(filterModel => filterModel.End).ToList();

        //Assert
        Assert.True(filteredEnds.All(end => end <= dateEnd));
    }

    [Fact]
    public async Task FilterActivityByDay()
    {
        //Arrange
        await _activityFacadeSUT.SaveAsync(_activity1, _activity1.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity2, _activity2.User.Id);
        await _activityFacadeSUT.SaveAsync(_activity3, _activity3.User.Id);

        var day = new DateTime(2021, 01, 02, 20, 50, 50);

        //Act
        var filtered = _filterFacadeSUT.GetByEverything(_activity1.User.Id, day, null, null, null);

        var filteredEnds = filtered.Select(filterModel => filterModel.End).ToList();

        //Assert
        Assert.True(filteredEnds.All(activity => activity.Date.Day == day.Day));
    }
}