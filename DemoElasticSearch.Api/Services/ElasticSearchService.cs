using DemoElasticSearch.Api.Models;
using Elastic.Clients.Elasticsearch;

namespace DemoElasticSearch.Api.Services;

public class ElasticSearchService
{
    private readonly ElasticsearchClient _client;

    public ElasticSearchService(IConfiguration configuration)
    {
        var settings = new ElasticsearchClientSettings(new Uri(configuration["ElasticSearch:Uri"]))
            .DefaultIndex("clientes");

        _client = new ElasticsearchClient(settings);
    }

    public async Task<bool> IndexClienteAsync(Cliente cliente)
    {
        var response = await _client.IndexAsync(cliente);
        return response.IsValidResponse;
    }

    public async Task<Cliente?> GetClienteByIdAsync(Guid id)
    {
        var response = await _client.GetAsync<Cliente>(id.ToString());
        return response?.Source;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        var response = await _client.SearchAsync<Cliente>(s => s
                .Index("clientes") // Define o índice a ser consultado
                .Query(q => q.MatchAll(_ => { })) // Configura a consulta MatchAll com uma expressão vazia
        );

        if (!response.IsValidResponse || response.Documents == null)
            return Enumerable.Empty<Cliente>();

        return response.Documents;
    }

    public async Task<bool> UpdateClienteAsync(Cliente cliente)
    {
        var response = await _client.UpdateAsync<Cliente, Cliente>(cliente.Id.ToString(), u => u.Doc(cliente));
        return response.IsValidResponse;
    }

    public async Task<bool> DeleteClienteAsync(Guid id)
    {
        var response = await _client.DeleteAsync<Cliente>(id.ToString());
        return response.IsValidResponse;
    }
}