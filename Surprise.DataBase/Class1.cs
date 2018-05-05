using System;
using Microsoft.EntityFrameworkCore;

namespace Surprise.Library
{
    public class Surprise_MessageData
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
    }
    public class Model : DbContext
    {
        public DbSet<Surprise_MessageData> Surprise_MessageDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source = Surprise_MessageData.db");
        }
    }
}