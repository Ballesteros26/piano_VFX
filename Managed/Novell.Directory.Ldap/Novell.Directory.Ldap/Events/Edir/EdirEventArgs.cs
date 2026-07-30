using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000AE RID: 174
	public class EdirEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00014790 File Offset: 0x00012990
		public EdirEventIntermediateResponse IntermediateResponse
		{
			get
			{
				if (this.ldap_message is EdirEventIntermediateResponse)
				{
					return (EdirEventIntermediateResponse)this.ldap_message;
				}
				return null;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000147AC File Offset: 0x000129AC
		public EdirEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage, aClassification)
		{
		}
	}
}
