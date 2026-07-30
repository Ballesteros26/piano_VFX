using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000253 RID: 595
	[ComVisible(true)]
	[Serializable]
	public class WeakReference : ISerializable
	{
		// Token: 0x06001B9B RID: 7067 RVA: 0x000686F0 File Offset: 0x000668F0
		private void AllocateHandle(object target)
		{
			if (this.isLongReference)
			{
				this.gcHandle = GCHandle.Alloc(target, GCHandleType.WeakTrackResurrection);
				return;
			}
			this.gcHandle = GCHandle.Alloc(target, GCHandleType.Weak);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object.</summary>
		/// <param name="target">The object to track or null. </param>
		// Token: 0x06001B9C RID: 7068 RVA: 0x00068715 File Offset: 0x00066915
		public WeakReference(object target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object and using the specified resurrection tracking.</summary>
		/// <param name="target">An object to track. </param>
		/// <param name="trackResurrection">Indicates when to stop tracking the object. If true, the object is tracked after finalization; if false, the object is only tracked until finalization. </param>
		// Token: 0x06001B9D RID: 7069 RVA: 0x0006871F File Offset: 0x0006691F
		public WeakReference(object target, bool trackResurrection)
		{
			this.isLongReference = trackResurrection;
			this.AllocateHandle(target);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, using deserialized data from the specified serialization and stream objects.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object. </param>
		/// <param name="context">(Reserved) Describes the source and destination of the serialized stream specified by <paramref name="info" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001B9E RID: 7070 RVA: 0x00068738 File Offset: 0x00066938
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.isLongReference = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(object));
			this.AllocateHandle(value);
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected.</summary>
		/// <returns>true if the object referenced by the current <see cref="T:System.WeakReference" /> object has not been garbage collected and is still accessible; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00068787 File Offset: 0x00066987
		public virtual bool IsAlive
		{
			get
			{
				return this.Target != null;
			}
		}

		/// <summary>Gets or sets the object (the target) referenced by the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <returns>null if the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected; otherwise, a reference to the object referenced by the current <see cref="T:System.WeakReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reference to the target object is invalid. This exception can be thrown while setting this property if the value is a null reference or if the object has been finalized during the set operation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00068792 File Offset: 0x00066992
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x000687AE File Offset: 0x000669AE
		public virtual object Target
		{
			get
			{
				if (!this.gcHandle.IsAllocated)
				{
					return null;
				}
				return this.gcHandle.Target;
			}
			set
			{
				this.gcHandle.Target = value;
			}
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object is tracked after it is finalized.</summary>
		/// <returns>true if the object the current <see cref="T:System.WeakReference" /> object refers to is tracked after finalization; or false if the object is only tracked until finalization.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000687BC File Offset: 0x000669BC
		public virtual bool TrackResurrection
		{
			get
			{
				return this.isLongReference;
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000687C4 File Offset: 0x000669C4
		~WeakReference()
		{
			this.gcHandle.Free();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data needed to serialize the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object. </param>
		/// <param name="context">(Reserved) The location where serialized data is stored and retrieved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x000687F8 File Offset: 0x000669F8
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.TrackResurrection);
			try
			{
				info.AddValue("TrackedObject", this.Target);
			}
			catch (Exception)
			{
				info.AddValue("TrackedObject", null);
			}
		}

		// Token: 0x04000F81 RID: 3969
		private bool isLongReference;

		// Token: 0x04000F82 RID: 3970
		private GCHandle gcHandle;
	}
}
