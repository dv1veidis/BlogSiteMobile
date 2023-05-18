using BlogsiteMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlogsiteMobile.Services
{
    public class ReactionRepository
    {
        private readonly SQLiteConnection _connection;

        public ReactionRepository()
        {
            _connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Reactions.db3"));
            _connection.CreateTable<Reaction>();
        }
        public ReactionRepository(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Reaction>();
        }
        public int AddReaction(Reaction reaction)
        {
            return _connection.Insert(reaction);
        }

        public List<Reaction> GetAll(string? includeProperties = null)
        {
            return _connection.Table<Reaction>().ToList();

        }
        public  List<Reaction> GetAllFromId(int id)
        {
            TableQuery<Reaction> query = null;
            query = _connection.Table<Reaction>().Where(u => u.BlogPostId == id);
            List<Reaction> matchingReactions = query.ToList();

            return matchingReactions;
        }

        public Reaction GetFirstOrDefault(int id, int ApplicationUserId)
        {
            Reaction query = _connection.Table<Reaction>().FirstOrDefault(u => u.BlogPostId == id && u.ApplicationUserId == ApplicationUserId);


            return query;
        }

        public int Remove(Reaction entity)
        {
            return _connection.Table<Reaction>().Delete(u => u.Id == entity.Id);
        }

        public void Update(Reaction obj)
        {
            var objFromDb = App.reactionRepository.GetFirstOrDefault(obj.BlogPostId, obj.ApplicationUserId);

            if (objFromDb != null)
            {
                objFromDb.Action = obj.Action;
                _connection.Update(objFromDb);
            }
        }
    }
}
