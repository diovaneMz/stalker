namespace Stalker.Data;

using Microsoft.EntityFrameworkCore;
using Stalker.Models;

public class LocalRepository : IGameRepository
{
    private readonly AppDbContext _db;

    public LocalRepository(AppDbContext db) => _db = db;

    public async Task<List<Game>> GetAllGamesAsync()
    {
        List<Game> gamesList = await _db.Games.Include(g => g.Sessions).ToListAsync();
        
        return gamesList;
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    { 
        return await _db.Games
            .Include(g => g.Sessions)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    
    public async Task<Game> AddGameAsync(Game game)
    {
        await _db.Games.AddAsync(game);
        await _db.SaveChangesAsync();

        return game;
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _db.Games.Update(game);

        await _db.SaveChangesAsync();
        return game;
    }

    public async Task<Game> DeleteGameAsync(int? id)
    {
        var game = await _db.Games.Include(g => g.Sessions)
                       .FirstOrDefaultAsync(g => g.Id == id) 
                   ?? throw new KeyNotFoundException();
        
        _db.Sessions.RemoveRange(game.Sessions);
        _db.Games.Remove(game);
        
        await _db.SaveChangesAsync();
        return game;
    }

    public async Task<Session> AddSessionAsync(Session session)
    {
        await _db.Sessions.AddAsync(session);
                
        await _db.SaveChangesAsync();

        return session;
    }

    public async Task<List<Session>> GetSessionsByGame(int gameId)
    {
        List<Session> sessions = await _db.Sessions.Where(s => s.GameId == gameId).ToListAsync();
        
        return sessions;
    }
}