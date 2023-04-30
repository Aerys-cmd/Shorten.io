using System;
using System.Security.Cryptography;
using System.Text;

namespace Shorten.io.Domain.Helpers;

public static class UrlHelper
{
    /// <summary>
    /// Checks if the given url is valid.
    /// </summary>
    /// <param name="url">Url to validate.</param>
    /// <returns></returns>
    public static bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out Uri result) &&
                          (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
    /// <summary>
    /// Shortens the given url.
    /// </summary>
    /// <param name="url">Url to shorten</param>
    /// <returns>Shortened url code</returns>
    /// <exception cref="ArgumentException">If url is not in a valid format.</exception>
    public static string GetShortenedUrl(string url)
    {
        if (!IsValidUrl(url))
            throw new ArgumentException($"Given url is not valid. Url : {url}");

        var hash = GetSha256Hash(url);

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(hash)).Substring(0, 6);

    }


    private static string GetSha256Hash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }



}
