using System;
using System.Reflection;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for defining connection point objects that enable the consumer control and the provider control in a Web Parts connection to share data.</summary>
	// Token: 0x0200047F RID: 1151
	public abstract class ConnectionPoint
	{
		// Token: 0x0600344B RID: 13387 RVA: 0x0008AA24 File Offset: 0x00088C24
		internal ConnectionPoint(MethodInfo callBack, Type interFace, Type control, string name, string id, bool allowsMultiConnections)
		{
			this.name = string.Empty;
			this.id = "default";
			base..ctor();
			this.allowMultiConn = allowsMultiConnections;
			this.interfaceType = interFace;
			this.controlType = control;
			this.name = name;
			this.id = id;
			this.callBackMethod = callBack;
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x0008AA7A File Offset: 0x00088C7A
		internal MethodInfo CallbackMethod
		{
			get
			{
				return this.callBackMethod;
			}
		}

		/// <summary>Returns a value that indicates whether a connection point can participate in connections. </summary>
		/// <returns>true if the control can create a connection point to participate in a connection; otherwise, false.</returns>
		/// <param name="control">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that is associated with a connection point.</param>
		// Token: 0x0600344D RID: 13389 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool GetEnabled(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether a connection point supports multiple simultaneous connections.</summary>
		/// <returns>true if the connection point supports multiple connections; otherwise, false. </returns>
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x0008AA82 File Offset: 0x00088C82
		public bool AllowsMultipleConnections
		{
			get
			{
				return this.allowMultiConn;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server control with which a connection point is associated.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the control type.</returns>
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x0008AA8A File Offset: 0x00088C8A
		public Type ControlType
		{
			get
			{
				return this.controlType;
			}
		}

		/// <summary>Gets a string that contains the identifier for a connection point.</summary>
		/// <returns>A string that contains the identifier for a connection point.</returns>
		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x0008AA92 File Offset: 0x00088C92
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the type of the interface used by a connection point.</summary>
		/// <returns>A <see cref="T:System.Type" /> that corresponds to the interface type provided or consumed by a control.</returns>
		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x0008AA9A File Offset: 0x00088C9A
		public Type InterfaceType
		{
			get
			{
				return this.interfaceType;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x0008AAA2 File Offset: 0x00088CA2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal ConnectionPoint()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a string that serves as a friendly display name to represent a connection point in the user interface (UI). </summary>
		/// <returns>A string that contains a friendly display name for a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> object. </returns>
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001D00 RID: 7424
		private bool allowMultiConn;

		// Token: 0x04001D01 RID: 7425
		private string name;

		// Token: 0x04001D02 RID: 7426
		private string id;

		// Token: 0x04001D03 RID: 7427
		private Type interfaceType;

		// Token: 0x04001D04 RID: 7428
		private Type controlType;

		// Token: 0x04001D05 RID: 7429
		private MethodInfo callBackMethod;

		/// <summary>Represents a string used to identify the default connection point within a collection of connection points associated with a server control. </summary>
		// Token: 0x04001D06 RID: 7430
		public const string DefaultID = "default";
	}
}
