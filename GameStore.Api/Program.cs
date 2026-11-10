using GameStore.Api.Dtos;

const string GetGameByIdEndpoint = "GetGameById";

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "Super Mario Bros.", "Platformer", 29.99M, new DateOnly(1985, 9, 13)),
    new (3, "The Legend of Zelda", "Action-Adventure", 39.99M, new DateOnly(1986, 2, 21))
];

app.MapGet("/", () => "Hello World!");
// GET /games
app.MapGet("/games", () => games);


// GET /games/{id}
app.MapGet("/games/{id}", (int id) => games.FirstOrDefault(g => g.Id == id))
    .WithName(GetGameByIdEndpoint);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameByIdEndpoint, new { id = game.Id }, game);
});

// PUT /games/{id}
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(g => g.Id == id);

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(g => g.Id == id);

    return Results.NoContent();
});

app.Run();
