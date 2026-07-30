using System;
using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Profile
{
	/// <summary>Manages the creation of the user profile and profile events. This class cannot be inherited.</summary>
	// Token: 0x0200050D RID: 1293
	public sealed class ProfileModule : IHttpModule
	{
		/// <summary>Occurs when the anonymous user for a profile logs in.</summary>
		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06003980 RID: 14720 RVA: 0x0009AB32 File Offset: 0x00098D32
		// (remove) Token: 0x06003981 RID: 14721 RVA: 0x0009AB45 File Offset: 0x00098D45
		public event ProfileMigrateEventHandler MigrateAnonymous
		{
			add
			{
				this.events.AddHandler(ProfileModule.migrateAnonymousEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.migrateAnonymousEvent, value);
			}
		}

		/// <summary>Occurs before the user profile is created.</summary>
		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06003982 RID: 14722 RVA: 0x0009AB58 File Offset: 0x00098D58
		// (remove) Token: 0x06003983 RID: 14723 RVA: 0x0009AB6B File Offset: 0x00098D6B
		[global::System.MonoTODO("implement event rising")]
		public event ProfileEventHandler Personalize
		{
			add
			{
				this.events.AddHandler(ProfileModule.personalizeEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.personalizeEvent, value);
			}
		}

		/// <summary>Occurs at the end of page execution if automatic profile saving is enabled.</summary>
		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06003984 RID: 14724 RVA: 0x0009AB7E File Offset: 0x00098D7E
		// (remove) Token: 0x06003985 RID: 14725 RVA: 0x0009AB91 File Offset: 0x00098D91
		public event ProfileAutoSaveEventHandler ProfileAutoSaving
		{
			add
			{
				this.events.AddHandler(ProfileModule.profileAutoSavingEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.profileAutoSavingEvent, value);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
		// Token: 0x06003987 RID: 14727 RVA: 0x0009ABB7 File Offset: 0x00098DB7
		public void Dispose()
		{
			this.app.EndRequest -= this.OnLeave;
			this.app.PostMapRequestHandler -= this.OnEnter;
		}

		/// <summary>Calls initialization code when a <see cref="T:System.Web.Profile.ProfileModule" /> object is created.</summary>
		/// <param name="app">The current application. </param>
		// Token: 0x06003988 RID: 14728 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		public void Init(HttpApplication app)
		{
			this.app = app;
			app.PostMapRequestHandler += this.OnEnter;
			app.EndRequest += this.OnLeave;
			AnonymousIdentificationSection anonymousIdentificationSection = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");
			if (anonymousIdentificationSection == null)
			{
				return;
			}
			this.anonymousCookieName = anonymousIdentificationSection.CookieName;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x0009AC40 File Offset: 0x00098E40
		private void OnEnter(object o, EventArgs eventArgs)
		{
			if (!ProfileManager.Enabled)
			{
				return;
			}
			if (HttpContext.Current.Request.IsAuthenticated)
			{
				HttpCookie httpCookie = this.app.Request.Cookies[this.anonymousCookieName];
				if (httpCookie != null && httpCookie.Expires != DateTime.MinValue && httpCookie.Expires > DateTime.Now)
				{
					ProfileMigrateEventHandler profileMigrateEventHandler = this.events[ProfileModule.migrateAnonymousEvent] as ProfileMigrateEventHandler;
					if (profileMigrateEventHandler != null)
					{
						ProfileMigrateEventArgs profileMigrateEventArgs = new ProfileMigrateEventArgs(HttpContext.Current, Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value)));
						profileMigrateEventHandler(this, profileMigrateEventArgs);
					}
					HttpCookie httpCookie2 = new HttpCookie(this.anonymousCookieName);
					httpCookie2.Path = this.app.Request.ApplicationPath;
					httpCookie2.Expires = new DateTime(1970, 1, 1);
					httpCookie2.Value = "";
					this.app.Response.AppendCookie(httpCookie2);
				}
			}
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0009AD48 File Offset: 0x00098F48
		private void OnLeave(object o, EventArgs eventArgs)
		{
			if (!ProfileManager.Enabled)
			{
				return;
			}
			if (!this.app.Context.ProfileInitialized)
			{
				return;
			}
			if (ProfileManager.AutomaticSaveEnabled)
			{
				this.profile = this.app.Context.Profile;
				if (this.profile == null)
				{
					return;
				}
				ProfileAutoSaveEventHandler profileAutoSaveEventHandler = this.events[ProfileModule.profileAutoSavingEvent] as ProfileAutoSaveEventHandler;
				if (profileAutoSaveEventHandler != null)
				{
					ProfileAutoSaveEventArgs profileAutoSaveEventArgs = new ProfileAutoSaveEventArgs(this.app.Context);
					profileAutoSaveEventHandler(this, profileAutoSaveEventArgs);
					if (!profileAutoSaveEventArgs.ContinueWithProfileAutoSave)
					{
						return;
					}
				}
				this.profile.Save();
			}
		}

		// Token: 0x04001F2F RID: 7983
		private static readonly object migrateAnonymousEvent = new object();

		// Token: 0x04001F30 RID: 7984
		private static readonly object personalizeEvent = new object();

		// Token: 0x04001F31 RID: 7985
		private static readonly object profileAutoSavingEvent = new object();

		// Token: 0x04001F32 RID: 7986
		private HttpApplication app;

		// Token: 0x04001F33 RID: 7987
		private ProfileBase profile;

		// Token: 0x04001F34 RID: 7988
		private string anonymousCookieName;

		// Token: 0x04001F35 RID: 7989
		private EventHandlerList events = new EventHandlerList();
	}
}
