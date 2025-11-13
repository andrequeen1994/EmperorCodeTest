using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Xml.Linq;
using System.Collections.Generic;
using System;

public interface INewsFeedService
{
    Task<List<XElement>> GetNewsItemsAsync(string feedUrl);
}

public class NewsFeedService : INewsFeedService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string CacheKeyPrefix = "NewsFeed_";
    private const int CacheExpiryMinutes = 30; 

    public NewsFeedService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<List<XElement>> GetNewsItemsAsync(string feedUrl)
    {
        if (string.IsNullOrWhiteSpace(feedUrl))
            return new List<XElement>();

        var cacheKey = $"{CacheKeyPrefix}{feedUrl.GetHashCode()}"; 

        if (_cache.TryGetValue(cacheKey, out List<XElement> cachedItems))
        {
            return cachedItems;  // Instant return on cache hit
        }

        try
        {
            var xmlContent = await _httpClient.GetStringAsync(feedUrl);
            var doc = XDocument.Parse(xmlContent);
            var items = doc.Descendants("item").ToList();


            var newsItems = items
                .Where(item =>
                {
                    try { DateTime.Parse(item.Element("pubDate")?.Value ?? "1900-01-01T00:00:00Z"); return true; }
                    catch { return false; }
                })
                .OrderByDescending(item => DateTime.Parse(item.Element("pubDate")?.Value ?? "1900-01-01T00:00:00Z"))
                .ToList();

            _cache.Set(cacheKey, newsItems, TimeSpan.FromMinutes(CacheExpiryMinutes));

            return newsItems;
        }
        catch (Exception ex)
        {
            return new List<XElement>();
        }
    }
}