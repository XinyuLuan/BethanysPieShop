using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    // intermedia with code and our data.
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        public PieRepository(AppDbContext appDbContext)
        // the parameter AppDbContext appDbContext is added in the DI container, so can directly use.
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            // in the dbContext, it will Call the Pie and sent query to SQL server, ask for the Pies DbSet
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PieOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
                                                                        //  p.IsPieOfTheWeek == true
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
