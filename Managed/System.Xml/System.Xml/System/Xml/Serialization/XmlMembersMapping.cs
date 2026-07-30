using System;
using System.Text;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between .NET Framework Web service methods and Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x02000338 RID: 824
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x06001F86 RID: 8070 RVA: 0x000A9F0C File Offset: 0x000A810C
		internal XmlMembersMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
			: base(scope, accessor, access)
		{
			MembersMapping membersMapping = (MembersMapping)accessor.Mapping;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(":");
			this.mappings = new XmlMemberMapping[membersMapping.Members.Length];
			for (int i = 0; i < this.mappings.Length; i++)
			{
				if (membersMapping.Members[i].TypeDesc.Type != null)
				{
					stringBuilder.Append(XmlMapping.GenerateKey(membersMapping.Members[i].TypeDesc.Type, null, null));
					stringBuilder.Append(":");
				}
				this.mappings[i] = new XmlMemberMapping(membersMapping.Members[i]);
			}
			base.SetKeyInternal(stringBuilder.ToString());
		}

		/// <summary>Gets the name of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The name of the .NET Framework type.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x000A9FCD File Offset: 0x000A81CD
		public string TypeName
		{
			get
			{
				return base.Accessor.Mapping.TypeName;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The .NET Framework namespace of the mapping.</returns>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000A9FDF File Offset: 0x000A81DF
		public string TypeNamespace
		{
			get
			{
				return base.Accessor.Mapping.Namespace;
			}
		}

		/// <summary>Gets an item that contains internal type mapping information for a .NET Framework code entity that belongs to a Web service method being mapped to a SOAP message.</summary>
		/// <returns>The requested <see cref="T:System.Xml.Serialization.XmlMemberMapping" />.</returns>
		/// <param name="index">The index of the mapping to return.</param>
		// Token: 0x1700067F RID: 1663
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this.mappings[index];
			}
		}

		/// <summary>Gets the number of .NET Framework code entities that belong to a Web service method to which a SOAP message is being mapped. </summary>
		/// <returns>The number of mappings in the collection.</returns>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000A9FFB File Offset: 0x000A81FB
		public int Count
		{
			get
			{
				return this.mappings.Length;
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlMembersMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001749 RID: 5961
		private XmlMemberMapping[] mappings;
	}
}
