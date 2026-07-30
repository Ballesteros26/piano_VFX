using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public abstract class TMP_InputValidator : ScriptableObject
	{
		// Token: 0x0600022F RID: 559
		public abstract char Validate(ref string text, ref int pos, char ch);
	}
}
