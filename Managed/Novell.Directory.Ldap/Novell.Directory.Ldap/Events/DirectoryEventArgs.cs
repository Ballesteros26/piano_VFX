using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A4 RID: 164
	public class DirectoryEventArgs : BaseEventArgs
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00013F80 File Offset: 0x00012180
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00013F88 File Offset: 0x00012188
		public EventClassifiers EventClassification
		{
			get
			{
				return this.eClassification;
			}
			set
			{
				this.eClassification = value;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013F91 File Offset: 0x00012191
		public DirectoryEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage)
		{
			this.eClassification = aClassification;
		}

		// Token: 0x04000303 RID: 771
		protected EventClassifiers eClassification;
	}
}
