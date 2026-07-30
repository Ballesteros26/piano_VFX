using System;
using System.Collections;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x02000539 RID: 1337
	[Serializable]
	internal sealed class SecurityDocument
	{
		// Token: 0x06003C36 RID: 15414 RVA: 0x000D8F49 File Offset: 0x000D7149
		public SecurityDocument(int numData)
		{
			this.m_data = new byte[numData];
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000D8F5D File Offset: 0x000D715D
		public SecurityDocument(byte[] data)
		{
			this.m_data = data;
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000D8F6C File Offset: 0x000D716C
		public SecurityDocument(SecurityElement elRoot)
		{
			this.m_data = new byte[32];
			int num = 0;
			this.ConvertElement(elRoot, ref num);
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000D8F98 File Offset: 0x000D7198
		public void GuaranteeSize(int size)
		{
			if (this.m_data.Length < size)
			{
				byte[] array = new byte[(size / 32 + 1) * 32];
				Array.Copy(this.m_data, 0, array, 0, this.m_data.Length);
				this.m_data = array;
			}
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000D8FDC File Offset: 0x000D71DC
		public void AddString(string str, ref int position)
		{
			this.GuaranteeSize(position + str.Length * 2 + 2);
			for (int i = 0; i < str.Length; i++)
			{
				this.m_data[position + 2 * i] = (byte)(str[i] >> 8);
				this.m_data[position + 2 * i + 1] = (byte)(str[i] & 'ÿ');
			}
			this.m_data[position + str.Length * 2] = 0;
			this.m_data[position + str.Length * 2 + 1] = 0;
			position += str.Length * 2 + 2;
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000D9078 File Offset: 0x000D7278
		public void AppendString(string str, ref int position)
		{
			if (position <= 1 || this.m_data[position - 1] != 0 || this.m_data[position - 2] != 0)
			{
				throw new XmlSyntaxException();
			}
			position -= 2;
			this.AddString(str, ref position);
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000D90AD File Offset: 0x000D72AD
		public static int EncodedStringSize(string str)
		{
			return str.Length * 2 + 2;
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000D90B9 File Offset: 0x000D72B9
		public string GetString(ref int position)
		{
			return this.GetString(ref position, true);
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000D90C4 File Offset: 0x000D72C4
		public string GetString(ref int position, bool bCreate)
		{
			int num = position;
			while (num < this.m_data.Length - 1 && (this.m_data[num] != 0 || this.m_data[num + 1] != 0))
			{
				num += 2;
			}
			Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
			string text;
			try
			{
				if (bCreate)
				{
					sharedStringMaker._outStringBuilder = null;
					sharedStringMaker._outIndex = 0;
					for (int i = position; i < num; i += 2)
					{
						char c = (char)(((int)this.m_data[i] << 8) | (int)this.m_data[i + 1]);
						if (sharedStringMaker._outIndex < 512)
						{
							char[] outChars = sharedStringMaker._outChars;
							Tokenizer.StringMaker stringMaker = sharedStringMaker;
							int outIndex = stringMaker._outIndex;
							stringMaker._outIndex = outIndex + 1;
							outChars[outIndex] = c;
						}
						else
						{
							if (sharedStringMaker._outStringBuilder == null)
							{
								sharedStringMaker._outStringBuilder = new StringBuilder();
							}
							sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
							sharedStringMaker._outChars[0] = c;
							sharedStringMaker._outIndex = 1;
						}
					}
				}
				position = num + 2;
				if (bCreate)
				{
					text = sharedStringMaker.MakeString();
				}
				else
				{
					text = null;
				}
			}
			finally
			{
				SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
			}
			return text;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000D91D8 File Offset: 0x000D73D8
		public void AddToken(byte b, ref int position)
		{
			this.GuaranteeSize(position + 1);
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			data[num] = b;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000D9200 File Offset: 0x000D7400
		public void ConvertElement(SecurityElement elCurrent, ref int position)
		{
			this.AddToken(1, ref position);
			this.AddString(elCurrent.m_strTag, ref position);
			if (elCurrent.m_lAttributes != null)
			{
				for (int i = 0; i < elCurrent.m_lAttributes.Count; i += 2)
				{
					this.AddToken(2, ref position);
					this.AddString((string)elCurrent.m_lAttributes[i], ref position);
					this.AddString((string)elCurrent.m_lAttributes[i + 1], ref position);
				}
			}
			if (elCurrent.m_strText != null)
			{
				this.AddToken(3, ref position);
				this.AddString(elCurrent.m_strText, ref position);
			}
			if (elCurrent.InternalChildren != null)
			{
				for (int j = 0; j < elCurrent.InternalChildren.Count; j++)
				{
					this.ConvertElement((SecurityElement)elCurrent.Children[j], ref position);
				}
			}
			this.AddToken(4, ref position);
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000D92D5 File Offset: 0x000D74D5
		public SecurityElement GetRootElement()
		{
			return this.GetElement(0, true);
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000D92DF File Offset: 0x000D74DF
		public SecurityElement GetElement(int position, bool bCreate)
		{
			return this.InternalGetElement(ref position, bCreate);
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000D92EC File Offset: 0x000D74EC
		internal SecurityElement InternalGetElement(ref int position, bool bCreate)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			if (data[num] != 1)
			{
				throw new XmlSyntaxException();
			}
			SecurityElement securityElement = null;
			string @string = this.GetString(ref position, bCreate);
			if (bCreate)
			{
				securityElement = new SecurityElement(@string);
			}
			while (this.m_data[position] == 2)
			{
				position++;
				string string2 = this.GetString(ref position, bCreate);
				string string3 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddAttribute(string2, string3);
				}
			}
			if (this.m_data[position] == 3)
			{
				position++;
				string string4 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.m_strText = string4;
				}
			}
			while (this.m_data[position] != 4)
			{
				SecurityElement securityElement2 = this.InternalGetElement(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddChild(securityElement2);
				}
			}
			position++;
			return securityElement;
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x000D93BD File Offset: 0x000D75BD
		public string GetTagForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			return this.GetString(ref position);
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x000D93F0 File Offset: 0x000D75F0
		public ArrayList GetChildrenPositionForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			ArrayList arrayList = new ArrayList();
			this.GetString(ref position);
			while (this.m_data[position] == 2)
			{
				position++;
				this.GetString(ref position, false);
				this.GetString(ref position, false);
			}
			if (this.m_data[position] == 3)
			{
				position++;
				this.GetString(ref position, false);
			}
			while (this.m_data[position] != 4)
			{
				arrayList.Add(position);
				this.InternalGetElement(ref position, false);
			}
			position++;
			return arrayList;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000D94A0 File Offset: 0x000D76A0
		public string GetAttributeForElement(int position, string attributeName)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			string text = null;
			this.GetString(ref position, false);
			while (this.m_data[position] == 2)
			{
				position++;
				string @string = this.GetString(ref position);
				string string2 = this.GetString(ref position);
				if (string.Equals(@string, attributeName))
				{
					text = string2;
					break;
				}
			}
			return text;
		}

		// Token: 0x04001F3A RID: 7994
		internal byte[] m_data;

		// Token: 0x04001F3B RID: 7995
		internal const byte c_element = 1;

		// Token: 0x04001F3C RID: 7996
		internal const byte c_attribute = 2;

		// Token: 0x04001F3D RID: 7997
		internal const byte c_text = 3;

		// Token: 0x04001F3E RID: 7998
		internal const byte c_children = 4;

		// Token: 0x04001F3F RID: 7999
		internal const int c_growthSize = 32;
	}
}
