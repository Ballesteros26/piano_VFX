using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object as encoded SOAP XML.</summary>
	// Token: 0x02000312 RID: 786
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapIncludeAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type of the object to include. </param>
		// Token: 0x06001D56 RID: 7510 RVA: 0x000A02C9 File Offset: 0x0009E4C9
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to use when serializing or deserializing an object.</summary>
		/// <returns>The type of the object to include.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000A02D8 File Offset: 0x0009E4D8
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x000A02E0 File Offset: 0x0009E4E0
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04001699 RID: 5785
		private Type type;
	}
}
