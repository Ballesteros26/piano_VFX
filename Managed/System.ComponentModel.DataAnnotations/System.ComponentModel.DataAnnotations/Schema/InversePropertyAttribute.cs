using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Represents an inverse property attribute.</summary>
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class InversePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.InversePropertyAttribute" /> class using the specified property.</summary>
		/// <param name="property">The property of the attribute.</param>
		// Token: 0x060001B9 RID: 441 RVA: 0x00005DCE File Offset: 0x00003FCE
		public InversePropertyAttribute(string property)
		{
			if (string.IsNullOrWhiteSpace(property))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "property"));
			}
			this._property = property;
		}

		/// <summary>Gets the property of the attribute.</summary>
		/// <returns>The property of the attribute.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005DFF File Offset: 0x00003FFF
		public string Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x04000119 RID: 281
		private readonly string _property;
	}
}
