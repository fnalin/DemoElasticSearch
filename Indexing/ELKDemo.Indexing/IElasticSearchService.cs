namespace ELKDemo.Indexing;

public interface IElasticSearchService
{
    Task IndexDocumentAsync(int applicationId, object dataIndex);
    Task<IndexedDocument> SearchDocumentsAsync(int applicationId);
}