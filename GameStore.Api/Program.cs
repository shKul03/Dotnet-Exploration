using GameStore.Api.Dtos;

const string EndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
int a = 10;
app.MapGet("/", () => $"Hello World! This be Mintu the great. The value of a is {a}");
app.MapGet("/dummy", ()=> "This is a dummy endpoint haha huhu ehehe");


List<GameDto> games = [
    new(1, "Call of Duty", "Action", 59.99m, new DateOnly(2020, 11, 5)),
    new(2, "FIFA 21", "Sports", 49.99m, new DateOnly(2021, 9, 10)),
    new(3, "The Witcher 3", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
    new(4, "Fortnite", "Action", 29.99m, new DateOnly(2018, 10, 19))
];


app.MapGet("/games", () => games);
app.MapGet("/first-game", () => games[0]);
app.MapGet("/one-game/{id}", (int id) => games.Find(game => game.Id == id));


app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(EndpointName);
app.MapPost("/new-game", (CreateGameDto newGame) => {
    
    GameDto game = new(
        games.Count + 1,    
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(EndpointName, new {id = game.Id}, game);
});


// app.MapPut("/games/update-game/{id}");












app.Run();