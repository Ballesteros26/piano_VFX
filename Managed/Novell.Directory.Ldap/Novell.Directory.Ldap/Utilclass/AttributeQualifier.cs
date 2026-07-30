using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000041 RID: 65
	public class AttributeQualifier
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000C6E3 File Offset: 0x0000A8E3
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public virtual string[] Values
		{
			get
			{
				string[] array = null;
				if (this.values.Count > 0)
				{
					array = new string[this.values.Count];
					for (int i = 0; i < this.values.Count; i++)
					{
						array[i] = (string)this.values[i];
					}
				}
				return array;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C748 File Offset: 0x0000A948
		public AttributeQualifier(string name, string[] value_Renamed)
		{
			if (name == null || value_Renamed == null)
			{
				throw new ArgumentException("A null name or value was passed in for a schema definition qualifier");
			}
			this.name = name;
			this.values = new ArrayList(5);
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				this.values.Add(value_Renamed[i]);
			}
		}

		// Token: 0x04000190 RID: 400
		internal string name;

		// Token: 0x04000191 RID: 401
		internal ArrayList values;
	}
}
