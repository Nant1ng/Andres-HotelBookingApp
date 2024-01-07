using LibraryAndService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Interface
{
    public interface IHardDelete
    {
        public void HardDelete(DbContextOptionsBuilder<ApplicationDbContext> options);
    }
}
