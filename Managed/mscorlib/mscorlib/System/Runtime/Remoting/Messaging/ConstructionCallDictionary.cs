using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000803 RID: 2051
	internal class ConstructionCallDictionary : MessageDictionary
	{
		// Token: 0x06005227 RID: 21031 RVA: 0x001228E1 File Offset: 0x00120AE1
		public ConstructionCallDictionary(IConstructionCallMessage message)
			: base(message)
		{
			base.MethodKeys = ConstructionCallDictionary.InternalKeys;
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x001228F8 File Offset: 0x00120AF8
		protected override object GetMethodProperty(string key)
		{
			if (key == "__Activator")
			{
				return ((IConstructionCallMessage)this._message).Activator;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				return ((IConstructionCallMessage)this._message).CallSiteActivationAttributes;
			}
			if (key == "__ActivationType")
			{
				return ((IConstructionCallMessage)this._message).ActivationType;
			}
			if (key == "__ContextProperties")
			{
				return ((IConstructionCallMessage)this._message).ContextProperties;
			}
			if (!(key == "__ActivationTypeName"))
			{
				return base.GetMethodProperty(key);
			}
			return ((IConstructionCallMessage)this._message).ActivationTypeName;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x001229A4 File Offset: 0x00120BA4
		protected override void SetMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				((IConstructionCallMessage)this._message).Activator = (IActivator)value;
				return;
			}
			if (!(key == "__CallSiteActivationAttributes") && !(key == "__ActivationType") && !(key == "__ContextProperties") && !(key == "__ActivationTypeName"))
			{
				base.SetMethodProperty(key, value);
				return;
			}
			throw new ArgumentException("key was invalid");
		}

		// Token: 0x04002B02 RID: 11010
		public static string[] InternalKeys = new string[]
		{
			"__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext", "__CallSiteActivationAttributes", "__ActivationType", "__ContextProperties", "__Activator",
			"__ActivationTypeName"
		};
	}
}
