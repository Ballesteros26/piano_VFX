using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Allows Web Parts controls to track the specific phases of the personalization load and save process.</summary>
	// Token: 0x02000461 RID: 1121
	public interface ITrackingPersonalizable
	{
		/// <summary>Indicates whether the control tracks the status of its changes.</summary>
		/// <returns>true if the Web Parts control is responsible for determining when the control is considered changed ("dirty"); otherwise, false.</returns>
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060033E0 RID: 13280
		bool TracksChanges { get; }

		/// <summary>Represents the beginning of the load phase for personalization information. </summary>
		// Token: 0x060033E1 RID: 13281
		void BeginLoad();

		/// <summary>Represents the phase prior to extracting personalization data from a control. </summary>
		// Token: 0x060033E2 RID: 13282
		void BeginSave();

		/// <summary>Represents the phase after personalization data has been applied to a control. </summary>
		// Token: 0x060033E3 RID: 13283
		void EndLoad();

		/// <summary>Represents the phase after personalization data has been extracted from a control. </summary>
		// Token: 0x060033E4 RID: 13284
		void EndSave();
	}
}
