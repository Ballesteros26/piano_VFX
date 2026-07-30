using System;
using System.ComponentModel;

namespace System.DirectoryServices
{
	/// <summary>Specifies how to sort the results of a search.          </summary>
	// Token: 0x02000031 RID: 49
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SortOption
	{
		/// <summary>Initializes a new instance of the <see cref="M:System.DirectoryServices.SortOption.#ctor" /> class.          </summary>
		// Token: 0x0600019F RID: 415 RVA: 0x00002050 File Offset: 0x00000250
		public SortOption()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.SortOption" /> class, which contains the specified property name and specified sort direction.          </summary>
		/// <param name="propertyName">The name of the property to sort by. The <see cref="P:System.DirectoryServices.SortOption.PropertyName" /> property is set to this value.</param>
		/// <param name="direction">One of the <see cref="T:System.DirectoryServices.SortDirection" /> values. The <see cref="P:System.DirectoryServices.SortOption.Direction" /> property is set to this value.</param>
		// Token: 0x060001A0 RID: 416 RVA: 0x00004A3C File Offset: 0x00002C3C
		public SortOption(string propertyName, SortDirection direction)
		{
			this.propertyName = propertyName;
			this.direction = direction;
		}

		/// <summary>Gets or sets the name of the property to sort on.</summary>
		/// <returns>The name of the property to sort on. The default is a null reference (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00004A52 File Offset: 0x00002C52
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00004A5A File Offset: 0x00002C5A
		[DSDescription("Name of propertion to be sorted on")]
		[DefaultValue(null)]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.propertyName = value;
			}
		}

		/// <summary>Gets or sets the direction in which to sort the results of a query.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.SortDirection" /> values. The default is Ascending.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.SortDirection" /> values.</exception>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004A71 File Offset: 0x00002C71
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00004A79 File Offset: 0x00002C79
		[DSDescription("Whether the sort is ascending or descending")]
		[DefaultValue(SortDirection.Ascending)]
		public SortDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		// Token: 0x040000B0 RID: 176
		private string propertyName;

		// Token: 0x040000B1 RID: 177
		private SortDirection direction;
	}
}
