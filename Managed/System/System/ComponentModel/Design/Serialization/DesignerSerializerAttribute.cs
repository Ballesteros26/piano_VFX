using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x0200034C RID: 844
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06001A57 RID: 6743 RVA: 0x0006B012 File Offset: 0x00069212
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06001A58 RID: 6744 RVA: 0x0006B032 File Offset: 0x00069232
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06001A59 RID: 6745 RVA: 0x0006B04D File Offset: 0x0006924D
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The fully qualified type name of the serializer.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0006B063 File Offset: 0x00069263
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0006B06B File Offset: 0x0006926B
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0006B074 File Offset: 0x00069274
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.serializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		// Token: 0x04001836 RID: 6198
		private string serializerTypeName;

		// Token: 0x04001837 RID: 6199
		private string serializerBaseTypeName;

		// Token: 0x04001838 RID: 6200
		private string typeId;
	}
}
