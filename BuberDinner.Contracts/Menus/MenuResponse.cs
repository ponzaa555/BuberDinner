
using BuberDinner.Contracts.Menus;

public record MenuResponse(
    string Id,
    string Name,
    string Description,
    float AverageRating,
    List<MenuSectionResponse> Sections,
    string HostId,
    List<string> DinerIds,
    List<string> MenuReviewsIds,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record MenuSectionResponse(
    string Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items
);

public record MenuItemResponse(
    string Id,
    string Name,
    string Description
);


     