using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001EC RID: 492
	internal sealed class SqlStreamingXml
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x000705B1 File Offset: 0x0006E7B1
		public SqlStreamingXml(int i, SqlDataReader reader)
		{
			this._columnOrdinal = i;
			this._reader = reader;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000705C7 File Offset: 0x0006E7C7
		public void Close()
		{
			((IDisposable)this._xmlWriter).Dispose();
			((IDisposable)this._xmlReader).Dispose();
			this._reader = null;
			this._xmlReader = null;
			this._xmlWriter = null;
			this._strWriter = null;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000705FB File Offset: 0x0006E7FB
		public int ColumnOrdinal
		{
			get
			{
				return this._columnOrdinal;
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00070604 File Offset: 0x0006E804
		public long GetChars(long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._xmlReader == null)
			{
				SqlStream sqlStream = new SqlStream(this._columnOrdinal, this._reader, true, false, false);
				this._xmlReader = sqlStream.ToXmlReader(false);
				this._strWriter = new StringWriter(null);
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.CloseOutput = true;
				xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				this._xmlWriter = XmlWriter.Create(this._strWriter, xmlWriterSettings);
			}
			int num = 0;
			if (dataIndex < this._charsRemoved)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._charsRemoved, "GetChars");
			}
			if (dataIndex > this._charsRemoved)
			{
				num = (int)(dataIndex - this._charsRemoved);
			}
			if (buffer == null)
			{
				return -1L;
			}
			StringBuilder stringBuilder = this._strWriter.GetStringBuilder();
			int num2;
			while (!this._xmlReader.EOF && stringBuilder.Length < length + num)
			{
				this.WriteXmlElement();
				if (num > 0)
				{
					num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
					stringBuilder.Remove(0, num2);
					num -= num2;
					this._charsRemoved += (long)num2;
				}
			}
			if (num > 0)
			{
				num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
				stringBuilder.Remove(0, num2);
				num -= num2;
				this._charsRemoved += (long)num2;
			}
			if (stringBuilder.Length == 0)
			{
				return 0L;
			}
			num2 = ((stringBuilder.Length < length) ? stringBuilder.Length : length);
			for (int i = 0; i < num2; i++)
			{
				buffer[bufferIndex + i] = stringBuilder[i];
			}
			stringBuilder.Remove(0, num2);
			this._charsRemoved += (long)num2;
			return (long)num2;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00070794 File Offset: 0x0006E994
		private void WriteXmlElement()
		{
			if (this._xmlReader.EOF)
			{
				return;
			}
			bool canReadValueChunk = this._xmlReader.CanReadValueChunk;
			char[] array = null;
			this._xmlReader.Read();
			switch (this._xmlReader.NodeType)
			{
			case XmlNodeType.Element:
				this._xmlWriter.WriteStartElement(this._xmlReader.Prefix, this._xmlReader.LocalName, this._xmlReader.NamespaceURI);
				this._xmlWriter.WriteAttributes(this._xmlReader, true);
				if (this._xmlReader.IsEmptyElement)
				{
					this._xmlWriter.WriteEndElement();
				}
				break;
			case XmlNodeType.Text:
				if (canReadValueChunk)
				{
					if (array == null)
					{
						array = new char[1024];
					}
					int num;
					while ((num = this._xmlReader.ReadValueChunk(array, 0, 1024)) > 0)
					{
						this._xmlWriter.WriteChars(array, 0, num);
					}
				}
				else
				{
					this._xmlWriter.WriteString(this._xmlReader.Value);
				}
				break;
			case XmlNodeType.CDATA:
				this._xmlWriter.WriteCData(this._xmlReader.Value);
				break;
			case XmlNodeType.EntityReference:
				this._xmlWriter.WriteEntityRef(this._xmlReader.Name);
				break;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.XmlDeclaration:
				this._xmlWriter.WriteProcessingInstruction(this._xmlReader.Name, this._xmlReader.Value);
				break;
			case XmlNodeType.Comment:
				this._xmlWriter.WriteComment(this._xmlReader.Value);
				break;
			case XmlNodeType.DocumentType:
				this._xmlWriter.WriteDocType(this._xmlReader.Name, this._xmlReader.GetAttribute("PUBLIC"), this._xmlReader.GetAttribute("SYSTEM"), this._xmlReader.Value);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this._xmlWriter.WriteWhitespace(this._xmlReader.Value);
				break;
			case XmlNodeType.EndElement:
				this._xmlWriter.WriteFullEndElement();
				break;
			}
			this._xmlWriter.Flush();
		}

		// Token: 0x04000F09 RID: 3849
		private int _columnOrdinal;

		// Token: 0x04000F0A RID: 3850
		private SqlDataReader _reader;

		// Token: 0x04000F0B RID: 3851
		private XmlReader _xmlReader;

		// Token: 0x04000F0C RID: 3852
		private XmlWriter _xmlWriter;

		// Token: 0x04000F0D RID: 3853
		private StringWriter _strWriter;

		// Token: 0x04000F0E RID: 3854
		private long _charsRemoved;
	}
}
