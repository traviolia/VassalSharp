/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
// For string related utils
using BshClassManager = bsh.BshClassManager;
using BshClassPath = bsh.classpath.BshClassPath;
using ClassPathListener = bsh.classpath.ClassPathListener;
using ClassPathException = bsh.ClassPathException;
using StringUtil = bsh.StringUtil;
using ConsoleInterface = bsh.ConsoleInterface;
using ClassManagerImpl = bsh.classpath.ClassManagerImpl;
namespace bsh.util
{
	
	/// <summary>A simple class browser for the BeanShell desktop.</summary>
	//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	[Serializable]
	public class ClassBrowser:SupportClass.SplitterPanelSupport, ClassPathListener
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTreeSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTreeSelectionListener
		{
			public AnonymousClassTreeSelectionListener(ClassBrowser enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ClassBrowser enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ClassBrowser enclosingInstance;
			public ClassBrowser Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
			{
				//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				//UPGRADE_ISSUE: Method 'javax.swing.event.TreeSelectionEvent.getPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeSelectionEventgetPath'"
				TreePath tp = e.getPath();
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				System.Object[] oa = tp.getPath();
				System.Text.StringBuilder selectedPackage = new System.Text.StringBuilder();
				for (int i = 1; i < oa.Length; i++)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					selectedPackage.Append(oa[i].ToString());
					if (i + 1 < oa.Length)
						selectedPackage.Append(".");
				}
				Enclosing_Instance.Clist = selectedPackage.ToString();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTreeSelectionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTreeSelectionListener1
		{
			public AnonymousClassTreeSelectionListener1(ClassBrowser enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ClassBrowser enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ClassBrowser enclosingInstance;
			public ClassBrowser Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				//UPGRADE_ISSUE: Method 'javax.swing.event.TreeSelectionEvent.getPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeSelectionEventgetPath'"
				Enclosing_Instance.driveToClass(e.getPath().getLastPathComponent().ToString());
			}
		}
		virtual internal System.String Clist
		{
			set
			{
				this.selectedPackage = value;
				
				SupportClass.SetSupport set_Renamed = classPath.getClassesForPackage(value);
				if (set_Renamed == null)
				{
					//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
					set_Renamed = new SupportClass.HashSetSupport();
				}
				
				// remove inner classes and shorten class names
				System.Collections.IList list = new System.Collections.ArrayList();
				System.Collections.IEnumerator it = set_Renamed.GetEnumerator();
				//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
				while (it.MoveNext())
				{
					//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
					System.String cname = (System.String) it.Current;
					if (cname.IndexOf("$") == - 1)
						list.Add(BshClassPath.splitClassname(cname)[1]);
				}
				
				classesList = toSortedStrings(list);
				classlist.Items.Clear();
				classlist.Items.AddRange(new System.Collections.ArrayList(classesList).ToArray());
				//setMlist( (String)classlist.getModel().getElementAt(0) );
			}
			
		}
		virtual internal System.Type Conslist
		{
			set
			{
				if (value == null)
				{
					conslist.Items.Clear();
					conslist.Items.AddRange(new System.Collections.ArrayList(new System.Object[]{}).ToArray());
					return ;
				}
				
				consList = getPublicConstructors(value.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly));
				conslist.Items.Clear();
				conslist.Items.AddRange(new System.Collections.ArrayList(parseConstructors(consList)).ToArray());
			}
			
		}
		virtual internal System.String Mlist
		{
			set
			{
				if (value == null)
				{
					mlist.Items.Clear();
					mlist.Items.AddRange(new System.Collections.ArrayList(new System.Object[]{}).ToArray());
					Conslist = null;
					ClassTree = null;
					return ;
				}
				
				System.Type clas;
				try
				{
					if (selectedPackage.Equals("<unpackaged>"))
						selectedClass = classManager.classForName(value);
					else
						selectedClass = classManager.classForName(selectedPackage + "." + value);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Error.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Error.WriteLine(e);
					return ;
				}
				if (selectedClass == null)
				{
					// not found?
					System.Console.Error.WriteLine("class not found: " + value);
					return ;
				}
				methodList = getPublicMethods(selectedClass.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static));
				mlist.Items.Clear();
				mlist.Items.AddRange(new System.Collections.ArrayList(parseMethods(methodList)).ToArray());
				
				ClassTree = selectedClass;
				Conslist = selectedClass;
				FieldList = selectedClass;
			}
			
		}
		virtual internal System.Type FieldList
		{
			set
			{
				if (value == null)
				{
					fieldlist.Items.Clear();
					fieldlist.Items.AddRange(new System.Collections.ArrayList(new System.Object[]{}).ToArray());
					return ;
				}
				
				fieldList = getPublicFields(value.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static));
				fieldlist.Items.Clear();
				fieldlist.Items.AddRange(new System.Collections.ArrayList(parseFields(fieldList)).ToArray());
			}
			
		}
		virtual internal System.Object MethodLine
		{
			set
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				methodLine.Text = value == null?"":value.ToString();
			}
			
		}
		virtual internal System.Type ClassTree
		{
			set
			{
				if (value == null)
				{
					SupportClass.TreeSupport.SetModel(tree, null);
					return ;
				}
				
				System.Windows.Forms.TreeNode bottom = null, top = null;
				System.Windows.Forms.TreeNode up;
				do 
				{
					up = new System.Windows.Forms.TreeNode(value.ToString().ToString());
					if (top != null)
						up.Nodes.Add(top);
					else
						bottom = up;
					top = up;
				}
				while ((value = value.BaseType) != null);
				//UPGRADE_TODO: Constructor 'javax.swing.tree.DefaultTreeModel.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelDefaultTreeModel_javaxswingtreeTreeNode'"
				SupportClass.TreeSupport.SetModel(tree, top);
				
				System.Windows.Forms.TreeNode tn = bottom.Parent;
				if (tn != null)
				{
					//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.tree.TreePath.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
					//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Windows.Forms.TreeNode generatedAux = ((System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(tree));
					//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.tree.DefaultTreeModel.getPathToRoot' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					TreePath tp = new TreePath(tn.FullPath);
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.expandPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreeexpandPath_javaxswingtreeTreePath'"
					tree.expandPath(tp);
				}
			}
			
		}
		internal BshClassPath classPath;
		internal BshClassManager classManager;
		
		// GUI
		internal System.Windows.Forms.Form frame;
		//UPGRADE_TODO: Class 'javax.swing.JInternalFrame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		internal System.Windows.Forms.Form iframe;
		internal System.Windows.Forms.ListBox classlist, conslist, mlist, fieldlist;
		internal PackageTree ptree;
		internal System.Windows.Forms.TextBox methodLine;
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		internal System.Windows.Forms.TreeView tree;
		// For JList models
		internal System.String[] packagesList;
		internal System.String[] classesList;
		internal System.Reflection.ConstructorInfo[] consList;
		internal System.Reflection.MethodInfo[] methodList;
		internal System.Reflection.FieldInfo[] fieldList;
		
		internal System.String selectedPackage;
		internal System.Type selectedClass;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'LIGHT_BLUE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Drawing.Color LIGHT_BLUE = System.Drawing.Color.FromArgb(245, 245, 255);
		
		public ClassBrowser():this(BshClassManager.createClassManager(null))
		{
		}
		
		//UPGRADE_TODO: Constructor 'javax.swing.JSplitPane.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJSplitPaneJSplitPane_int_boolean'"
		public ClassBrowser(BshClassManager classManager):base((int) System.Windows.Forms.Orientation.Vertical)
		{
			this.classManager = classManager;
			
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(CreateGraphics(), 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Flat);
			//UPGRADE_TODO: Class 'javax.swing.plaf.SplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanegetUI'"
			javax.swing.plaf.SplitPaneUI ui = getUI();
			//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			if (ui is javax.swing.plaf.basic.BasicSplitPaneUI)
			{
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneDivider.setBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneUI.getDivider' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				((javax.swing.plaf.basic.BasicSplitPaneUI) ui).getDivider().setBorder(null);
			}
		}
		
		internal virtual System.String[] toSortedStrings(System.Collections.ICollection c)
		{
			System.Collections.IList l = new System.Collections.ArrayList(c);
			System.String[] sa = (System.String[]) (SupportClass.ICollectionSupport.ToArray(l, new System.String[0]));
			return StringUtil.bubbleSort(sa);
		}
		
		internal virtual System.String[] parseConstructors(System.Reflection.ConstructorInfo[] constructors)
		{
			System.String[] sa = new System.String[constructors.Length];
			for (int i = 0; i < sa.Length; i++)
			{
				System.Reflection.ConstructorInfo con = constructors[i];
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Constructor.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				sa[i] = StringUtil.methodString(con.DeclaringType.ToString(), con.GetParameters());
			}
			//return bubbleSort(sa);
			return sa;
		}
		
		internal virtual System.String[] parseMethods(System.Reflection.MethodInfo[] methods)
		{
			System.String[] sa = new System.String[methods.Length];
			for (int i = 0; i < sa.Length; i++)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				sa[i] = StringUtil.methodString(methods[i].Name, methods[i].GetParameters());
			}
			//return bubbleSort(sa);
			return sa;
		}
		
		internal virtual System.String[] parseFields(System.Reflection.FieldInfo[] fields)
		{
			System.String[] sa = new System.String[fields.Length];
			for (int i = 0; i < sa.Length; i++)
			{
				System.Reflection.FieldInfo f = fields[i];
				sa[i] = f.Name;
			}
			return sa;
		}
		
		internal virtual System.Reflection.ConstructorInfo[] getPublicConstructors(System.Reflection.ConstructorInfo[] constructors)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			for (int i = 0; i < constructors.Length; i++)
				if (constructors[i].IsPublic)
					v.Add(constructors[i]);
			
			System.Reflection.ConstructorInfo[] ca = new System.Reflection.ConstructorInfo[v.Count];
			v.CopyTo(ca);
			return ca;
		}
		
		internal virtual System.Reflection.MethodInfo[] getPublicMethods(System.Reflection.MethodInfo[] methods)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			for (int i = 0; i < methods.Length; i++)
				if (methods[i].IsPublic)
					v.Add(methods[i]);
			
			System.Reflection.MethodInfo[] ma = new System.Reflection.MethodInfo[v.Count];
			v.CopyTo(ma);
			return ma;
		}
		
		internal virtual System.Reflection.FieldInfo[] getPublicFields(System.Reflection.FieldInfo[] fields)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			for (int i = 0; i < fields.Length; i++)
				if (fields[i].IsPublic)
					v.Add(fields[i]);
			
			System.Reflection.FieldInfo[] fa = new System.Reflection.FieldInfo[v.Count];
			v.CopyTo(fa);
			return fa;
		}
		
		internal virtual System.Windows.Forms.Panel labeledPane(System.Windows.Forms.Control comp, System.String label)
		{
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel jp = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			jp.Controls.Add(comp);
			System.Windows.Forms.Label temp_label2;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.SwingConstants.CENTER' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = label;
			temp_label2.ImageAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
			temp_label2.TextAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			jp.Controls.Add(temp_Control);
			return jp;
		}
		
		public virtual void  init()
		{
			// Currently we have to cast because BshClassPath is not known by
			// the core.
			classPath = ((ClassManagerImpl) classManager).getClassPath();
			
			// maybe add MappingFeedbackListener here... or let desktop if it has
			/*
			classPath.insureInitialized( null 
			// get feedback on mapping...
			new ConsoleInterface() {
			public Reader getIn() { return null; }
			public PrintStream getOut() { return System.out; }
			public PrintStream getErr() { return System.err; }
			public void println( String s ) { System.out.println(s); }
			public void print( String s ) { System.out.print(s); }
			public void print( String s, Color color ) { print( s ); }
			public void error( String s ) { print( s ); }
			}
			);
			*/
			
			classPath.addListener(this);
			
			SupportClass.SetSupport pset = classPath.PackagesSet;
			
			ptree = new PackageTree(this, pset);
			ptree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(new AnonymousClassTreeSelectionListener(this).valueChanged);
			
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			classlist = temp_ListBox;
			classlist.BackColor = LIGHT_BLUE;
			classlist.SelectedValueChanged += new System.EventHandler(this.valueChanged);
			
			System.Windows.Forms.ListBox temp_ListBox2;
			temp_ListBox2 = new System.Windows.Forms.ListBox();
			temp_ListBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			conslist = temp_ListBox2;
			conslist.SelectedValueChanged += new System.EventHandler(this.valueChanged);
			
			System.Windows.Forms.ListBox temp_ListBox3;
			temp_ListBox3 = new System.Windows.Forms.ListBox();
			temp_ListBox3.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			mlist = temp_ListBox3;
			mlist.BackColor = LIGHT_BLUE;
			mlist.SelectedValueChanged += new System.EventHandler(this.valueChanged);
			
			System.Windows.Forms.ListBox temp_ListBox4;
			temp_ListBox4 = new System.Windows.Forms.ListBox();
			temp_ListBox4.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			fieldlist = temp_ListBox4;
			fieldlist.SelectedValueChanged += new System.EventHandler(this.valueChanged);
			
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(conslist);
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			temp_scrollablecontrol2.Controls.Add(mlist);
			SupportClass.SplitterPanelSupport methodConsPane = splitPane((int) System.Windows.Forms.Orientation.Vertical, true, labeledPane(temp_scrollablecontrol, "Constructors"), labeledPane(temp_scrollablecontrol2, "Methods"));
			
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol3;
			temp_scrollablecontrol3 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol3.AutoScroll = true;
			temp_scrollablecontrol3.Controls.Add(fieldlist);
			SupportClass.SplitterPanelSupport rightPane = splitPane((int) System.Windows.Forms.Orientation.Vertical, true, methodConsPane, labeledPane(temp_scrollablecontrol3, "Fields"));
			
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol4;
			temp_scrollablecontrol4 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol4.AutoScroll = true;
			temp_scrollablecontrol4.Controls.Add(classlist);
			SupportClass.SplitterPanelSupport sp = splitPane((int) System.Windows.Forms.Orientation.Horizontal, true, labeledPane(temp_scrollablecontrol4, "Classes"), rightPane);
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol5;
			temp_scrollablecontrol5 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol5.AutoScroll = true;
			temp_scrollablecontrol5.Controls.Add(ptree);
			sp = splitPane((int) System.Windows.Forms.Orientation.Horizontal, true, labeledPane(temp_scrollablecontrol5, "Packages"), sp);
			
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel bottompanel = new System.Windows.Forms.Panel();
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			methodLine = temp_TextBox;
			methodLine.BackColor = LIGHT_BLUE;
			methodLine.ReadOnly = !false;
			methodLine.WordWrap = true;
			methodLine.WordWrap = true;
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			methodLine.Font = new System.Drawing.Font("Monospaced", 14, System.Drawing.FontStyle.Bold);
			//UPGRADE_ISSUE: Method 'javax.swing.text.JTextComponent.setMargin' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextJTextComponentsetMargin_javaawtInsets'"
			methodLine.setMargin(new System.Int32[]{5, 5, 5, 5});
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.MatteBorder.MatteBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderMatteBorder'"
			System.Drawing.Color temp_Color2;
			temp_Color2 = LIGHT_BLUE;
			System.Drawing.Color temp_Color;
			temp_Color = System.Drawing.Color.FromArgb(System.Convert.ToInt32(temp_Color2.R * 0.7f), System.Convert.ToInt32(temp_Color2.G * 0.7f), System.Convert.ToInt32(temp_Color2.B * 0.7f));
			System.Windows.Forms.ControlPaint.DrawBorder3D(methodLine.CreateGraphics(), 0, 0, methodLine.Width, methodLine.Height, new MatteBorder(1, 0, 1, 0, System.Drawing.Color.FromArgb(System.Convert.ToInt32(temp_Color.R * 0.7f), System.Convert.ToInt32(temp_Color.G * 0.7f), System.Convert.ToInt32(temp_Color.B * 0.7f))));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			bottompanel.Controls.Add(methodLine);
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			tree = new System.Windows.Forms.TreeView();
			tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(new AnonymousClassTreeSelectionListener1(this).valueChanged);
			
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(tree.CreateGraphics(), 0, 0, tree.Width, tree.Height, System.Windows.Forms.Border3DStyle.Raised);
			ClassTree = null;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			p.Controls.Add(tree);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			bottompanel.Controls.Add(p);
			
			// give it a preferred height
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			bottompanel.Size = new System.Drawing.Size(150, 150);
			
			FirstControl = sp;
			SecondControl = bottompanel;
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private SupportClass.SplitterPanelSupport splitPane(int orientation, bool redraw, System.Windows.Forms.Control c1, System.Windows.Forms.Control c2)
		{
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JSplitPane.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJSplitPaneJSplitPane_int_boolean_javaawtComponent_javaawtComponent'"
			SupportClass.SplitterPanelSupport sp = new SupportClass.SplitterPanelSupport(orientation, c1, c2);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(sp.CreateGraphics(), 0, 0, sp.Width, sp.Height, System.Windows.Forms.Border3DStyle.Flat);
			//UPGRADE_TODO: Class 'javax.swing.plaf.SplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanegetUI'"
			javax.swing.plaf.SplitPaneUI ui = sp.getUI();
			//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			if (ui is javax.swing.plaf.basic.BasicSplitPaneUI)
			{
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneDivider.setBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneUI.getDivider' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				((javax.swing.plaf.basic.BasicSplitPaneUI) ui).getDivider().setBorder(null);
			}
			return sp;
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			ClassBrowser cb = new ClassBrowser();
			cb.init();
			
			System.Windows.Forms.Form f = SupportClass.FormSupport.CreateForm("BeanShell Class Browser v1.0");
			//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			((System.Windows.Forms.ContainerControl) f).Controls.Add(cb);
			cb.setFrame(f);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
		
		public virtual void  setFrame(System.Windows.Forms.Form frame)
		{
			this.frame = frame;
		}
		//UPGRADE_TODO: Class 'javax.swing.JInternalFrame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual void  setFrame(System.Windows.Forms.Form frame)
		{
			this.iframe = frame;
		}
		
		public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
			if (e.getSource() == classlist)
			{
				System.String classname = (System.String) classlist.SelectedItem;
				Mlist = classname;
				
				// hack
				// show the class source in the "method" line...
				System.String methodLineString;
				if (classname == null)
					methodLineString = "Package: " + selectedPackage;
				else
				{
					System.String fullClassName = selectedPackage.Equals("<unpackaged>")?classname:selectedPackage + "." + classname;
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					methodLineString = fullClassName + " (from " + classPath.getClassSource(fullClassName) + ")";
				}
				
				MethodLine = methodLineString;
			}
			else
			{
				//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
				if (e.getSource() == mlist)
				{
					int i = mlist.SelectedIndex;
					if (i == - 1)
						MethodLine = null;
					else
						MethodLine = methodList[i];
				}
				else
				{
					//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
					if (e.getSource() == conslist)
					{
						int i = conslist.SelectedIndex;
						if (i == - 1)
							MethodLine = null;
						else
							MethodLine = consList[i];
					}
					else
					{
						//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
						if (e.getSource() == fieldlist)
						{
							int i = fieldlist.SelectedIndex;
							if (i == - 1)
								MethodLine = null;
							else
								MethodLine = fieldList[i];
						}
					}
				}
			}
		}
		
		// fully qualified classname
		public virtual void  driveToClass(System.String classname)
		{
			System.String[] sa = BshClassPath.splitClassname(classname);
			System.String packn = sa[0];
			System.String classn = sa[1];
			
			// Do we have the package?
			if (classPath.getClassesForPackage(packn).Count == 0)
				return ;
			
			ptree.SelectedPackage = packn;
			
			for (int i = 0; i < classesList.Length; i++)
			{
				if (classesList[i].Equals(classn))
				{
					//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
					classlist.SelectedIndex = i;
					//UPGRADE_ISSUE: Method 'javax.swing.JList.ensureIndexIsVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListensureIndexIsVisible_int'"
					classlist.ensureIndexIsVisible(i);
					break;
				}
			}
		}
		
		public virtual void  toFront()
		{
			if (frame != null)
				frame.BringToFront();
			else if (iframe != null)
				iframe.BringToFront();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PackageTree' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		internal class PackageTree:System.Windows.Forms.TreeView
		{
			private void  InitBlock(ClassBrowser enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ClassBrowser enclosingInstance;
			virtual public System.Collections.ICollection Packages
			{
				set
				{
					treeModel = makeTreeModel(value);
					SupportClass.TreeSupport.SetModel(this, treeModel);
				}
				
			}
			virtual internal System.String SelectedPackage
			{
				set
				{
					System.Windows.Forms.TreeNode node = (System.Windows.Forms.TreeNode) nodeForPackage[value];
					if (node == null)
						return ;
					
					//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.tree.TreePath.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.tree.DefaultTreeModel.getPathToRoot' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					TreePath tp = new TreePath(node.FullPath);
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.setSelectionPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetSelectionPath_javaxswingtreeTreePath'"
					setSelectionPath(tp);
					Enclosing_Instance.Clist = value;
					
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.scrollPathToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreescrollPathToVisible_javaxswingtreeTreePath'"
					scrollPathToVisible(tp);
				}
				
			}
			public ClassBrowser Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal System.Windows.Forms.TreeNode root;
			//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Windows.Forms.TreeNode treeModel;
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			internal System.Collections.IDictionary nodeForPackage = new System.Collections.Hashtable();
			
			internal PackageTree(ClassBrowser enclosingInstance, System.Collections.ICollection packages)
			{
				InitBlock(enclosingInstance);
				Packages = packages;
				
				//UPGRADE_ISSUE: Method 'javax.swing.JTree.setRootVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetRootVisible_boolean'"
				setRootVisible(false);
				ShowRootLines = true;
				//UPGRADE_ISSUE: Method 'javax.swing.JTree.setExpandsSelectedPaths' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetExpandsSelectedPaths_boolean'"
				setExpandsSelectedPaths(true);
				
				// open top level paths
				/*
				Enumeration e1=root.children();
				while( e1.hasMoreElements() ) {
				TreePath tp = new TreePath( 
				treeModel.getPathToRoot( (TreeNode)e1.nextElement() ) );
				expandPath( tp );
				}
				*/
			}
			
			//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal virtual System.Windows.Forms.TreeNode makeTreeModel(System.Collections.ICollection packages)
			{
				//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
				System.Collections.IDictionary packageTree = new System.Collections.Hashtable();
				
				System.Collections.IEnumerator it = packages.GetEnumerator();
				//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
				while (it.MoveNext())
				{
					//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
					System.String pack = (System.String) (it.Current);
					System.String[] sa = StringUtil.split(pack, ".");
					System.Collections.IDictionary level = packageTree;
					for (int i = 0; i < sa.Length; i++)
					{
						System.String name = sa[i];
						System.Collections.IDictionary map = (System.Collections.IDictionary) level[name];
						
						if (map == null)
						{
							//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
							map = new System.Collections.Hashtable();
							level[name] = map;
						}
						level = map;
					}
				}
				
				root = makeNode(packageTree, "root");
				mapNodes(root);
				//UPGRADE_TODO: Constructor 'javax.swing.tree.DefaultTreeModel.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelDefaultTreeModel_javaxswingtreeTreeNode'"
				return root;
			}
			
			
			internal virtual System.Windows.Forms.TreeNode makeNode(System.Collections.IDictionary map, System.String nodeName)
			{
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode(nodeName.ToString());
				//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
				System.Collections.IEnumerator it = new SupportClass.HashSetSupport(map.Keys).GetEnumerator();
				//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
				while (it.MoveNext())
				{
					//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
					System.String name = (System.String) it.Current;
					System.Collections.IDictionary val = (System.Collections.IDictionary) map[name];
					if (val.Count == 0)
					{
						System.Windows.Forms.TreeNode leaf = new System.Windows.Forms.TreeNode(name.ToString());
						root.Nodes.Add(leaf);
					}
					else
					{
						System.Windows.Forms.TreeNode node = makeNode(val, name);
						root.Nodes.Add(node);
					}
				}
				return root;
			}
			
			/// <summary>Map out the location of the nodes by package name.
			/// Seems like we should be able to do this while we build above...
			/// I'm tired... just going to do this.
			/// </summary>
			internal virtual void  mapNodes(System.Windows.Forms.TreeNode node)
			{
				addNodeMap(node);
				
				System.Collections.IEnumerator e = node.Nodes.GetEnumerator();
				//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
				while (e.MoveNext())
				{
					//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
					System.Windows.Forms.TreeNode tn = (System.Windows.Forms.TreeNode) e.Current;
					mapNodes(tn);
				}
			}
			
			/// <summary>map a single node up to the root</summary>
			internal virtual void  addNodeMap(System.Windows.Forms.TreeNode node)
			{
				
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				System.Windows.Forms.TreeNode tn = node;
				while (tn != root)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					sb.Insert(0, tn.ToString());
					if (tn.Parent != root)
						sb.Insert(0, ".");
					tn = tn.Parent;
				}
				System.String pack = sb.ToString();
				
				nodeForPackage[pack] = node;
			}
		}
		
		public virtual void  classPathChanged()
		{
			SupportClass.SetSupport pset = classPath.PackagesSet;
			ptree.Packages = pset;
			Clist = null;
		}
	}
}