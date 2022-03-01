using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryNetworkMessageSerializer 
{
    public byte[] Serialize(NetworkMessage netMsg)
    {
        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            formatter.Serialize(ms, netMsg);
            return ms.ToArray();
        }
    }

    public NetworkMessage Deserialize(byte[] bytes)
    {
        IFormatter formatter = new BinaryFormatter();
        
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            NetworkMessage netMsg = formatter.Deserialize(ms) as NetworkMessage;
            return netMsg;
        }
    }    
}
