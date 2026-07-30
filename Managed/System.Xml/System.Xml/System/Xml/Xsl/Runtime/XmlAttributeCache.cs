using System;
using System.Xml.Schema;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005FB RID: 1531
	internal sealed class XmlAttributeCache : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x06003B98 RID: 15256 RVA: 0x0014E8BD File Offset: 0x0014CABD
		public void Init(XmlRawWriter wrapped)
		{
			this.SetWrappedWriter(wrapped);
			this.numEntries = 0;
			this.idxLastName = 0;
			this.hashCodeUnion = 0;
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x0014E8DB File Offset: 0x0014CADB
		public int Count
		{
			get
			{
				return this.numEntries;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x0014E8E3 File Offset: 0x0014CAE3
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x0014E8EB File Offset: 0x0014CAEB
		public OnRemoveWriter OnRemoveWriterEvent
		{
			get
			{
				return this.onRemove;
			}
			set
			{
				this.onRemove = value;
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x0014E8F4 File Offset: 0x0014CAF4
		private void SetWrappedWriter(XmlRawWriter writer)
		{
			IRemovableWriter removableWriter = writer as IRemovableWriter;
			if (removableWriter != null)
			{
				removableWriter.OnRemoveWriterEvent = new OnRemoveWriter(this.SetWrappedWriter);
			}
			this.wrapped = writer;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x0014E924 File Offset: 0x0014CB24
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			int num = 0;
			int num2 = 1 << (int)localName[0];
			if ((this.hashCodeUnion & num2) != 0)
			{
				while (!this.arrAttrs[num].IsDuplicate(localName, ns, num2))
				{
					num = this.arrAttrs[num].NextNameIndex;
					if (num == 0)
					{
						break;
					}
				}
			}
			else
			{
				this.hashCodeUnion |= num2;
			}
			this.EnsureAttributeCache();
			if (this.numEntries != 0)
			{
				this.arrAttrs[this.idxLastName].NextNameIndex = this.numEntries;
			}
			int num3 = this.numEntries;
			this.numEntries = num3 + 1;
			this.idxLastName = num3;
			this.arrAttrs[this.idxLastName].Init(prefix, localName, ns, num2);
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteEndAttribute()
		{
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x0014E9E3 File Offset: 0x0014CBE3
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.FlushAttributes();
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x0014E9F8 File Offset: 0x0014CBF8
		public override void WriteString(string text)
		{
			this.EnsureAttributeCache();
			XmlAttributeCache.AttrNameVal[] array = this.arrAttrs;
			int num = this.numEntries;
			this.numEntries = num + 1;
			array[num].Init(text);
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x0014EA30 File Offset: 0x0014CC30
		public override void WriteValue(object value)
		{
			this.EnsureAttributeCache();
			XmlAttributeCache.AttrNameVal[] array = this.arrAttrs;
			int num = this.numEntries;
			this.numEntries = num + 1;
			array[num].Init((XmlAtomicValue)value);
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0014EA6A File Offset: 0x0014CC6A
		public override void WriteValue(string value)
		{
			this.WriteValue(value);
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0014EA73 File Offset: 0x0014CC73
		internal override void StartElementContent()
		{
			this.FlushAttributes();
			this.wrapped.StartElementContent();
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0014EA86 File Offset: 0x0014CC86
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0014EA93 File Offset: 0x0014CC93
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0014EAA0 File Offset: 0x0014CCA0
		private void FlushAttributes()
		{
			int num = 0;
			while (num != this.numEntries)
			{
				int nextNameIndex = this.arrAttrs[num].NextNameIndex;
				if (nextNameIndex == 0)
				{
					nextNameIndex = this.numEntries;
				}
				string localName = this.arrAttrs[num].LocalName;
				if (localName != null)
				{
					string prefix = this.arrAttrs[num].Prefix;
					string @namespace = this.arrAttrs[num].Namespace;
					this.wrapped.WriteStartAttribute(prefix, localName, @namespace);
					while (++num != nextNameIndex)
					{
						string text = this.arrAttrs[num].Text;
						if (text != null)
						{
							this.wrapped.WriteString(text);
						}
						else
						{
							this.wrapped.WriteValue(this.arrAttrs[num].Value);
						}
					}
					this.wrapped.WriteEndAttribute();
				}
				else
				{
					num = nextNameIndex;
				}
			}
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0014EB9C File Offset: 0x0014CD9C
		private void EnsureAttributeCache()
		{
			if (this.arrAttrs == null)
			{
				this.arrAttrs = new XmlAttributeCache.AttrNameVal[32];
				return;
			}
			if (this.numEntries >= this.arrAttrs.Length)
			{
				XmlAttributeCache.AttrNameVal[] array = new XmlAttributeCache.AttrNameVal[this.numEntries * 2];
				Array.Copy(this.arrAttrs, array, this.numEntries);
				this.arrAttrs = array;
			}
		}

		// Token: 0x0400273A RID: 10042
		private XmlRawWriter wrapped;

		// Token: 0x0400273B RID: 10043
		private OnRemoveWriter onRemove;

		// Token: 0x0400273C RID: 10044
		private XmlAttributeCache.AttrNameVal[] arrAttrs;

		// Token: 0x0400273D RID: 10045
		private int numEntries;

		// Token: 0x0400273E RID: 10046
		private int idxLastName;

		// Token: 0x0400273F RID: 10047
		private int hashCodeUnion;

		// Token: 0x04002740 RID: 10048
		private const int DefaultCacheSize = 32;

		// Token: 0x020005FC RID: 1532
		private struct AttrNameVal
		{
			// Token: 0x17000C2F RID: 3119
			// (get) Token: 0x06003BAE RID: 15278 RVA: 0x0014EBFE File Offset: 0x0014CDFE
			public string LocalName
			{
				get
				{
					return this.localName;
				}
			}

			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x06003BAF RID: 15279 RVA: 0x0014EC06 File Offset: 0x0014CE06
			public string Prefix
			{
				get
				{
					return this.prefix;
				}
			}

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x0014EC0E File Offset: 0x0014CE0E
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000C32 RID: 3122
			// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x0014EC16 File Offset: 0x0014CE16
			public string Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x17000C33 RID: 3123
			// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x0014EC1E File Offset: 0x0014CE1E
			public XmlAtomicValue Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000C34 RID: 3124
			// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x0014EC26 File Offset: 0x0014CE26
			// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x0014EC2E File Offset: 0x0014CE2E
			public int NextNameIndex
			{
				get
				{
					return this.nextNameIndex;
				}
				set
				{
					this.nextNameIndex = value;
				}
			}

			// Token: 0x06003BB5 RID: 15285 RVA: 0x0014EC37 File Offset: 0x0014CE37
			public void Init(string prefix, string localName, string ns, int hashCode)
			{
				this.localName = localName;
				this.prefix = prefix;
				this.namespaceName = ns;
				this.hashCode = hashCode;
				this.nextNameIndex = 0;
			}

			// Token: 0x06003BB6 RID: 15286 RVA: 0x0014EC5D File Offset: 0x0014CE5D
			public void Init(string text)
			{
				this.text = text;
				this.value = null;
			}

			// Token: 0x06003BB7 RID: 15287 RVA: 0x0014EC6D File Offset: 0x0014CE6D
			public void Init(XmlAtomicValue value)
			{
				this.text = null;
				this.value = value;
			}

			// Token: 0x06003BB8 RID: 15288 RVA: 0x0014EC7D File Offset: 0x0014CE7D
			public bool IsDuplicate(string localName, string ns, int hashCode)
			{
				if (this.localName != null && this.hashCode == hashCode && this.localName.Equals(localName) && this.namespaceName.Equals(ns))
				{
					this.localName = null;
					return true;
				}
				return false;
			}

			// Token: 0x04002741 RID: 10049
			private string localName;

			// Token: 0x04002742 RID: 10050
			private string prefix;

			// Token: 0x04002743 RID: 10051
			private string namespaceName;

			// Token: 0x04002744 RID: 10052
			private string text;

			// Token: 0x04002745 RID: 10053
			private XmlAtomicValue value;

			// Token: 0x04002746 RID: 10054
			private int hashCode;

			// Token: 0x04002747 RID: 10055
			private int nextNameIndex;
		}
	}
}
