using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base class for a Web server control <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
	// Token: 0x020000B8 RID: 184
	[MonoTODO]
	[Serializable]
	public class WebControlToolboxItem : ToolboxItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class.</summary>
		// Token: 0x06000551 RID: 1361 RVA: 0x0000953B File Offset: 0x0000773B
		public WebControlToolboxItem()
		{
			this.toolData = null;
			this.persistChildren = -1;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class using the provided type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the tool for this toolbox item. </param>
		// Token: 0x06000552 RID: 1362 RVA: 0x0000953B File Offset: 0x0000773B
		[MonoTODO]
		public WebControlToolboxItem(Type type)
		{
			this.toolData = null;
			this.persistChildren = -1;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class using the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A SerializationInfo object containing information needed to instantiate the Web control through deserialization.</param>
		/// <param name="context">A StreamingContext object.</param>
		// Token: 0x06000553 RID: 1363 RVA: 0x00009551 File Offset: 0x00007751
		protected WebControlToolboxItem(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates objects from each type contained in this <see cref="T:System.Drawing.Design.ToolboxItem" />, and adds them to the specified designer.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Web.UI.Design.WebControlToolboxItem.CreateComponentsCore(System.ComponentModel.Design.IDesignerHost)" /> method is only available in Windows Forms.</exception>
		// Token: 0x06000554 RID: 1364 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used to save the state of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the serialization stream characteristics.</param>
		// Token: 0x06000555 RID: 1365 RVA: 0x0000955E File Offset: 0x0000775E
		[MonoTODO]
		protected override void Serialize(SerializationInfo info, StreamingContext context)
		{
			base.Serialize(info, context);
			if (this.toolData != null)
			{
				info.AddValue("ToolData", this.toolData);
			}
			if (this.persistChildren != -1)
			{
				info.AddValue("PersistChildren", this.persistChildren);
			}
		}

		/// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the serialization stream characteristics. </param>
		// Token: 0x06000556 RID: 1366 RVA: 0x0000959B File Offset: 0x0000779B
		[MonoTODO]
		protected override void Deserialize(SerializationInfo info, StreamingContext context)
		{
			base.Deserialize(info, context);
			this.toolData = info.GetString("ToolData");
			this.persistChildren = info.GetInt32("PersistChildren");
		}

		/// <summary>Initializes this toolbox item.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the Web server control toolbox item. </param>
		// Token: 0x06000557 RID: 1367 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void Initialize(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the specified type of attribute of the toolbox item.</summary>
		/// <returns>The value of the specified type of attribute.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		/// <param name="attributeType">The type of attribute to retrieve the value of. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="attributeType" /> parameter is not a <see cref="T:System.Web.UI.PersistChildrenAttribute" />. </exception>
		// Token: 0x06000558 RID: 1368 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public object GetToolAttributeValue(IDesignerHost host, Type attributeType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the HTML for the Web control that the tool creates.</summary>
		/// <returns>The HTML for the Web control that the tool creates.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		// Token: 0x06000559 RID: 1369 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GetToolHtml(IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of this toolbox item.</summary>
		/// <returns>The <see cref="T:System.Type" /> of this toolbox item.</returns>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document. </param>
		// Token: 0x0600055A RID: 1370 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Type GetToolType(IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000148 RID: 328
		private int persistChildren;

		// Token: 0x04000149 RID: 329
		private string toolData;
	}
}
