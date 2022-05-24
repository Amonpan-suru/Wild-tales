using Unity.Collections;
using Unity.Netcode;

public struct NetworkString : INetworkSerializable
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerName );
    }

    public string PlayerName;
    public NetworkString(string playerName)
    {
        PlayerName = playerName;

    }

    public void SetDataCollect(string playerName)
    {
        PlayerName = playerName;

    }

}