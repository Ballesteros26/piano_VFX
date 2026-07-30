using System;
using System.ComponentModel;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides an abstract definition of an action supported by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F7 RID: 247
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Operation : NamedItem
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001C6C1 File Offset: 0x0001A8C1
		internal void SetParent(PortType parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Operation" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Operation" />.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001C6CA File Offset: 0x0001A8CA
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.PortType" /> of which the <see cref="T:System.Web.Services.Description.Operation" /> is a member.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.PortType" /> object.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001C6E6 File Offset: 0x0001A8E6
		public PortType PortType
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets an optional Remote Procedure Call (RPC) signature that orders specification for request-response or solicit-response operations.</summary>
		/// <returns>A list of names of the <see cref="T:System.Web.Services.Description.MessagePart" /> instances separated by a single space.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001C746 File Offset: 0x0001A946
		[DefaultValue("")]
		[XmlAttribute("parameterOrder")]
		public string ParameterOrderString
		{
			get
			{
				if (this.parameters == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.parameters.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(this.parameters[i]);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					this.parameters = null;
					return;
				}
				this.parameters = value.Split(new char[] { ' ' });
			}
		}

		/// <summary>Gets or sets an array of the elements contained in the <see cref="P:System.Web.Services.Description.Operation.ParameterOrderString" />.</summary>
		/// <returns>An array of names of <see cref="T:System.Web.Services.Description.MessagePart" /> instances.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001C76A File Offset: 0x0001A96A
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001C772 File Offset: 0x0001A972
		[XmlIgnore]
		public string[] ParameterOrder
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		/// <summary>Gets the collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</summary>
		/// <returns>The collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001C77B File Offset: 0x0001A97B
		[XmlElement("input", typeof(OperationInput))]
		[XmlElement("output", typeof(OperationOutput))]
		public OperationMessageCollection Messages
		{
			get
			{
				if (this.messages == null)
				{
					this.messages = new OperationMessageCollection(this);
				}
				return this.messages;
			}
		}

		/// <summary>Gets the collection of faults, or error messages, defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</summary>
		/// <returns>A collection of faults, or error messages, defined by the current operation.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001C797 File Offset: 0x0001A997
		[XmlElement("fault")]
		public OperationFaultCollection Faults
		{
			get
			{
				if (this.faults == null)
				{
					this.faults = new OperationFaultCollection(this);
				}
				return this.faults;
			}
		}

		/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> matches with the <see cref="T:System.Web.Services.Description.Operation" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.Operation" /> instance matches the <paramref name="operationBinding" /> parameter; otherwise, false.</returns>
		/// <param name="operationBinding">An <see cref="T:System.Web.Services.Description.OperationBinding" /> to be checked to determine whether it matches with the <see cref="T:System.Web.Services.Description.Operation" />. </param>
		// Token: 0x06000691 RID: 1681 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		public bool IsBoundBy(OperationBinding operationBinding)
		{
			if (operationBinding.Name != base.Name)
			{
				return false;
			}
			OperationMessage input = this.Messages.Input;
			if (input != null)
			{
				if (operationBinding.Input == null)
				{
					return false;
				}
				string messageName = this.GetMessageName(base.Name, input.Name, true);
				if (this.GetMessageName(operationBinding.Name, operationBinding.Input.Name, true) != messageName)
				{
					return false;
				}
			}
			else if (operationBinding.Input != null)
			{
				return false;
			}
			OperationMessage output = this.Messages.Output;
			if (output != null)
			{
				if (operationBinding.Output == null)
				{
					return false;
				}
				string messageName2 = this.GetMessageName(base.Name, output.Name, false);
				if (this.GetMessageName(operationBinding.Name, operationBinding.Output.Name, false) != messageName2)
				{
					return false;
				}
			}
			else if (operationBinding.Output != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001C88C File Offset: 0x0001AA8C
		private string GetMessageName(string operationName, string messageName, bool isInput)
		{
			if (messageName != null && messageName.Length > 0)
			{
				return messageName;
			}
			switch (this.Messages.Flow)
			{
			case OperationFlow.OneWay:
				if (isInput)
				{
					return operationName;
				}
				return null;
			case OperationFlow.Notification:
				return null;
			case OperationFlow.RequestResponse:
				if (isInput)
				{
					return operationName + "Request";
				}
				return operationName + "Response";
			case OperationFlow.SolicitResponse:
				return null;
			default:
				return null;
			}
		}

		// Token: 0x04000401 RID: 1025
		private string[] parameters;

		// Token: 0x04000402 RID: 1026
		private OperationMessageCollection messages;

		// Token: 0x04000403 RID: 1027
		private OperationFaultCollection faults;

		// Token: 0x04000404 RID: 1028
		private PortType parent;

		// Token: 0x04000405 RID: 1029
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
