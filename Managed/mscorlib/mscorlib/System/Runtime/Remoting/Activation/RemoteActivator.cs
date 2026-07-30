using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007C3 RID: 1987
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x0600503C RID: 20540 RVA: 0x0011F634 File Offset: 0x0011D834
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] array = null;
			if (msg.ActivationType.IsContextful)
			{
				array = new object[]
				{
					new RemoteActivationAttribute(msg.ContextProperties)
				};
			}
			return new ConstructionResponse(RemotingServices.Marshal((MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, array)), null, msg);
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0011F6B0 File Offset: 0x0011D8B0
		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromMinutes(30.0);
				lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
				lease.RenewOnCallTime = TimeSpan.FromMinutes(10.0);
			}
			return lease;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x00014B5A File Offset: 0x00012D5A
		public ActivatorLevel Level
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x00014B5A File Offset: 0x00012D5A
		// (set) Token: 0x06005040 RID: 20544 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
