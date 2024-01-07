using LibraryAndService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Interface
{
    public interface IRecover
    {
        void Recover(DbContextOptionsBuilder<ApplicationDbContext> options);
    }
}
