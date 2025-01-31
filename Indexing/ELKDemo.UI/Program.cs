using System.Text.Json;
using ELKDemo.Indexing;

internal class Program
{
    static async Task Main()
    {
        var elasticsearchUri = "http://localhost:9200"; // Ajuste conforme necessário
        var elasticService = new ElasticSearchService(elasticsearchUri);

        var data = new
        {
            Key1 = "Value1",
            Key2 = "Value2"
        };

        int applicationId = 123;

        // Indexar documento
        await elasticService.IndexDocumentAsync(applicationId, data);
        Console.WriteLine("Documento indexado com sucesso!");

        // Buscar documentos
        var response = await elasticService.SearchDocumentsAsync(applicationId);

        Console.WriteLine($"\nDocumentos encontrados: {response.Hits.Count}");

        foreach (var hit in response.Hits)
        {
            // Serializar o JSON com formatação bonita
            string json = JsonSerializer.Serialize(hit.Source, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}