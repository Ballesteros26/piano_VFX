using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005E RID: 94
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[StaticAccessor("AnimatorJobExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorJobExtensions.bindings.h")]
	public static class AnimatorJobExtensions
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00007958 File Offset: 0x00005B58
		public static void AddJobDependency(this Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency(animator, jobHandle);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00007964 File Offset: 0x00005B64
		public static TransformStreamHandle BindStreamTransform(this Animator animator, Transform transform)
		{
			TransformStreamHandle transformStreamHandle = default(TransformStreamHandle);
			AnimatorJobExtensions.InternalBindStreamTransform(animator, transform, out transformStreamHandle);
			return transformStreamHandle;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000798C File Offset: 0x00005B8C
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindStreamProperty(transform, type, property, false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000079A8 File Offset: 0x00005BA8
		public static PropertyStreamHandle BindCustomStreamProperty(this Animator animator, string property, CustomStreamPropertyType type)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindCustomStreamProperty(animator, property, type, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000079D0 File Offset: 0x00005BD0
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindStreamProperty(animator, transform, type, property, isObjectReference, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000079FC File Offset: 0x00005BFC
		public static TransformSceneHandle BindSceneTransform(this Animator animator, Transform transform)
		{
			TransformSceneHandle transformSceneHandle = default(TransformSceneHandle);
			AnimatorJobExtensions.InternalBindSceneTransform(animator, transform, out transformSceneHandle);
			return transformSceneHandle;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00007A24 File Offset: 0x00005C24
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindSceneProperty(transform, type, property, false);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00007A40 File Offset: 0x00005C40
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertySceneHandle propertySceneHandle = default(PropertySceneHandle);
			AnimatorJobExtensions.InternalBindSceneProperty(animator, transform, type, property, isObjectReference, out propertySceneHandle);
			return propertySceneHandle;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00007A6C File Offset: 0x00005C6C
		public static bool OpenAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			return AnimatorJobExtensions.InternalOpenAnimationStream(animator, ref stream);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00007A85 File Offset: 0x00005C85
		public static void CloseAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			AnimatorJobExtensions.InternalCloseAnimationStream(animator, ref stream);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00007A90 File Offset: 0x00005C90
		public static void ResolveAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllStreamHandles(animator);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00007A9A File Offset: 0x00005C9A
		public static void ResolveAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllSceneHandles(animator);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00007AA4 File Offset: 0x00005CA4
		internal static void UnbindAllHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllHandles(animator);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00007AAE File Offset: 0x00005CAE
		private static void InternalAddJobDependency([NotNull] Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency_Injected(animator, ref jobHandle);
		}

		// Token: 0x06000559 RID: 1369
		[MethodImpl(4096)]
		private static extern void InternalBindStreamTransform([NotNull] Animator animator, [NotNull] Transform transform, out TransformStreamHandle transformStreamHandle);

		// Token: 0x0600055A RID: 1370
		[MethodImpl(4096)]
		private static extern void InternalBindStreamProperty([NotNull] Animator animator, [NotNull] Transform transform, [NotNull] Type type, [NotNull] string property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x0600055B RID: 1371
		[MethodImpl(4096)]
		private static extern void InternalBindCustomStreamProperty([NotNull] Animator animator, [NotNull] string property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x0600055C RID: 1372
		[MethodImpl(4096)]
		private static extern void InternalBindSceneTransform([NotNull] Animator animator, [NotNull] Transform transform, out TransformSceneHandle transformSceneHandle);

		// Token: 0x0600055D RID: 1373
		[MethodImpl(4096)]
		private static extern void InternalBindSceneProperty([NotNull] Animator animator, [NotNull] Transform transform, [NotNull] Type type, [NotNull] string property, bool isObjectReference, out PropertySceneHandle propertySceneHandle);

		// Token: 0x0600055E RID: 1374
		[MethodImpl(4096)]
		private static extern bool InternalOpenAnimationStream([NotNull] Animator animator, ref AnimationStream stream);

		// Token: 0x0600055F RID: 1375
		[MethodImpl(4096)]
		private static extern void InternalCloseAnimationStream([NotNull] Animator animator, ref AnimationStream stream);

		// Token: 0x06000560 RID: 1376
		[MethodImpl(4096)]
		private static extern void InternalResolveAllStreamHandles([NotNull] Animator animator);

		// Token: 0x06000561 RID: 1377
		[MethodImpl(4096)]
		private static extern void InternalResolveAllSceneHandles([NotNull] Animator animator);

		// Token: 0x06000562 RID: 1378
		[MethodImpl(4096)]
		private static extern void InternalUnbindAllHandles([NotNull] Animator animator);

		// Token: 0x06000563 RID: 1379
		[MethodImpl(4096)]
		private static extern void InternalAddJobDependency_Injected(Animator animator, ref JobHandle jobHandle);
	}
}
