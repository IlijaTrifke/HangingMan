using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    public class CommunicationHelper
    {
        BinaryFormatter formatter;
        NetworkStream stream;
        Socket socket;
        public CommunicationHelper(Socket soket)
        {
            socket = soket;
            formatter = new BinaryFormatter();
            stream = new NetworkStream(socket);

        }
        public void Send(object Argument)
        {
            formatter.Serialize(stream, Argument);
        }
        public object Recieve()
        {
            return formatter.Deserialize(stream);
        }


    }
}
