using SiiConcatelPractice.Models;
using System.Collections.Generic;

namespace SiiConcatelPractice.Repositorys
{
    public interface IRebelRepository
    {

        IList<Rebel> GetAllRebels();
        Rebel GetRebelById(int id);
        void AddRebels(List<Rebel> rebels);
        void UpdateRebel(Rebel id);
        void DeleteRebel(Rebel rebel);
    }
}