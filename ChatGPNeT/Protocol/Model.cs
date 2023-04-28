namespace ChatGPNet;

public enum Model
{
    Gpt35Turbo0301,
    Gpt4,
    Gpt4_0314
}

public static class ModelExtensions
{
    public static string GetTag(this Model model)
    {
        switch (model)
        {
            case Model.Gpt35Turbo0301: return "gpt-3.5-turbo-0301";
            case Model.Gpt4: return "gpt-4";
            case Model.Gpt4_0314: return "gpt-4-0314";
        }

        return "";
    }
}