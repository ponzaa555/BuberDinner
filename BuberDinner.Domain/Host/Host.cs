using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.DinnerAggregate.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.User.ValueObjects;

namespace BuberDinner.Domain.Host;

public sealed class Host : AggregateRoot<HostId>
{
    private readonly List<MenuId> _menuIds = new();
    private readonly List<DinnerId> _dinnerIds = new();
    public string FirstName {get; }
    public string LastName {get; }
    public string ProfileImage {get; }
    public AverageRating AverageRating {get; }
    public UserId UserId {get; }
    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public DateTime CreatedDateTime {get;}
    public DateTime LastUpdatedDateTime {get;}

    private Host(
        HostId hostId,
        string firstName,
        string lastName,
        string profileImage,
        AverageRating averageRating,
        UserId userId,
        DateTime createdDateTime,
        DateTime lastUpdatedDateTime
    ):base(hostId)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileImage = profileImage;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        LastUpdatedDateTime = lastUpdatedDateTime;
        AverageRating = averageRating;
    }
    public static Host Create(
        string firstName,
        string lastName,
        string profileImage,
        UserId userId
    )
    {
        return new Host(
            HostId.CreateUnique(),
            firstName,
            lastName,
            profileImage,
            AverageRating.CreateNew(), 
            userId,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}