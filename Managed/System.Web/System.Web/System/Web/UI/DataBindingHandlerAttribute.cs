using System;

namespace System.Web.UI
{
	/// <summary>Specifies a design-time class that performs data binding of controls within a designer. This class cannot be inherited.</summary>
	// Token: 0x02000159 RID: 345
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataBindingHandlerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class using no parameters. This is the default constructor.</summary>
		// Token: 0x06000F29 RID: 3881 RVA: 0x0002B153 File Offset: 0x00029353
		public DataBindingHandlerAttribute()
		{
			this._typeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for the data-binding handler. </param>
		// Token: 0x06000F2A RID: 3882 RVA: 0x0002B166 File Offset: 0x00029366
		public DataBindingHandlerAttribute(Type type)
		{
			this._typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class with the specified type name.</summary>
		/// <param name="typeName">The fully qualified name of the data-binding handler <see cref="T:System.Type" />. </param>
		// Token: 0x06000F2B RID: 3883 RVA: 0x0002B17A File Offset: 0x0002937A
		public DataBindingHandlerAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		/// <summary>Gets the type name of the data-binding handler. </summary>
		/// <returns>The type name of the handler. If the type name is null, this property returns an empty string ("").</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0002B189 File Offset: 0x00029389
		public string HandlerTypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		/// <summary>Determines whether two object instances are equal.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter equals the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current <see cref="T:System.Web.UI.DataBindingHandlerAttribute" />.</param>
		// Token: 0x06000F2D RID: 3885 RVA: 0x0002B1A0 File Offset: 0x000293A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataBindingHandlerAttribute dataBindingHandlerAttribute = obj as DataBindingHandlerAttribute;
			return dataBindingHandlerAttribute != null && string.Compare(this.HandlerTypeName, dataBindingHandlerAttribute.HandlerTypeName, StringComparison.Ordinal) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F2E RID: 3886 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public override int GetHashCode()
		{
			return this.HandlerTypeName.GetHashCode();
		}

		// Token: 0x04001231 RID: 4657
		private string _typeName;

		/// <summary>Defines the default attribute for the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class.</summary>
		// Token: 0x04001232 RID: 4658
		public static readonly DataBindingHandlerAttribute Default = new DataBindingHandlerAttribute();
	}
}
