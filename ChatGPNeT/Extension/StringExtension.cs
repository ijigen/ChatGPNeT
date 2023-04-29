using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatGPNet.Extension;

public static class StringExtension
{
    public static async Task<string> ChatGpt(this String str, Request request, string token)
    {
        (request.messages ??= new()).Add(new() { role = "user", content = str });


        return string.Join("\n", ((await request.Send(token))!).choices.Select(choice => choice.message.content));
    }

    private static readonly Request Request = new()
        { messages = new List<Message>(), model = Model.Gpt4_0314.GetTag(), temperature = .7f };


    public static async Task<string> ChatGpt(this String str, int memMsgLimit)
    {
        (Request.messages ??= new() { })
            .RemoveRange(0, Math.Max(Request.messages.Count - memMsgLimit, 0));
        
        Request.messages.Add(new() { role = "user", content = str });
        var response = await Request.Send(Environment.GetEnvironmentVariable("OPEN_AI_TOKEN"));
        Request.messages.AddRange(response.choices.Select(choice => choice.message));
        var result = string.Join("\n",
            response.choices.Select(choice =>
                choice.message.content));
        return result;
    }

    public static async Task<string> ChatGpt(this String str)
    {
        return await str.ChatGpt(0);
    }
}