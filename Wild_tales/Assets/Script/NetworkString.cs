using Unity.Collections;
using Unity.Netcode;

public struct NetworkString : INetworkSerializable
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref Number);
    }

    public string PlayerName;
    public int Number;
    public NetworkString(string playerName, int number)
    {
        PlayerName = playerName;
        Number = number;

    }

    public void SetDataCollect(string playerName)
    {
        PlayerName = playerName;

    }

}