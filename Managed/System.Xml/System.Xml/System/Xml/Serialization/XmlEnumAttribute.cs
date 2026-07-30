using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
	// Token: 0x02000332 RID: 818
	[AttributeUsage(AttributeTargets.Field)]
	public class XmlEnumAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class.</summary>
		// Token: 0x06001F5B RID: 8027 RVA: 0x0009F79F File Offset: 0x0009D99F
		public XmlEnumAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class, and specifies the XML value that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates or recognizes (when it serializes or deserializes the enumeration, respectively).</summary>
		/// <param name="name">The overriding name of the enumeration member. </param>
		// Token: 0x06001F5C RID: 8028 RVA: 0x000A9C62 File Offset: 0x000A7E62
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration, or the value recognized when it deserializes the enumeration member.</summary>
		/// <returns>The value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes the enumeration, or the value recognized when it is deserializes the enumeration member.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000A9C71 File Offset: 0x000A7E71
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x000A9C79 File Offset: 0x000A7E79
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0400173B RID: 5947
		private string name;
	}
}
