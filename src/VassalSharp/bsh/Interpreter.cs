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
using System.Runtime.InteropServices;
namespace bsh
{
	
	/// <summary>The BeanShell script interpreter.
	/// An instance of Interpreter can be used to source scripts and evaluate 
	/// statements or expressions.  
	/// <p>
	/// Here are some examples:
	/// <p><blockquote><pre>
	/// Interpeter bsh = new Interpreter();
	/// // Evaluate statements and expressions
	/// bsh.eval("foo=Math.sin(0.5)");
	/// bsh.eval("bar=foo*5; bar=Math.cos(bar);");
	/// bsh.eval("for(i=0; i<10; i++) { print(\"hello\"); }");
	/// // same as above using java syntax and apis only
	/// bsh.eval("for(int i=0; i<10; i++) { System.out.println(\"hello\"); }");
	/// // Source from files or streams
	/// bsh.source("myscript.bsh");  // or bsh.eval("source(\"myscript.bsh\")");
	/// // Use set() and get() to pass objects in and out of variables
	/// bsh.set( "date", new Date() );
	/// Date date = (Date)bsh.get( "date" );
	/// // This would also work:
	/// Date date = (Date)bsh.eval( "date" );
	/// bsh.eval("year = date.getYear()");
	/// Integer year = (Integer)bsh.get("year");  // primitives use wrappers
	/// // With Java1.3+ scripts can implement arbitrary interfaces...
	/// // Script an awt event handler (or source it from a file, more likely)
	/// bsh.eval( "actionPerformed( e ) { print( e ); }");
	/// // Get a reference to the script object (implementing the interface)
	/// ActionListener scriptedHandler = 
	/// (ActionListener)bsh.eval("return (ActionListener)this");
	/// // Use the scripted event handler normally...
	/// new JButton.addActionListener( script );
	/// </pre></blockquote>
	/// <p>
	/// In the above examples we showed a single interpreter instance, however 
	/// you may wish to use many instances, depending on the application and how
	/// you structure your scripts.  Interpreter instances are very light weight
	/// to create, however if you are going to execute the same script repeatedly
	/// and require maximum performance you should consider scripting the code as 
	/// a method and invoking the scripted method each time on the same interpreter
	/// instance (using eval()). 
	/// <p>
	/// See the BeanShell User's Manual for more information.
	/// </summary>
	[Serializable]
	public class Interpreter : ConsoleInterface, System.Runtime.Serialization.ISerializable, IThreadRunnable
	{
		private class AnonymousClassFilterInputStream:System.IO.BinaryReader
		{
			internal AnonymousClassFilterInputStream(System.IO.Stream Param1):base(Param1)
			{
			}
			//UPGRADE_NOTE: The equivalent of method 'java.io.FilterInputStream.available' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public int available()
			{
				return 0;
			}
		}
		/// <summary>Attach a console
		/// Note: this method is incomplete.
		/// </summary>
		virtual public ConsoleInterface Console
		{
			set
			{
				this.console = value;
				setu("bsh.console", value);
				// redundant with constructor
				setOut(value.getOut());
				setErr(value.getErr());
				// need to set the input stream - reinit the parser?
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>Get the global namespace of this interpreter.
		/// <p>
		/// Note: This is here for completeness.  If you're using this a lot 
		/// it may be an indication that you are doing more work than you have 
		/// to.  For example, caching the interpreter instance rather than the 
		/// namespace should not add a significant overhead.  No state other than 
		/// the debug status is stored in the interpreter.  
		/// <p>
		/// All features of the namespace can also be accessed using the 
		/// interpreter via eval() and the script variable 'this.namespace'
		/// (or global.namespace as necessary).
		/// </summary>
		/// <summary>Set the global namespace for this interpreter.
		/// <p>
		/// Note: This is here for completeness.  If you're using this a lot 
		/// it may be an indication that you are doing more work than you have 
		/// to.  For example, caching the interpreter instance rather than the 
		/// namespace should not add a significant overhead.  No state other 
		/// than the debug status is stored in the interpreter.
		/// <p>
		/// All features of the namespace can also be accessed using the 
		/// interpreter via eval() and the script variable 'this.namespace'
		/// (or global.namespace as necessary).
		/// </summary>
		virtual public NameSpace NameSpace
		{
			get
			{
				return globalNameSpace;
			}
			
			set
			{
				this.globalNameSpace = value;
			}
			
		}
		/// <summary>Get the input stream associated with this interpreter.
		/// This may be be stdin or the GUI console.
		/// </summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.IO.StreamReader In
		{
			get
			{
				return in_Renamed;
			}
			
		}
		/// <summary>Set an external class loader to be used as the base classloader
		/// for BeanShell.  The base classloader is used for all classloading 
		/// unless/until the addClasspath()/setClasspath()/reloadClasses()  
		/// commands are called to modify the interpreter's classpath.  At that 
		/// time the new paths /updated paths are added on top of the base 
		/// classloader.
		/// <p>
		/// BeanShell will use this at the same point it would otherwise use the 
		/// plain Class.forName().
		/// i.e. if no explicit classpath management is done from the script
		/// (addClassPath(), setClassPath(), reloadClasses()) then BeanShell will
		/// only use the supplied classloader.  If additional classpath management
		/// is done then BeanShell will perform that in addition to the supplied
		/// external classloader.  
		/// However BeanShell is not currently able to reload
		/// classes supplied through the external classloader.
		/// <p>
		/// </summary>
		/// <seealso cref="BshClassManager.setClassLoader( ClassLoader )">
		/// </seealso>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		virtual public ClassLoader ClassLoader
		{
			set
			{
				ClassManager.ClassLoader = value;
			}
			
		}
		/// <summary>Get the class manager associated with this interpreter
		/// (the BshClassManager of this interpreter's global namespace).
		/// This is primarily a convenience method.
		/// </summary>
		virtual public BshClassManager ClassManager
		{
			get
			{
				return NameSpace.getClassManager();
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <seealso cref="setStrictJava( boolean )">
		/// </seealso>
		/// <summary>Set strict Java mode on or off.  
		/// This mode attempts to make BeanShell syntax behave as Java
		/// syntax, eliminating conveniences like loose variables, etc.
		/// When enabled, variables are required to be declared or initialized 
		/// before use and method arguments are reqired to have types. 
		/// <p>
		/// This mode will become more strict in a future release when 
		/// classes are interpreted and there is an alternative to scripting
		/// objects as method closures.
		/// </summary>
		virtual public bool StrictJava
		{
			get
			{
				return this.strictJava;
			}
			
			set
			{
				this.strictJava = value;
			}
			
		}
		/// <summary>Specify the source of the text from which this interpreter is reading.
		/// Note: there is a difference between what file the interrpeter is 
		/// sourcing and from what file a method was originally parsed.  One
		/// file may call a method sourced from another file.  See SimpleNode
		/// for origination file info.
		/// </summary>
		/// <seealso cref="bsh.SimpleNode.getSourceFile()">
		/// </seealso>
		virtual public System.String SourceFileInfo
		{
			get
			{
				if (sourceFileInfo != null)
					return sourceFileInfo;
				else
					return "<unknown source>";
			}
			
		}
		/// <summary>Get the parent Interpreter of this interpreter, if any.
		/// Currently this relationship implies the following:
		/// 1) Parent and child share a BshClassManager
		/// 2) Children indicate the parent's source file information in error
		/// reporting.
		/// When created as part of a source() / eval() the child also shares
		/// the parent's namespace.  But that is not necessary in general.
		/// </summary>
		virtual public Interpreter Parent
		{
			get
			{
				return parent;
			}
			
		}
		/// <summary>Get the prompt string defined by the getBshPrompt() method in the
		/// global namespace.  This may be from the getBshPrompt() command or may
		/// be defined by the user as with any other method.
		/// Defaults to "bsh % " if the method is not defined or there is an error.
		/// </summary>
		private System.String BshPrompt
		{
			get
			{
				try
				{
					return (System.String) eval("getBshPrompt()");
				}
				catch (System.Exception e)
				{
					return "bsh % ";
				}
			}
			
		}
		/// <summary>Specify whether, in interactive mode, the interpreter exits Java upon
		/// end of input.  If true, when in interactive mode the interpreter will
		/// issue a System.exit(0) upon eof.  If false the interpreter no
		/// System.exit() will be done.
		/// <p/>
		/// Note: if you wish to cause an EOF externally you can try closing the
		/// input stream.  This is not guaranteed to work in older versions of Java
		/// due to Java limitations, but should work in newer JDK/JREs.  (That was
		/// the motivation for the Java NIO package).
		/// </summary>
		virtual public bool ExitOnEOF
		{
			set
			{
				exitOnEOF = value; // ug
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>Show on/off verbose printing status for the show() command.
		/// See the BeanShell show() command.
		/// If this interpreter has a parent the call is delegated.
		/// </summary>
		/// <summary>Turn on/off the verbose printing of results as for the show()
		/// command.
		/// If this interpreter has a parent the call is delegated.
		/// See the BeanShell show() command.
		/// </summary>
		virtual public bool ShowResults
		{
			get
			{
				return showResults;
			}
			
			set
			{
				this.showResults = value;
			}
			
		}
		/* --- Begin static members --- */
		
		public const System.String VERSION = "2.0b4";
		/*
		Debug utils are static so that they are reachable by code that doesn't
		necessarily have an interpreter reference (e.g. tracing in utils).
		In the future we may want to allow debug/trace to be turned on on
		a per interpreter basis, in which case we'll need to use the parent 
		reference in some way to determine the scope of the command that 
		turns it on or off.
		*/
		public static bool DEBUG;
		public static bool TRACE;
		public static bool LOCALSCOPING;
		
		// This should be per instance
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[NonSerialized]
		internal static System.IO.StreamWriter debug_Renamed_Field;
		internal static System.String systemLineSeparator = "\n"; // default
		
		/// <summary>Shared system object visible under bsh.system </summary>
		internal static This sharedObject;
		
		/// <summary>Strict Java mode </summary>
		/// <seealso cref="setStrictJava( boolean )">
		/// </seealso>
		private bool strictJava = false;
		
		/* --- End static members --- */
		
		/* --- Instance data --- */
		
		[NonSerialized]
		internal Parser parser;
		internal NameSpace globalNameSpace;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[NonSerialized]
		internal System.IO.StreamReader in_Renamed;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[NonSerialized]
		internal System.IO.StreamWriter out_Renamed;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[NonSerialized]
		internal System.IO.StreamWriter err;
		internal ConsoleInterface console;
		
		/// <summary>If this interpeter is a child of another, the parent </summary>
		internal Interpreter parent;
		
		/// <summary>The name of the file or other source that this interpreter is reading </summary>
		internal System.String sourceFileInfo;
		
		/// <summary>by default in interactive mode System.exit() on EOF </summary>
		private bool exitOnEOF = true;
		
		protected internal bool evalOnly, interactive; // Interpreter has a user, print prompts, etc.
		
		/// <summary>Control the verbose printing of results for the show() command. </summary>
		private bool showResults;
		
		/* --- End instance data --- */
		
		/// <summary>The main constructor.
		/// All constructors should now pass through here.
		/// </summary>
		/// <param name="namespace">If namespace is non-null then this interpreter's 
		/// root namespace will be set to the one provided.  If it is null a new 
		/// one will be created for it.
		/// </param>
		/// <param name="parent">The parent interpreter if this interpreter is a child 
		/// of another.  May be null.  Children share a BshClassManager with
		/// their parent instance.
		/// </param>
		/// <param name="sourceFileInfo">An informative string holding the filename 
		/// or other description of the source from which this interpreter is
		/// reading... used for debugging.  May be null.
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public Interpreter(System.IO.StreamReader in_Renamed, System.IO.StreamWriter out_Renamed, System.IO.StreamWriter err, bool interactive, NameSpace namespace_Renamed, Interpreter parent, System.String sourceFileInfo)
		{
			//System.out.println("New Interpreter: "+this +", sourcefile = "+sourceFileInfo );
			parser = new Parser(in_Renamed);
			long t1 = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			this.in_Renamed = in_Renamed;
			this.out_Renamed = out_Renamed;
			this.err = err;
			this.interactive = interactive;
			debug_Renamed_Field = err;
			this.parent = parent;
			if (parent != null)
				StrictJava = parent.StrictJava;
			this.sourceFileInfo = sourceFileInfo;
			
			BshClassManager bcm = BshClassManager.createClassManager(this);
			if (namespace_Renamed == null)
				this.globalNameSpace = new NameSpace(bcm, "global");
			else
				this.globalNameSpace = namespace_Renamed;
			
			// now done in NameSpace automatically when root
			// The classes which are imported by default
			//globalNameSpace.loadDefaultImports();
			
			/* 
			Create the root "bsh" system object if it doesn't exist.
			*/
			if (!(getu("bsh") is bsh.This))
				initRootSystemObject();
			
			if (interactive)
				loadRCFiles();
			
			long t2 = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			if (Interpreter.DEBUG)
				Interpreter.debug("Time to initialize interpreter: " + (t2 - t1));
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public Interpreter(System.IO.StreamReader in_Renamed, System.IO.StreamWriter out_Renamed, System.IO.StreamWriter err, bool interactive, NameSpace namespace_Renamed):this(in_Renamed, out_Renamed, err, interactive, namespace_Renamed, null, null)
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public Interpreter(System.IO.StreamReader in_Renamed, System.IO.StreamWriter out_Renamed, System.IO.StreamWriter err, bool interactive):this(in_Renamed, out_Renamed, err, interactive, null)
		{
		}
		
		/// <summary>Construct a new interactive interpreter attached to the specified 
		/// console using the specified parent namespace.
		/// </summary>
		public Interpreter(ConsoleInterface console, NameSpace globalNameSpace):this(console.In, console.getOut(), console.getErr(), true, globalNameSpace)
		{
			
			Console = console;
		}
		
		/// <summary>Construct a new interactive interpreter attached to the specified 
		/// console.
		/// </summary>
		public Interpreter(ConsoleInterface console):this(console, null)
		{
		}
		
		/// <summary>Create an interpreter for evaluation only.</summary>
		public Interpreter():this(new System.IO.StringReader(""), temp_writer, temp_writer2, false, null)
		{
			System.IO.StreamWriter temp_writer;
			temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardOutput(), System.Console.Out.Encoding);
			temp_writer.AutoFlush = true;
			System.IO.StreamWriter temp_writer2;
			temp_writer2 = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
			temp_writer2.AutoFlush = true;
			evalOnly = true;
			setu("bsh.evalOnly", new Primitive(true));
		}
		
		// End constructors
		
		private void  initRootSystemObject()
		{
			BshClassManager bcm = ClassManager;
			// bsh
			setu("bsh", new NameSpace(bcm, "Bsh Object").getThis(this));
			
			// init the static shared sharedObject if it's not there yet
			if (sharedObject == null)
				sharedObject = new NameSpace(bcm, "Bsh Shared System Object").getThis(this);
			// bsh.system
			setu("bsh.system", sharedObject);
			setu("bsh.shared", sharedObject); // alias
			
			// bsh.help
			This helpText = new NameSpace(bcm, "Bsh Command Help Text").getThis(this);
			setu("bsh.help", helpText);
			
			// bsh.cwd
			try
			{
				//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.CurrentDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
				setu("bsh.cwd", System.Environment.CurrentDirectory);
			}
			catch (System.Security.SecurityException e)
			{
				// applets can't see sys props
				setu("bsh.cwd", ".");
			}
			
			// bsh.interactive
			setu("bsh.interactive", new Primitive(interactive));
			// bsh.evalOnly
			setu("bsh.evalOnly", new Primitive(evalOnly));
		}
		
		/// <summary>Run the text only interpreter on the command line or specify a file.</summary>
		[STAThread]
		public static void  Main(System.String[] args)
		{
			if (args.Length > 0)
			{
				System.String filename = args[0];
				
				System.String[] bshArgs;
				if (args.Length > 1)
				{
					bshArgs = new System.String[args.Length - 1];
					Array.Copy(args, 1, bshArgs, 0, args.Length - 1);
				}
				else
					bshArgs = new System.String[0];
				
				Interpreter interpreter = new Interpreter();
				//System.out.println("run i = "+interpreter);
				interpreter.setu("bsh.args", bshArgs);
				try
				{
					System.Object result = interpreter.source(filename, interpreter.globalNameSpace);
					if (result is System.Type)
						try
						{
							invokeMain((System.Type) result, bshArgs);
						}
						catch (System.Exception e)
						{
							System.Object o = e;
							if (e is System.Reflection.TargetInvocationException)
								o = ((System.Reflection.TargetInvocationException) e).GetBaseException();
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							System.Console.Error.WriteLine("Class: " + result + " main method threw exception:" + o);
						}
				}
				catch (System.IO.FileNotFoundException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine("File not found: " + e);
				}
				catch (TargetError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine("Script threw exception: " + e);
					if (e.inNativeCode())
					{
						System.IO.StreamWriter temp_writer;
						temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
						temp_writer.AutoFlush = true;
						e.printStackTrace(DEBUG, temp_writer);
					}
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine("Evaluation Error: " + e);
				}
				catch (System.IO.IOException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine("I/O Error: " + e);
				}
			}
			else
			{
				// Workaround for JDK bug 4071281, where system.in.available() 
				// returns too large a value. This bug has been fixed in JDK 1.2.
				System.IO.Stream src;
				//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
				//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				if (System.Environment.GetEnvironmentVariable("OS").StartsWith("Windows") && System_Renamed.getProperty("java.version").StartsWith("1.1."))
				{
					src = new AnonymousClassFilterInputStream(System.Console.OpenStandardInput());
				}
				else
					src = System.Console.OpenStandardInput();
				
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.IO.StreamReader in_Renamed = new CommandLineReader(new System.IO.StreamReader(src, System.Text.Encoding.Default));
				System.IO.StreamWriter temp_writer;
				temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardOutput(), System.Console.Out.Encoding);
				temp_writer.AutoFlush = true;
				System.IO.StreamWriter temp_writer2;
				temp_writer2 = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
				temp_writer2.AutoFlush = true;
				Interpreter interpreter = new Interpreter(in_Renamed, temp_writer, temp_writer2, true);
				interpreter.Run();
			}
		}
		
		public static void  invokeMain(System.Type clas, System.String[] args)
		{
			System.Reflection.MethodInfo main = Reflect.resolveJavaMethod(null, clas, "main", new System.Type[]{typeof(System.String[])}, true);
			if (main != null)
				main.Invoke(null, new System.Object[]{args});
		}
		
		/// <summary>Run interactively.  (printing prompts, etc.)</summary>
		public virtual void  Run()
		{
			if (evalOnly)
				throw new System.SystemException("bsh Interpreter: No stream");
			
			/*
			We'll print our banner using eval(String) in order to
			exercise the parser and get the basic expression classes loaded...
			This ameliorates the delay after typing the first statement.
			*/
			if (interactive)
				try
				{
					eval("printBanner();");
				}
				catch (EvalError e)
				{
					println("BeanShell " + VERSION + " - by Pat Niemeyer (pat@pat.net)");
				}
			
			// init the callstack.  
			CallStack callstack = new CallStack(globalNameSpace);
			
			bool eof = false;
			while (!eof)
			{
				try
				{
					// try to sync up the console
					System.Console.Out.Flush();
					System.Console.Error.Flush();
					System.Threading.Thread.Sleep(0); // this helps a little
					
					if (interactive)
						print(BshPrompt);
					
					eof = Line();
					
					if (get_jjtree().nodeArity() > 0)
					// number of child nodes 
					{
						SimpleNode node = (SimpleNode) (get_jjtree().rootNode());
						
						if (DEBUG)
							node.dump(">");
						
						System.Object ret = node.eval(callstack, this);
						
						// sanity check during development
						if (callstack.depth() > 1)
						{
							throw new InterpreterError("Callstack growing: " + callstack);
						}
						
						if (ret is ReturnControl)
							ret = ((ReturnControl) ret).value_Renamed;
						
						if (ret != Primitive.VOID)
						{
							setu("$_", ret);
							if (showResults)
							{
								//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
								println("<" + ret + ">");
							}
						}
					}
				}
				catch (ParseException e)
				{
					error("Parser Error: " + e.getMessage(DEBUG));
					if (DEBUG)
						SupportClass.WriteStackTrace(e, Console.Error);
					if (!interactive)
						eof = true;
					
					parser.reInitInput(in_Renamed);
				}
				catch (InterpreterError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					error("Internal Error: " + e.Message);
					SupportClass.WriteStackTrace(e, Console.Error);
					if (!interactive)
						eof = true;
				}
				catch (TargetError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					error("// Uncaught Exception: " + e);
					if (e.inNativeCode())
						e.printStackTrace(DEBUG, err);
					if (!interactive)
						eof = true;
					setu("$_e", e.Target);
				}
				catch (EvalError e)
				{
					if (interactive)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						error("EvalError: " + e.ToString());
					}
					else
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						error("EvalError: " + e.Message);
					}
					
					if (DEBUG)
						SupportClass.WriteStackTrace(e, Console.Error);
					
					if (!interactive)
						eof = true;
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					error("Unknown error: " + e);
					if (DEBUG)
						if (e is bsh.TargetError)
							((bsh.TargetError) e).printStackTrace();
						else
							SupportClass.WriteStackTrace(e, Console.Error);
					if (!interactive)
						eof = true;
				}
				catch (TokenMgrError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					error("Error parsing input: " + e);
					
					/*
					We get stuck in infinite loops here when unicode escapes
					fail.  Must re-init the char stream reader 
					(ASCII_UCodeESC_CharStream.java)
					*/
					parser.reInitTokenInput(in_Renamed);
					
					if (!interactive)
						eof = true;
				}
				finally
				{
					get_jjtree().reset();
					// reinit the callstack
					if (callstack.depth() > 1)
					{
						callstack.clear();
						callstack.push(globalNameSpace);
					}
				}
			}
			
			if (interactive && exitOnEOF)
				System.Environment.Exit(0);
		}
		
		// begin source and eval
		
		/// <summary>Read text from fileName and eval it.</summary>
		public virtual System.Object source(System.String filename, NameSpace nameSpace)
		{
			System.IO.FileInfo file = pathToFile(filename);
			if (Interpreter.DEBUG)
				debug("Sourcing file: " + file);
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.IO.StreamReader sourceIn = new System.IO.StreamReader(new System.IO.StreamReader(file.FullName, System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(file.FullName, System.Text.Encoding.Default).CurrentEncoding);
			try
			{
				return eval(sourceIn, nameSpace, filename);
			}
			finally
			{
				sourceIn.Close();
			}
		}
		
		/// <summary>Read text from fileName and eval it.
		/// Convenience method.  Use the global namespace.
		/// </summary>
		public virtual System.Object source(System.String filename)
		{
			return source(filename, globalNameSpace);
		}
		
		/// <summary>Spawn a non-interactive local interpreter to evaluate text in the 
		/// specified namespace.  
		/// Return value is the evaluated object (or corresponding primitive 
		/// wrapper).
		/// </summary>
		/// <param name="sourceFileInfo">is for information purposes only.  It is used to
		/// display error messages (and in the future may be made available to
		/// the script).
		/// </param>
		/// <throws>  EvalError on script problems </throws>
		/// <throws>  TargetError on unhandled exceptions from the script </throws>
		/*
		Note: we need a form of eval that passes the callstack through...
		*/
		/*
		Can't this be combined with run() ?
		run seems to have stuff in it for interactive vs. non-interactive...
		compare them side by side and see what they do differently, aside from the
		exception handling.
		*/
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.Object eval(System.IO.StreamReader in_Renamed, NameSpace nameSpace, System.String sourceFileInfo)
		{
			System.Object retVal = null;
			if (Interpreter.DEBUG)
			{
				debug("eval: nameSpace = " + nameSpace);
			}
			
			/* 
			Create non-interactive local interpreter for this namespace
			with source from the input stream and out/err same as 
			this interpreter.
			*/
			Interpreter localInterpreter = new Interpreter(in_Renamed, out_Renamed, err, false, nameSpace, this, sourceFileInfo);
			
			CallStack callstack = new CallStack(nameSpace);
			
			bool eof = false;
			while (!eof)
			{
				SimpleNode node = null;
				try
				{
					eof = localInterpreter.Line();
					if (localInterpreter.get_jjtree().nodeArity() > 0)
					{
						node = (SimpleNode) localInterpreter.get_jjtree().rootNode();
						// nodes remember from where they were sourced
						node.setSourceFile(sourceFileInfo);
						
						if (TRACE)
							println("// " + node.Text);
						
						retVal = node.eval(callstack, localInterpreter);
						
						// sanity check during development
						if (callstack.depth() > 1)
						{
							throw new InterpreterError("Callstack growing: " + callstack);
						}
						
						if (retVal is ReturnControl)
						{
							retVal = ((ReturnControl) retVal).value_Renamed;
							break; // non-interactive, return control now
						}
						
						if (localInterpreter.showResults && retVal != Primitive.VOID)
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							println("<" + retVal + ">");
						}
					}
				}
				catch (ParseException e)
				{
					/*
					throw new EvalError(
					"Sourced file: "+sourceFileInfo+" parser Error: " 
					+ e.getMessage( DEBUG ), node, callstack );
					*/
					if (DEBUG)
					// show extra "expecting..." info
						error(e.getMessage(DEBUG));
					
					// add the source file info and throw again
					e.setErrorSourceFile(sourceFileInfo);
					throw e;
				}
				catch (InterpreterError e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new EvalError("Sourced file: " + sourceFileInfo + " internal Error: " + e.Message, node, callstack);
				}
				catch (TargetError e)
				{
					// failsafe, set the Line as the origin of the error.
					if (e.Node == null)
						e.Node = node;
					e.reThrow("Sourced file: " + sourceFileInfo);
				}
				catch (EvalError e)
				{
					if (DEBUG)
						SupportClass.WriteStackTrace(e, Console.Error);
					// failsafe, set the Line as the origin of the error.
					if (e.Node == null)
						e.Node = node;
					e.reThrow("Sourced file: " + sourceFileInfo);
				}
				catch (System.Exception e)
				{
					if (DEBUG)
						if (e is bsh.TargetError)
							((bsh.TargetError) e).printStackTrace();
						else
							SupportClass.WriteStackTrace(e, Console.Error);
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new EvalError("Sourced file: " + sourceFileInfo + " unknown error: " + e.Message, node, callstack);
				}
				catch (TokenMgrError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new EvalError("Sourced file: " + sourceFileInfo + " Token Parsing Error: " + e.Message, node, callstack);
				}
				finally
				{
					localInterpreter.get_jjtree().reset();
					
					// reinit the callstack
					if (callstack.depth() > 1)
					{
						callstack.clear();
						callstack.push(nameSpace);
					}
				}
			}
			return Primitive.unwrap(retVal);
		}
		
		/// <summary>Evaluate the inputstream in this interpreter's global namespace.</summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.Object eval(System.IO.StreamReader in_Renamed)
		{
			return eval(in_Renamed, globalNameSpace, "eval stream");
		}
		
		/// <summary>Evaluate the string in this interpreter's global namespace.</summary>
		public virtual System.Object eval(System.String statements)
		{
			if (Interpreter.DEBUG)
				debug("eval(String): " + statements);
			return eval(statements, globalNameSpace);
		}
		
		/// <summary>Evaluate the string in the specified namespace.</summary>
		public virtual System.Object eval(System.String statements, NameSpace nameSpace)
		{
			
			System.String s = (statements.EndsWith(";")?statements:statements + ";");
			return eval(new System.IO.StringReader(s), nameSpace, "inline evaluation of: ``" + showEvalString(s) + "''");
		}
		
		private System.String showEvalString(System.String s)
		{
			s = s.Replace('\n', ' ');
			s = s.Replace('\r', ' ');
			if (s.Length > 80)
				s = s.Substring(0, (80) - (0)) + " . . . ";
			return s;
		}
		
		// end source and eval
		
		/// <summary>Print an error message in a standard format on the output stream
		/// associated with this interpreter. On the GUI console this will appear 
		/// in red, etc.
		/// </summary>
		public void  error(System.Object o)
		{
			if (console != null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				console.error("// Error: " + o + "\n");
			}
			else
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				err.WriteLine("// Error: " + o);
				err.Flush();
			}
		}
		
		// ConsoleInterface
		// The interpreter reflexively implements the console interface that it 
		// uses.  Should clean this up by using an inner class to implement the
		// console for us.
		
		/// <summary>Get the outptut stream associated with this interpreter.
		/// This may be be stdout or the GUI console.
		/// </summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.IO.StreamWriter getOut()
		{
			return out_Renamed;
		}
		
		/// <summary>Get the error output stream associated with this interpreter.
		/// This may be be stderr or the GUI console.
		/// </summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.IO.StreamWriter getErr()
		{
			return err;
		}
		
		public void  println(System.Object o)
		{
			print(System.Convert.ToString(o) + systemLineSeparator);
		}
		
		public void  print(System.Object o)
		{
			if (console != null)
			{
				console.print(o);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.print' was converted to 'System.IO.StreamWriter.Write' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprint_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				out_Renamed.Write(o);
				out_Renamed.Flush();
			}
		}
		
		// End ConsoleInterface
		
		/// <summary>Print a debug message on debug stream associated with this interpreter
		/// only if debugging is turned on.
		/// </summary>
		public static void  debug(System.String s)
		{
			if (DEBUG)
				debug_Renamed_Field.WriteLine("// Debug: " + s);
		}
		
		/* 
		Primary interpreter set and get variable methods
		Note: These are squeltching errors... should they?
		*/
		
		/// <summary>Get the value of the name.
		/// name may be any value. e.g. a variable or field
		/// </summary>
		public virtual System.Object get_Renamed(System.String name)
		{
			try
			{
				System.Object ret = globalNameSpace.get_Renamed(name, this);
				return Primitive.unwrap(ret);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(SimpleNode.JAVACODE, new CallStack());
			}
		}
		
		/// <summary>Unchecked get for internal use</summary>
		internal virtual System.Object getu(System.String name)
		{
			try
			{
				return get_Renamed(name);
			}
			catch (EvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("set: " + e);
			}
		}
		
		/// <summary>Assign the value to the name.	
		/// name may evaluate to anything assignable. e.g. a variable or field.
		/// </summary>
		public virtual void  set_Renamed(System.String name, System.Object value_Renamed)
		{
			// map null to Primtive.NULL coming in...
			if (value_Renamed == null)
				value_Renamed = Primitive.NULL;
			
			CallStack callstack = new CallStack();
			try
			{
				if (Name.isCompound(name))
				{
					LHS lhs = globalNameSpace.getNameResolver(name).toLHS(callstack, this);
					lhs.assign(value_Renamed, false);
				}
				// optimization for common case
				else
					globalNameSpace.setVariable(name, value_Renamed, false);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(SimpleNode.JAVACODE, callstack);
			}
		}
		
		/// <summary>Unchecked set for internal use</summary>
		internal virtual void  setu(System.String name, System.Object value_Renamed)
		{
			try
			{
				set_Renamed(name, value_Renamed);
			}
			catch (EvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("set: " + e);
			}
		}
		
		public virtual void  set_Renamed(System.String name, long value_Renamed)
		{
			set_Renamed(name, new Primitive(value_Renamed));
		}
		public virtual void  set_Renamed(System.String name, int value_Renamed)
		{
			set_Renamed(name, new Primitive(value_Renamed));
		}
		public virtual void  set_Renamed(System.String name, double value_Renamed)
		{
			set_Renamed(name, new Primitive(value_Renamed));
		}
		public virtual void  set_Renamed(System.String name, float value_Renamed)
		{
			set_Renamed(name, new Primitive(value_Renamed));
		}
		public virtual void  set_Renamed(System.String name, bool value_Renamed)
		{
			set_Renamed(name, new Primitive(value_Renamed));
		}
		
		/// <summary>Unassign the variable name.	
		/// Name should evaluate to a variable.
		/// </summary>
		public virtual void  unset(System.String name)
		{
			/*
			We jump through some hoops here to handle arbitrary cases like
			unset("bsh.foo");
			*/
			CallStack callstack = new CallStack();
			try
			{
				LHS lhs = globalNameSpace.getNameResolver(name).toLHS(callstack, this);
				
				if (lhs.type != LHS.VARIABLE)
					throw new EvalError("Can't unset, not a variable: " + name, SimpleNode.JAVACODE, new CallStack());
				
				//lhs.assign( null, false );
				lhs.nameSpace.unsetVariable(name);
			}
			catch (UtilEvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new EvalError(e.Message, SimpleNode.JAVACODE, new CallStack());
			}
		}
		
		// end primary set and get methods
		
		/// <summary>Get a reference to the interpreter (global namespace), cast 
		/// to the specified interface type.  Assuming the appropriate 
		/// methods of the interface are defined in the interpreter, then you may 
		/// use this interface from Java, just like any other Java object.
		/// <p>
		/// For example:
		/// <pre>
		/// Interpreter interpreter = new Interpreter();
		/// // define a method called run()
		/// interpreter.eval("run() { ... }");
		/// </summary>
		/// <summary>// Fetch a reference to the interpreter as a Runnable
		/// Runnable runnable = 
		/// (Runnable)interpreter.getInterface( Runnable.class );
		/// </pre>
		/// <p>
		/// Note that the interpreter does *not* require that any or all of the
		/// methods of the interface be defined at the time the interface is
		/// generated.  However if you attempt to invoke one that is not defined
		/// you will get a runtime exception.
		/// <p>
		/// Note also that this convenience method has exactly the same effect as 
		/// evaluating the script:
		/// <pre>
		/// (Type)this;
		/// </pre>
		/// <p>
		/// For example, the following is identical to the previous example:
		/// <p>
		/// <pre>
		/// // Fetch a reference to the interpreter as a Runnable
		/// Runnable runnable = 
		/// (Runnable)interpreter.eval( "(Runnable)this" );
		/// </pre>
		/// <p>
		/// <em>Version requirement</em> Although standard Java interface types 
		/// are always available, to be used with arbitrary interfaces this 
		/// feature requires that you are using Java 1.3 or greater.
		/// <p>
		/// </summary>
		/// <throws>  EvalError if the interface cannot be generated because the </throws>
		/// <summary>version of Java does not support the proxy mechanism. 
		/// </summary>
		public virtual System.Object getInterface(System.Type interf)
		{
			try
			{
				return globalNameSpace.getThis(this).getInterface(interf);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(SimpleNode.JAVACODE, new CallStack());
			}
		}
		
		/*	Methods for interacting with Parser */
		
		private JJTParserState get_jjtree()
		{
			return parser.jjtree;
		}
		
		private JavaCharStream get_jj_input_stream()
		{
			return parser.jj_input_stream;
		}
		
		private bool Line()
		{
			return parser.Line();
		}
		
		/*	End methods for interacting with Parser */
		
		internal virtual void  loadRCFiles()
		{
			try
			{
				//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
				System.String rcfile = System.Environment.GetEnvironmentVariable("userprofile") + System.IO.Path.DirectorySeparatorChar.ToString() + ".bshrc";
				source(rcfile, globalNameSpace);
			}
			catch (System.Exception e)
			{
				// squeltch security exception, filenotfoundexception
				if (Interpreter.DEBUG)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					debug("Could not find rc file: " + e);
				}
			}
		}
		
		/// <summary>Localize a path to the file name based on the bsh.cwd interpreter 
		/// working directory.
		/// </summary>
		public virtual System.IO.FileInfo pathToFile(System.String fileName)
		{
			System.IO.FileInfo file = new System.IO.FileInfo(fileName);
			
			// if relative, fix up to bsh.cwd
			//UPGRADE_ISSUE: Method 'java.io.File.isAbsolute' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFileisAbsolute'"
			if (!file.isAbsolute())
			{
				System.String cwd = (System.String) getu("bsh.cwd");
				file = new System.IO.FileInfo(cwd + System.IO.Path.DirectorySeparatorChar.ToString() + fileName);
			}
			
			// The canonical file name is also absolute.
			// No need for getAbsolutePath() here...
			return new System.IO.FileInfo(file.FullName);
		}
		
		public static void  redirectOutputToFile(System.String filename)
		{
			try
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
				System.IO.StreamWriter pout = new System.IO.StreamWriter(new System.IO.FileStream(filename, System.IO.FileMode.Create));
				//UPGRADE_TODO: Method 'java.lang.System.setOut' was converted to 'System.Console.SetOut' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemsetOut_javaioPrintStream'"
				System.Console.SetOut(pout);
				//UPGRADE_TODO: Method 'java.lang.System.setErr' was converted to 'System.Console.SetError' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemsetErr_javaioPrintStream'"
				System.Console.SetError(pout);
			}
			catch (System.IO.IOException e)
			{
				System.Console.Error.WriteLine("Can't redirect output to file: " + filename);
			}
		}
		
