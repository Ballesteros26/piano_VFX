using System;
using System.Reflection;

namespace System.Web.UI
{
	/// <summary>Acts as the base class for all property entry classes.</summary>
	// Token: 0x0200021E RID: 542
	public abstract class PropertyEntry
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x00002050 File Offset: 0x00000250
		internal PropertyEntry()
		{
		}

		/// <summary>Gets the type of the class that declares this member.</summary>
		/// <returns>The <see cref="T:System.Type" /> that declares this member.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003BB13 File Offset: 0x00039D13
		public Type DeclaringType
		{
			get
			{
				return this.pinfo.DeclaringType;
			}
		}

		/// <summary>Gets or sets the value pertaining to the filter portion of an expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the filter on an expression.</returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0003BB20 File Offset: 0x00039D20
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x0003BB28 File Offset: 0x00039D28
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets the property name that the expression applies to.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the property name.</returns>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0003BB31 File Offset: 0x00039D31
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0003BB39 File Offset: 0x00039D39
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets an object containing attributes of the property the expression applies to.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> containing the attributes of the property.</returns>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0003BB42 File Offset: 0x00039D42
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0003BB4A File Offset: 0x00039D4A
		public PropertyInfo PropertyInfo
		{
			get
			{
				return this.pinfo;
			}
			set
			{
				this.pinfo = value;
			}
		}

		/// <summary>Gets or sets the type of the entry.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the entry.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0003BB53 File Offset: 0x00039D53
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x0003BB5B File Offset: 0x00039D5B
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0400155F RID: 5471
		private Type type;

		// Token: 0x04001560 RID: 5472
		private string name;

		// Token: 0x04001561 RID: 5473
		private string filter;

		// Token: 0x04001562 RID: 5474
		private PropertyInfo pinfo;
	}
}
