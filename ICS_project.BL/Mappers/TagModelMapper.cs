using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class TagModelMapper : ITagModelMapper
{
    public TagModel MapToTagModel(TagEntity? entity)
        => entity is null
            ? TagModel.Empty
            : new TagModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

    public TagEntity MapToTagEntity(TagModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
        };

    public IEnumerable<TagModel> MapToTagListModel(IEnumerable<TagEntity> entities)
    => entities.Select(MapToTagModelFromList);

    public TagModel MapToTagModelFromList(TagEntity? entity)
    => entity is null
        ? TagModel.Empty
        : new TagModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
}