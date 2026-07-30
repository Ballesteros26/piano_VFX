using System;

namespace System.Web.UI
{
	/// <summary>Defines the attribute that controls use to identify string properties containing URL values. This class cannot be inherited.</summary>
	// Token: 0x0200023F RID: 575
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class UrlPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class.</summary>
		// Token: 0x060017B7 RID: 6071 RVA: 0x0004075D File Offset: 0x0003E95D
		public UrlPropertyAttribute()
			: this("*.*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class, setting the <see cref="P:System.Web.UI.UrlPropertyAttribute.Filter" /> property to the specified string.</summary>
		/// <param name="filter">A file filter associated with the URL-specific property.</param>
		// Token: 0x060017B8 RID: 6072 RVA: 0x0004076A File Offset: 0x0003E96A
		public UrlPropertyAttribute(string filter)
		{
			this.filter = filter;
		}

		/// <summary>Gets a file filter associated with the URL-specific property. </summary>
		/// <returns>A file filter associated with the URL-specific property. The default is "*.*".</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00040779 File Offset: 0x0003E979
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x060017BA RID: 6074 RVA: 0x00040784 File Offset: 0x0003E984
		public override bool Equals(object obj)
		{
			UrlPropertyAttribute urlPropertyAttribute = obj as UrlPropertyAttribute;
			return urlPropertyAttribute != null && this.filter.Equals(urlPropertyAttribute.Filter);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060017BB RID: 6075 RVA: 0x000407AE File Offset: 0x0003E9AE
		public override int GetHashCode()
		{
			return this.filter.GetHashCode();
		}

		// Token: 0x040015F4 RID: 5620
		private string filter;
	}
}
