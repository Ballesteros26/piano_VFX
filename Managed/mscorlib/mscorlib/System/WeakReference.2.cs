using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a typed weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <typeparam name="T">The type of the object referenced.</typeparam>
	// Token: 0x02000254 RID: 596
	[Serializable]
	public sealed class WeakReference<T> : ISerializable where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object.</summary>
		/// <param name="target">The object to reference, or null.</param>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x00068858 File Offset: 0x00066A58
		public WeakReference(T target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object and uses the specified resurrection tracking.</summary>
		/// <param name="target">The object to reference, or null.</param>
		/// <param name="trackResurrection">true to track the object after finalization; false to track the object only until finalization.</param>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x00068864 File Offset: 0x00066A64
		public WeakReference(T target, bool trackResurrection)
		{
			this.trackResurrection = trackResurrection;
			GCHandleType gchandleType = (trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(target, gchandleType);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00068898 File Offset: 0x00066A98
		private WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.trackResurrection = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(T));
			GCHandleType gchandleType = (this.trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(value, gchandleType);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data necessary to serialize the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="info">An object that holds all the data necessary to serialize or deserialize the current <see cref="T:System.WeakReference`1" /> object.</param>
		/// <param name="context">The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001BA8 RID: 7080 RVA: 0x000688FC File Offset: 0x00066AFC
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.trackResurrection);
			if (this.handle.IsAllocated)
			{
				info.AddValue("TrackedObject", this.handle.Target);
				return;
			}
			info.AddValue("TrackedObject", null);
		}

		/// <summary>Sets the target object that is referenced by this <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="target">The new target object.</param>
		// Token: 0x06001BA9 RID: 7081 RVA: 0x00068958 File Offset: 0x00066B58
		public void SetTarget(T target)
		{
			this.handle.Target = target;
		}

		/// <summary>Tries to retrieve the target object that is referenced by the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <returns>true if the target was retrieved; otherwise, false.</returns>
		/// <param name="target">When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.</param>
		// Token: 0x06001BAA RID: 7082 RVA: 0x0006896B File Offset: 0x00066B6B
		public bool TryGetTarget(out T target)
		{
			if (!this.handle.IsAllocated)
			{
				target = default(T);
				return false;
			}
			target = (T)((object)this.handle.Target);
			return target != null;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000689A8 File Offset: 0x00066BA8
		~WeakReference()
		{
			this.handle.Free();
		}

		// Token: 0x04000F83 RID: 3971
		private GCHandle handle;

		// Token: 0x04000F84 RID: 3972
		private bool trackResurrection;
	}
}
