/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
////UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using SwingWorker = org.jdesktop.swingworker.SwingWorker;
////UPGRADE_TODO: The type 'org.netbeans.api.wizard.WizardDisplayer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardDisplayer = org.netbeans.api.wizard.WizardDisplayer;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.Wizard' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Wizard = org.netbeans.spi.wizard.Wizard;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardBranchController' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardBranchController = org.netbeans.spi.wizard.WizardBranchController;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardController' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardController = org.netbeans.spi.wizard.WizardController;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardException = org.netbeans.spi.wizard.WizardException;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardPage' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardPage = org.netbeans.spi.wizard.WizardPage;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardPage.WizardResultProducer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardResultProducer = org.netbeans.spi.wizard.WizardPage.WizardResultProducer;
////UPGRADE_TODO: The type 'org.netbeans.spi.wizard.WizardPanelProvider' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using WizardPanelProvider = org.netbeans.spi.wizard.WizardPanelProvider;
////UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Logger = org.slf4j.Logger;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;

using GameModule = VassalSharp.build.GameModule;
//using Tutorial = VassalSharp.build.module.documentation.Tutorial;
//using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using Command = VassalSharp.command.Command;
//using CommandFilter = VassalSharp.command.CommandFilter;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using FileConfigurer = VassalSharp.configure.FileConfigurer;
//using PasswordConfigurer = VassalSharp.configure.PasswordConfigurer;
//using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
//using BasicModule = VassalSharp.launch.BasicModule;
using Prefs = VassalSharp.preferences.Prefs;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
using SplashScreen = VassalSharp.tools.SplashScreen;
//using UsernameAndPasswordDialog = VassalSharp.tools.UsernameAndPasswordDialog;
//using ImageUtils = VassalSharp.tools.image.ImageUtils;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace VassalSharp.build.module
{
	
	/// <summary> Provides support for two different wizards. The WelcomeWizard is the initial screen shown to the user when loading a
	/// module in play mode. The GameSetupWizard is shown whenever the user starts a new game on- or off-line.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public class WizardSupport
	{
#if NEVER_DEFINED
		/// <summary> Specify a {@link Tutorial} that the user may load from the {@link InitialWelcomeSteps}
		/// 
		/// </summary>
		/// <param name="tutorial">
		/// </param>
		virtual public Tutorial Tutorial
		{
			set
			{
				this.tutorial = value;
			}
			
		}
#endif
		
		private static readonly ILogger logger;
		
		public const System.String POST_INITIAL_STEPS_WIZARD = "postInitialStepsWizard"; //$NON-NLS-1$
		public const System.String POST_LOAD_GAME_WIZARD = "postLoadGameWizard"; //$NON-NLS-1$
		public const System.String POST_PLAY_OFFLINE_WIZARD = "postPlayOfflineWizard"; //$NON-NLS-1$
		public const System.String WELCOME_WIZARD_KEY = "welcomeWizard"; //$NON-NLS-1$
		public const System.String SETUP_KEY = "setup"; //$NON-NLS-1$
		public const System.String ACTION_KEY = "action"; //$NON-NLS-1$
		public const System.String LOAD_TUTORIAL_ACTION = "tutorial"; //$NON-NLS-1$
		public const System.String PLAY_ONLINE_ACTION = "online"; //$NON-NLS-1$
		public const System.String PLAY_OFFLINE_ACTION = "offline"; //$NON-NLS-1$
		public const System.String LOAD_GAME_ACTION = "loadGame"; //$NON-NLS-1$
		public const System.String WELCOME_WIZARD_ENABLED = "showWelcomeWizard"; //$NON-NLS-1$
		protected internal System.Drawing.Size logoSize = new System.Drawing.Size(200, 200);
#if NEVER_DEFINED
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < PredefinedSetup > setups = new ArrayList < PredefinedSetup >();
		protected internal Tutorial tutorial;
#endif
		
		public WizardSupport()
		{
		}

		///// <summary> Add a {@link PredefinedSetup} to the wizard page for starting a new game offline
		///// 
		///// </summary>
		///// <param name="setup">
		///// </param>
		//public virtual void  addPredefinedSetup(PredefinedSetup setup)
		//{
		//	setups.add(setup);
		//}
		
		//public virtual void  removePredefinedSetup(PredefinedSetup setup)
		//{
		//	setups.remove(setup);
		//}
		
		/// <summary> Show the "Welcome" wizard, shown when loading a module in play mode
		/// 
		/// </summary>
		public virtual void  showWelcomeWizard()
		{
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//GameModule g = GameModule.getGameModule();
			////UPGRADE_NOTE: Final was removed from the declaration of 'showWizard '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.Boolean showWizard = (System.Boolean) Prefs.GlobalPrefs.getValue(WELCOME_WIZARD_KEY);
			
			//if (!true.Equals(showWizard))
			//{
			//	//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//	//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//	g.Frame.Visible = true;
				
			//	// prompt for username and password if wizard is off
			//	// but no username is set
			//	// FIXME: this belongs outside of the wizard, not here
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	System.String name = (System.String) g.getPrefs().getValue(GameModule.REAL_NAME);
			//	if (name == null || name.Equals("newbie"))
			//	{
			//		//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//		//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//		new UsernameAndPasswordDialog(g.Frame).Visible = true;
			//	}
				
			//	return ;
			//}
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//WizardBranchController c = createWelcomeWizard();
			////UPGRADE_NOTE: Final was removed from the declaration of 'welcomeWizard '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//Wizard welcomeWizard = c.createWizard();
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//final HashMap < String, Wizard > props = new HashMap < String, Wizard >();
			//props.put(WELCOME_WIZARD_KEY, welcomeWizard);
			
			//SupportClass.ActionSupport help = null;
			//try
			//{
			//	//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			//	help = new ShowHelpAction(new System.Uri("http://www.vassalengine.org/wiki/doku.php?id=getting_started:getting_started"), null);
			//}
			//catch (System.UriFormatException e)
			//{
			//	ErrorDialog.bug(e);
			//}
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.Object result = WizardDisplayer.showWizard(welcomeWizard, null, help, props);
			
			//if (result is System.Collections.IDictionary)
			//{
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	System.Collections.IDictionary m = (System.Collections.IDictionary) result;
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'action '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	System.Object action = m[ACTION_KEY];
			//	if (PLAY_ONLINE_ACTION.Equals(action))
			//	{
			//		//UPGRADE_NOTE: Final was removed from the declaration of 'controls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//		ChatServerControls controls = ((BasicModule) g).ServerControls;
			//		//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//		//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//		g.Frame.Visible = true;
			//		controls.toggleVisible();
					
			//		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//		new SwingWorker < Void, Void >()
			//		{
			//			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//			Override
			//		}
			//	}
			//}
		}
#if NEVER_DEFINED
		protected internal virtual System.Void doInBackground()
		{
			controls.getClient().setConnected(true);
			//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			return null;
		}
		static WizardSupport()
		{
			logger = LoggerFactory.getLogger(typeof(WizardSupport));
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	.execute();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		g.getGameState().setup(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	g.getFrame().setVisible(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		g.getFrame().setVisible(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected BranchingWizard createWelcomeWizard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		InitialWelcomeSteps info = createInitialWelcomeSteps();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	info.setTutorial(tutorial);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return new BranchingWizard(info, POST_INITIAL_STEPS_WIZARD);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public WizardPanelProvider createPlayOfflinePanels()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ArrayList < PredefinedSetup > l = new ArrayList < PredefinedSetup >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(PredefinedSetup ps: setups)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(!ps.isMenu()) 
		l.add(ps);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(l.isEmpty())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return GameSetupPanels.newInstance();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new PlayOfflinePanels(
		Resources.getString(WizardSupport.WizardSupport.PlayOffline), Resources.getString(WizardSupport.WizardSupport.SelectSetup), l); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Show a wizard that prompts the user to specify information for unfinished {@link GameSetupStep}s</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void showGameSetupWizard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameSetupPanels panels = GameSetupPanels.newInstance();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(panels != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		WizardDisplayer.showWizard(panels.newWizard(logoSize), new Rectangle(0, 0, logoSize.width + 400, logoSize.height));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public InitialWelcomeSteps createInitialWelcomeSteps()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Object realName = GameModule.getGameModule().getPrefs().getValue(GameModule.REAL_NAME);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(realName == null || realName.equals(Resources.getString(Prefs.newbie)))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new InitialWelcomeSteps(new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ InitialWelcomeSteps.NAME_STEP, ACTION_KEY
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}, 
	new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ Resources.getString(WizardSupport.WizardSupport.EnterName), Resources.getString(WizardSupport.WizardSupport.SelectPlayMode)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new InitialWelcomeSteps(new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ ACTION_KEY
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}, new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ Resources.getString(WizardSupport.SelectPlayMode)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	/// <summary> Wizard pages for the welcome wizard (initial module load). Prompts for username/password if not yet specified, and
	/// prompts to load a saved game or start a new one
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class InitialWelcomeSteps:WizardPanelProvider
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(System.Windows.Forms.RadioButton clickOnMe, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(clickOnMe, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.RadioButton clickOnMe, InitialWelcomeSteps enclosingInstance)
			{
				this.clickOnMe = clickOnMe;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable clickOnMe was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.RadioButton clickOnMe;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.doClick' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtondoClick'"
				clickOnMe.doClick();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(InitialWelcomeSteps enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				//UPGRADE_ISSUE: Method 'java.awt.event.ItemEvent.getStateChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventgetStateChange'"
				//UPGRADE_ISSUE: Field 'java.awt.event.ItemEvent.DESELECTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventDESELECTED_f'"
				if (e.getStateChange() == ItemEvent.DESELECTED)
				{
					Enclosing_Instance.tutorial.markAsViewed();
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(VassalSharp.configure.BooleanConfigurer wizardConf, System.Windows.Forms.CheckBox show, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(wizardConf, show, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.BooleanConfigurer wizardConf, System.Windows.Forms.CheckBox show, InitialWelcomeSteps enclosingInstance)
			{
				this.wizardConf = wizardConf;
				this.show = show;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable wizardConf was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.BooleanConfigurer wizardConf;
			//UPGRADE_NOTE: Final variable show was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.CheckBox show;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				wizardConf.setValue(Boolean.valueOf(show.Checked));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(WizardController controller, System.Collections.IDictionary settings, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(controller, settings, enclosingInstance);
			}
			private void  InitBlock(WizardController controller, System.Collections.IDictionary settings, InitialWelcomeSteps enclosingInstance)
			{
				this.controller = controller;
				this.settings = settings;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				controller.setProblem(Resources.getString("WizardSupport.LoadingTutorial")); //$NON-NLS-1$
				try
				{
					new TutorialLoader(controller, settings, new BufferedInputStream(Enclosing_Instance.tutorial.getTutorialContents()), POST_INITIAL_STEPS_WIZARD, Enclosing_Instance.tutorial).Start();
				}
				catch (System.IO.IOException e1)
				{
					logger.error("", e1);
					controller.setProblem(Resources.getString("WizardSupport.ErrorLoadingTutorial")); //$NON-NLS-1$
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(settings, controller, enclosingInstance);
			}
			private void  InitBlock(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				this.settings = settings;
				this.controller = controller;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unchecked)
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				settings[WizardSupport.ACTION_KEY] = LOAD_GAME_ACTION;
				Wizard wiz = new BranchingWizard(new LoadSavedGamePanels(), POST_LOAD_GAME_WIZARD).createWizard();
				settings[POST_INITIAL_STEPS_WIZARD] = wiz;
				controller.setForwardNavigationMode(WizardController.MODE_CAN_CONTINUE);
				controller.setProblem(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(settings, controller, enclosingInstance);
			}
			private void  InitBlock(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				this.settings = settings;
				this.controller = controller;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unchecked)
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				settings[WizardSupport.ACTION_KEY] = PLAY_ONLINE_ACTION;
				controller.setForwardNavigationMode(WizardController.MODE_CAN_FINISH);
				controller.setProblem(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(settings, controller, enclosingInstance);
			}
			private void  InitBlock(System.Collections.IDictionary settings, WizardController controller, InitialWelcomeSteps enclosingInstance)
			{
				this.settings = settings;
				this.controller = controller;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassActionListener4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassActionListener4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unchecked)
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().getGameState().setup(false);
				settings[WizardSupport.ACTION_KEY] = PLAY_OFFLINE_ACTION;
				//UPGRADE_NOTE: Final was removed from the declaration of 'panels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				WizardPanelProvider panels = createPlayOfflinePanels();
				if (panels == null)
				{
					controller.setForwardNavigationMode(WizardController.MODE_CAN_FINISH);
				}
				else
				{
					Wizard wiz = new BranchingWizard(panels, POST_PLAY_OFFLINE_WIZARD).createWizard();
					settings[POST_INITIAL_STEPS_WIZARD] = wiz;
					controller.setForwardNavigationMode(WizardController.MODE_CAN_CONTINUE);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(System.Collections.IDictionary settings, VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, WizardController controller, VassalSharp.configure.StringConfigurer pwd2, InitialWelcomeSteps enclosingInstance)
			{
				InitBlock(settings, nameConfig, pwd, controller, pwd2, enclosingInstance);
			}
			private void  InitBlock(System.Collections.IDictionary settings, VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, WizardController controller, VassalSharp.configure.StringConfigurer pwd2, InitialWelcomeSteps enclosingInstance)
			{
				this.settings = settings;
				this.nameConfig = nameConfig;
				this.pwd = pwd;
				this.controller = controller;
				this.pwd2 = pwd2;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			//UPGRADE_NOTE: Final variable nameConfig was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer nameConfig;
			//UPGRADE_NOTE: Final variable pwd was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer pwd;
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			//UPGRADE_NOTE: Final variable pwd2 was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer pwd2;
			private InitialWelcomeSteps enclosingInstance;
			public InitialWelcomeSteps Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unchecked)
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				settings[GameModule.REAL_NAME] = nameConfig.getValue();
				settings[GameModule.SECRET_NAME] = pwd.getValue();
				if (nameConfig.getValue() == null || "".Equals(nameConfig.getValue()))
				{
					//$NON-NLS-1$
					controller.setProblem(Resources.getString("WizardSupport.EnterYourName")); //$NON-NLS-1$
				}
				else if (pwd.getValue() == null || "".Equals(pwd.getValue()))
				{
					//$NON-NLS-1$
					controller.setProblem(Resources.getString("WizardSupport.EnterYourPassword")); //$NON-NLS-1$
				}
				else if (!pwd.getValue().Equals(pwd2.getValue()))
				{
					controller.setProblem(Resources.getString("WizardSupport.PasswordsDontMatch")); //$NON-NLS-1$
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Prefs p = GameModule.getGameModule().getPrefs();
					
					p.getOption(GameModule.REAL_NAME).setValue(nameConfig.ValueString);
					
					p.getOption(GameModule.SECRET_NAME).setValue(pwd.ValueString);
					
					try
					{
						p.save();
						controller.setProblem(null);
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						System.String msg = e.Message;
						if (msg == null)
						{
							msg = Resources.getString("Prefs.unable_to_save");
						}
						controller.setProblem(msg);
					}
				}
			}
		}
		virtual public Tutorial Tutorial
		{
			set
			{
				this.tutorial = value;
			}
			
		}
		public const System.String NAME_STEP = "name"; //$NON-NLS-1$
		protected internal System.Windows.Forms.Control nameControls;
		protected internal System.Windows.Forms.Control actionControls;
		protected internal Tutorial tutorial;
		
		public InitialWelcomeSteps(System.String[] steps, System.String[] stepDescriptions):base(Resources.getString("WizardSupport.Welcome"), steps, stepDescriptions)
		{ //$NON-NLS-1$
		}
		
		protected internal virtual System.Windows.Forms.Control createPanel(WizardController controller, System.String id, System.Collections.IDictionary settings)
		{
			System.Windows.Forms.Control c = null;
			if (NAME_STEP.Equals(id))
			{
				c = getNameControls(controller, settings);
			}
			else if (ACTION_KEY.equals(id))
			{
				c = getActionControls(controller, settings);
			}
			else
			{
				throw new System.ArgumentException("Illegal step: " + id); //$NON-NLS-1$
			}
			SplashScreen.disposeAll();
			return c;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		private System.Windows.Forms.Control getActionControls(WizardController controller, System.Collections.IDictionary settings)
		{
			if (actionControls == null)
			{
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createVerticalBox();
				//UPGRADE_TODO: Class 'javax.swing.ButtonGroup' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Collections.ArrayList group = new System.Collections.ArrayList();
				System.Windows.Forms.RadioButton tutorialButton = null;
				if (tutorial != null && tutorial.isFirstRun())
				{
					tutorialButton = createTutorialButton(controller, settings);
					addButton(tutorialButton, group, box);
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = Box.createVerticalStrut(20);
					box.Controls.Add(temp_Control);
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.RadioButton b = createPlayOfflineButton(controller, settings);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.doClick' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtondoClick'"
				b.doClick();
				addButton(b, group, box);
				settings[ACTION_KEY] = PLAY_OFFLINE_ACTION;
				addButton(createPlayOnlineButton(controller, settings), group, box);
				addButton(createLoadSavedGameButton(controller, settings), group, box);
				if (tutorialButton != null)
				{
					// Select tutorial button by default, but not until wizard is built.  Bug #2286742
					//UPGRADE_NOTE: Final was removed from the declaration of 'clickOnMe '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.RadioButton clickOnMe = tutorialButton;
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(new AnonymousClassRunnable(clickOnMe, this));
					tutorialButton.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
				}
				else if (tutorial != null)
				{
					addButton(createTutorialButton(controller, settings), group, box);
				}
				actionControls = box;
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = Box.createVerticalGlue();
				box.Controls.Add(temp_Control2);
				//UPGRADE_NOTE: Final was removed from the declaration of 'wizardConf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BooleanConfigurer wizardConf = (BooleanConfigurer) Prefs.GlobalPrefs.getOption(WELCOME_WIZARD_KEY);
				//UPGRADE_NOTE: Final was removed from the declaration of 'show '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.CheckBox show = SupportClass.CheckBoxSupport.CreateCheckBox(wizardConf.getName());
				show.setSelected(wizardConf.booleanValue());
				show.Click += new System.EventHandler(new AnonymousClassActionListener(wizardConf, show, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(show);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(show);
			}
			return actionControls;
		}
		
		private System.Windows.Forms.RadioButton createTutorialButton(WizardController controller, System.Collections.IDictionary settings)
		{
			System.Windows.Forms.RadioButton temp_radiobutton;
			temp_radiobutton = new System.Windows.Forms.RadioButton();
			temp_radiobutton.Text = Resources.getString("WizardSupport.LoadTutorial");
			System.Windows.Forms.RadioButton b = temp_radiobutton; //$NON-NLS-1$
			b.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener1(controller, settings, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			return b;
		}
		
		private System.Windows.Forms.RadioButton createLoadSavedGameButton(WizardController controller, System.Collections.IDictionary settings)
		{
			System.Windows.Forms.RadioButton temp_radiobutton;
			temp_radiobutton = new System.Windows.Forms.RadioButton();
			temp_radiobutton.Text = Resources.getString("WizardSupport.LoadSavedGame");
			System.Windows.Forms.RadioButton b = temp_radiobutton; //$NON-NLS-1$
			b.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener2(settings, controller, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			return b;
		}
		
		private System.Windows.Forms.RadioButton createPlayOnlineButton(WizardController controller, System.Collections.IDictionary settings)
		{
			System.Windows.Forms.RadioButton temp_radiobutton;
			temp_radiobutton = new System.Windows.Forms.RadioButton();
			temp_radiobutton.Text = Resources.getString("WizardSupport.PlayOnline");
			System.Windows.Forms.RadioButton b = temp_radiobutton; //$NON-NLS-1$
			b.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener3(settings, controller, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			return b;
		}
		
		private System.Windows.Forms.RadioButton createPlayOfflineButton(WizardController controller, System.Collections.IDictionary settings)
		{
			System.Windows.Forms.RadioButton temp_radiobutton;
			temp_radiobutton = new System.Windows.Forms.RadioButton();
			temp_radiobutton.Text = Resources.getString("WizardSupport.PlayOffline");
			System.Windows.Forms.RadioButton b = temp_radiobutton; //$NON-NLS-1$
			b.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener4(settings, controller, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			return b;
		}
		
		//UPGRADE_TODO: Class 'javax.swing.ButtonGroup' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		private void  addButton(System.Windows.Forms.RadioButton button, System.Collections.ArrayList group, Box box)
		{
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(button);
			group.Add((System.Object) button);
		}
		
		protected internal virtual System.Windows.Forms.Control getNameControls(WizardController controller, System.Collections.IDictionary settings)
		{
			if (nameControls == null)
			{
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createVerticalBox();
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = Box.createVerticalGlue();
				box.Controls.Add(temp_Control);
				controller.setProblem(Resources.getString("WizardSupport.EnterNameAndPassword")); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'nameConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringConfigurer nameConfig = new StringConfigurer(null, Resources.getString("WizardSupport.RealName")); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'pwd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringConfigurer pwd = new PasswordConfigurer(null, Resources.getString("WizardSupport.Password")); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'pwd2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringConfigurer pwd2 = new PasswordConfigurer(null, Resources.getString("WizardSupport.ConfirmPassword")); //$NON-NLS-1$
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener pl = new AnonymousClassPropertyChangeListener(settings, nameConfig, pwd, controller, pwd2, this);
				nameConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				pwd.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				pwd2.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(nameConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(pwd.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(pwd2.Controls);
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = Resources.getString("WizardSupport.NameAndPasswordDetails");
				System.Windows.Forms.Label l = temp_label;
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
				//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
				l.setAlignmentX(Box.CENTER_ALIGNMENT);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(l);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = Box.createVerticalGlue();
				box.Controls.Add(temp_Control2);
				nameControls = box;
			}
			return nameControls;
		}
	}
	/// <summary> Wizard pages for starting a new game offline
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PlayOfflinePanels:WizardPanelProvider
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(System.Windows.Forms.ComboBox setupSelection, WizardController controller, System.Collections.IDictionary settings, PlayOfflinePanels enclosingInstance)
			{
				InitBlock(setupSelection, controller, settings, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.ComboBox setupSelection, WizardController controller, System.Collections.IDictionary settings, PlayOfflinePanels enclosingInstance)
			{
				this.setupSelection = setupSelection;
				this.controller = controller;
				this.settings = settings;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable setupSelection was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.ComboBox setupSelection;
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			private PlayOfflinePanels enclosingInstance;
			public PlayOfflinePanels Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unchecked)
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (setupSelection.SelectedItem is PredefinedSetup)
				{
					PredefinedSetup setup = (PredefinedSetup) setupSelection.SelectedItem;
					if (setup.isUseFile() && setup.getFileName() != null)
					{
						loadSetup(setup, controller, settings);
					}
					else
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'panels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GameSetupPanels panels = GameSetupPanels.newInstance();
						settings[POST_PLAY_OFFLINE_WIZARD] = panels;
						controller.setProblem(null);
						controller.setForwardNavigationMode(panels == null?WizardController.MODE_CAN_FINISH:WizardController.MODE_CAN_CONTINUE);
					}
				}
				else
				{
					controller.setProblem(Enclosing_Instance.description);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultListCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		private class AnonymousClassDefaultListCellRenderer:DefaultListCellRenderer
		{
			public AnonymousClassDefaultListCellRenderer(PlayOfflinePanels enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PlayOfflinePanels enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PlayOfflinePanels enclosingInstance;
			public PlayOfflinePanels Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				System.Windows.Forms.Label c = (System.Windows.Forms.Label) base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
				if (value_Renamed is PredefinedSetup)
				{
					PredefinedSetup pds = (PredefinedSetup) value_Renamed;
					c.Text = (pds).getConfigureName();
					if (pds.isMenu())
					{
						//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
						c.Size = new System.Drawing.Size(0, 0);
					}
				}
				else
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.ToString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					c.Text = value_Renamed == null?"":value_Renamed.ToString();
				}
				return c;
			}
		}
		private System.Collections.IList setups;
		private System.String description;
		
		protected internal PlayOfflinePanels(System.String title, System.String singleDescription, System.Collections.IList setups):base(title, SETUP_KEY, singleDescription)
		{
			this.setups = setups;
			this.description = singleDescription;
		}
		
		protected internal virtual System.Windows.Forms.Control createPanel(WizardController controller, System.String id, System.Collections.IDictionary settings)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'setupSelection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.ComboBox setupSelection = SupportClass.ComboBoxSupport.CreateComboBox(SupportClass.ICollectionSupport.ToArray(setups));
			//UPGRADE_TODO: Class 'javax.swing.DefaultComboBoxModel' was converted to 'System.Windows.Forms.ComboBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			((System.Windows.Forms.ComboBox.ObjectCollection) setupSelection.Items).Insert(0, description);
			setupSelection.SelectedIndex = 0;
			setupSelection.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener(setupSelection, controller, settings, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(setupSelection);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			setupSelection.setMaximumSize(new System.Drawing.Size(setupSelection.getMaximumSize().Width, setupSelection.Size.Height));
			//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
			setupSelection.setRenderer(new AnonymousClassDefaultListCellRenderer(this));
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box box = Box.createVerticalBox();
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = Box.createVerticalGlue();
			box.Controls.Add(temp_Control);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(setupSelection);
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = Box.createVerticalGlue();
			box.Controls.Add(temp_Control2);
			controller.setProblem(description);
			return box;
		}
		
		protected internal virtual void  loadSetup(PredefinedSetup setup, WizardController controller, System.Collections.IDictionary settings)
		{
			try
			{
				new SavedGameLoader(controller, settings, new BufferedInputStream(setup.getSavedGameContents()), POST_PLAY_OFFLINE_WIZARD).Start();
			}
			// FIXME: review error message
			catch (System.IO.IOException e1)
			{
				controller.setProblem(Resources.getString("WizardSupport.UnableToLoad"));
			}
		}
	}
	/// <summary> Branches the wizard by forwarding to the Wizard stored in the wizard settings under a specified key</summary>
	public class BranchingWizard:WizardBranchController
	{
		private System.String wizardKey;
		
		public BranchingWizard(WizardPanelProvider base_Renamed, System.String key):base(base_Renamed)
		{
			this.wizardKey = key;
		}
		
		protected internal virtual WizardPanelProvider getPanelProviderForStep(System.String step, System.Collections.IDictionary settings)
		{
			return (WizardPanelProvider) settings[wizardKey];
		}
		
		protected internal virtual Wizard getWizardForStep(System.String step, System.Collections.IDictionary settings)
		{
			Wizard w = null;
			System.Object next = settings[wizardKey];
			if (next is Wizard)
			{
				w = (Wizard) next;
			}
			else
			{
				w = base.getWizardForStep(step, settings);
			}
			return w;
		}
	}
	/// <summary> Loads a saved game in the background. Add a branch to the wizard if the loaded game has unfinished
	/// {@link GameSetupStep}s. Otherwise, enable the finish button.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class SavedGameLoader:SupportClass.ThreadClass
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassCommandFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassCommandFilter:CommandFilter
		{
			public AnonymousClassCommandFilter(SavedGameLoader enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameLoader enclosingInstance;
			public SavedGameLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			protected internal override bool accept(Command c)
			{
				return !(c is GameState.SetupCommand) || !((GameState.SetupCommand) c).GameStarting;
			}
		}
		private WizardController controller;
		private System.Collections.IDictionary settings;
		// FIXME: this is a bad design---when can we safely close this stream?!
		private System.IO.Stream in_Renamed;
		private System.String wizardKey;
		
		public SavedGameLoader(WizardController controller, System.Collections.IDictionary settings, System.IO.Stream in_Renamed, System.String wizardKey):base()
		{
			this.controller = controller;
			this.settings = settings;
			this.in_Renamed = in_Renamed;
			this.wizardKey = wizardKey;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		override public void  Run()
		{
			try
			{
				controller.setProblem(Resources.getString("WizardSupport.LoadingGame")); //$NON-NLS-1$
				Command setupCommand = loadSavedGame();
				setupCommand.execute();
				controller.setProblem(null);
				//UPGRADE_NOTE: Final was removed from the declaration of 'panels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GameSetupPanels panels = GameSetupPanels.newInstance();
				settings[wizardKey] = panels;
				controller.setForwardNavigationMode(panels == null?WizardController.MODE_CAN_FINISH:WizardController.MODE_CAN_CONTINUE);
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				controller.setProblem(Resources.getString("WizardSupport.UnableToLoad")); //$NON-NLS-1$
			}
		}
		
		protected internal virtual Command loadSavedGame()
		{
			Command setupCommand = GameModule.getGameModule().getGameState().decodeSavedGame(in_Renamed);
			if (setupCommand == null)
			{
				throw new System.IO.IOException(Resources.getString("WizardSupport.InvalidSavefile")); //$NON-NLS-1$
			}
			// Strip out the setup(true) command. This will be applied when the "Finish" button is pressed
			setupCommand = new AnonymousClassCommandFilter(this).apply(setupCommand);
			return setupCommand;
		}
	}
	
	public class TutorialLoader:SavedGameLoader
	{
		private Tutorial tutorial;
		
		public TutorialLoader(WizardController controller, System.Collections.IDictionary settings, System.IO.Stream in_Renamed, System.String wizardKey, Tutorial tutorial):base(controller, settings, in_Renamed, wizardKey)
		{
			this.tutorial = tutorial;
		}
		
		protected internal override Command loadSavedGame()
		{
			System.String msg = tutorial.getWelcomeMessage();
			Command c = msg == null?new NullCommand():new Chatter.DisplayText(GameModule.getGameModule().getChatter(), msg);
			c = c.append(base.loadSavedGame());
			return c;
		}
	}
	/// <summary> Wizard pages for loading a saved game
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class LoadSavedGamePanels:WizardPanelProvider
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(WizardController controller, System.Collections.IDictionary settings, LoadSavedGamePanels enclosingInstance)
			{
				InitBlock(controller, settings, enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassSavedGameLoader' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassSavedGameLoader:SavedGameLoader
			{
				public AnonymousClassSavedGameLoader(System.IO.FileInfo f, AnonymousClassPropertyChangeListener enclosingInstance)
				{
					InitBlock(f, enclosingInstance);
				}
				private void  InitBlock(System.IO.FileInfo f, AnonymousClassPropertyChangeListener enclosingInstance)
				{
					this.f = f;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable f was copied into class AnonymousClassSavedGameLoader. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.IO.FileInfo f;
				private AnonymousClassPropertyChangeListener enclosingInstance;
				public AnonymousClassPropertyChangeListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  run()
				{
					base.run();
					processing.remove(f);
				}
			}
			private void  InitBlock(WizardController controller, System.Collections.IDictionary settings, LoadSavedGamePanels enclosingInstance)
			{
				this.controller = controller;
				this.settings = settings;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable controller was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private WizardController controller;
			//UPGRADE_NOTE: Final variable settings was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.IDictionary settings;
			private LoadSavedGamePanels enclosingInstance;
			public LoadSavedGamePanels Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Set < File > processing = new HashSet < File >();
			
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo f = (System.IO.FileInfo) evt.NewValue;
				bool tmpBool;
				if (System.IO.File.Exists(f.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(f.FullName);
				if (f == null || !tmpBool)
				{
					controller.setProblem(Resources.getString("WizardSupport.NoSuchFile")); //$NON-NLS-1$
				}
				else if (System.IO.Directory.Exists(f.FullName))
				{
					controller.setProblem(""); //$NON-NLS-1$
				}
				else if (!processing.contains(f))
				{
					// Sometimes the FileConfigurer fires more than one event for the same
					// file
					processing.add(f);
					try
					{
						//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
						new AnonymousClassSavedGameLoader(f, this, controller, settings, new System.IO.BufferedStream(new System.IO.FileStream(f.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)), POST_LOAD_GAME_WIZARD).start();
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						controller.setProblem(Resources.getString("WizardSupport.UnableToLoad")); //$NON-NLS-1$
					}
				}
			}
		}
		private FileConfigurer fileConfig;
		
		public LoadSavedGamePanels():base(Resources.getString("WizardSupport.LoadGame"), LOAD_GAME_ACTION, Resources.getString("WizardSupport.LoadSavedGame"))
		{ //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		protected internal virtual System.Windows.Forms.Control createPanel(WizardController controller, System.String id, System.Collections.IDictionary settings)
		{
			if (fileConfig == null)
			{
				fileConfig = new FileConfigurer(null, Resources.getString("WizardSupport.SavedGame"), GameModule.getGameModule().getGameState().getSavedGameDirectoryPreference()); //$NON-NLS-1$
				fileConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(controller, settings, this).propertyChange);
				controller.setProblem(Resources.getString("WizardSupport.SelectSavedGame")); //$NON-NLS-1$
			}
			return (System.Windows.Forms.Control) fileConfig.Controls;
		}
	}
	/// <summary> Wizard page for an unfinished {@link GameSetupStep}
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class SetupStepPage:WizardPage
	{
		private const long serialVersionUID = 1L;
		
		public SetupStepPage(GameSetupStep step):base(step.StepTitle)
		{
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			setLayout(new BorderLayout());
			add(step.Controls);
			putWizardData(step, step);
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setBackgroundImage(Image image)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(image != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ImageIcon icon = new ImageIcon(image);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	logoSize = new Dimension(icon.getIconWidth(), icon.getIconHeight());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final BufferedImage img = 
	ImageUtils.createCompatibleTranslucentImage(logoSize.width, 
	logoSize.height);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Graphics2D g = img.createGraphics();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	g.setColor(Color.white);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	g.fillRect(0, 0, icon.getIconWidth(), icon.getIconHeight());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	g.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, 0.5F));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	icon.paintIcon(null, g, 0, 0);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	g.dispose();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	UIManager.put(wizard.sidebar.image, img); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	/// <summary> Wizard pages for starting a new game. One page will be added for each unfinished {@link GameSetupStep}
	/// 
	/// </summary>
	/// <seealso cref="GameState.getUnfinishedSetupSteps()">
	/// </seealso>
	/// <author>  rkinney
	/// </author>
	public class GameSetupPanels:WizardPanelProvider, WizardResultProducer
	{
		public GameSetupPanels()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			base(steps, descriptions);
			this.pages = pages;
			this.setupSteps = setupSteps;
		}
		private WizardPage[] pages;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < GameSetupStep > setupSteps;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private GameSetupPanels(String [] steps, String [] descriptions, WizardPage [] pages, List < GameSetupStep > setupSteps)
		
		public static GameSetupPanels newInstance()
		{
			GameSetupPanels panels = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < SetupStepPage > pages = new ArrayList < SetupStepPage >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < GameSetupStep > setupSteps = new ArrayList < GameSetupStep >();
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			if (!pages.isEmpty())
			{
				WizardPage[] wizardPages = pages.toArray(new WizardPage[pages.size()]);
				System.String[] steps = new System.String[setupSteps.size()];
				System.String[] desc = new System.String[setupSteps.size()];
				for (int i = 0, n = setupSteps.size(); i < n; i++)
				{
					steps[i] = System.Convert.ToString(i);
					desc[i] = setupSteps.get_Renamed(i).getStepTitle();
				}
				panels = new GameSetupPanels(steps, desc, wizardPages, setupSteps);
			}
			return panels;
		}
		
		protected internal virtual System.Windows.Forms.Control createPanel(WizardController controller, System.String id, System.Collections.IDictionary settings)
		{
			int index = indexOfStep(id);
			controller.setForwardNavigationMode(index == pages.length - 1?WizardController.MODE_CAN_FINISH:WizardController.MODE_CAN_CONTINUE);
			return pages[index];
		}
		
		public virtual bool cancel(System.Collections.IDictionary settings)
		{
			GameModule.getGameModule().getGameState().setup(false);
			return true;
		}
		
		public virtual System.Object finish(System.Collections.IDictionary wizardData)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GameSetupStep step: setupSteps)
			{
				step.finish();
			}
			return wizardData;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual Wizard newWizard(ref System.Drawing.Size logoSize)
		{
			return createWizard();
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
#endif
	}
}