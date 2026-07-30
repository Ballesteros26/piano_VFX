using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Represents a column attribute.</summary>
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ColumnAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> class.</summary>
		// Token: 0x060001AC RID: 428 RVA: 0x00005CB1 File Offset: 0x00003EB1
		public ColumnAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> class.</summary>
		/// <param name="name">The name of the column attribute.</param>
		// Token: 0x060001AD RID: 429 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public ColumnAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "name"));
			}
			this._name = name;
		}

		/// <summary>Gets the name of the attribute.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets or sets the order of the column. </summary>
		/// <returns>The order of the column.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005D00 File Offset: 0x00003F00
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005D08 File Offset: 0x00003F08
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._order = value;
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> represents.</summary>
		/// <returns>The name of the class that the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> represents.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005D20 File Offset: 0x00003F20
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005D28 File Offset: 0x00003F28
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "value"));
				}
				this._typeName = value;
			}
		}

		// Token: 0x04000110 RID: 272
		private readonly string _name;

		// Token: 0x04000111 RID: 273
		private string _typeName;

		// Token: 0x04000112 RID: 274
		private int _order = -1;
	}
}
