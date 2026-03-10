using Microsoft.EntityFrameworkCore;
using OpticalServer.Models;

namespace OpticalServer.Functions
{
    public class LevelReactions
    {
        private readonly DatabaseContext _db;

        public LevelReactions(DatabaseContext db) => _db = db;

        // add reactions related to levels here
        public async Task<LevelReaction> AddRating(long levelId, long userId, bool isLike)
        {
            var level = await _db.levels.FindAsync(levelId);
            var user = await _db.users.FindAsync(userId);

            if (level == null || user == null)
                return null;

            var existingReaction = await _db.level_reactions.FirstOrDefaultAsync(r => r.LevelId == levelId && r.UserId == userId);

            if (existingReaction != null)
            {
                if (existingReaction.ReactionType != (isLike ? ReactionType.like : ReactionType.dislike))
                {
                    existingReaction.ReactionType = isLike ? ReactionType.like : ReactionType.dislike;
                    await _db.SaveChangesAsync();

                    RuntimeFunctions.Request($"User {userId} changed reaction to '{existingReaction.ReactionType}' for level {level.LevelName} (ID: {level.LevelId})");
                    return existingReaction;
                }
                else
                {
                    _db.level_reactions.Remove(existingReaction);
                    await _db.SaveChangesAsync();

                    RuntimeFunctions.Request($"User {userId} removed reaction '{existingReaction.ReactionType}' from level {level.LevelName} (ID: {level.LevelId})");
                    return existingReaction;
                }
            }

            var reaction = new LevelReaction
            {
                LevelId = levelId,
                UserId = userId,
                ReactionType = isLike ? ReactionType.like : ReactionType.dislike
            };

            _db.level_reactions.Add(reaction);

            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"User {userId} added reaction '{reaction.ReactionType}' to level {level.LevelName} (ID: {level.LevelId})");

            return reaction;
        }
        public async Task<(int likes, int dislikes)> GetReactions (long levelId)
        {
            int likes = await _db.level_reactions.CountAsync(r => r.LevelId == levelId && r.ReactionType == ReactionType.like);
            int dislikes = await _db.level_reactions.CountAsync(r => r.LevelId == levelId && r.ReactionType == ReactionType.dislike);
            return (likes, dislikes);
        }

        public async Task<int> GetLikes(long levelId)
        {
            return await _db.level_reactions.CountAsync(r => r.LevelId == levelId && r.ReactionType == ReactionType.like);
        }
        public async Task<int> GetDislikes(long levelId)
        {
            return await _db.level_reactions.CountAsync(r => r.LevelId == levelId && r.ReactionType == ReactionType.dislike);
        }

        public async Task<ReactionType[]> GetAllReactions ()
        {
            ReactionType[] reactionTypes = await _db.level_reactions.Select(r => r.ReactionType).ToArrayAsync();
            return reactionTypes;
        }
    }
}