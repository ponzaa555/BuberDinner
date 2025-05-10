using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.DinnerAggregate.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new(); 
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();
    public string Name {get;}
    public string Description {get;}
    public AverageRating AverageRating {get;}
    public IReadOnlyList<MenuSection> Section => _sections.AsReadOnly();
    public HostId HostId {get;}
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public DateTime CreatedDateTime {get;}
    public DateTime LastUpdatedDateTime {get;}

    private Menu(
        MenuId menuId,
        string name,
        string description,
        AverageRating averageRating,
        HostId hostId,
        List<MenuSection> sections
        ) : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        AverageRating = averageRating;
        _sections = sections;
    }
    public static Menu Create(
        string name,
        string description,
        HostId hostId,
        List<MenuSection> sections)
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            AverageRating.CreateNew(),
            hostId,
            sections ?? new()
        );
    }
}