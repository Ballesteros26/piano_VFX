using System;

namespace System.Xml.Serialization
{
	/// <summary>When applied to a type, stores the name of a static method of the type that returns an XML schema and a <see cref="T:System.Xml.XmlQualifiedName" /> (or <see cref="T:System.Xml.Schema.XmlSchemaType" /> for anonymous types) that controls the serialization of the type.</summary>
	// Token: 0x02000345 RID: 837
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> class, taking the name of the static method that supplies the type's XML schema.</summary>
		/// <param name="methodName">The name of the static method that must be implemented.</param>
		// Token: 0x06002072 RID: 8306 RVA: 0x000B5301 File Offset: 0x000B3501
		public XmlSchemaProviderAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		/// <summary>Gets the name of the static method that supplies the type's XML schema and the name of its XML Schema data type.</summary>
		/// <returns>The name of the method that is invoked by the XML infrastructure to return an XML schema.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x000B5310 File Offset: 0x000B3510
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets or sets a value that determines whether the target class is a wildcard, or that the schema for the class has contains only an xs:any element.</summary>
		/// <returns>true, if the class is a wildcard, or if the schema contains only the xs:any element; otherwise, false.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x000B5318 File Offset: 0x000B3518
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x000B5320 File Offset: 0x000B3520
		public bool IsAny
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x0400177E RID: 6014
		private string methodName;

		// Token: 0x0400177F RID: 6015
		private bool any;
	}
}
