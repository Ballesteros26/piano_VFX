using System;
using System.Collections;
using System.Web.Services.Protocols;
using System.Xml;

namespace System.Web.Services.Description
{
	// Token: 0x0200011B RID: 283
	internal class Soap12ProtocolReflector : SoapProtocolReflector
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override WsiProfiles ConformsTo
		{
			get
			{
				return WsiProfiles.None;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0003BE97 File Offset: 0x0003A097
		public override string ProtocolName
		{
			get
			{
				return "Soap12";
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0003BF93 File Offset: 0x0003A193
		protected override void BeginClass()
		{
			this.requestElements = new Hashtable();
			this.actions = new Hashtable();
			this.soap11PortType = null;
			base.BeginClass();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		protected override bool ReflectMethod()
		{
			if (base.ReflectMethod())
			{
				if (base.Binding != null)
				{
					this.soap11PortType = base.SoapMethod.portType;
					if (this.soap11PortType != base.Binding.Type)
					{
						base.HeaderMessages.Clear();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003C00C File Offset: 0x0003A20C
		protected override void EndClass()
		{
			if (base.PortType == null || base.Binding == null)
			{
				return;
			}
			if (this.soap11PortType != null && this.soap11PortType != base.Binding.Type)
			{
				foreach (object obj in base.PortType.Operations)
				{
					foreach (object obj2 in ((Operation)obj).Messages)
					{
						OperationMessage operationMessage = (OperationMessage)obj2;
						ServiceDescription serviceDescription = base.GetServiceDescription(operationMessage.Message.Namespace);
						if (serviceDescription != null)
						{
							Message message = serviceDescription.Messages[operationMessage.Message.Name];
							if (message != null)
							{
								serviceDescription.Messages.Remove(message);
							}
						}
					}
				}
				base.Binding.Type = this.soap11PortType;
				base.PortType.ServiceDescription.PortTypes.Remove(base.PortType);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0003C158 File Offset: 0x0003A358
		protected override SoapBinding CreateSoapBinding(SoapBindingStyle style)
		{
			return new Soap12Binding
			{
				Transport = "http://schemas.xmlsoap.org/soap/http",
				Style = style
			};
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003C174 File Offset: 0x0003A374
		protected override SoapAddressBinding CreateSoapAddressBinding(string serviceUrl)
		{
			Soap12AddressBinding soapAddress = new Soap12AddressBinding();
			soapAddress.Location = serviceUrl;
			if (base.UriFixups != null)
			{
				base.UriFixups.Add(delegate(Uri current)
				{
					soapAddress.Location = DiscoveryServerType.CombineUris(current, soapAddress.Location);
				});
			}
			return soapAddress;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		protected override SoapOperationBinding CreateSoapOperationBinding(SoapBindingStyle style, string action)
		{
			Soap12OperationBinding soap12OperationBinding = new Soap12OperationBinding();
			soap12OperationBinding.SoapAction = action;
			soap12OperationBinding.Style = style;
			soap12OperationBinding.Method = base.SoapMethod;
			this.DealWithAmbiguity(action, base.SoapMethod.requestElementName.ToString(), soap12OperationBinding);
			return soap12OperationBinding;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0003C20C File Offset: 0x0003A40C
		protected override SoapBodyBinding CreateSoapBodyBinding(SoapBindingUse use, string ns)
		{
			Soap12BodyBinding soap12BodyBinding = new Soap12BodyBinding();
			soap12BodyBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soap12BodyBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
			}
			soap12BodyBinding.Namespace = ns;
			return soap12BodyBinding;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0003C23D File Offset: 0x0003A43D
		protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, SoapBindingUse use)
		{
			return this.CreateSoapHeaderBinding(message, partName, null, use);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0003C24C File Offset: 0x0003A44C
		protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, string ns, SoapBindingUse use)
		{
			Soap12HeaderBinding soap12HeaderBinding = new Soap12HeaderBinding();
			soap12HeaderBinding.Message = message;
			soap12HeaderBinding.Part = partName;
			soap12HeaderBinding.Namespace = ns;
			soap12HeaderBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soap12HeaderBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
			}
			return soap12HeaderBinding;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0003C290 File Offset: 0x0003A490
		private void DealWithAmbiguity(string action, string requestElement, Soap12OperationBinding operation)
		{
			Soap12OperationBinding soap12OperationBinding = (Soap12OperationBinding)this.actions[action];
			if (soap12OperationBinding != null)
			{
				operation.DuplicateBySoapAction = soap12OperationBinding;
				soap12OperationBinding.DuplicateBySoapAction = operation;
				this.CheckOperationDuplicates(soap12OperationBinding);
			}
			else
			{
				this.actions[action] = operation;
			}
			Soap12OperationBinding soap12OperationBinding2 = (Soap12OperationBinding)this.requestElements[requestElement];
			if (soap12OperationBinding2 != null)
			{
				operation.DuplicateByRequestElement = soap12OperationBinding2;
				soap12OperationBinding2.DuplicateByRequestElement = operation;
				this.CheckOperationDuplicates(soap12OperationBinding2);
			}
			else
			{
				this.requestElements[requestElement] = operation;
			}
			this.CheckOperationDuplicates(operation);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0003C318 File Offset: 0x0003A518
		private void CheckOperationDuplicates(Soap12OperationBinding operation)
		{
			if (operation.DuplicateByRequestElement == null)
			{
				operation.SoapActionRequired = false;
				return;
			}
			if (operation.DuplicateBySoapAction != null)
			{
				throw new InvalidOperationException(Res.GetString("TheMethodsAndUseTheSameRequestElementAndSoapActionXmlns6", new object[]
				{
					operation.Method.name,
					operation.DuplicateByRequestElement.Method.name,
					operation.Method.requestElementName.Name,
					operation.Method.requestElementName.Namespace,
					operation.DuplicateBySoapAction.Method.name,
					operation.Method.action
				}));
			}
			operation.SoapActionRequired = true;
		}

		// Token: 0x04000523 RID: 1315
		private Hashtable requestElements;

		// Token: 0x04000524 RID: 1316
		private Hashtable actions;

		// Token: 0x04000525 RID: 1317
		private XmlQualifiedName soap11PortType;
	}
}
