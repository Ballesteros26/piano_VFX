using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004A RID: 74
	public interface IPropertyCollector
	{
		// Token: 0x060002BF RID: 703
		void PushActiveGameObject(GameObject gameObject);

		// Token: 0x060002C0 RID: 704
		void PopActiveGameObject();

		// Token: 0x060002C1 RID: 705
		void AddFromClip(AnimationClip clip);

		// Token: 0x060002C2 RID: 706
		void AddFromClips(IEnumerable<AnimationClip> clips);

		// Token: 0x060002C3 RID: 707
		void AddFromName<T>(string name) where T : Component;

		// Token: 0x060002C4 RID: 708
		void AddFromName(string name);

		// Token: 0x060002C5 RID: 709
		void AddFromClip(GameObject obj, AnimationClip clip);

		// Token: 0x060002C6 RID: 710
		void AddFromClips(GameObject obj, IEnumerable<AnimationClip> clips);

		// Token: 0x060002C7 RID: 711
		void AddFromName<T>(GameObject obj, string name) where T : Component;

		// Token: 0x060002C8 RID: 712
		void AddFromName(GameObject obj, string name);

		// Token: 0x060002C9 RID: 713
		void AddFromName(Component component, string name);

		// Token: 0x060002CA RID: 714
		void AddFromComponent(GameObject obj, Component component);

		// Token: 0x060002CB RID: 715
		void AddObjectProperties(Object obj, AnimationClip clip);
	}
}