		internal static void  staticInit()
		{
			/* 
			Apparently in some environments you can't catch the security exception
			at all...  e.g. as an applet in IE  ... will probably have to work 
			around 
			*/
			try
			{
				systemLineSeparator = System.Environment.NewLine;
				System.IO.StreamWriter temp_writer;
				temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
				temp_writer.AutoFlush = true;
				debug_Renamed_Field = temp_writer;
				//UPGRADE_ISSUE: Method 'java.lang.Boolean.getBoolean' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangBooleangetBoolean_javalangString'"
				DEBUG = Boolean.getBoolean("debug");
				//UPGRADE_ISSUE: Method 'java.lang.Boolean.getBoolean' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangBooleangetBoolean_javalangString'"
				TRACE = Boolean.getBoolean("trace");
				//UPGRADE_ISSUE: Method 'java.lang.Boolean.getBoolean' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangBooleangetBoolean_javalangString'"
				LOCALSCOPING = Boolean.getBoolean("localscoping");
				//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				System.String outfilename = System_Renamed.getProperty("outfile");
				if (outfilename != null)
					redirectOutputToFile(outfilename);
			}
			catch (System.Security.SecurityException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Could not init static:" + e);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Could not init static(2):" + e);
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Could not init static(3):" + e);
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  setOut(System.IO.StreamWriter out_Renamed)
		{
			this.out_Renamed = out_Renamed;
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  setErr(System.IO.StreamWriter err)
		{
			this.err = err;
		}
		
		/// <summary>De-serialization setup.
		/// Default out and err streams to stdout, stderr if they are null.
		/// </summary>
		//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
		//UPGRADE_TODO: Method 'readObject' was converted to 'Interpreter' and its parameters were changed. The code must be reviewed in order to assure that no calls to non-member methods of the converted parameters are made. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1192'"
		protected Interpreter(System.Runtime.Serialization.SerializationInfo stream, System.Runtime.Serialization.StreamingContext context)
		{
			SupportClass.DefaultReadObject(stream, context, this);
			
			// set transient fields
			if (console != null)
			{
				setOut(console.getOut());
				setErr(console.getErr());
			}
			else
			{
				System.IO.StreamWriter temp_writer;
				temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardOutput(), System.Console.Out.Encoding);
				temp_writer.AutoFlush = true;
				setOut(temp_writer);
				System.IO.StreamWriter temp_writer2;
				temp_writer2 = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
				temp_writer2.AutoFlush = true;
				setErr(temp_writer2);
			}
		}
		public virtual void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			SupportClass.DefaultWriteObject(info, context, this);
		}
		static Interpreter()
		{
			{
				staticInit();
			}
		}
	}
}