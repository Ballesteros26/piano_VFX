using System;
using System.ComponentModel;

namespace System.Timers
{
	/// <summary>Sets the description that visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x02000131 RID: 305
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.TimersDescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06000852 RID: 2130 RVA: 0x000269B3 File Offset: 0x00024BB3
		public TimersDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description that visual designers can display when referencing an event, extender, or property.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00028700 File Offset: 0x00026900
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = global::SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000DA8 RID: 3496
		private bool replaced;
	}
}
