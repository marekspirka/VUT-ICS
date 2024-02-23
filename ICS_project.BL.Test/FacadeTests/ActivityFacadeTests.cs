using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using ICS_project.Common.Test;
using ICS_project.Common.Test.Seeds;
using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;


namespace ICS_project.BL.Tests.FacadeTests;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityDetailModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);
    }

    [Fact]
    public async Task NewActivity_Insert_ActivityAdded()
    {
        //Arrange
        var activity = new ActivityDetailModel
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        var activityGet = ActivityDetailModelMapper.MapToActivityDetailModel(activityFromDb);
        DeepAssert.Equal(activityGet.Id, activity.Id);
    }

    [Fact]
    public async Task NewActivity_FindActivity_ActivityFound()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        var activityGet = await _activityFacadeSUT.GetAsync(activity.Id);

        DeepAssert.Equal(activityGet.Id, activity.Id);
    }

    [Fact]
    public async Task NewActivity_ErrorInsert_Exception()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 08, 34, 50),
            End = new DateTime(2021, 03, 24, 03, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected start is after selected end.", exception.Message);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = default,
            Name = default!,
            Start = default!,
            End = default!,
            User = new UserDetailModel()
            {
                Id = default,
                Name = default!,
                Surname = default!,
            }
        };

        //Act
        var empty_activity = await _activityFacadeSUT.GetAsync(activity.Id);

        //Assert
        Assert.Null(empty_activity);
    }

    [Fact]
    public async Task CreateActivity_EditStart_EditedStart()
    {
        //Arange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Basketball",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2022, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Marek",
                Surname = "Klavesnica",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        activity.Start = new DateTime(2021, 03, 21, 03, 34, 50);

        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        var updated_activity = await _activityFacadeSUT.GetAsync(activity.Id);

        //Assert
        DeepAssert.Equal(updated_activity.Id, activity.Id);
        DeepAssert.Equal(updated_activity.Id, activity.Id);
        DeepAssert.Equal(updated_activity.Start, activity.Start);
    }

    [Fact]
    public async Task CreateActivity_ChangeName_ChangedName()
    {
        //Arange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Futbal",
            Start = new DateTime(2023, 01, 29, 12, 30, 00),
            End = new DateTime(2023, 01, 29, 14, 00, 00),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jozko",
                Surname = "Mrkvicka",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        activity.Name = "Hokej";

        activity = await _activityFacadeSUT.SaveAsync(activity, activity.User.Id);

        var updated_activity = await _activityFacadeSUT.GetAsync(activity.Id);

        //Assert
        DeepAssert.Equal(updated_activity.Id, activity.Id);
        DeepAssert.Equal(updated_activity.Name, activity.Name);
    }

    [Fact]
    public async Task TwoNewActivity_GetActivity_CorrectActivityFound()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 26, 03, 34, 50),
            End = new DateTime(2021, 03, 26, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity2 = await _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        var activityGet1 = await _activityFacadeSUT.GetAsync(activity1.Id);
        var activityGet2 = await _activityFacadeSUT.GetAsync(activity2.Id);

        DeepAssert.Equal(activity1.Id, activityGet1.Id);
        DeepAssert.Equal(activity1.Name, activityGet1.Name);

        DeepAssert.Equal(activity2.Id, activityGet2.Id);
        DeepAssert.Equal(activity2.Name, activityGet2.Name);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithSameTime_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task TwoNewActivity_UpdatedActivityWithSameTime_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 23, 03, 34, 50),
            End = new DateTime(2021, 03, 23, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity2 = await _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Act
        activity2.Start = new DateTime(2021, 03, 24, 03, 34, 50);
        activity2.End = new DateTime(2021, 03, 24, 08, 30, 50);
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithStartTime_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 04, 34, 50),
            End = new DateTime(2021, 03, 24, 10, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithEndException()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 01, 34, 50),
            End = new DateTime(2021, 03, 24, 04, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithDuringTime_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 04, 34, 50),
            End = new DateTime(2021, 03, 24, 06, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithLongerTime_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 02, 34, 50),
            End = new DateTime(2021, 03, 24, 10, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithSameEndTimeLikeStart_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 06, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 04, 34, 50),
            End = new DateTime(2021, 03, 24, 06, 34, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }

    [Fact]
    public async Task NewActivity_NewActivityWithSameStartTimeLikeEnd_Exception()
    {
        //Arrange
        var activity1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Programming",
            Start = new DateTime(2021, 03, 24, 03, 34, 50),
            End = new DateTime(2021, 03, 24, 08, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };
        activity1 = await _activityFacadeSUT.SaveAsync(activity1, activity1.User.Id);

        var activity2 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sleeping",
            Start = new DateTime(2021, 03, 24, 08, 30, 50),
            End = new DateTime(2021, 03, 24, 10, 30, 50),
            User = new UserDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
                Surname = "Hrncir",
            },
            Project = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Jindra",
            }
        };

        //Act
        Func<Task> act = () => _activityFacadeSUT.SaveAsync(activity2, activity2.User.Id);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);
        Assert.Equal("The selected time slot is already occupied by another activity.", exception.Message);
    }
}