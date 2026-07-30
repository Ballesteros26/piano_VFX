using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace System.Web.Util
{
	// Token: 0x02000149 RID: 329
	internal class SerializationHelper
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x0002A60B File Offset: 0x0002880B
		internal string SerializeToBase64(object value)
		{
			return Convert.ToBase64String(this.SerializeToBinary(value));
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002A619 File Offset: 0x00028819
		internal object DeserializeFromBase64(string value)
		{
			return this.DeserializeFromBinary(Convert.FromBase64String(value));
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002A628 File Offset: 0x00028828
		internal string SerializeToXml(object value)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Serialize(memoryStream, value);
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			return text;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002A680 File Offset: 0x00028880
		internal object DeserializeFromXml(string value)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value)))
			{
				obj = new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Deserialize(memoryStream);
			}
			return obj;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002A6D4 File Offset: 0x000288D4
		internal byte[] SerializeToBinary(object value)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, value);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002A718 File Offset: 0x00028918
		internal object DeserializeFromBinary(byte[] value)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(value))
			{
				obj = new BinaryFormatter().Deserialize(memoryStream);
			}
			return obj;
		}
	}
}
