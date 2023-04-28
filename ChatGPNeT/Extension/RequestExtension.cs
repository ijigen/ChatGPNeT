using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatGPNet.Extension;

public static class RequestExtension
{
    private static readonly HttpClient Http = new();

    private static readonly Dictionary<string, AuthenticationHeaderValue> TokenHeader =
        new Dictionary<string, AuthenticationHeaderValue>();

    public static async Task<Response?> Send(this Request request, string token)
    {
        for (;;)
        {
            using var httpRequest =
                new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
                {
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
                };


            httpRequest.Headers.Authorization = TokenHeader.ContainsKey(token)
                ? TokenHeader[token]
                : TokenHeader[token] = new("Bearer", token);

            // httpRequest.Headers.Add("OpenAI-Organization", "org-QjT5KmkXd68WWi6JSzwwBMUB");
            using var httpResponse = await Http.SendAsync(httpRequest);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("!chatGpt response fail.");
                continue;
            }

            var response = JsonSerializer.Deserialize<Response>(await httpResponse.Content.ReadAsStringAsync());
            if (response == null)
            {
                Console.WriteLine("!chatGpt response deserialize fail.");
                continue;
            }

            return response;
        }
    }
}