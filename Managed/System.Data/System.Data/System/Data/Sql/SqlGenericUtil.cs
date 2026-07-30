using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x0200013D RID: 317
	internal sealed class SqlGenericUtil
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x00005C14 File Offset: 0x00003E14
		private SqlGenericUtil()
		{
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000512EE File Offset: 0x0004F4EE
		internal static Exception NullCommandText()
		{
			return ADP.Argument(Res.GetString("Command parameter must have a non null and non empty command text."));
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000512FF File Offset: 0x0004F4FF
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(Res.GetString("MetaData parameter array must have length equivalent to ParameterDirection array argument."));
		}
	}
}
