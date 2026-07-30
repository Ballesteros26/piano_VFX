using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that enable applications to trace the execution of code and associate trace messages with their source. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CC RID: 460
	public class TraceSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source. </summary>
		/// <param name="name">The name of the source (typically, the name of the application).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06000E0B RID: 3595 RVA: 0x00041F7D File Offset: 0x0004017D
		public TraceSource(string name)
			: this(name, SourceLevels.Off)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source and the default source level at which tracing is to occur.  </summary>
		/// <param name="name">The name of the source, typically the name of the application.</param>
		/// <param name="defaultLevel">A bitwise combination of the enumeration values that specifies the default source level at which to trace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06000E0C RID: 3596 RVA: 0x00041F88 File Offset: 0x00040188
		public TraceSource(string name, SourceLevels defaultLevel)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.sourceName = name;
			this.switchLevel = defaultLevel;
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				TraceSource._pruneCachedTraceSources();
				TraceSource.tracesources.Add(new WeakReference(this));
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00042010 File Offset: 0x00040210
		private static void _pruneCachedTraceSources()
		{
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				if (TraceSource.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(TraceSource.tracesources.Count);
					for (int i = 0; i < TraceSource.tracesources.Count; i++)
					{
						if ((TraceSource)TraceSource.tracesources[i].Target != null)
						{
							list2.Add(TraceSource.tracesources[i]);
						}
					}
					if (list2.Count < TraceSource.tracesources.Count)
					{
						TraceSource.tracesources.Clear();
						TraceSource.tracesources.AddRange(list2);
						TraceSource.tracesources.TrimExcess();
					}
					TraceSource.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000420E4 File Offset: 0x000402E4
		private void Initialize()
		{
			if (!this._initCalled)
			{
				lock (this)
				{
					if (!this._initCalled)
					{
						SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
						if (sources != null)
						{
							SourceElement sourceElement = sources[this.sourceName];
							if (sourceElement != null)
							{
								if (!string.IsNullOrEmpty(sourceElement.SwitchName))
								{
									this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
								}
								else
								{
									this.CreateSwitch(sourceElement.SwitchType, this.sourceName);
									if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
									{
										this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
									}
								}
								this.listeners = sourceElement.Listeners.GetRuntimeObject();
								this.attributes = new StringDictionary();
								TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
								this.attributes.ReplaceHashtable(sourceElement.Attributes);
							}
							else
							{
								this.NoConfigInit();
							}
						}
						else
						{
							this.NoConfigInit();
						}
						this._initCalled = true;
					}
				}
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0004221C File Offset: 0x0004041C
		private void NoConfigInit()
		{
			this.internalSwitch = new SourceSwitch(this.sourceName, this.switchLevel.ToString());
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener());
			this.attributes = null;
		}

		/// <summary>Closes all the trace listeners in the trace listener collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000E10 RID: 3600 RVA: 0x00042278 File Offset: 0x00040478
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Close()
		{
			if (this.listeners != null)
			{
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					foreach (object obj in this.listeners)
					{
						((TraceListener)obj).Close();
					}
				}
			}
		}

		/// <summary>Flushes all the trace listeners in the trace listener collection.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E11 RID: 3601 RVA: 0x00042300 File Offset: 0x00040500
		public void Flush()
		{
			if (this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						using (IEnumerator enumerator = this.listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								((TraceListener)obj).Flush();
							}
							return;
						}
					}
				}
				foreach (object obj2 in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					if (!traceListener.IsThreadSafe)
					{
						TraceListener traceListener2 = traceListener;
						lock (traceListener2)
						{
							traceListener.Flush();
							continue;
						}
					}
					traceListener.Flush();
				}
			}
		}

		/// <summary>Gets the custom attributes supported by the trace source.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace source, or null if there are no custom attributes.</returns>
		// Token: 0x06000E12 RID: 3602 RVA: 0x00009E57 File Offset: 0x00008057
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00042418 File Offset: 0x00040618
		internal static void RefreshAll()
		{
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				TraceSource._pruneCachedTraceSources();
				for (int i = 0; i < TraceSource.tracesources.Count; i++)
				{
					TraceSource traceSource = (TraceSource)TraceSource.tracesources[i].Target;
					if (traceSource != null)
					{
						traceSource.Refresh();
					}
				}
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0004248C File Offset: 0x0004068C
		internal void Refresh()
		{
			if (!this._initCalled)
			{
				this.Initialize();
				return;
			}
			SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
			if (sources != null)
			{
				SourceElement sourceElement = sources[this.Name];
				if (sourceElement != null)
				{
					if ((string.IsNullOrEmpty(sourceElement.SwitchType) && this.internalSwitch.GetType() != typeof(SourceSwitch)) || sourceElement.SwitchType != this.internalSwitch.GetType().AssemblyQualifiedName)
					{
						if (!string.IsNullOrEmpty(sourceElement.SwitchName))
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.CreateSwitch(sourceElement.SwitchType, this.Name);
							if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
							{
								this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
							}
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchName))
					{
						if (sourceElement.SwitchName != this.internalSwitch.DisplayName)
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.internalSwitch.Refresh();
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
					{
						this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
					}
					else
					{
						this.internalSwitch.Level = SourceLevels.Off;
					}
					TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
					foreach (object obj in sourceElement.Listeners)
					{
						ListenerElement listenerElement = (ListenerElement)obj;
						TraceListener traceListener = this.listeners[listenerElement.Name];
						if (traceListener != null)
						{
							traceListenerCollection.Add(listenerElement.RefreshRuntimeObject(traceListener));
						}
						else
						{
							traceListenerCollection.Add(listenerElement.GetRuntimeObject());
						}
					}
					TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
					this.attributes = new StringDictionary();
					this.attributes.ReplaceHashtable(sourceElement.Attributes);
					this.listeners = traceListenerCollection;
					return;
				}
				this.internalSwitch.Level = this.switchLevel;
				this.listeners.Clear();
				this.attributes = null;
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type and event identifier.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E15 RID: 3605 RVA: 0x000426FC File Offset: 0x000408FC
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_010F;
						}
						goto IL_00F1;
					}
					goto IL_00F1;
					IL_010F:
					j++;
					continue;
					IL_00F1:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_010F;
					}
					goto IL_010F;
				}
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and message.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E16 RID: 3606 RVA: 0x00042850 File Offset: 0x00040A50
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_0112;
						}
						goto IL_00F3;
					}
					goto IL_00F3;
					IL_0112:
					j++;
					continue;
					IL_00F3:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, message);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_0112;
					}
					goto IL_0112;
				}
			}
		}

		/// <summary>Writes a trace event to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and argument array and format.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format. </exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E17 RID: 3607 RVA: 0x000429A8 File Offset: 0x00040BA8
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_0118;
						}
						goto IL_00F7;
					}
					goto IL_00F7;
					IL_0118:
					j++;
					continue;
					IL_00F7:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_0118;
					}
					goto IL_0118;
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E18 RID: 3608 RVA: 0x00042B04 File Offset: 0x00040D04
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, object data)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_0112;
						}
						goto IL_00F3;
					}
					goto IL_00F3;
					IL_0112:
					j++;
					continue;
					IL_00F3:
					traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_0112;
					}
					goto IL_0112;
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data array.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An object array containing the trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E19 RID: 3609 RVA: 0x00042C5C File Offset: 0x00040E5C
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, params object[] data)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_0112;
						}
						goto IL_00F3;
					}
					goto IL_00F3;
					IL_0112:
					j++;
					continue;
					IL_00F3:
					traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_0112;
					}
					goto IL_0112;
				}
			}
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E1A RID: 3610 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		public void TraceInformation(string message)
		{
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified object array and formatting information.</summary>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format. </exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E1B RID: 3611 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		public void TraceInformation(string format, params object[] args)
		{
		}

		/// <summary>Writes a trace transfer message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified numeric identifier, message, and related activity identifier.</summary>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <param name="relatedActivityId">A structure that identifies the related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E1C RID: 3612 RVA: 0x00042DB4 File Offset: 0x00040FB4
		[Conditional("TRACE")]
		public void TraceTransfer(int id, string message, Guid relatedActivityId)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(TraceEventType.Transfer) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_0116;
						}
						goto IL_00F7;
					}
					goto IL_00F7;
					IL_0116:
					j++;
					continue;
					IL_00F7:
					traceListener2.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_0116;
					}
					goto IL_0116;
				}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00042F10 File Offset: 0x00041110
		private void CreateSwitch(string typename, string name)
		{
			if (!string.IsNullOrEmpty(typename))
			{
				this.internalSwitch = (SourceSwitch)TraceUtils.GetRuntimeObject(typename, typeof(SourceSwitch), name);
				return;
			}
			this.internalSwitch = new SourceSwitch(name, this.switchLevel.ToString());
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace switch.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00042F63 File Offset: 0x00041163
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00042F84 File Offset: 0x00041184
		public string Name
		{
			get
			{
				return this.sourceName;
			}
		}

		/// <summary>Gets the collection of trace listeners for the trace source.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that contains the active trace listeners associated with the source. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00042F8E File Offset: 0x0004118E
		public TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.Initialize();
				return this.listeners;
			}
		}

		/// <summary>Gets or sets the source switch value.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.SourceSwitch" /> object representing the source switch value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.TraceSource.Switch" /> is set to null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00042F9E File Offset: 0x0004119E
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00042FAE File Offset: 0x000411AE
		public SourceSwitch Switch
		{
			get
			{
				this.Initialize();
				return this.internalSwitch;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Switch");
				}
				this.Initialize();
				this.internalSwitch = value;
			}
		}

		// Token: 0x04001086 RID: 4230
		private static List<WeakReference> tracesources = new List<WeakReference>();

		// Token: 0x04001087 RID: 4231
		private static int s_LastCollectionCount;

		// Token: 0x04001088 RID: 4232
		private volatile SourceSwitch internalSwitch;

		// Token: 0x04001089 RID: 4233
		private volatile TraceListenerCollection listeners;

		// Token: 0x0400108A RID: 4234
		private StringDictionary attributes;

		// Token: 0x0400108B RID: 4235
		private SourceLevels switchLevel;

		// Token: 0x0400108C RID: 4236
		private volatile string sourceName;

		// Token: 0x0400108D RID: 4237
		internal volatile bool _initCalled;
	}
}
