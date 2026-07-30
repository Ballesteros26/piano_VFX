using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
	// Token: 0x0200036D RID: 877
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x000DCB04 File Offset: 0x000DAD04
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNumber, int linePosition, object o, string qnames)
		{
			this.attr = attr;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The object being deserialized.</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000DCB31 File Offset: 0x000DAD31
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets an object that represents the unknown XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlAttribute" /> that represents the unknown XML attribute.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000DCB39 File Offset: 0x000DAD39
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		/// <summary>Gets the line number of the unknown XML attribute.</summary>
		/// <returns>The line number of the unknown XML attribute.</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000DCB41 File Offset: 0x000DAD41
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML attribute.</summary>
		/// <returns>The position number of the unknown XML attribute.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000DCB49 File Offset: 0x000DAD49
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML attribute names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML attribute names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000DCB51 File Offset: 0x000DAD51
		public string ExpectedAttributes
		{
			get
			{
				if (this.qnames != null)
				{
					return this.qnames;
				}
				return string.Empty;
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlAttributeEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001877 RID: 6263
		private object o;

		// Token: 0x04001878 RID: 6264
		private XmlAttribute attr;

		// Token: 0x04001879 RID: 6265
		private string qnames;

		// Token: 0x0400187A RID: 6266
		private int lineNumber;

		// Token: 0x0400187B RID: 6267
		private int linePosition;
	}
}
