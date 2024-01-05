using LibraryAndService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Interface
{
    public interface IEntityManager
    {
        void Create(DbContextOptionsBuilder<ApplicationDbContext> options);
        void GetOne(DbContextOptionsBuilder<ApplicationDbContext> options);
        void GetAll(DbContextOptionsBuilder<ApplicationDbContext> options);
        void Update(DbContextOptionsBuilder<ApplicationDbContext> options);
        void Delete(DbContextOptionsBuilder<ApplicationDbContext> options);
        void Recover(DbContextOptionsBuilder<ApplicationDbContext> options);
    }
}
