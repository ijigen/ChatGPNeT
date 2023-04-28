namespace ChatGPNet;

public class Request
{
    public string model { set; get; }
    public List<Message> messages { set; get; }
    public float temperature { set; get; }
}