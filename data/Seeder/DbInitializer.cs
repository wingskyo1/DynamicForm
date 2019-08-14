using System.Threading.Tasks;

namespace data.Seeder
{
    public class DbInitializer
    {
        private static ProjectContext _context;

        public DbInitializer(ProjectContext context) { _context = context; }

        public async Task SeedSync()
        {
            
            // add Role
            
            // add User
            
            await _context.SaveChangesAsync();
            
        }
    }
}
