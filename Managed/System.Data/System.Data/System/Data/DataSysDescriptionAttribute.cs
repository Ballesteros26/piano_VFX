using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Marks a property, event, or extender with a description. Visual designers can display this description when referencing the member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008B RID: 139
	[AttributeUsage(AttributeTargets.All)]
	[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSysDescriptionAttribute" /> class using the specified description string.</summary>
		/// <param name="description">The description string. </param>
		// Token: 0x06000749 RID: 1865 RVA: 0x0001E816 File Offset: 0x0001CA16
		[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DataSysDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the text for the description. </summary>
		/// <returns>The description string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001E81F File Offset: 0x0001CA1F
		public override string Description
		{
			get
			{
				if (!this._replaced)
				{
					this._replaced = true;
					base.DescriptionValue = base.Description;
				}
				return base.Description;
			}
		}

		// Token: 0x040005D5 RID: 1493
		private bool _replaced;
	}
}
