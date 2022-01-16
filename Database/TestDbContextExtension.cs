using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities.Enums;
using Entities.Structure;

namespace Database
{
    public static class TestDbContextExtension
    {
        // To reduce traffic selecting only data that is actually needed
        private static readonly Func<TestDbContext, int, string, IEnumerable<Document>> GetDocumentsFunc =
            EF.CompileQuery((TestDbContext db, int systemId, string kid) =>
                db.Documents
                    .Where(d => d.Kid == kid && d.SourceSystemId == systemId && d.Type != DocumentType.CreditInvoice)
                    .Select(d => new Document {Id = d.Id, OrgNoClient = d.OrgNoClient, RecNo = d.RecNo, Type = d.Type})
                    .AsNoTracking());

        private static readonly Func<TestDbContext, int, IEnumerable<Reference>> GetReferencesFunc =
            EF.CompileQuery((TestDbContext db, int documentId) =>
                db.References.Where(r => r.DocumentId == documentId)
                    .Select(r => new Reference { Id = r.Id, Type = r.Type, Value = r.Value })
                    .AsNoTracking());

        /// <summary>
        /// Returns an IEnumerable of truncated version of Document which are marked "AsNoTracking" and not cached by the context
        /// </summary>
        /// <param name="db"></param>
        /// <param name="systemId"></param>
        /// <param name="kid"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetDocuments(this TestDbContext db, int systemId, string kid)
        {
            return GetDocumentsFunc(db, systemId, kid);
        }

        /// <summary>
        /// Returns an IEnumerable of truncated version of Reference which are marked "AsNoTracking" and not cached by the context
        /// </summary>
        /// <param name="db"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static IEnumerable<Reference> GetReferences(this TestDbContext db, int documentId)
        {
            return GetReferencesFunc(db, documentId);
        }
    }
}
