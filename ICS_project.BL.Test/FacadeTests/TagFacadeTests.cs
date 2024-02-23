using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using ICS_project.Common.Test;
using ICS_project.Common.Test.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;


namespace ICS_project.BL.Tests.FacadeTests;

public class TagFacadeTests : FacadeTestsBase
{
    private readonly ITagFacade _tagFacadeSUT;

    public TagFacadeTests(ITestOutputHelper output) : base(output)
    {
        _tagFacadeSUT = new TagFacade(UnitOfWorkFactory, TagModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var tag = new TagModel()
        {
            Id = Guid.Empty,
            Name = "ICS",
        };

        tag = await _tagFacadeSUT.SaveAsync(tag);
    }

    [Fact]
    public async Task NewTag_Insert_TagAdded()
    {
        var tag = new TagModel()
        {
            Id = Guid.NewGuid(),
            Name = "ICS",
        };

        tag = await _tagFacadeSUT.SaveAsync(tag);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tag.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToTagModel(tagFromDb));
    }

    [Fact]
    public async Task NewTag_FindTag_TagFound()
    {
        var tag = new TagModel()
        {
            Id = Guid.NewGuid(),
            Name = "FreeTime",
        };
        tag = await _tagFacadeSUT.SaveAsync(tag);

        var tagGet = await _tagFacadeSUT.GetAsync(tag.Id);
        DeepAssert.Equal(tag, tagGet);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var tag = new TagModel()
        {
            Id = default,
            Name = default!,
        };

        tag = await _tagFacadeSUT.GetAsync(tag.Id);
        Assert.Null(tag);
    }

    [Fact]
    public async Task CreateTag_DeleteTagById_DeletedTag()
    {
        var tag = new TagModel()
        {
            Id = Guid.NewGuid(),
            Name = "ICS",
        };
        tag = await _tagFacadeSUT.SaveAsync(tag);
        await _tagFacadeSUT.DeleteAsync(tag.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tag.AnyAsync(i => i.Id == tag.Id));
    }

    [Fact]
    public async Task CreateTag_UpdateTag_UpdatedTag()
    {
        //Arrange
        var tag = new TagModel()
        {
            Id = Guid.Empty,
            Name = "ICS",
        };

        //Act
        tag = await _tagFacadeSUT.SaveAsync(tag);

        tag.Name += "-project";

        tag = await _tagFacadeSUT.SaveAsync(tag);

        var updated_tag = await _tagFacadeSUT.GetAsync(tag.Id);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tag.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(updated_tag.Name, "ICS-project");
    }

}