using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the class used to implement design-time services for a component.</summary>
	// Token: 0x02000260 RID: 608
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the name of the type that provides design-time services.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		// Token: 0x0600137E RID: 4990 RVA: 0x000516D9 File Offset: 0x0004F8D9
		public DesignerAttribute(string designerTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the type that provides design-time services.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		// Token: 0x0600137F RID: 4991 RVA: 0x00051709 File Offset: 0x0004F909
		public DesignerAttribute(Type designerType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the designer type and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseTypeName">The fully qualified name of the base class to associate with the designer class. </param>
		// Token: 0x06001380 RID: 4992 RVA: 0x00051732 File Offset: 0x0004F932
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class, using the name of the designer class and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerTypeName" />. </param>
		// Token: 0x06001381 RID: 4993 RVA: 0x00051754 File Offset: 0x0004F954
		public DesignerAttribute(string designerTypeName, Type designerBaseType)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the types of the designer and designer base class.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerType" />. </param>
		// Token: 0x06001382 RID: 4994 RVA: 0x0005177B File Offset: 0x0004F97B
		public DesignerAttribute(Type designerType, Type designerBaseType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base type of this designer.</summary>
		/// <returns>The name of the base type of this designer.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0005179B File Offset: 0x0004F99B
		public string DesignerBaseTypeName
		{
			get
			{
				return this.designerBaseTypeName;
			}
		}

		/// <summary>Gets the name of the designer type associated with this designer attribute.</summary>
		/// <returns>The name of the designer type associated with this designer attribute.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000517A3 File Offset: 0x0004F9A3
		public string DesignerTypeName
		{
			get
			{
				return this.designerTypeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000517AC File Offset: 0x0004F9AC
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.designerBaseTypeName;
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

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignerAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001386 RID: 4998 RVA: 0x000517FC File Offset: 0x0004F9FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerAttribute designerAttribute = obj as DesignerAttribute;
			return designerAttribute != null && designerAttribute.designerBaseTypeName == this.designerBaseTypeName && designerAttribute.designerTypeName == this.designerTypeName;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005183F File Offset: 0x0004FA3F
		public override int GetHashCode()
		{
			return this.designerTypeName.GetHashCode() ^ this.designerBaseTypeName.GetHashCode();
		}

		// Token: 0x040012B8 RID: 4792
		private readonly string designerTypeName;

		// Token: 0x040012B9 RID: 4793
		private readonly string designerBaseTypeName;

		// Token: 0x040012BA RID: 4794
		private string typeId;
	}
}
