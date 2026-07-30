using System;

namespace System.Web.UI.Design
{
	/// <summary>Represents the kind of event that has occurred on a view of a control at design time. This class cannot be inherited. </summary>
	// Token: 0x020000B3 RID: 179
	public sealed class ViewEvent
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00002352 File Offset: 0x00000552
		private ViewEvent()
		{
		}

		/// <summary>Indicates that a view event was raised for a click on a designer region.</summary>
		// Token: 0x0400013E RID: 318
		public static readonly ViewEvent Click = new ViewEvent();

		/// <summary>Indicates that a view event was raised for drawing a control on the design surface.</summary>
		// Token: 0x0400013F RID: 319
		public static readonly ViewEvent Paint = new ViewEvent();

		/// <summary>Indicates that a view event was raised for changing the template mode of a control designer.</summary>
		// Token: 0x04000140 RID: 320
		public static readonly ViewEvent TemplateModeChanged = new ViewEvent();
	}
}
