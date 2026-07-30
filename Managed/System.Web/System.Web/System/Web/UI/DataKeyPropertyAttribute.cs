using System;

namespace System.Web.UI
{
	/// <summary>Specifies the default property of a control that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> property binds to at run time.</summary>
	// Token: 0x0200015A RID: 346
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataKeyPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class by using the name of the data-key property attribute.</summary>
		/// <param name="name">The name of the data-key property attribute.</param>
		// Token: 0x06000F30 RID: 3888 RVA: 0x0002B1ED File Offset: 0x000293ED
		public DataKeyPropertyAttribute(string name)
		{
			this._name = name;
		}

		/// <summary>Gets the name of the data-key property attribute.</summary>
		/// <returns>The name of the data-key property attribute.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002B1FC File Offset: 0x000293FC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Compares the name of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> object to a specified object.</summary>
		/// <returns>true if the name is the same as the object; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000F32 RID: 3890 RVA: 0x0002B204 File Offset: 0x00029404
		public override bool Equals(object obj)
		{
			DataKeyPropertyAttribute dataKeyPropertyAttribute = obj as DataKeyPropertyAttribute;
			return dataKeyPropertyAttribute != null && string.Equals(this._name, dataKeyPropertyAttribute.Name, StringComparison.Ordinal);
		}

		/// <summary>Gets the hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</summary>
		/// <returns>The hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</returns>
		// Token: 0x06000F33 RID: 3891 RVA: 0x0002B22F File Offset: 0x0002942F
		public override int GetHashCode()
		{
			if (this.Name == null)
			{
				return 0;
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x04001233 RID: 4659
		private readonly string _name;
	}
}
