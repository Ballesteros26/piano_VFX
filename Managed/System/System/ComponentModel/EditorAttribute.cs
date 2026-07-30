using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x02000268 RID: 616
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x060013AE RID: 5038 RVA: 0x00051AD1 File Offset: 0x0004FCD1
		public EditorAttribute()
		{
			this.typeName = string.Empty;
			this.baseTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060013AF RID: 5039 RVA: 0x00051AEF File Offset: 0x0004FCEF
		public EditorAttribute(string typeName, string baseTypeName)
		{
			typeName.ToUpperInvariant();
			this.typeName = typeName;
			this.baseTypeName = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060013B0 RID: 5040 RVA: 0x00051B0C File Offset: 0x0004FD0C
		public EditorAttribute(string typeName, Type baseType)
		{
			typeName.ToUpperInvariant();
			this.typeName = typeName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060013B1 RID: 5041 RVA: 0x00051B2E File Offset: 0x0004FD2E
		public EditorAttribute(Type type, Type baseType)
		{
			this.typeName = type.AssemblyQualifiedName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00051B4E File Offset: 0x0004FD4E
		public string EditorBaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00051B56 File Offset: 0x0004FD56
		public string EditorTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00051B60 File Offset: 0x0004FD60
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.baseTypeName;
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

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060013B5 RID: 5045 RVA: 0x00051BB0 File Offset: 0x0004FDB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.typeName == this.typeName && editorAttribute.baseTypeName == this.baseTypeName;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012CE RID: 4814
		private string baseTypeName;

		// Token: 0x040012CF RID: 4815
		private string typeName;

		// Token: 0x040012D0 RID: 4816
		private string typeId;
	}
}
