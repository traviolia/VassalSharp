/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Configurer = VassalSharp.configure.Configurer;
using ImageConfigurer = VassalSharp.configure.ImageConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageFileFilter = VassalSharp.tools.filechooser.ImageFileFilter;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using Op = VassalSharp.tools.imageop.Op;
using OpIcon = VassalSharp.tools.imageop.OpIcon;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.tools.icon
{
	
	/// <summary> An IconFamily is a named set of Icons in the four standard Tango sizes.
	/// 
	/// Each IconFamily consists of at least a Scalable Icon, plus zero or more
	/// specifically sized icons.
	/// 
	/// If a specific sized Icon is missing, the IconFamily will supply a scaled icon
	/// based on the Scalable icon.
	/// 
	/// Icons are created as lazily as possible.
	/// 
	/// IconFamilys are created in two ways: - For Vassal inbuilt Icons by
	/// IconFactory when it scans the Vengine for inbuilt Icons - For Modules,
	/// IconFamilys can be added to IconFamilyContainer by the module designer.
	/// 
	/// Each IconFamily consists of at least a Scalable Icon, plus zero or more
	/// specifically sized icons. If an
	/// </summary>
	public class IconFamily:AbstractConfigurable
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
			return new System.Type[0];
			return new System.Type[0];
		}
		[System.ComponentModel.Browsable(true)]
		public new  event SupportClass.PropertyChangeEventHandler PropertyChange;
		/// <summary> Return list of Icon Size names in local language
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		public static System.String[] IconSizeNames
		{
			get
			{
				lock (SIZE_NAMES)
				{
					if (SIZE_NAMES[0] == null)
					{
						SIZE_NAMES[XSMALL] = Resources.getString("Icon.extra_small"); //$NON-NLS-1$
						SIZE_NAMES[SMALL] = Resources.getString("Icon.small"); //$NON-NLS-1$
						SIZE_NAMES[MEDIUM] = Resources.getString("Icon.medium"); //$NON-NLS-1$
						SIZE_NAMES[LARGE] = Resources.getString("Icon.large"); //$NON-NLS-1$
					}
				}
				return SIZE_NAMES;
			}
			
		}
		virtual public System.String ScalableIconPath
		{
			set
			{
				scalablePath = value;
				scalableIcon = null;
			}
			
		}
		virtual public bool Legacy
		{
			get
			{
				return Name.contains("."); //$NON-NLS-1$
			}
			
		}
		/// <summary> Return the scalable icon directly (used by {@link IconImageConfigurer})
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public System.Drawing.Image ScalableIcon
		{
			get
			{
				lock (this)
				{
					buildScalableIcon();
				}
				return scalableIcon;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return getConfigureName();
			}
			
		}
		public static System.String ConfigureTypeName
		{
			// Note: Custom Configurer
			
			get
			{
				return Resources.getString("Editor.IconFamily.component_type"); //$NON-NLS-1$
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{VassalSharp.build.Configurable_Fields.NAME_PROPERTY, SCALABLE_ICON, ICON0, ICON1, ICON2, ICON3};
			}
			
		}
		override public Configurer Configurer
		{
			get
			{
				return new IconFamilyConfig(this);
			}
			
		}
		
		public const System.String SCALABLE_ICON = "scalableIcon"; //$NON-NLS-1$
		public const System.String ICON0 = "icon0"; //$NON-NLS-1$
		public const System.String ICON1 = "icon1"; //$NON-NLS-1$
		public const System.String ICON2 = "icon2"; //$NON-NLS-1$
		public const System.String ICON3 = "icon3"; //$NON-NLS-1$
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'propSupport '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport propSupport;
		
		// Tango Icon sizes
		public const int XSMALL = 0;
		public const int SMALL = 1;
		public const int MEDIUM = 2;
		public const int LARGE = 3;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'MAX_SIZE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly int MAX_SIZE = LARGE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'SIZE_COUNT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly int SIZE_COUNT = MAX_SIZE + 1;
		
		// Pixel size of each Tango Size
		//UPGRADE_NOTE: Final was removed from the declaration of 'SIZES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly int[] SIZES = new int[]{16, 22, 32, 48};
		
		// Directories within the icons directory to locate each Tango Size
		//UPGRADE_NOTE: Final was removed from the declaration of 'SIZE_DIRS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly System.String[] SIZE_DIRS = new System.String[]{"16x16/", "22x22/", "32x32/", "48x48/"};
		
		// Names of sizes in local language
		//UPGRADE_NOTE: Final was removed from the declaration of 'SIZE_NAMES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly System.String[] SIZE_NAMES = new System.String[SIZE_COUNT]; 
		
		// Directory within the icons directory holding the Scalable versions of the
		// icons
		internal const System.String SCALABLE_DIR = "scalable/"; //$NON-NLS-1$
		
		// Cache of the icons in this family
		protected internal OpIcon[] icons;
		protected internal OpIcon scalableIcon;
		
		// Paths to the source of the icons in this family
		protected internal System.String scalablePath;
		protected internal System.String[] sizePaths = new System.String[SIZE_COUNT];
		
		/// <summary> Return an Icon Size based on the local language name</summary>
		public static int getIconSize(System.String name)
		{
			int size = SMALL;
			//UPGRADE_NOTE: Final was removed from the declaration of 'options '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] options = IconSizeNames;
			for (int i = 0; i < options.Length; i++)
			{
				if (options[i].Equals(name))
				{
					return i;
				}
			}
			return size;
		}
		
		public static int getIconHeight(int size)
		{
			if (size < 0 || size > MAX_SIZE)
			{
				return 0;
			}
			return SIZES[size];
		}
		
		/// <summary> Create a new IconFamily with the given name. The name supplied will
		/// normally be the name of an IconFamily, with no suffix.
		/// 
		/// These constructors are used by IconFactory to create IconFamilys for the
		/// Vassal inbuilt Icons
		/// 
		/// FIXME: Write this bit...Will be needed once Toolbar Icon support is added
		/// Backward Compatibility: If the name supplied does have a file type suffix,
		/// then it is a specific Icon name from a pre-IconFamily module. By throwing
		/// away the suffix, IconFamily will use the supplied icon as a base icon to
		/// create the full IconFamily.
		/// 
		/// </summary>
		/// <param name="familyName">IconFamily name or Icon name
		/// </param>
		/// <param name="scalableName">Name of the scalable icon
		/// </param>
		/// <param name="sizeNames">Names of the sized Icons
		/// </param>
		public IconFamily(System.String familyName, System.String scalableName, System.String[] sizeName):this(familyName)
		{
			ScalableIconPath = scalableName;
			for (int i = 0; i < MAX_SIZE; i++)
			{
				setSizeIconPath(i, sizeName[i]);
			}
		}
		
		public IconFamily(System.String familyName):this()
		{
			setConfigureName(familyName);
		}
		
		public IconFamily()
		{
			InitBlock();
			icons = new OpIcon[SIZE_COUNT];
			setConfigureName(""); //$NON-NLS-1$
		}
		
		public virtual void  setSizeIconPath(int size, System.String path)
		{
			sizePaths[size] = path;
			icons[size] = null;
		}
		
		/// <summary> Return a particular sized icon. If it can't be found, then build it by
		/// scaling the base Icon.
		/// 
		/// </summary>
		/// <param name="size">Icon size
		/// </param>
		/// <returns> Icon
		/// </returns>
		public virtual System.Drawing.Image getIcon(int size)
		{
			if (size < 0 || size > MAX_SIZE)
			{
				return null;
			}
			
			lock (this)
			{
				if (icons[size] == null)
				{
					icons[size] = buildIcon(size);
				}
			}
			return icons[size];
		}
		
		/// <summary> Return a particular sized Icon, but do not build one from the scalable Icon
		/// if it is not found.
		/// 
		/// </summary>
		/// <param name="size">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.Drawing.Image getRawIcon(int size)
		{
			if (size < 0 || size > MAX_SIZE || sizePaths[size] == null)
			{
				return null;
			}
			return getIcon(size);
		}
		
		public virtual System.Drawing.Bitmap getImage(int size)
		{
			if (size < 0 || size > MAX_SIZE)
			{
				return null;
			}
			getIcon(size);
			return (System.Drawing.Bitmap) (icons[size] == null?null:icons[size].getImage());
		}
		
		protected internal virtual OpIcon buildIcon(int size)
		{
			// Do we have it ready to go?
			if (icons[size] != null)
			{
				return icons[size];
			}
			
			// This size exists?
			if (sizePaths[size] != null)
			{
				icons[size] = new OpIcon(Op.load(sizePaths[size]));
				icons[size].getImage();
				return icons[size];
			}
			
			// No, So we need to build it from the Scalable version
			buildScalableIcon();
			
			icons[size] = scaleIcon(scalableIcon, SIZES[size]);
			icons[size].getImage();
			return icons[size];
		}
		
		protected internal virtual void  buildScalableIcon()
		{
			if (scalableIcon == null)
			{
				if (scalablePath != null)
				{
					scalableIcon = new OpIcon(Op.load(scalablePath));
				}
			}
		}
		
		/// <summary> Scale an Icon to desired size
		/// 
		/// </summary>
		/// <param name="base">Base Icon
		/// </param>
		/// <param name="toSizePixels">Required Size in Pixels
		/// </param>
		/// <returns> Scaled Icon
		/// </returns>
		protected internal virtual OpIcon scaleIcon(OpIcon base_Renamed, int toSizePixels)
		{
			
			if (base_Renamed == null)
			{
				return null;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'baseHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int baseHeight = base_Renamed.Height;
			if (baseHeight == toSizePixels)
			{
				return base_Renamed;
			}
			
			return new OpIcon(Op.scale(base_Renamed.Op, ((double) toSizePixels) / base_Renamed.Height));
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public override void  addPropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(l);
		}
		
		public override void  setConfigureName(System.String s)
		{
			System.String oldName = name;
			this.name = s;
			SupportClass.PropertyChangingEventArgs me46 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.Configurable_Fields.NAME_PROPERTY, oldName, name);
			if (PropertyChange != null)
				PropertyChange(this, me46);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(key))
			{
				return getConfigureName();
			}
			else if (SCALABLE_ICON.Equals(key))
			{
				return scalablePath;
			}
			else if (ICON0.Equals(key))
			{
				return sizePaths[0];
			}
			else if (ICON1.Equals(key))
			{
				return sizePaths[1];
			}
			else if (ICON2.Equals(key))
			{
				return sizePaths[2];
			}
			else if (ICON3.Equals(key))
			{
				return sizePaths[3];
			}
			return null;
		}
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(key))
			{
				setConfigureName((System.String) value_Renamed);
			}
			else if (SCALABLE_ICON.Equals(key))
			{
				ScalableIconPath = ((System.String) value_Renamed);
			}
			else if (ICON0.Equals(key))
			{
				setSizeIconPath(0, (System.String) value_Renamed);
			}
			else if (ICON1.Equals(key))
			{
				setSizeIconPath(1, (System.String) value_Renamed);
			}
			else if (ICON2.Equals(key))
			{
				setSizeIconPath(2, (System.String) value_Renamed);
			}
			else if (ICON3.Equals(key))
			{
				setSizeIconPath(3, (System.String) value_Renamed);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override HelpFile getHelpFile()
		{
			return null;
		}
		
		public override void  removeFrom(Buildable parent)
		{
			
		}
		
		public override void  addTo(Buildable parent)
		{
			
		}
		
		/// <summary>****************************************************
		/// Custom Configurer for Icon Family
		/// 
		/// </summary>
		internal class IconFamilyConfig:Configurer
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(IconFamilyConfig enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(IconFamilyConfig enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private IconFamilyConfig enclosingInstance;
				public IconFamilyConfig Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					if (evt.NewValue != null)
					{
						Enclosing_Instance.family.setConfigureName((System.String) evt.NewValue);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(IconFamilyConfig enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(IconFamilyConfig enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private IconFamilyConfig enclosingInstance;
				public IconFamilyConfig Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(evt.PropertyName))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'savedFamily '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						IconFamily savedFamily = IconFactory.getIconFamily(Enclosing_Instance.family.Name);
						Enclosing_Instance.errorLabel.Visible = savedFamily != null && savedFamily != Enclosing_Instance.family;
					}
				}
			}
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					return null;
				}
				
			}
			protected internal IconFamily family;
			protected internal System.Windows.Forms.Panel controls;
			protected internal StringConfigurer title;
			protected internal System.Windows.Forms.Label errorLabel;
			protected internal ImageConfigurer scalableConfig;
			
			public IconFamilyConfig(IconFamily f):base(null, null)
			{
				family = f;
				
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'mig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel mig = new JPanel(new MigLayout("inset 5")); //$NON-NLS-1$
				
				title = new StringConfigurer(null, "", family.getConfigureName()); //$NON-NLS-1$
				title.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("Editor.IconFamily.family_name");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				mig.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				mig.Controls.Add(title.Controls);
				title.Controls.Dock = new System.Windows.Forms.DockStyle();
				title.Controls.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label3;
				temp_label3 = new System.Windows.Forms.Label();
				temp_label3.Text = Resources.getString("Editor.IconFamily.name_taken");
				errorLabel = temp_label3; //$NON-NLS-1$
				errorLabel.ForeColor = System.Drawing.Color.Red;
				errorLabel.Visible = false;
				family.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				mig.Controls.Add(errorLabel);
				errorLabel.Dock = new System.Windows.Forms.DockStyle();
				errorLabel.BringToFront(); //$NON-NLS-1$
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'scalableConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IconImageConfigurer scalableConfig = new IconImageConfigurer(family);
				System.Windows.Forms.Label temp_label5;
				temp_label5 = new System.Windows.Forms.Label();
				temp_label5.Text = Resources.getString("Editor.IconFamily.scalable_icon_label");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label5;
				mig.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				mig.Controls.Add(scalableConfig.Controls);
				scalableConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				scalableConfig.Controls.BringToFront(); //$NON-NLS-1$
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sizeConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IconImageConfigurer[] sizeConfig = new IconImageConfigurer[IconFamily.SIZE_COUNT];
				for (int size = 0; size < IconFamily.SIZE_COUNT; size++)
				{
					sizeConfig[size] = new IconImageConfigurer(family, size);
					//UPGRADE_NOTE: Final was removed from the declaration of 'px '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String px = System.Convert.ToString(IconFamily.SIZES[size]);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control3;
					temp_Control3 = new JLabel(Resources.getString("Editor.IconFamily.icon_label", IconFamily.IconSizeNames[size], px));
					mig.Controls.Add(temp_Control3); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					mig.Controls.Add(sizeConfig[size].Controls);
					sizeConfig[size].Controls.Dock = new System.Windows.Forms.DockStyle();
					sizeConfig[size].Controls.BringToFront(); //$NON-NLS-1$
				}
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(mig);
			}
			
			public override void  setValue(System.String s)
			{
				
			}
		}
		
		/// <summary>***********************************************
		/// Configure an individual Icon Image
		/// </summary>
		internal class IconImageConfigurer:Configurer
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			[Serializable]
			private class AnonymousClassJPanel:System.Windows.Forms.Panel
			{
				public AnonymousClassJPanel(IconImageConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(IconImageConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private IconImageConfigurer enclosingInstance;
				public IconImageConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
				{
					System.Drawing.Graphics g = null;
					if (g_EventArg != null)
						g = g_EventArg.Graphics;
					g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, Size.Width, Size.Height)));
					//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Image i = Enclosing_Instance.IconValue;
					if (i != null)
					{
						//UPGRADE_TODO: Method 'javax.swing.Icon.paintIcon' was converted to 'System.Drawing.Graphics.drawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingIconpaintIcon_javaawtComponent_javaawtGraphics_int_int'"
						g.DrawImage(i, Size.Width / 2 - i.Width / 2, Size.Height / 2 - i.Height / 2);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(System.Windows.Forms.Panel p, IconImageConfigurer enclosingInstance)
				{
					InitBlock(p, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.Panel p, IconImageConfigurer enclosingInstance)
				{
					this.p = p;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Panel p;
				private IconImageConfigurer enclosingInstance;
				public IconImageConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.selectImage();
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					p.Refresh();
				}
			}
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					if (controls == null)
					{
						controls = new JPanel(new MigLayout());
						System.Windows.Forms.Label temp_label2;
						temp_label2 = new System.Windows.Forms.Label();
						temp_label2.Text = getName();
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						System.Windows.Forms.Control temp_Control;
						temp_Control = temp_label2;
						controls.Controls.Add(temp_Control);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Windows.Forms.Panel p = new AnonymousClassJPanel(this);
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						p.Size = new System.Drawing.Size(px, px);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						controls.Controls.Add(p);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'select '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Windows.Forms.Button select = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SELECT));
						select.Click += new System.EventHandler(new AnonymousClassActionListener(p, this).actionPerformed);
						SupportClass.CommandManager.CheckCommand(select);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(select);
						select.Dock = new System.Windows.Forms.DockStyle();
						select.BringToFront(); //$NON-NLS-1$
						
						warningLabel = new System.Windows.Forms.Label();
						warningLabel.ForeColor = System.Drawing.Color.Red;
						warningLabel.Visible = false;
						checkIconSize();
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(warningLabel);
						warningLabel.Dock = new System.Windows.Forms.DockStyle();
						warningLabel.BringToFront(); //$NON-NLS-1$
					}
					return controls;
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					if (size < 0)
					{
						return family.scalablePath;
					}
					else
					{
						return family.sizePaths[size];
					}
				}
				
			}
			virtual public System.Drawing.Image IconValue
			{
				get
				{
					System.Drawing.Image icon = null;
					if (family != null)
					{
						if (size < 0)
						{
							if (family.scalablePath != null)
							{
								icon = family.ScalableIcon;
							}
						}
						else
						{
							if (family.sizePaths[size] != null)
							{
								icon = family.getIcon(size);
							}
						}
					}
					return icon;
				}
				
			}
			virtual protected internal System.String Warning
			{
				set
				{
					warningLabel.Text = value;
					warningLabel.Visible = value != null && value.Length > 0;
					repack();
				}
				
			}
			
			protected internal int size;
			protected internal System.Windows.Forms.Panel controls;
			protected internal IconFamily family;
			protected internal int px;
			protected internal System.Windows.Forms.Label warningLabel;
			
			public IconImageConfigurer(IconFamily family, int size):base(null, null)
			{
				this.size = size;
				this.family = family;
				if (size < 0)
				{
					px = IconFamily.SIZES[IconFamily.LARGE];
				}
				else
				{
					px = IconFamily.SIZES[size];
				}
			}
			
			/// <summary> Constructor to Configure the scalable icon;
			/// 
			/// </summary>
			/// <param name="key">
			/// </param>
			/// <param name="name">
			/// </param>
			/// <param name="testElementName">
			/// </param>
			public IconImageConfigurer(IconFamily family):this(family, - 1)
			{
			}
			
			public override void  setValue(System.String s)
			{
				if (size < 0)
				{
					family.ScalableIconPath = buildPath(s);
				}
				else
				{
					family.setSizeIconPath(size, buildPath(s));
					checkIconSize();
				}
			}
			
			protected internal virtual void  checkIconSize()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'check '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Image check = family.getRawIcon(size);
				if (check != null)
				{
					if (check.Height != IconFamily.SIZES[size])
					{
						Warning = Resources.getString("Editor.IconFamily.size_warning", IconFamily.SIZES[size], check.Height); //$NON-NLS-1$
					}
				}
			}
			
			protected internal virtual System.String buildPath(System.String s)
			{
				if (s == null || s.Length == 0)
				{
					return null;
				}
				
				if (size < 0)
				{
					return ArchiveWriter.ICON_DIR + IconFamily.SCALABLE_DIR + s; //$NON-NLS-1$
				}
				else
				{
					return ArchiveWriter.ICON_DIR + IconFamily.SIZE_DIRS[size] + s; //$NON-NLS-1$
				}
			}
			
			protected internal virtual void  repack()
			{
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
			
			protected internal virtual void  selectImage()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = GameModule.getGameModule().getFileChooser();
				fc.FileFilter = new FamilyImageFilter(family.Name);
				fc.SelectedFile = new System.IO.FileInfo(family.Name + ".*"); //$NON-NLS-1$
				if (fc.showOpenDialog(Controls) != FileChooser.APPROVE_OPTION)
				{
					Warning = ""; //$NON-NLS-1$
					setValue(null);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo f = fc.SelectedFile;
					bool tmpBool;
					if (System.IO.File.Exists(f.FullName))
						tmpBool = true;
					else
						tmpBool = System.IO.Directory.Exists(f.FullName);
					if (f != null && tmpBool)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String name = f.Name;
						if (name.split("\\.").length != 2)
						{
							//$NON-NLS-1$
							showError(Resources.getString("Editor.IconFamily.illegal_icon_name")); //$NON-NLS-1$
						}
						else if (!name.StartsWith(family.Name))
						{
							showError(Resources.getString("Editor.IconFamily.bad_icon_name", family.Name)); //$NON-NLS-1$
						}
						else if (!ImageUtils.hasImageSuffix(name))
						{
							showError(Resources.getString("Editor.IconFamily.bad_icon_file")); //$NON-NLS-1$
						}
						else
						{
							GameModule.getGameModule().getArchiveWriter().addImage(f.FullName, buildPath(f.Name));
							Warning = ""; //$NON-NLS-1$
							setValue(name);
						}
					}
					else
					{
						Warning = ""; //$NON-NLS-1$
						setValue(null);
					}
				}
			}
			
			protected internal virtual void  showError(System.String message)
			{
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				Dialogs.showMessageDialog((System.Windows.Forms.Form) controls.Parent, Resources.getString("Editor.IconFamily.icon_load_error"), Resources.getString("Editor.IconFamily.cannot_load_icon"), message, (int) System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
		
		/// <summary> Filter Icon files matching this family
		/// 
		/// </summary>
		internal class FamilyImageFilter:ImageFileFilter
		{
			private System.String familyName;
			
			public FamilyImageFilter(System.String family):base()
			{
				familyName = family;
			}
			
			public override bool accept(System.IO.FileInfo f)
			{
				if (base.accept(f))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String s = f.Name.split("\\.")[0]; //$NON-NLS-1$
					return s.Equals(familyName);
				}
				return false;
			}
		}
	}
}