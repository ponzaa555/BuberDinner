# Domain Model

## Menu

```csharp
class Menu
{
    Menu Create();
    void AddDiner(Diner diner);
    void RemoveDiner(Diner diner);
    void UpdateSection(MenuSection section);
}
```

```json
    "id":"000000-0000-0000-0000-000000000001",
    "name":"Yummy menu",
    "description":"A menu with yummy food",
    "averageRating":4.5,
    "sections": [
        {
            "id": "000000-0000-0000-0000-000000000001",
            "name":"Appetizers",
            "description":"A section with appetizers",
            "items": [
                {
                    "id":"000000-0000-0000-0000-000000000001",
                    "name":"Spring Rolls",
                    "description":"Crispy spring rolls with dipping sauce",
                    "price": 5.99,
                    "allergens": ["gluten", "soy"]
                },
                {
                    "id":"000000-0000-0000-0000-000000000002",
                    "name":"Garlic Bread",
                    "description":"Toasted garlic bread with herbs",
                    "price": 3.99,
                    "allergens": ["gluten", "dairy"]
                }
            ]
        }
    ],
    "createdDateTime":"2023-10-01T12:00:00Z",
    "lastUpdatedDateTime":"2023-10-01T12:00:00Z",
    "hostId":"000000-0000-0000-0000-000000000001",
    "dinnerIds" : [
        "000000-0000-0000-0000-000000000001",
        "000000-0000-0000-0000-000000000002"
    ],
    "menuReviewIds" : [
        "000000-0000-0000-0000-000000000001",
        "000000-0000-0000-0000-000000000002"
    ],
```