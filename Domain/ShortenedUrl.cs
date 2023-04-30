namespace Shorten.io.Domain;

/// <summary>
/// Represents a shortened url.
/// </summary>
public class ShortenedUrl
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public Guid Id { get; private init; } = Guid.NewGuid();
    /// <summary>
    /// Original url 
    /// </summary>
    public string OriginalUrl { get; private set; }
    /// <summary>
    /// Shortened url
    /// </summary>
    public string ShortenedUrlCode { get; private set; }
    /// <summary>
    /// How many requests made to this shortened url.
    /// </summary>
    public int RequestCount { get; private set; }

    private ShortenedUrl(string originalUrl, string shortenedUrlCode)
    {
        OriginalUrl = originalUrl;
        ShortenedUrlCode = shortenedUrlCode;
    }

    /// <summary>
    /// Creates a new ShortenedUrl instance with specially assigned shortened url.
    /// </summary>
    /// <param name="originalUrl">Url to shorten : Ex:/ https://google.com/test?q=11333</param>
    /// <param name="url">The shortened url code : Ex:/ g11333</param>
    /// <returns></returns>
    public static ShortenedUrl Create(string originalUrl, string shortenedUrlCode)
    {
        var isValidUrl = Helpers.UrlHelper.IsValidUrl(originalUrl);

        if (!isValidUrl)
            throw new ArgumentException($"Given url is not valid. Url : {originalUrl}");

        if (shortenedUrlCode.Length > 6 || string.IsNullOrWhiteSpace(shortenedUrlCode))
            throw new ArgumentException($"Given shortened url code is not valid. It's length should be less than 6 and greater than 0");


        return new ShortenedUrl(originalUrl, shortenedUrlCode);
    }
    /// <summary>
    /// Creates a new ShortenedUrl instance. 
    /// The shortened url code will automatically created.
    /// </summary>
    /// <param name="originalUrl"></param>
    /// <returns></returns>
    public static ShortenedUrl Create(string originalUrl)
    {
        var isUrlValid = Helpers.UrlHelper.IsValidUrl(originalUrl);

        if (!isUrlValid)
            throw new ArgumentException($"Given url is not valid. Url : {originalUrl}");

        var shortenedUrlCode = Helpers.UrlHelper.GetShortenedUrl(originalUrl);

        return new ShortenedUrl(originalUrl, shortenedUrlCode);
    }

    public void ShortenedUrlVisited()
    {
        RequestCount++;
    }
}
