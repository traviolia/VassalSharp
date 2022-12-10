/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using URLUtils = VassalSharp.tools.URLUtils;
using AudioFileFilter = VassalSharp.tools.filechooser.AudioFileFilter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.configure
{
	
	/// <summary> Configurer for specifying an AudioClip. This class is intended to allow
	/// players to override a default sound with their own sound file on their
	/// local file system.
	/// </summary>
	public class SoundConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(SoundConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SoundConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SoundConfigurer enclosingInstance;
			public SoundConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.play();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(SoundConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SoundConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SoundConfigurer enclosingInstance;
			public SoundConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setValue(VassalSharp.configure.SoundConfigurer.DEFAULT);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(SoundConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SoundConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SoundConfigurer enclosingInstance;
			public SoundConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.chooseClip();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAudioClipFactory' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassAudioClipFactory : SoundConfigurer.AudioClipFactory
		{
			public AnonymousClassAudioClipFactory(SoundConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SoundConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SoundConfigurer enclosingInstance;
			public SoundConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
			public virtual AudioClip getAudioClip(System.Uri url)
			{
				//UPGRADE_ISSUE: Method 'java.applet.Applet.newAudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAppletnewAudioClip_javanetURL'"
				return Applet.newAudioClip(url);
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					controls = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = name;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					controls.Controls.Add(temp_Control);
					System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Play");
					b.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(b);
					b = SupportClass.ButtonSupport.CreateStandardButton("Default");
					b.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(b);
					b = SupportClass.ButtonSupport.CreateStandardButton("Select");
					b.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(b);
					textField = new System.Windows.Forms.TextBox();
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					textField.setMaximumSize(new System.Drawing.Size(textField.getMaximumSize().Width, textField.Size.Height));
					textField.ReadOnly = !false;
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					textField.Text = DEFAULT.Equals(clipName)?defaultResource:clipName;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(textField);
				}
				return controls;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				System.String s = NO_VALUE;
				if (clipName != null)
				{
					s = clipName;
				}
				return s;
			}
			
		}
		public const System.String DEFAULT = "default";
		private System.String defaultResource;
		private System.String clipName;
		private System.Windows.Forms.Panel controls;
		private System.Windows.Forms.TextBox textField;
		private SoundConfigurer.AudioClipFactory clipFactory;
		private const System.String NO_VALUE = "<disabled>";
		
		public SoundConfigurer(System.String key, System.String name, System.String defaultResource):base(key, name)
		{
			this.defaultResource = defaultResource;
			clipFactory = createAudioClipFactory();
			setValue(DEFAULT);
		}
		
		public override void  setValue(System.String s)
		{
			if (clipFactory == null)
			{
				return ;
			}
			System.Uri url = null;
			if (DEFAULT.Equals(s))
			{
				GetType();
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				url = new System.Uri(System.IO.Path.GetFullPath("/images/" + defaultResource));
				clipName = s;
			}
			else if (NO_VALUE.Equals(s))
			{
				clipName = s;
			}
			else if (s != null)
			{
				try
				{
					url = URLUtils.toURL(new System.IO.FileInfo(s));
					clipName = s;
				}
				catch (System.UriFormatException e)
				{
					ReadErrorDialog.error(e, s);
					clipName = null;
				}
			}
			if (textField != null)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				textField.Text = DEFAULT.Equals(clipName)?defaultResource:clipName;
			}
			if (url != null)
			{
				try
				{
					setValue(clipFactory.getAudioClip(url));
				}
				catch (System.IO.IOException e)
				{
					ReadErrorDialog.error(e, url.ToString());
				}
			}
			else
			{
				if (textField != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					textField.Text = null;
				}
				setValue((System.Object) null);
			}
		}
		
		protected internal interface AudioClipFactory
		{
			//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
			AudioClip getAudioClip(System.Uri url);
		}
		
		protected internal virtual SoundConfigurer.AudioClipFactory createAudioClipFactory()
		{
			return new AnonymousClassAudioClipFactory(this);
		}
		
		public virtual void  play()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'clip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
			AudioClip clip = (AudioClip) getValue();
			if (clip != null)
			{
				//UPGRADE_ISSUE: Method 'java.applet.AudioClip.play' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
				clip.play();
			}
		}
		
		public virtual void  chooseClip()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = GameModule.getGameModule().getFileChooser();
			fc.FileFilter = new AudioFileFilter();
			
			if (fc.showOpenDialog(Controls) != FileChooser.APPROVE_OPTION)
			{
				setValue(NO_VALUE);
			}
			else
			{
				System.IO.FileInfo f = fc.SelectedFile;
				setValue(f.Name);
			}
		}
	}
}