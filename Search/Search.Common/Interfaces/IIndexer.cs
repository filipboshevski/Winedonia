namespace Search.Common.Interfaces;

public interface IIndexer
{
    Task IndexSingle<T>(string indexName, T document, CancellationToken token = default)
        where T : class, IDocument;

    Task IndexMultiple<T>(string indexName, IEnumerable<T> documents, int timeoutInMinutes = 10, CancellationToken token = default)
        where T : class, IDocument;
}