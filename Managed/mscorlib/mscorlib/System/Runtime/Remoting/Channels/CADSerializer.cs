using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200079F RID: 1951
	internal class CADSerializer
	{
		// Token: 0x06004FBF RID: 20415 RVA: 0x0011EE0C File Offset: 0x0011D00C
		internal static IMessage DeserializeMessage(MemoryStream mem, IMethodCallMessage msg)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.SurrogateSelector = null;
			mem.Position = 0L;
			if (msg == null)
			{
				return (IMessage)binaryFormatter.Deserialize(mem, null);
			}
			return (IMessage)binaryFormatter.DeserializeMethodResponse(mem, null, msg);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0011EE50 File Offset: 0x0011D050
		internal static MemoryStream SerializeMessage(IMessage msg)
		{
			MemoryStream memoryStream = new MemoryStream();
			new BinaryFormatter
			{
				SurrogateSelector = new RemotingSurrogateSelector()
			}.Serialize(memoryStream, msg);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0011EE84 File Offset: 0x0011D084
		internal static object DeserializeObjectSafe(byte[] mem)
		{
			byte[] array = new byte[mem.Length];
			Array.Copy(mem, array, mem.Length);
			return CADSerializer.DeserializeObject(new MemoryStream(array));
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0011EEB0 File Offset: 0x0011D0B0
		internal static MemoryStream SerializeObject(object obj)
		{
			MemoryStream memoryStream = new MemoryStream();
			new BinaryFormatter
			{
				SurrogateSelector = new RemotingSurrogateSelector()
			}.Serialize(memoryStream, obj);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0011EEE3 File Offset: 0x0011D0E3
		internal static object DeserializeObject(MemoryStream mem)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.SurrogateSelector = null;
			mem.Position = 0L;
			return binaryFormatter.Deserialize(mem);
		}
	}
}
