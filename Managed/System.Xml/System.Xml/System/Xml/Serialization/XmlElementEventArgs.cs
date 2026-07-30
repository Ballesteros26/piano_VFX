using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
	// Token: 0x0200036F RID: 879
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x000DCB67 File Offset: 0x000DAD67
		internal XmlElementEventArgs(XmlElement elem, int lineNumber, int linePosition, object o, string qnames)
		{
			this.elem = elem;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is deserializing.</summary>
		/// <returns>The object that is being deserialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000DCB94 File Offset: 0x000DAD94
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the object that represents the unknown XML element.</summary>
		/// <returns>The object that represents the unknown XML element.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000DCB9C File Offset: 0x000DAD9C
		public XmlElement Element
		{
			get
			{
				return this.elem;
			}
		}

		/// <summary>Gets the line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000DCBA4 File Offset: 0x000DADA4
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the place in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The number in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000DCBAC File Offset: 0x000DADAC
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML element names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML element names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000DCBB4 File Offset: 0x000DADB4
		public string ExpectedElements
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

		// Token: 0x060023F9 RID: 9209 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlElementEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400187C RID: 6268
		private object o;

		// Token: 0x0400187D RID: 6269
		private XmlElement elem;

		// Token: 0x0400187E RID: 6270
		private string qnames;

		// Token: 0x0400187F RID: 6271
		private int lineNumber;

		// Token: 0x04001880 RID: 6272
		private int linePosition;
	}
}
