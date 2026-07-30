using System;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	// Token: 0x020000D7 RID: 215
	public class PanelDesigner : ReadWriteControlDesigner
	{
		/// <summary>Maps a specified property and value to a specified markup style.</summary>
		/// <param name="propName">A string containing the property name. </param>
		/// <param name="varPropValue">An object that is the property value. </param>
		// Token: 0x0600063D RID: 1597 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void MapPropertyToStyle(string propName, object varPropValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides notification when a behavior is attached to the designer.</summary>
		// Token: 0x0600063E RID: 1598 RVA: 0x0000234B File Offset: 0x0000054B
		protected override void OnBehaviorAttached()
		{
			throw new NotImplementedException();
		}
	}
}
