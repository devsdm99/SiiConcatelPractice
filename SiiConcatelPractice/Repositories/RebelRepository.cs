using SiiConcatelPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Repositorys
{
    public class RebelRepository : IRebelRepository
    {
        private readonly RebelDbContext _dbContext;

        public RebelRepository(RebelDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IList<Rebel> GetAllRebels()
        {
            return _dbContext.Rebels.ToList();
        }

        public Rebel GetRebelById(int id)
        {
            Rebel rebel = _dbContext.Rebels.Where(x => x.Id == id).FirstOrDefault();
            return rebel;
        }

        public void AddRebels(List<Rebel> rebels)
        {     
            _dbContext.Rebels.AddRange(rebels);
            _dbContext.SaveChanges();
        }

        public void UpdateRebel(Rebel rebel)
        {
            _dbContext.Update(rebel);
            _dbContext.SaveChanges();
        }

        public void DeleteRebel(Rebel rebel)
        {
            _dbContext.Remove(rebel);
            _dbContext.SaveChanges();
        }
    }
}
