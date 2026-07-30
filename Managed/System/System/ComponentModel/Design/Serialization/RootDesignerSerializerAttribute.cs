using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x0200035A RID: 858
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06001A9F RID: 6815 RVA: 0x0006B70F File Offset: 0x0006990F
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The name of the base type of the serializer. A class can include multiple serializers, as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x0006B736 File Offset: 0x00069936
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0006B758 File Offset: 0x00069958
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
			this.reloadable = reloadable;
		}

		/// <summary>Gets a value indicating whether the root serializer supports reloading of the design document without first disposing the designer host.</summary>
		/// <returns>true if the root serializer supports reloading; otherwise, false.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0006B775 File Offset: 0x00069975
		public bool Reloadable
		{
			get
			{
				return this.reloadable;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The name of the type of the serializer.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0006B77D File Offset: 0x0006997D
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0006B785 File Offset: 0x00069985
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0006B790 File Offset: 0x00069990
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

		// Token: 0x04001845 RID: 6213
		private bool reloadable;

		// Token: 0x04001846 RID: 6214
		private string serializerTypeName;

		// Token: 0x04001847 RID: 6215
		private string serializerBaseTypeName;

		// Token: 0x04001848 RID: 6216
		private string typeId;
	}
}
