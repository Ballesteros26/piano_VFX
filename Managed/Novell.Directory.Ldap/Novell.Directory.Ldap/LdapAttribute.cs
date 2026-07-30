using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000C RID: 12
	public class LdapAttribute : ICloneable, IComparable
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000037BE File Offset: 0x000019BE
		public virtual IEnumerator ByteValues
		{
			get
			{
				return new ArrayEnumeration(this.ByteValueArray);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000037CB File Offset: 0x000019CB
		public virtual IEnumerator StringValues
		{
			get
			{
				return new ArrayEnumeration(this.StringValueArray);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000037D8 File Offset: 0x000019D8
		[CLSCompliant(false)]
		public virtual sbyte[][] ByteValueArray
		{
			get
			{
				if (this.values == null)
				{
					return new sbyte[0][];
				}
				int num = this.values.Length;
				sbyte[][] array = new sbyte[num][];
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					array[i] = new sbyte[((sbyte[])this.values[i]).Length];
					Array.Copy((Array)this.values[i], 0, array[i], 0, array[i].Length);
					i++;
				}
				return array;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003844 File Offset: 0x00001A44
		public virtual string[] StringValueArray
		{
			get
			{
				if (this.values == null)
				{
					return new string[0];
				}
				int num = this.values.Length;
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					try
					{
						char[] chars = Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[i]));
						array[i] = new string(chars);
					}
					catch (IOException ex)
					{
						throw new SystemException(ex.ToString());
					}
				}
				return array;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000038C8 File Offset: 0x00001AC8
		public virtual string StringValue
		{
			get
			{
				string text = null;
				if (this.values != null)
				{
					try
					{
						text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[0])));
					}
					catch (IOException ex)
					{
						throw new SystemException(ex.ToString());
					}
				}
				return text;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003924 File Offset: 0x00001B24
		[CLSCompliant(false)]
		public virtual sbyte[] ByteValue
		{
			get
			{
				sbyte[] array = null;
				if (this.values != null)
				{
					array = new sbyte[((sbyte[])this.values[0]).Length];
					Array.Copy((Array)this.values[0], 0, array, 0, array.Length);
				}
				return array;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000396C File Offset: 0x00001B6C
		public virtual string LangSubtype
		{
			get
			{
				if (this.subTypes != null)
				{
					for (int i = 0; i < this.subTypes.Length; i++)
					{
						if (this.subTypes[i].StartsWith("lang-"))
						{
							return this.subTypes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000039B2 File Offset: 0x00001BB2
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001B RID: 27
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000039BC File Offset: 0x00001BBC
		protected internal virtual string Value
		{
			set
			{
				this.values = null;
				try
				{
					sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(value));
					this.add(array);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A0C File Offset: 0x00001C0C
		public LdapAttribute(LdapAttribute attr)
		{
			if (attr == null)
			{
				throw new ArgumentException("LdapAttribute class cannot be null");
			}
			this.name = attr.name;
			this.baseName = attr.baseName;
			if (attr.subTypes != null)
			{
				this.subTypes = new string[attr.subTypes.Length];
				Array.Copy(attr.subTypes, 0, this.subTypes, 0, this.subTypes.Length);
			}
			if (attr.values != null)
			{
				this.values = new object[attr.values.Length];
				Array.Copy(attr.values, 0, this.values, 0, this.values.Length);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003AB1 File Offset: 0x00001CB1
		public LdapAttribute(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			this.name = attrName;
			this.baseName = LdapAttribute.getBaseName(attrName);
			this.subTypes = LdapAttribute.getSubtypes(attrName);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003AE8 File Offset: 0x00001CE8
		[CLSCompliant(false)]
		public LdapAttribute(string attrName, sbyte[] attrBytes)
			: this(attrName)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			sbyte[] array = new sbyte[attrBytes.Length];
			Array.Copy(attrBytes, 0, array, 0, attrBytes.Length);
			this.add(array);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003B28 File Offset: 0x00001D28
		public LdapAttribute(string attrName, string attrString)
			: this(attrName)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.add(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003B88 File Offset: 0x00001D88
		public LdapAttribute(string attrName, string[] attrStrings)
			: this(attrName)
		{
			if (attrStrings == null)
			{
				throw new ArgumentException("Attribute values array cannot be null");
			}
			int i = 0;
			int num = attrStrings.Length;
			while (i < num)
			{
				try
				{
					if (attrStrings[i] == null)
					{
						throw new ArgumentException("Attribute value at array index " + i + " cannot be null");
					}
					sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrStrings[i]));
					this.add(array);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
				i++;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C18 File Offset: 0x00001E18
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.values != null)
				{
					Array.Copy(this.values, 0, ((LdapAttribute)obj).values, 0, this.values.Length);
				}
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C78 File Offset: 0x00001E78
		public virtual void addValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.add(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003CD0 File Offset: 0x00001ED0
		[CLSCompliant(false)]
		public virtual void addValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(attrBytes);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003CE7 File Offset: 0x00001EE7
		public virtual void addBase64Value(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D03 File Offset: 0x00001F03
		public virtual void addBase64Value(StringBuilder attrString, int start, int end)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString, start, end));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D21 File Offset: 0x00001F21
		public virtual void addBase64Value(char[] attrChars)
		{
			if (attrChars == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrChars));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D3D File Offset: 0x00001F3D
		public virtual void addURLValue(string url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			this.addURLValue(new Uri(url));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D5C File Offset: 0x00001F5C
		public virtual void addURLValue(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			try
			{
				Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
				ArrayList arrayList = new ArrayList();
				sbyte[] array = new sbyte[4096];
				int num = 0;
				int num2;
				while ((num2 = SupportClass.ReadInput(responseStream, ref array, 0, 4096)) != -1)
				{
					arrayList.Add(new LdapAttribute.URLData(this, array, num2));
					array = new sbyte[4096];
					num += num2;
				}
				sbyte[] array2 = new sbyte[num];
				int num3 = 0;
				for (int i = 0; i < arrayList.Count; i++)
				{
					LdapAttribute.URLData urldata = (LdapAttribute.URLData)arrayList[i];
					num2 = urldata.getLength();
					Array.Copy(urldata.getData(), 0, array2, num3, num2);
					num3 += num2;
				}
				this.add(array2);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E48 File Offset: 0x00002048
		public virtual string getBaseName()
		{
			return this.baseName;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E50 File Offset: 0x00002050
		public static string getBaseName(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			int num = attrName.IndexOf(';');
			if (-1 == num)
			{
				return attrName;
			}
			return attrName.Substring(0, num);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E82 File Offset: 0x00002082
		public virtual string[] getSubtypes()
		{
			return this.subTypes;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E8C File Offset: 0x0000208C
		public static string[] getSubtypes(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(attrName, ";");
			string[] array = null;
			int count = tokenizer.Count;
			if (count > 0)
			{
				tokenizer.NextToken();
				array = new string[count - 1];
				int num = 0;
				while (tokenizer.HasMoreTokens())
				{
					array[num++] = tokenizer.NextToken();
				}
			}
			return array;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EEC File Offset: 0x000020EC
		public virtual bool hasSubtype(string subtype)
		{
			if (subtype == null)
			{
				throw new ArgumentException("subtype cannot be null");
			}
			if (this.subTypes != null)
			{
				for (int i = 0; i < this.subTypes.Length; i++)
				{
					if (this.subTypes[i].ToUpper().Equals(subtype.ToUpper()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003F40 File Offset: 0x00002140
		public virtual bool hasSubtypes(string[] subtypes)
		{
			if (subtypes == null)
			{
				throw new ArgumentException("subtypes cannot be null");
			}
			int i = 0;
			IL_006C:
			while (i < subtypes.Length)
			{
				for (int j = 0; j < this.subTypes.Length; j++)
				{
					if (this.subTypes[j] == null)
					{
						throw new ArgumentException("subtype at array index " + i + " cannot be null");
					}
					if (this.subTypes[j].ToUpper().Equals(subtypes[i].ToUpper()))
					{
						i++;
						goto IL_006C;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003FC0 File Offset: 0x000021C0
		public virtual void removeValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.removeValue(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004018 File Offset: 0x00002218
		[CLSCompliant(false)]
		public virtual void removeValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			int i = 0;
			while (i < this.values.Length)
			{
				if (this.equals(attrBytes, (sbyte[])this.values[i]))
				{
					if (i == 0 && 1 == this.values.Length)
					{
						this.values = null;
						return;
					}
					if (this.values.Length == 1)
					{
						this.values = null;
						return;
					}
					int num = this.values.Length - i - 1;
					object[] array = new object[this.values.Length - 1];
					if (i != 0)
					{
						Array.Copy(this.values, 0, array, 0, i);
					}
					if (num != 0)
					{
						Array.Copy(this.values, i + 1, array, i, num);
					}
					this.values = array;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000040D8 File Offset: 0x000022D8
		public virtual int size()
		{
			if (this.values != null)
			{
				return this.values.Length;
			}
			return 0;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000040EC File Offset: 0x000022EC
		public virtual int CompareTo(object attribute)
		{
			return this.name.CompareTo(((LdapAttribute)attribute).name);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004104 File Offset: 0x00002304
		private void add(sbyte[] bytes)
		{
			if (this.values == null)
			{
				this.values = new object[] { bytes };
				return;
			}
			for (int i = 0; i < this.values.Length; i++)
			{
				if (this.equals(bytes, (sbyte[])this.values[i]))
				{
					return;
				}
			}
			object[] array = new object[this.values.Length + 1];
			Array.Copy(this.values, 0, array, 0, this.values.Length);
			array[this.values.Length] = bytes;
			this.values = array;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004190 File Offset: 0x00002390
		private bool equals(sbyte[] e1, sbyte[] e2)
		{
			if (e1 == e2)
			{
				return true;
			}
			if (e1 == null || e2 == null)
			{
				return false;
			}
			int num = e1.Length;
			if (e2.Length != num)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (e1[i] != e2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000041D0 File Offset: 0x000023D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttribute: ");
			try
			{
				stringBuilder.Append("{type='" + this.name + "'");
				if (this.values != null)
				{
					stringBuilder.Append(", ");
					if (this.values.Length == 1)
					{
						stringBuilder.Append("value='");
					}
					else
					{
						stringBuilder.Append("values='");
					}
					for (int i = 0; i < this.values.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append("','");
						}
						if (((sbyte[])this.values[i]).Length != 0)
						{
							string text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[i])));
							if (text.Length == 0)
							{
								stringBuilder.Append("<binary value, length:" + text.Length);
							}
							else
							{
								stringBuilder.Append(text);
							}
						}
					}
					stringBuilder.Append("'");
				}
				stringBuilder.Append("}");
			}
			catch (Exception ex)
			{
				throw new SystemException(ex.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400005C RID: 92
		private string name;

		// Token: 0x0400005D RID: 93
		private string baseName;

		// Token: 0x0400005E RID: 94
		private string[] subTypes;

		// Token: 0x0400005F RID: 95
		private object[] values;

		// Token: 0x020000F2 RID: 242
		private class URLData
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x00019399 File Offset: 0x00017599
			private void InitBlock(LdapAttribute enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000620 RID: 1568 RVA: 0x000193A2 File Offset: 0x000175A2
			public LdapAttribute Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x000193AA File Offset: 0x000175AA
			public URLData(LdapAttribute enclosingInstance, sbyte[] data, int length)
			{
				this.InitBlock(enclosingInstance);
				this.length = length;
				this.data = data;
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x000193C7 File Offset: 0x000175C7
			public int getLength()
			{
				return this.length;
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x000193CF File Offset: 0x000175CF
			public sbyte[] getData()
			{
				return this.data;
			}

			// Token: 0x040004E2 RID: 1250
			private LdapAttribute enclosingInstance;

			// Token: 0x040004E3 RID: 1251
			private int length;

			// Token: 0x040004E4 RID: 1252
			private sbyte[] data;
		}
	}
}
