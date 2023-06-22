namespace Todo.Api;

public static class HttpExtensions
{
    public static Uri GetRequestUri(this HttpRequest request)
    {
        var uri = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? 80,
            Path = request.Path,
            Query = request.QueryString.ToString(),
        };

        return uri.Uri;
    }

    public static HttpRequestMessage ToHttpRequestMessage(this HttpRequest httpRequest)
    {
        var uri = httpRequest.GetRequestUri();

        var body = new StreamContent(httpRequest.Body);

        var request = new HttpRequestMessage(new HttpMethod(httpRequest.Method), uri)
        {
            Content = body
        };

        foreach (var header in httpRequest.Headers)
        {
            if (!request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && request.Content != null)
            {
                _ = request.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        return request;
    }

    public static async Task SetHttpResponse(this HttpContext httpContext, HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        httpContext.Response.StatusCode = (int)response.StatusCode;
        foreach (var header in response.Headers)
        {
            httpContext.Response.Headers[header.Key] = header.Value.ToArray();
        }

        foreach (var header in response.Content.Headers)
        {
            httpContext.Response.Headers[header.Key] = header.Value.ToArray();
        }

        httpContext.Response.Headers.Remove("Transfer-Encoding");

        await httpContext.Response.WriteAsync(await response.Content.ReadAsStringAsync());
        await httpContext.Response.CompleteAsync();
    }

    public static async Task ProxyRequestAsync(this HttpClient httpClient, HttpContext httpContext)
    {
        var request = httpContext.Request.ToHttpRequestMessage();
        var response = await httpClient.SendAsync(request);

        await httpContext.SetHttpResponse(response);
    }
}