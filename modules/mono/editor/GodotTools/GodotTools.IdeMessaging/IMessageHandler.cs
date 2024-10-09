using System.Threading.Tasks;

namespace scardotTools.IdeMessaging
{
    public interface IMessageHandler
    {
        Task<MessageContent> HandleRequest(Peer peer, string id, MessageContent content, ILogger logger);
    }
}
