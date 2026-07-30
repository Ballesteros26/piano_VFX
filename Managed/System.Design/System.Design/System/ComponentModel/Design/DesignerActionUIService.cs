using System;

namespace System.ComponentModel.Design
{
	/// <summary>Manages the user interface (UI) for a smart tag panel. This class cannot be inherited.</summary>
	// Token: 0x02000119 RID: 281
	public sealed class DesignerActionUIService : IDisposable
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00002352 File Offset: 0x00000552
		internal DesignerActionUIService()
		{
		}

		/// <summary>Occurs when a request is made to show or hide a smart tag panel.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000830 RID: 2096 RVA: 0x0000D99C File Offset: 0x0000BB9C
		// (remove) Token: 0x06000831 RID: 2097 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public event DesignerActionUIStateChangeEventHandler DesignerActionUIStateChange;

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionUIService" /> class.</summary>
		// Token: 0x06000832 RID: 2098 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public void Dispose()
		{
		}

		/// <summary>Displays the smart tag panel for a component.</summary>
		/// <param name="component">The component whose smart tag panel should be displayed.</param>
		// Token: 0x06000833 RID: 2099 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void ShowUI(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Hides the smart tag panel for a component.</summary>
		/// <param name="component">The component whose smart tag panel should be hidden.</param>
		// Token: 0x06000834 RID: 2100 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void HideUI(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the smart tag panel.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to refresh.</param>
		// Token: 0x06000835 RID: 2101 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Refresh(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether to automatically show the smart tag panel.</summary>
		/// <returns>true to automatically show the smart tag panel; otherwise, false.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to automatically show.</param>
		// Token: 0x06000836 RID: 2102 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool ShouldAutoShow(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
