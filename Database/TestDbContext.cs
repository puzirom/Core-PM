using System;
using Microsoft.EntityFrameworkCore;
using Entities.Enums;
using Entities.Structure;

namespace Database
{
    public class TestDbContext : DbContext
    {
        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<Reference> References { get; set; }

        public virtual DbSet<DocumentTypeItem> DocumentTypes { get; set; }

        public virtual DbSet<ReferenceTypeItem> ReferenceTypes { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert DocumentType enum into lookup table DocumentTypes
            ConvertEnumToLookupTable<DocumentTypeItem>(modelBuilder, typeof(DocumentType));
            // Convert ReferenceType enum into lookup table ReferenceTypes
            ConvertEnumToLookupTable<ReferenceTypeItem>(modelBuilder, typeof(ReferenceType));
        }

        private static void ConvertEnumToLookupTable<T>(ModelBuilder modelBuilder, Type enumType) where T : EnumTypeItem, new()
        {
            var values = Enum.GetValues(enumType);
            var items = new T[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                var value = (int) values.GetValue(i); // values[i] does not work here
                var name = Enum.GetName(enumType, value);
                var element = new T { Id = value, Name = name };
                items[i] = element;
            }
            modelBuilder.Entity<T>().HasData(items);
        }
    }
}
