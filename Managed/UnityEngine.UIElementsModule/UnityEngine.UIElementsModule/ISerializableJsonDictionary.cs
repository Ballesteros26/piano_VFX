using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002C RID: 44
	internal interface ISerializableJsonDictionary
	{
		// Token: 0x060000F2 RID: 242
		void Set<T>(string key, T value) where T : class;

		// Token: 0x060000F3 RID: 243
		T Get<T>(string key) where T : class;

		// Token: 0x060000F4 RID: 244
		T GetScriptable<T>(string key) where T : ScriptableObject;

		// Token: 0x060000F5 RID: 245
		void Overwrite(object obj, string key);

		// Token: 0x060000F6 RID: 246
		bool ContainsKey(string key);

		// Token: 0x060000F7 RID: 247
		void OnBeforeSerialize();

		// Token: 0x060000F8 RID: 248
		void OnAfterDeserialize();
	}
}
