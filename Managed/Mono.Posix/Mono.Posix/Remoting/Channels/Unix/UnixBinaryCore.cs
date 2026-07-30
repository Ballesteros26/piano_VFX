using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000080 RID: 128
	internal class UnixBinaryCore
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0000E478 File Offset: 0x0000C678
		public UnixBinaryCore(object owner, IDictionary properties, string[] allowedProperties)
		{
			this._properties = properties;
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (Array.IndexOf<string>(allowedProperties, text) == -1)
				{
					throw new RemotingException(owner.GetType().Name + " does not recognize '" + text + "' configuration property");
				}
				if (!(text == "includeVersions"))
				{
					if (text == "strictBinding")
					{
						this._strictBinding = Convert.ToBoolean(dictionaryEntry.Value);
					}
				}
				else
				{
					this._includeVersions = Convert.ToBoolean(dictionaryEntry.Value);
				}
			}
			this.Init();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000E560 File Offset: 0x0000C760
		public UnixBinaryCore()
		{
			this._properties = new Hashtable();
			this.Init();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000E580 File Offset: 0x0000C780
		public void Init()
		{
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Remoting, null);
			this._serializationFormatter = new BinaryFormatter(remotingSurrogateSelector, streamingContext);
			this._deserializationFormatter = new BinaryFormatter(null, streamingContext);
			if (!this._includeVersions)
			{
				this._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				this._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
			}
			if (!this._strictBinding)
			{
				this._serializationFormatter.Binder = SimpleBinder.Instance;
				this._deserializationFormatter.Binder = SimpleBinder.Instance;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000E5FF File Offset: 0x0000C7FF
		public BinaryFormatter Serializer
		{
			get
			{
				return this._serializationFormatter;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000E607 File Offset: 0x0000C807
		public BinaryFormatter Deserializer
		{
			get
			{
				return this._deserializationFormatter;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000E60F File Offset: 0x0000C80F
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x0400049E RID: 1182
		private BinaryFormatter _serializationFormatter;

		// Token: 0x0400049F RID: 1183
		private BinaryFormatter _deserializationFormatter;

		// Token: 0x040004A0 RID: 1184
		private bool _includeVersions = true;

		// Token: 0x040004A1 RID: 1185
		private bool _strictBinding;

		// Token: 0x040004A2 RID: 1186
		private IDictionary _properties;

		// Token: 0x040004A3 RID: 1187
		public static UnixBinaryCore DefaultInstance = new UnixBinaryCore();
	}
}
