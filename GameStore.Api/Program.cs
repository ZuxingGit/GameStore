using GameStore.Api.Dtos;

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
app.MapGet("/games/{id}", (int id) => games.FirstOrDefault(g => g.Id == id));

app.Run();
