using Microsoft.EntityFrameworkCore;

namespace BDD_Trello.Models
{
    public class Repository<T> where T : class
    {
        private ProjetContext _db;
        private DbSet<T> _table;
        public Repository(ProjetContext db)
        {
            _db = db;
            _table = db.Set<T>();
            System.Console.WriteLine("Repository created.");
        }

        public void AddEntity(T entity)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _table.Add(entity);
                    _db.SaveChanges();
                    transaction.Commit();           
                    System.Console.WriteLine("Add Done");       
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void DeleteEntity(T entity)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _table.Remove(entity);
                    _db.SaveChanges();
                    transaction.Commit();         
                    System.Console.WriteLine("Delete succeed");      
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

       public void UpdateEntity()
       {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
       } 

        public T ReadEntity(int ID)
        {
            try
            {
                return _table.Find(ID);              
            }
            catch (Exception)
            {
                System.Console.WriteLine("Cet ID est introuvable...");
                return null;
            }
        }    
    }
}