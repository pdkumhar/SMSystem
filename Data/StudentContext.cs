using Microsoft.EntityFrameworkCore;
using SMSystem.Models;

namespace SMSystem.Data
{
    public class StudentContext:DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
