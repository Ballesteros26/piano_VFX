using System;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Represents a database generated attribute.</summary>
	// Token: 0x02000049 RID: 73
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DatabaseGeneratedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute" /> class.</summary>
		/// <param name="databaseGeneratedOption">The database generated option.</param>
		// Token: 0x060001B4 RID: 436 RVA: 0x00005D53 File Offset: 0x00003F53
		public DatabaseGeneratedAttribute(DatabaseGeneratedOption databaseGeneratedOption)
		{
			if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			this.DatabaseGeneratedOption = databaseGeneratedOption;
		}

		/// <summary>Gets or sets the database generated option.</summary>
		/// <returns>The database generated option.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005D84 File Offset: 0x00003F84
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005D8C File Offset: 0x00003F8C
		public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }
	}
}
