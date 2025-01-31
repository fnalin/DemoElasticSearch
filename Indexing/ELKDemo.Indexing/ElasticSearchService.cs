using Elastic.Clients.Elasticsearch;

namespace ELKDemo.Indexing;

public class ElasticSearchService
{
    private readonly ElasticsearchClient _client;
    private const string IndexName = "application_"; // Nome do índice

    public ElasticSearchService(string elasticsearchUri)
    {
        var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUri))
            .DefaultIndex(IndexName);

        _client = new ElasticsearchClient(settings);
    }

    public async Task IndexDocumentAsync(int applicationId, object dataIndex)
    {
        var document = new IndexedDocument
        {
            ApplicationId = applicationId,
            DateCreated = DateTime.UtcNow,
            DataIndex = dataIndex
        };

        var response = await _client.IndexAsync(document, IndexName + document.ApplicationId);

        if (!response.IsSuccess())
        {
            throw new Exception($"Erro ao indexar documento: {response.DebugInformation}");
        }
    }

    public async Task<SearchResponse<IndexedDocument>> SearchDocumentsAsync(int applicationId)
    {
        var response = await _client.SearchAsync<IndexedDocument>(s => s
            .Index(IndexName)
            .Query(q => q
                .Term(t => t.Field(f => f.ApplicationId).Value(applicationId))
            )
        );

        return response;
    }
}