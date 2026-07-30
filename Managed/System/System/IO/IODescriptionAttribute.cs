using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003D2 RID: 978
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06001E05 RID: 7685 RVA: 0x000269B3 File Offset: 0x00024BB3
		public IODescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00051547 File Offset: 0x0004F747
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
