using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Extends design-time behavior for read/write server controls.</summary>
	// Token: 0x0200009D RID: 157
	[Obsolete("Use ContainerControlDesigner instead")]
	[MonoTODO]
	public class ReadWriteControlDesigner : ControlDesigner
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.ReadWriteControlDesigner" /> class.</summary>
		// Token: 0x060004A8 RID: 1192 RVA: 0x000092B3 File Offset: 0x000074B3
		[MonoTODO]
		public ReadWriteControlDesigner()
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a property, including description and value, to an intrinsic HTML style.</summary>
		/// <param name="propName">The name of the property to map. </param>
		/// <param name="varPropValue">The value of the property. </param>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void MapPropertyToStyle(string propName, object varPropValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides notification that is raised when a behavior is attached to the designer.</summary>
		// Token: 0x060004AA RID: 1194 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use ControlDesigner.Tag instead")]
		protected override void OnBehaviorAttached()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event of the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> class.</summary>
		/// <param name="sender">The object sending the event. </param>
		/// <param name="ce">The <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> object that provides data for the event. </param>
		// Token: 0x060004AB RID: 1195 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
		/// <returns>The HTML that is used to represent the control at design time.</returns>
		// Token: 0x060004AC RID: 1196 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the display of the control.</summary>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void UpdateDesignTimeHtml()
		{
			throw new NotImplementedException();
		}
	}
}
