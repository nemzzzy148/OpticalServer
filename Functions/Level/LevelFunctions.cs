using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OpticalServer.Models;

namespace OpticalServer.Functions
{
    public class LevelFunctions
    {
        private readonly DatabaseContext _db;

        public LevelFunctions(DatabaseContext db) => _db = db;

        public async Task<int> GetLevelViews(long levelId)
        {
            var level = await _db.levels.FindAsync(levelId);
            if (level == null)
                return 0;
            
            return level.Views;
        }
        public async Task<Level> CreateLevel(string name, long ownerId)
        {
            var existingLevel = await _db.levels.FirstOrDefaultAsync(l => l.LevelName == name);

            if (existingLevel != null)
                return null;

            var level = new Level
            {
                LevelName = name,
                OwnerId = ownerId,
                Path = "pending"
            };

            _db.levels.Add(level);

            await _db.SaveChangesAsync();

            level.Path = $"{level.LevelId}.json";

            File.WriteAllText(Path.Combine(Configurations.LevelDataPath, level.Path), Configurations.LevelExampleData);

            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"Created level {level.LevelName} with ID {level.LevelId} by user {ownerId}");

            return level;
        }
        public async Task<List<Level>> GetLevelList()
        {
            RuntimeFunctions.Request($"Fetching level list");
            return await _db.levels.ToListAsync();
        }
        public async Task<Level> EditLevel(long levelId, JsonElement data)
        {
            var level = await _db.levels.FindAsync(levelId);

            if (level == null)
                return null;

            File.WriteAllText(Path.Combine(Configurations.LevelDataPath, level.Path), data.GetRawText());

            RuntimeFunctions.Request($"Edited level {level.LevelName} with ID {level.LevelId}");

            return level;
        }
        public async Task<string> GetLevelData (long levelId, bool incrementViews = true)
        {
            var level = await _db.levels.FindAsync(levelId);

            if (level == null)
            {
                RuntimeFunctions.Request($"Failed to fetch level with ID {levelId} - not found", true);
                return null;
            }

            if (incrementViews)
            {
                level.Views++;
                await _db.SaveChangesAsync();
            }

            string data = File.ReadAllText(Path.Combine(Configurations.LevelDataPath, level.Path));

            RuntimeFunctions.Request($"Fetched level {level.LevelName} data with ID {level.LevelId}");

            return data;
        }
        public async Task<Level> DeleteLevel(long levelId)
        {
            var level = await _db.levels.FindAsync(levelId);

            if (level == null)
                return null;

            _db.levels.Remove(level);
            await _db.SaveChangesAsync();

            File.Delete(Path.Combine(Configurations.LevelDataPath, level.Path));

            RuntimeFunctions.Request($"Deleted level {level.LevelName} with ID {level.LevelId}");

            return level;
        }
        public async Task<Level> EditLevelName(long levelId, string newName)
        {
            var level = await _db.levels.FindAsync(levelId);

            if (level == null)
                return null;

            level.LevelName = newName;
            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"Changed name of level with ID {level.LevelId} to {newName}");

            return level;
        }
    }
}