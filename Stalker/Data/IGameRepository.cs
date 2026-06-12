namespace Stalker.Data;

using Stalker.Models;

public interface IGameRepository
{
    Task<List<Game>> GetAllGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> AddGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task<Game> DeleteGameAsync(int? id);
    Task<Session> AddSessionAsync(Session session);
    Task<List<Session>> GetSessionsByGame(int gameId);
}
