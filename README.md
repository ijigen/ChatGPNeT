# Example Project for ChatGPT in C#

This example project demonstrates how to use the ChatGPT API in C#.
It is implemented as a simple string extension method that makes it easy to send a message and receive a response from ChatGPT.

## Installation

1. Download or clone this repository.
2. Open the project in your favorite C# IDE or text editor.
3. Add your OpenAI API key as an environment variable named `OPEN_AI_TOKEN`.

## Usage

```csharp
using System;
using ChatGptApi;

class Program
{
    static async Task Main()
    {
        string message = "Tell me a joke.";
        string response = await message.ChatGpt();
        Console.WriteLine(response);
    }
}
```

# ChatGPT在C＃中的示例項目

此示例項目演示了如何在C＃中使用ChatGPT API。它實現為一個簡單的字符串擴展方法，使其易於發送消息並從ChatGPT接收響應。

## 安裝

1. 下載或克隆此存儲庫。
2. 使用您喜愛的C＃IDE或文本編輯器打開項目。
3. 將您的OpenAI API密鑰添加為名為`OPEN_AI_TOKEN`的環境變量。

## 使用

```csharp
using System;
using ChatGptApi;

class Program
{
    static async Task Main()
    {
        string message = "告訴我一個笑話。";
        string response = await message.ChatGpt();
        Console.WriteLine(response);
    }
}
```
