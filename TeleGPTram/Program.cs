using System;
using ChatGPNet.Extension;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

const string telegramToken = "TELEGRAM_TOKEN";
var memberInviters = new Dictionary<User, User>();

string token = Environment.GetEnvironmentVariable(telegramToken) ?? ((Func<string>)(() =>
{
    var token = Console.ReadLine();
    Environment.SetEnvironmentVariable(telegramToken, token);
    return token;
})).Invoke();

ITelegramBotClient bot = new TelegramBotClient(token);


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type != UpdateType.Message)
        return;
    var message = update.Message;

    if (message.Type == MessageType.Text)
    {
        Console.WriteLine($"{message.Chat.Username}:{message.Text}");
        var response = await message.Text.ChatGpt();
        Console.WriteLine($"bot:{response}");

        await botClient.SendTextMessageAsync(message.Chat.Id, response);
    }
}

async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    if (exception is ApiRequestException apiRequestException)
    {
        await botClient.SendTextMessageAsync(123, apiRequestException.ToString());
    }
}


var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { } // receive all update types
};


for (;;)
{
    try
    {
        await bot.ReceiveAsync(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
    }
    catch (Exception)
    {
    }
}