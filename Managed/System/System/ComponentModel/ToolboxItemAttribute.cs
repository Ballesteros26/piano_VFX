using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Represents an attribute of a toolbox item.</summary>
	// Token: 0x020002FF RID: 767
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x060018AD RID: 6317 RVA: 0x00068FB3 File Offset: 0x000671B3
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and specifies whether to use default initialization values.</summary>
		/// <param name="defaultType">true to create a toolbox item attribute for a default type; false to associate no default toolbox item support for this attribute. </param>
		// Token: 0x060018AE RID: 6318 RVA: 0x00068FC0 File Offset: 0x000671C0
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this.toolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified name of the type.</summary>
		/// <param name="toolboxItemTypeName">The names of the type of the toolbox item and of the assembly that contains the type. </param>
		// Token: 0x060018AF RID: 6319 RVA: 0x00068FD6 File Offset: 0x000671D6
		public ToolboxItemAttribute(string toolboxItemTypeName)
		{
			toolboxItemTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.toolboxItemTypeName = toolboxItemTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified type of the toolbox item.</summary>
		/// <param name="toolboxItemType">The type of the toolbox item. </param>
		// Token: 0x060018B0 RID: 6320 RVA: 0x00068FF1 File Offset: 0x000671F1
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this.toolboxItemType = toolboxItemType;
			this.toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
		}

		/// <summary>Gets or sets the type of the toolbox item.</summary>
		/// <returns>The type of the toolbox item.</returns>
		/// <exception cref="T:System.ArgumentException">The type cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0006900C File Offset: 0x0006720C
		public Type ToolboxItemType
		{
			get
			{
				if (this.toolboxItemType == null && this.toolboxItemTypeName != null)
				{
					try
					{
						this.toolboxItemType = Type.GetType(this.toolboxItemTypeName, true);
					}
					catch (Exception ex)
					{
						throw new ArgumentException(global::SR.GetString("Failed to create ToolboxItem of type: {0}", new object[] { this.toolboxItemTypeName }), ex);
					}
				}
				return this.toolboxItemType;
			}
		}

		/// <summary>Gets or sets the name of the type of the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>The fully qualified type name of the current toolbox item.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0006907C File Offset: 0x0006727C
		public string ToolboxItemTypeName
		{
			get
			{
				if (this.toolboxItemTypeName == null)
				{
					return string.Empty;
				}
				return this.toolboxItemTypeName;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x060018B3 RID: 6323 RVA: 0x00069094 File Offset: 0x00067294
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemAttribute toolboxItemAttribute = obj as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000690C4 File Offset: 0x000672C4
		public override int GetHashCode()
		{
			if (this.toolboxItemTypeName != null)
			{
				return this.toolboxItemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04001445 RID: 5189
		private Type toolboxItemType;

		// Token: 0x04001446 RID: 5190
		private string toolboxItemTypeName;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to the default, <see cref="T:System.Drawing.Design.ToolboxItem" />. This field is read-only.</summary>
		// Token: 0x04001447 RID: 5191
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to null. This field is read-only.</summary>
		// Token: 0x04001448 RID: 5192
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);
	}
}
