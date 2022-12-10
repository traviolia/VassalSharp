//
// In order to convert some functionality to Visual C#, the Java Language Conversion Assistant
// creates "support classes" that duplicate the original functionality.  
//
// Support classes replicate the functionality of the original code, but in some cases they are 
// substantially different architecturally. Although every effort is made to preserve the 
// original architecture of the application in the converted project, the user should be aware that 
// the primary goal of these support classes is to replicate functionality, and that at times 
// the architecture of the resulting solution may differ somewhat.
//

using System;

	/// <summary>
	/// This interface should be implemented by any class whose instances are intended 
	/// to be executed by a thread.
	/// </summary>
	public interface IThreadRunnable
	{
		/// <summary>
		/// This method has to be implemented in order that starting of the thread causes the object's 
		/// run method to be called in that separately executing thread.
		/// </summary>
		void Run();
	}

	/*******************************/
	/// <summary>
	/// This class is used to encapsulate a source of Xml code in an single class.
	/// </summary>
	public class XmlSourceSupport
	{
		private System.IO.Stream bytes;
		private System.IO.StreamReader characters;
		private string uri;

		/// <summary>
		/// Constructs an empty XmlSourceSupport instance.
		/// </summary>
		public XmlSourceSupport()
		{
			bytes = null;
			characters = null;
			uri = null;
		}

		/// <summary>
		/// Constructs a XmlSource instance with the specified source System.IO.Stream.
		/// </summary>
		/// <param name="stream">The stream containing the document.</param>
		public XmlSourceSupport(System.IO.Stream stream)
		{
			bytes = stream;
			characters = null;
			uri = null;
		}

		/// <summary>
		/// Constructs a XmlSource instance with the specified source System.IO.StreamReader.
		/// </summary>
		/// <param name="reader">The reader containing the document.</param>
		public XmlSourceSupport(System.IO.StreamReader reader)
		{
			bytes = null;
			characters = reader;
			uri = null;
		}

		/// <summary>
		/// Construct a XmlSource instance with the specified source Uri string.
		/// </summary>
		/// <param name="source">The source containing the document.</param>
		public XmlSourceSupport(string source)
		{
			bytes = null;
			characters = null;
			uri = source;
		}

		/// <summary>
		/// Represents the source Stream of the XmlSource.
		/// </summary>
		public System.IO.Stream Bytes	
		{
			get
			{
				return bytes;
			}
			set
			{
				bytes = value;
			}
		}

		/// <summary>
		/// Represents the source StreamReader of the XmlSource.
		/// </summary>
		public System.IO.StreamReader Characters
		{
			get
			{
				return characters;
			}
			set
			{
				characters = value;
			}
		}

		/// <summary>
		/// Represents the source URI of the XmlSource.
		/// </summary>
		public string Uri
		{
			get
			{
				return uri;
			}
			set
			{
				uri = value;
			}
		}
	}

	/*******************************/
	/// <summary>
	/// Basic interface for resolving entities.
	/// </summary>
	public interface XmlSaxEntityResolver
	{
		/// <summary>
		/// Allow the application to resolve external entities.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity being referenced.</param>
		/// <returns>A XmlSourceSupport object describing the new input source, or null to request that the parser open a regular URI connection to the system identifier.</returns>
		XmlSourceSupport resolveEntity(string publicId, string systemId);
	}

	/*******************************/
	/// <summary>
	/// This interface will manage the Content events of a XML document.
	/// </summary>
	public interface XmlSaxContentHandler
	{
		/// <summary>
		/// This method manage the notification when Characters elements were found.
		/// </summary>
		/// <param name="ch">The array with the characters found.</param>
		/// <param name="start">The index of the first position of the characters found.</param>
		/// <param name="length">Specify how many characters must be read from the array.</param>
		void characters(char[] ch, int start, int length);

		/// <summary>
		/// This method manage the notification when the end document node were found.
		/// </summary>
		void endDocument();

		/// <summary>
		/// This method manage the notification when the end element node was found.
		/// </summary>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="qName">The long (qualified) name of the element.</param>
		void endElement(string namespaceURI, string localName, string qName);

		/// <summary>
		/// This method manage the event when an area of expecific URI prefix was ended.
		/// </summary>
		/// <param name="prefix">The prefix that ends.</param>
		void endPrefixMapping(string prefix);

		/// <summary>
		/// This method manage the event when a ignorable whitespace node was found.
		/// </summary>
		/// <param name="Ch">The array with the ignorable whitespaces.</param>
		/// <param name="Start">The index in the array with the ignorable whitespace.</param>
		/// <param name="Length">The length of the whitespaces.</param>
		void ignorableWhitespace(char[] Ch, int Start, int Length);

		/// <summary>
		/// This method manage the event when a processing instruction was found.
		/// </summary>
		/// <param name="target">The processing instruction target.</param>
		/// <param name="data">The processing instruction data.</param>
		void processingInstruction(string target, string data);

		/// <summary>
		/// This method is not supported, it is included for compatibility.
		/// </summary>
		void setDocumentLocator(XmlSaxLocator locator);

		/// <summary>
		/// This method manage the event when a skipped entity was found.
		/// </summary>
		/// <param name="name">The name of the skipped entity.</param>
		void skippedEntity(string name);

		/// <summary>
		/// This method manage the event when a start document node was found.
		/// </summary>
		void startDocument();

		/// <summary>
		/// This method manage the event when a start element node was found.
		/// </summary>
		/// <param name="namespaceURI">The namespace uri of the element tag.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="qName">The long (qualified) name of the element.</param>
		/// <param name="atts">The list of attributes of the element.</param>
		void startElement(string namespaceURI, string localName, string qName, SaxAttributesSupport atts);

		/// <summary>
		/// This methods indicates the start of a prefix area in the XML document.
		/// </summary>
		/// <param name="prefix">The prefix of the area.</param>
		/// <param name="uri">The namespace URI of the prefix area.</param>
		void startPrefixMapping(string prefix, string uri);
	}

	/*******************************/
	/// <summary>
	/// This interface will manage errors during the parsing of a XML document.
	/// </summary>
	public interface XmlSaxErrorHandler
	{
		/// <summary>
		/// This method manage an error exception ocurred during the parsing process.
		/// </summary>
		/// <param name="exception">The exception thrown by the parser.</param>
		void error(System.Xml.XmlException exception);

		/// <summary>
		/// This method manage a fatal error exception ocurred during the parsing process.
		/// </summary>
		/// <param name="exception">The exception thrown by the parser.</param>
		void fatalError(System.Xml.XmlException exception);

		/// <summary>
		/// This method manage a warning exception ocurred during the parsing process.
		/// </summary>
		/// <param name="exception">The exception thrown by the parser.</param>
		void warning(System.Xml.XmlException exception);
	}

	/*******************************/
	/// <summary>
	/// This interface is created to emulate the SAX Locator interface behavior.
	/// </summary>
	public interface XmlSaxLocator
	{
		/// <summary>
		/// This method return the column number where the current document event ends.
		/// </summary>
		/// <returns>The column number where the current document event ends.</returns>
		int getColumnNumber();

		/// <summary>
		/// This method return the line number where the current document event ends.
		/// </summary>
		/// <returns>The line number where the current document event ends.</returns>
		int getLineNumber();

		/// <summary>
		/// This method is not supported, it is included for compatibility.	
		/// </summary>
		/// <returns>The saved public identifier.</returns>
		string getPublicId();

		/// <summary>
		/// This method is not supported, it is included for compatibility.		
		/// </summary>
		/// <returns>The saved system identifier.</returns>
		string getSystemId();
	}

	/*******************************/
	/// <summary>
	/// This class is created for emulates the SAX LocatorImpl behaviors.
	/// </summary>
	public class XmlSaxLocatorImpl : XmlSaxLocator
	{
		/// <summary>
		/// This method returns a new instance of 'XmlSaxLocatorImpl'.
		/// </summary>
		/// <returns>A new 'XmlSaxLocatorImpl' instance.</returns>
		public XmlSaxLocatorImpl()
		{
		}

		/// <summary>
		/// This method returns a new instance of 'XmlSaxLocatorImpl'.
		/// Create a persistent copy of the current state of a locator.
		/// </summary>
		/// <param name="locator">The current state of a locator.</param>
		/// <returns>A new 'XmlSaxLocatorImpl' instance.</returns>
		public XmlSaxLocatorImpl(XmlSaxLocator locator)
		{
			setPublicId(locator.getPublicId());
			setSystemId(locator.getSystemId());
			setLineNumber(locator.getLineNumber());
			setColumnNumber(locator.getColumnNumber());
		}

		/// <summary>
		/// This method is not supported, it is included for compatibility.
		/// Return the saved public identifier.
		/// </summary>
		/// <returns>The saved public identifier.</returns>
		public virtual string getPublicId()
		{
			return publicId;
		}

		/// <summary>
		/// This method is not supported, it is included for compatibility.
		/// Return the saved system identifier.
		/// </summary>
		/// <returns>The saved system identifier.</returns>
		public virtual string getSystemId()
		{
			return systemId;
		}

		/// <summary>
		/// Return the saved line number.
		/// </summary>
		/// <returns>The saved line number.</returns>
		public virtual int getLineNumber()
		{
			return lineNumber;
		}

		/// <summary>
		/// Return the saved column number.
		/// </summary>
		/// <returns>The saved column number.</returns>
		public virtual int getColumnNumber()
		{
			return columnNumber;
		}

		/// <summary>
		/// This method is not supported, it is included for compatibility.
		/// Set the public identifier for this locator.
		/// </summary>
		/// <param name="publicId">The new public identifier.</param>
		public virtual void setPublicId(string publicId)
		{
			this.publicId = publicId;
		}

		/// <summary>
		/// This method is not supported, it is included for compatibility.
		/// Set the system identifier for this locator.
		/// </summary>
		/// <param name="systemId">The new system identifier.</param>
		public virtual void setSystemId(string systemId)
		{
			this.systemId = systemId;
		}

		/// <summary>
		/// Set the line number for this locator.
		/// </summary>
		/// <param name="lineNumber">The line number.</param>
		public virtual void setLineNumber(int lineNumber)
		{
			this.lineNumber = lineNumber;
		}

		/// <summary>
		/// Set the column number for this locator.
		/// </summary>
		/// <param name="columnNumber">The column number.</param>
		public virtual void setColumnNumber(int columnNumber)
		{
			this.columnNumber = columnNumber;
		}

		// Internal state.
		private string publicId;
		private string systemId;
		private int lineNumber;
		private int columnNumber;
	}

	/*******************************/
	/// <summary>
	/// This interface will manage the Content events of a XML document.
	/// </summary>
	public interface XmlSaxLexicalHandler
	{
		/// <summary>
		/// This method manage the notification when Characters elements were found.
		/// </summary>
		/// <param name="ch">The array with the characters found.</param>
		/// <param name="start">The index of the first position of the characters found.</param>
		/// <param name="length">Specify how many characters must be read from the array.</param>
		void comment(char[] ch, int start, int length);

		/// <summary>
		/// This method manage the notification when the end of a CDATA section were found.
		/// </summary>
		void endCDATA();

		/// <summary>
		/// This method manage the notification when the end of DTD declarations were found.
		/// </summary>
		void endDTD();

		/// <summary>
		/// This method report the end of an entity.
		/// </summary>
		/// <param name="name">The name of the entity that is ending.</param>
		void endEntity(string name);

		/// <summary>
		/// This method manage the notification when the start of a CDATA section were found.
		/// </summary>
		void startCDATA();

		/// <summary>
		/// This method manage the notification when the start of DTD declarations were found.
		/// </summary>
		/// <param name="name">The name of the DTD entity.</param>
		/// <param name="publicId">The public identifier.</param>
		/// <param name="systemId">The system identifier.</param>
		void startDTD(string name, string publicId, string systemId);

		/// <summary>
		/// This method report the start of an entity.
		/// </summary>
		/// <param name="name">The name of the entity that is ending.</param>
		void startEntity(string name);
	}

	/*******************************/
	/// <summary>
	/// This class will manage all the parsing operations emulating the SAX parser behavior
	/// </summary>
	public class SaxAttributesSupport
	{
		private System.Collections.ArrayList MainList;

		/// <summary>
		/// Builds a new instance of SaxAttributesSupport.
		/// </summary>
		public SaxAttributesSupport()
		{
			MainList = new System.Collections.ArrayList();
		}

		/// <summary>
		/// Creates a new instance of SaxAttributesSupport from an ArrayList of Att_Instance class.
		/// </summary>
		/// <param name="arrayList">An ArraList of Att_Instance class instances.</param>
		/// <returns>A new instance of SaxAttributesSupport</returns>
		public SaxAttributesSupport(SaxAttributesSupport List)
		{
			SaxAttributesSupport temp = new SaxAttributesSupport();
			temp.MainList = (System.Collections.ArrayList) List.MainList.Clone();
		}

		/// <summary>
		/// Adds a new attribute elment to the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="Uri">The Uri of the attribute to be added.</param>
		/// <param name="Lname">The Local name of the attribute to be added.</param>
		/// <param name="Qname">The Long(qualify) name of the attribute to be added.</param>
		/// <param name="Type">The type of the attribute to be added.</param>
		/// <param name="Value">The value of the attribute to be added.</param>
		public virtual void Add(string Uri, string Lname, string Qname, string Type, string Value)
		{
			Att_Instance temp_Attributes = new Att_Instance(Uri, Lname, Qname, Type, Value);
			MainList.Add(temp_Attributes);
		}

		/// <summary>
		/// Clears the list of attributes in the given AttributesSupport instance.
		/// </summary>
		public virtual void Clear()
		{
			MainList.Clear();
		}

		/// <summary>
		/// Obtains the index of an attribute of the AttributeSupport from its qualified (long) name.
		/// </summary>
		/// <param name="Qname">The qualified name of the attribute to search.</param>
		/// <returns>An zero-based index of the attribute if it is found, otherwise it returns -1.</returns>
		public virtual int GetIndex(string Qname)
		{
			int index = GetLength() - 1;
			while ((index >= 0) && !(((Att_Instance) (MainList[index])).att_fullName.Equals(Qname)))
				index--;
			if (index >= 0)
				return index;
			else
				return -1;
		}

		/// <summary>
		/// Obtains the index of an attribute of the AttributeSupport from its namespace URI and its localname.
		/// </summary>
		/// <param name="Uri">The namespace URI of the attribute to search.</param>
		/// <param name="Lname">The local name of the attribute to search.</param>
		/// <returns>An zero-based index of the attribute if it is found, otherwise it returns -1.</returns>
		public virtual int GetIndex(string Uri, string Lname)
		{
			int index = GetLength() - 1;
			while ((index >= 0) && !(((Att_Instance) (MainList[index])).att_localName.Equals(Lname) && ((Att_Instance)(MainList[index])).att_URI.Equals(Uri)))
				index--;
			if (index >= 0)
				return index;
			else
				return -1;
		}

		/// <summary>
		/// Returns the number of attributes saved in the SaxAttributesSupport instance.
		/// </summary>
		/// <returns>The number of elements in the given SaxAttributesSupport instance.</returns>
		public virtual int GetLength()
		{
			return MainList.Count;
		}

		/// <summary>
		/// Returns the local name of the attribute in the given SaxAttributesSupport instance that indicates the given index.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <returns>The local name of the attribute indicated by the index or null if the index is out of bounds.</returns>
		public virtual string GetLocalName(int index)
		{
			try
			{
				return ((Att_Instance) MainList[index]).att_localName;
			}
			catch (System.ArgumentOutOfRangeException)
			{
				return "";
			}
		}

		/// <summary>
		/// Returns the qualified name of the attribute in the given SaxAttributesSupport instance that indicates the given index.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <returns>The qualified name of the attribute indicated by the index or null if the index is out of bounds.</returns>
		public virtual string GetFullName(int index)
		{
			try
			{
				return ((Att_Instance) MainList[index]).att_fullName;
			}
			catch (System.ArgumentOutOfRangeException)
			{
				return "";
			}
		}

		/// <summary>
		/// Returns the type of the attribute in the given SaxAttributesSupport instance that indicates the given index.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <returns>The type of the attribute indicated by the index or null if the index is out of bounds.</returns>
		public virtual string GetType(int index)
		{
			try
			{
				return ((Att_Instance) MainList[index]).att_type;
			}
			catch(System.ArgumentOutOfRangeException)
			{
				return "";
			}
		}

		/// <summary>
		/// Returns the namespace URI of the attribute in the given SaxAttributesSupport instance that indicates the given index.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <returns>The namespace URI of the attribute indicated by the index or null if the index is out of bounds.</returns>
		public virtual string GetURI(int index)
		{
			try
			{
				return ((Att_Instance) MainList[index]).att_URI;
			}
			catch(System.ArgumentOutOfRangeException)
			{
				return "";
			}
		}

		/// <summary>
		/// Returns the value of the attribute in the given SaxAttributesSupport instance that indicates the given index.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <returns>The value of the attribute indicated by the index or null if the index is out of bounds.</returns>
		public virtual string GetValue(int index)
		{
			try
			{
				return ((Att_Instance) MainList[index]).att_value;
			}
			catch(System.ArgumentOutOfRangeException)
			{
				return "";
			}
		}

		/// <summary>
		/// Modifies the local name of the attribute in the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <param name="LocalName">The new Local name for the attribute.</param>
		public virtual void SetLocalName(int index, string LocalName)
		{
			((Att_Instance) MainList[index]).att_localName = LocalName;
		}

		/// <summary>
		/// Modifies the qualified name of the attribute in the given SaxAttributesSupport instance.
		/// </summary>	
		/// <param name="index">The attribute index.</param>
		/// <param name="FullName">The new qualified name for the attribute.</param>
		public virtual void SetFullName(int index, string FullName)
		{
			((Att_Instance) MainList[index]).att_fullName = FullName;
		}

		/// <summary>
		/// Modifies the type of the attribute in the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <param name="Type">The new type for the attribute.</param>
		public virtual void SetType(int index, string Type)
		{
			((Att_Instance) MainList[index]).att_type = Type;
		}

		/// <summary>
		/// Modifies the namespace URI of the attribute in the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <param name="URI">The new namespace URI for the attribute.</param>
		public virtual void SetURI(int index, string URI)
		{
			((Att_Instance) MainList[index]).att_URI = URI;
		}

		/// <summary>
		/// Modifies the value of the attribute in the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="index">The attribute index.</param>
		/// <param name="Value">The new value for the attribute.</param>
		public virtual void SetValue(int index, string Value)
		{
			((Att_Instance) MainList[index]).att_value = Value;
		}

		/// <summary>
		/// This method eliminates the Att_Instance instance at the specified index.
		/// </summary>
		/// <param name="index">The index of the attribute.</param>
		public virtual void RemoveAttribute(int index)
		{
			try
			{
				MainList.RemoveAt(index);
			}
			catch(System.ArgumentOutOfRangeException)
			{
				throw new System.IndexOutOfRangeException("The index is out of range");
			}
		}

		/// <summary>
		/// This method eliminates the Att_Instance instance in the specified index.
		/// </summary>
		/// <param name="indexName">The index name of the attribute.</param>
		public virtual void RemoveAttribute(string indexName)
		{
			try
			{
				int pos = GetLength() - 1;
				while ((pos >= 0) && !(((Att_Instance) (MainList[pos])).att_localName.Equals(indexName)))
					pos--;
				if (pos >= 0)
					MainList.RemoveAt(pos);
			}
			catch(System.ArgumentOutOfRangeException)
			{
				throw new System.IndexOutOfRangeException("The index is out of range");
			}
		}

		/// <summary>
		/// Replaces an Att_Instance in the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="index">The index of the attribute.</param>
		/// <param name="Uri">The namespace URI of the new Att_Instance.</param>
		/// <param name="Lname">The local name of the new Att_Instance.</param>
		/// <param name="Qname">The namespace URI of the new Att_Instance.</param>
		/// <param name="Type">The type of the new Att_Instance.</param>
		/// <param name="Value">The value of the new Att_Instance.</param>
		public virtual void SetAttribute(int index, string Uri, string Lname, string Qname, string Type, string Value)
		{
			MainList[index] = new Att_Instance(Uri, Lname, Qname, Type, Value);
		}

		/// <summary>
		/// Replaces all the list of Att_Instance of the given SaxAttributesSupport instance.
		/// </summary>
		/// <param name="Source">The source SaxAttributesSupport instance.</param>
		public virtual void SetAttributes(SaxAttributesSupport Source)
		{
			MainList = Source.MainList;
		}

		/// <summary>
		/// Returns the type of the Attribute that match with the given qualified name.
		/// </summary>
		/// <param name="Qname">The qualified name of the attribute to search.</param>
		/// <returns>The type of the attribute if it exist otherwise returns null.</returns>
		public virtual string GetType(string Qname)
		{
			int temp_Index = GetIndex(Qname);
			if (temp_Index != -1)
				return ((Att_Instance) MainList[temp_Index]).att_type;
			else
				return "";
		}

		/// <summary>
		/// Returns the type of the Attribute that match with the given namespace URI and local name.
		/// </summary>
		/// <param name="Uri">The namespace URI of the attribute to search.</param>
		/// <param name="Lname">The local name of the attribute to search.</param>
		/// <returns>The type of the attribute if it exist otherwise returns null.</returns>
		public virtual string GetType(string Uri, string Lname)
		{
			int temp_Index = GetIndex(Uri, Lname);
			if (temp_Index != -1)
				return ((Att_Instance) MainList[temp_Index]).att_type;
			else
				return "";
		}

		/// <summary>
		/// Returns the value of the Attribute that match with the given qualified name.
		/// </summary>
		/// <param name="Qname">The qualified name of the attribute to search.</param>
		/// <returns>The value of the attribute if it exist otherwise returns null.</returns>
		public virtual string GetValue(string Qname)
		{
			int temp_Index = GetIndex(Qname);
			if (temp_Index != -1)
				return ((Att_Instance) MainList[temp_Index]).att_value;
			else
				return "";
		}

		/// <summary>
		/// Returns the value of the Attribute that match with the given namespace URI and local name.
		/// </summary>
		/// <param name="Uri">The namespace URI of the attribute to search.</param>
		/// <param name="Lname">The local name of the attribute to search.</param>
		/// <returns>The value of the attribute if it exist otherwise returns null.</returns>
		public virtual string GetValue(string Uri, string Lname)
		{
			int temp_Index = GetIndex(Uri, Lname);
			if (temp_Index != -1)
				return ((Att_Instance) MainList[temp_Index]).att_value;
			else
				return "";
		}

		/*******************************/
		/// <summary>
		/// This class is created to save the information of each attributes in the SaxAttributesSupport.
		/// </summary>
		public class Att_Instance 
		{
			public string att_URI;
			public string att_localName;
			public string att_fullName;
			public string att_type;
			public string att_value;				

			/// <summary>
			/// This is the constructor of the Att_Instance
			/// </summary>
			/// <param name="Uri">The namespace URI of the attribute</param>
			/// <param name="Lname">The local name of the attribute</param>
			/// <param name="Qname">The long(Qualify) name of attribute</param>
			/// <param name="Type">The type of the attribute</param>
			/// <param name="Value">The value of the attribute</param>
			public Att_Instance(string Uri, string Lname, string Qname, string Type, string Value)
			{
				this.att_URI = Uri;
				this.att_localName = Lname;
				this.att_fullName = Qname;
				this.att_type = Type;
				this.att_value = Value;
			}
		}
	}

	/*******************************/
	/// <summary>
	/// This exception is thrown by the XmlSaxDocumentManager in the SetProperty and SetFeature 
	/// methods if a property or method couldn't be found.
	/// </summary>
	public class ManagerNotRecognizedException : System.Exception
	{
		/// <summary>
		/// Creates a new ManagerNotRecognizedException with the message specified.
		/// </summary>
		/// <param name="Message">Error message of the exception.</param>
		public ManagerNotRecognizedException(string Message) : base(Message)
		{
		}
	}

	/*******************************/
	/// <summary>
	/// This exception is thrown by the XmlSaxDocumentManager in the SetProperty and SetFeature methods 
	/// if a property or method couldn't be supported.
	/// </summary>
	public class ManagerNotSupportedException : System.Exception
	{
		/// <summary>
		/// Creates a new ManagerNotSupportedException with the message specified.
		/// </summary>
		/// <param name="Message">Error message of the exception.</param>
		public ManagerNotSupportedException(string Message) : base(Message)
		{
		}
	}

	/*******************************/
	/// <summary>
	/// This class provides the base implementation for the management of XML documents parsing.
	/// </summary>
	public class XmlSaxDefaultHandler : XmlSaxContentHandler, XmlSaxErrorHandler, XmlSaxEntityResolver
	{
		/// <summary>
		/// This method manage the notification when Characters element were found.
		/// </summary>
		/// <param name="ch">The array with the characters founds</param>
		/// <param name="start">The index of the first position of the characters found</param>
		/// <param name="length">Specify how many characters must be read from the array</param>
		public virtual void characters(char[] ch, int start, int length) 
		{
		}

		/// <summary>
		/// This method manage the notification when the end document node were found
		/// </summary>
		public virtual void endDocument() 
		{
		}

		/// <summary>
		/// This method manage the notification when the end element node were found
		/// </summary>
		/// <param name="namespaceURI">The namespace URI of the element</param>
		/// <param name="localName">The local name of the element</param>
		/// <param name="qName">The long name (qualify name) of the element</param>
		public virtual void endElement(string uri, string localName, string qName)
		{
		}
	
		/// <summary>
		/// This method manage the event when an area of expecific URI prefix was ended.
		/// </summary>
		/// <param name="prefix">The prefix that ends</param>
		public virtual void endPrefixMapping(string prefix)
		{
		}

		/// <summary>
		/// This method manage when an error exception ocurrs in the parsing process
		/// </summary>
		/// <param name="exception">The exception throws by the parser</param>
		public virtual void error(System.Xml.XmlException e)
		{
		}

		/// <summary>
		/// This method manage when a fatal error exception ocurrs in the parsing process
		/// </summary>
		/// <param name="exception">The exception Throws by the parser</param>
		public virtual void fatalError(System.Xml.XmlException e) 
		{
		}

		/// <summary>
		/// This method manage the event when a ignorable whitespace node were found
		/// </summary>
		/// <param name="Ch">The array with the ignorable whitespaces</param>
		/// <param name="Start">The index in the array with the ignorable whitespace</param>
		/// <param name="Length">The length of the whitespaces</param>
		public virtual void ignorableWhitespace(char[] ch, int start, int length)
		{
		}

		/// <summary>
		/// This method is not supported only is created for compatibility
		/// </summary>
		public virtual void notationDecl(string name, string publicId, string systemId) 
		{
		}

		/// <summary>
		/// This method manage the event when a processing instruction were found
		/// </summary>
		/// <param name="target">The processing instruction target</param>
		/// <param name="data">The processing instruction data</param>
		public virtual void processingInstruction(string target, string data) 
		{
		}

		/// <summary>
		/// Allow the application to resolve external entities.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity being referenced.</param>
		/// <returns>A XmlSourceSupport object describing the new input source, or null to request that the parser open a regular URI connection to the system identifier.</returns>
		public virtual XmlSourceSupport resolveEntity(string publicId, string systemId)
		{
			return null;
		}

		/// <summary>
		/// This method is not supported, is include for compatibility
		/// </summary>		 
		public virtual void setDocumentLocator(XmlSaxLocator locator)
		{
		}

		/// <summary>
		/// This method manage the event when a skipped entity were found
		/// </summary>
		/// <param name="name">The name of the skipped entity</param>
		public virtual void skippedEntity(string name)
		{
		}

		/// <summary>
		/// This method manage the event when a start document node were found 
		/// </summary>
		public virtual void startDocument()
		{
		}

		/// <summary>
		/// This method manage the event when a start element node were found
		/// </summary>
		/// <param name="namespaceURI">The namespace uri of the element tag</param>
		/// <param name="localName">The local name of the element</param>
		/// <param name="qName">The Qualify (long) name of the element</param>
		/// <param name="atts">The list of attributes of the element</param>
		public virtual void startElement(string uri, string localName, string qName, SaxAttributesSupport attributes) 
		{
		}

		/// <summary>
		/// This methods indicates the start of a prefix area in the XML document.
		/// </summary>
		/// <param name="prefix">The prefix of the area</param>
		/// <param name="uri">The namespace uri of the prefix area</param>
		public virtual void startPrefixMapping(string prefix, string uri) 
		{
		}

		/// <summary>
		/// This method is not supported only is created for compatibility
		/// </summary>        
		public virtual void unparsedEntityDecl(string name, string publicId, string systemId, string notationName)
		{
		}

		/// <summary>
		/// This method manage when a warning exception ocurrs in the parsing process
		/// </summary>
		/// <param name="exception">The exception Throws by the parser</param>
		public virtual void warning(System.Xml.XmlException e)
		{
		}
	}
	/*******************************/
	/// <summary>
	/// This class provides the base implementation for the management of XML documents parsing.
	/// </summary>
	public class XmlSaxParserAdapter : XmlSAXDocumentManager, XmlSaxContentHandler 
	{

		/// <summary>
		/// This method manage the notification when Characters element were found.
		/// </summary>
		/// <param name="ch">The array with the characters founds</param>
		/// <param name="start">The index of the first position of the characters found</param>
		/// <param name="length">Specify how many characters must be read from the array</param>
		public virtual void characters(char[] ch, int start, int length){}

		/// <summary>
		/// This method manage the notification when the end document node were found
		/// </summary>
		public virtual void endDocument(){}

		/// <summary>
		/// This method manage the notification when the end element node were found
		/// </summary>
		/// <param name="namespaceURI">The namespace URI of the element</param>
		/// <param name="localName">The local name of the element</param>
		/// <param name="qName">The long name (qualify name) of the element</param>
		public virtual void endElement(string namespaceURI, string localName, string qName){}

		/// <summary>
		/// This method manage the event when an area of expecific URI prefix was ended.
		/// </summary>
		/// <param name="prefix">The prefix that ends.</param>
		public virtual void endPrefixMapping(string prefix){}

		/// <summary>
		/// This method manage the event when a ignorable whitespace node were found
		/// </summary>
		/// <param name="ch">The array with the ignorable whitespaces</param>
		/// <param name="start">The index in the array with the ignorable whitespace</param>
		/// <param name="length">The length of the whitespaces</param>
		public virtual void ignorableWhitespace(char[] ch, int start, int length){}

		/// <summary>
		/// This method manage the event when a processing instruction were found
		/// </summary>
		/// <param name="target">The processing instruction target</param>
		/// <param name="data">The processing instruction data</param>
		public virtual void processingInstruction(string target, string data){}

		/// <summary>
		/// Receive an object for locating the origin of events into the XML document
		/// </summary>
		/// <param name="locator">A 'XmlSaxLocator' object that can return the location of any events into the XML document</param>
		public virtual void setDocumentLocator(XmlSaxLocator locator){}

		/// <summary>
		/// This method manage the event when a skipped entity was found.
		/// </summary>
		/// <param name="name">The name of the skipped entity.</param>
		public virtual void skippedEntity(string name){}

		/// <summary>
		/// This method manage the event when a start document node were found 
		/// </summary>
		public virtual void startDocument(){}

		/// <summary>
		/// This method manage the event when a start element node were found
		/// </summary>
		/// <param name="namespaceURI">The namespace uri of the element tag</param>
		/// <param name="localName">The local name of the element</param>
		/// <param name="qName">The Qualify (long) name of the element</param>
		/// <param name="qAtts">The list of attributes of the element</param>
		public virtual void startElement(string namespaceURI, string localName, string qName, SaxAttributesSupport qAtts){}

		/// <summary>
		/// This methods indicates the start of a prefix area in the XML document.
		/// </summary>
		/// <param name="prefix">The prefix of the area.</param>
		/// <param name="uri">The namespace URI of the prefix area.</param>
		public virtual void startPrefixMapping(string prefix, string uri){}

	}


	/*******************************/
	/// <summary>
	/// Emulates the SAX parsers behaviours.
	/// </summary>
	public class XmlSAXDocumentManager
	{	
		protected bool isValidating;
		protected bool namespaceAllowed;
		protected System.Xml.XmlValidatingReader reader;
		protected XmlSaxContentHandler callBackHandler;
		protected XmlSaxErrorHandler errorHandler;
		protected XmlSaxLocatorImpl locator;
		protected XmlSaxLexicalHandler lexical;
		protected XmlSaxEntityResolver entityResolver;
		protected string parserFileName;

		/// <summary>
		/// Public constructor for the class.
		/// </summary>
		public XmlSAXDocumentManager()
		{
			isValidating = false;
			namespaceAllowed = false;
			reader = null;
			callBackHandler = null;
			errorHandler = null;
			locator = null;
			lexical = null;
			entityResolver = null;
			parserFileName = "";
		}

		/// <summary>
		/// Returns a new instance of 'XmlSAXDocumentManager'.
		/// </summary>
		/// <returns>A new 'XmlSAXDocumentManager' instance.</returns>
		public static XmlSAXDocumentManager NewInstance()
		{
			return new XmlSAXDocumentManager();
		}

		/// <summary>
		/// Returns a clone instance of 'XmlSAXDocumentManager'.
		/// </summary>
		/// <returns>A clone 'XmlSAXDocumentManager' instance.</returns>
		public static XmlSAXDocumentManager CloneInstance(XmlSAXDocumentManager instance)
		{
			XmlSAXDocumentManager temp = new XmlSAXDocumentManager();
			temp.NamespaceAllowed = instance.NamespaceAllowed;
			temp.isValidating = instance.isValidating;
			XmlSaxContentHandler contentHandler = instance.getContentHandler();
			if (contentHandler != null)
				temp.setContentHandler(contentHandler);
			XmlSaxErrorHandler errorHandler = instance.getErrorHandler();
			if (errorHandler != null)
				temp.setErrorHandler(errorHandler);
			temp.setFeature("http://xml.org/sax/features/namespaces", instance.getFeature("http://xml.org/sax/features/namespaces"));
			temp.setFeature("http://xml.org/sax/features/namespace-prefixes", instance.getFeature("http://xml.org/sax/features/namespace-prefixes"));
			temp.setFeature("http://xml.org/sax/features/validation", instance.getFeature("http://xml.org/sax/features/validation"));
			temp.setProperty("http://xml.org/sax/properties/lexical-handler", instance.getProperty("http://xml.org/sax/properties/lexical-handler"));
			temp.parserFileName = instance.parserFileName;
			return temp;
		}

		/// <summary>
		/// Indicates whether the 'XmlSAXDocumentManager' are validating the XML over a DTD.
		/// </summary>
		public bool IsValidating
		{
			get
			{
				return isValidating;
			}
			set
			{
				isValidating = value;
			}
		}

		/// <summary>
		/// Indicates whether the 'XmlSAXDocumentManager' manager allows namespaces.
		/// </summary>
		public  bool NamespaceAllowed
		{
			get
			{
				return namespaceAllowed;
			}
			set
			{
				namespaceAllowed = value;
			}
		}

		/// <summary>
		/// Emulates the behaviour of a SAX LocatorImpl object.
		/// </summary>
		/// <param name="locator">The 'XmlSaxLocatorImpl' instance to assing the document location.</param>
		/// <param name="textReader">The XML document instance to be used.</param>
		private void UpdateLocatorData(XmlSaxLocatorImpl locator, System.Xml.XmlTextReader textReader)
		{
			if ((locator != null) && (textReader != null))
			{
				locator.setColumnNumber(textReader.LinePosition);
				locator.setLineNumber(textReader.LineNumber);
				locator.setSystemId(parserFileName);
			}
		}

		/// <summary>
		/// Emulates the behavior of a SAX parsers. Set the value of a feature.
		/// </summary>
		/// <param name="name">The feature name, which is a fully-qualified URI.</param>
		/// <param name="value">The requested value for the feature.</param>
		public virtual void setFeature(string name, bool value)
		{
			switch (name)
			{
				case "http://xml.org/sax/features/namespaces":
				{
					try
					{
						this.NamespaceAllowed = value;
						break;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				case "http://xml.org/sax/features/namespace-prefixes":
				{
					try
					{
						this.NamespaceAllowed = value;
						break;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				case "http://xml.org/sax/features/validation":
				{
					try
					{
						this.isValidating = value;
						break;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				default:
					throw new ManagerNotRecognizedException("The specified feature: " + name + " are not supported");
			}
		}

		/// <summary>
		/// Emulates the behavior of a SAX parsers. Gets the value of a feature.
		/// </summary>
		/// <param name="name">The feature name, which is a fully-qualified URI.</param>
		/// <returns>The requested value for the feature.</returns>
		public virtual bool getFeature(string name)
		{
			switch (name)
			{
				case "http://xml.org/sax/features/namespaces":
				{
					try
					{
						return this.NamespaceAllowed;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				case "http://xml.org/sax/features/namespace-prefixes":
				{
					try
					{
						return this.NamespaceAllowed;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				case "http://xml.org/sax/features/validation":
				{
					try
					{
						return this.isValidating;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				default:
					throw new ManagerNotRecognizedException("The specified feature: " + name +" are not supported");
			}
		}

		/// <summary>
		/// Emulates the behavior of a SAX parsers. Sets the value of a property.
		/// </summary>
		/// <param name="name">The property name, which is a fully-qualified URI.</param>
		/// <param name="value">The requested value for the property.</param>
		public virtual void setProperty(string name, System.Object value)
		{
			switch (name)
			{
				case "http://xml.org/sax/properties/lexical-handler":
				{
					try
					{
						lexical = (XmlSaxLexicalHandler) value;
						break;
					}
					catch (System.Exception e)
					{
						throw new ManagerNotSupportedException("The property is not supported as an internal exception was thrown when trying to set it: " + e.Message);
					}
				}
				default:
					throw new ManagerNotRecognizedException("The specified feature: " + name + " is not recognized");
			}
		}

		/// <summary>
		/// Emulates the behavior of a SAX parsers. Gets the value of a property.
		/// </summary>
		/// <param name="name">The property name, which is a fully-qualified URI.</param>
		/// <returns>The requested value for the property.</returns>
		public virtual System.Object getProperty(string name)
		{
			switch (name)
			{
				case "http://xml.org/sax/properties/lexical-handler":
				{
					try
					{
						return this.lexical;
					}
					catch
					{
						throw new ManagerNotSupportedException("The specified operation was not performed");
					}
				}
				default:
					throw new ManagerNotRecognizedException("The specified feature: " + name + " are not supported");
			}
		}

		/// <summary>
		/// Emulates the behavior of a SAX parser, it realizes the callback events of the parser.
		/// </summary>
		private void DoParsing()
		{
			System.Collections.Hashtable prefixes = new System.Collections.Hashtable();
			System.Collections.Stack stackNameSpace = new System.Collections.Stack();
			locator = new XmlSaxLocatorImpl();
			try 
			{
				UpdateLocatorData(this.locator, (System.Xml.XmlTextReader) (this.reader.Reader));
				if (this.callBackHandler != null)
					this.callBackHandler.setDocumentLocator(locator);
				if (this.callBackHandler != null)
					this.callBackHandler.startDocument();
				while (this.reader.Read())
				{
					UpdateLocatorData(this.locator, (System.Xml.XmlTextReader) (this.reader.Reader));
					switch (this.reader.NodeType)
					{
						case System.Xml.XmlNodeType.Element:
							bool Empty = reader.IsEmptyElement;
							string namespaceURI = "";
							string localName = "";
							if (this.namespaceAllowed)
							{
								namespaceURI = reader.NamespaceURI;
								localName = reader.LocalName;
							}
							string name = reader.Name;
							SaxAttributesSupport attributes = new SaxAttributesSupport();
							if (reader.HasAttributes)
							{
								for (int i = 0; i < reader.AttributeCount; i++)
								{
									reader.MoveToAttribute(i);
									string prefixName = (reader.Name.IndexOf(":") > 0) ? reader.Name.Substring(reader.Name.IndexOf(":") + 1, reader.Name.Length - reader.Name.IndexOf(":") - 1) : "";
									string prefix = (reader.Name.IndexOf(":") > 0) ? reader.Name.Substring(0, reader.Name.IndexOf(":")) : reader.Name;
									bool IsXmlns = prefix.ToLower().Equals("xmlns");
									if (this.namespaceAllowed)
									{
										if (!IsXmlns)
											attributes.Add(reader.NamespaceURI, reader.LocalName, reader.Name, "" + reader.NodeType, reader.Value);
									}
									else
										attributes.Add("", "", reader.Name, "" + reader.NodeType, reader.Value);
									if (IsXmlns)
									{
										string namespaceTemp = "";
										namespaceTemp = (namespaceURI.Length == 0) ? reader.Value : namespaceURI;
										if (this.namespaceAllowed && !prefixes.ContainsKey(namespaceTemp) && namespaceTemp.Length > 0 )
										{
											stackNameSpace.Push(name);
											System.Collections.Stack namespaceStack = new System.Collections.Stack();
											namespaceStack.Push(prefixName);
											prefixes.Add(namespaceURI, namespaceStack);
											if (this.callBackHandler != null)
												((XmlSaxContentHandler) this.callBackHandler).startPrefixMapping(prefixName, namespaceTemp);
										}
										else
										{
											if (this.namespaceAllowed && namespaceTemp.Length > 0  && !((System.Collections.Stack) prefixes[namespaceTemp]).Contains(reader.Name))
											{
												((System.Collections.Stack) prefixes[namespaceURI]).Push(prefixName);
												if (this.callBackHandler != null)
													((XmlSaxContentHandler) this.callBackHandler).startPrefixMapping(prefixName, reader.Value);
											}
										}
									}
								}
							}
							if (this.callBackHandler != null)
								this.callBackHandler.startElement(namespaceURI, localName, name, attributes);
							if (Empty)
							{
								if (this.NamespaceAllowed)
								{
									if (this.callBackHandler != null)
										this.callBackHandler.endElement(namespaceURI, localName, name);
								}
								else
									if (this.callBackHandler != null)
									this.callBackHandler.endElement("", "", name);
							}
							break;

						case System.Xml.XmlNodeType.EndElement:
							if (this.namespaceAllowed)
							{
								if (this.callBackHandler != null)
									this.callBackHandler.endElement(reader.NamespaceURI, reader.LocalName, reader.Name);
							}
							else
								if (this.callBackHandler != null)
								this.callBackHandler.endElement("", "", reader.Name);
							if (this.namespaceAllowed && prefixes.ContainsKey(reader.NamespaceURI) && ((System.Collections.Stack) stackNameSpace).Contains(reader.Name))
							{
								stackNameSpace.Pop();
								System.Collections.Stack namespaceStack = (System.Collections.Stack) prefixes[reader.NamespaceURI];
								while (namespaceStack.Count > 0)
								{
									string tempString = (string) namespaceStack.Pop();
									if (this.callBackHandler != null )
										((XmlSaxContentHandler) this.callBackHandler).endPrefixMapping(tempString);
								}
								prefixes.Remove(reader.NamespaceURI);
							}
							break;

						case System.Xml.XmlNodeType.Text:
							if (this.callBackHandler != null)
								this.callBackHandler.characters(reader.Value.ToCharArray(), 0, reader.Value.Length);
							break;

						case System.Xml.XmlNodeType.Whitespace:
							if (this.callBackHandler != null)
								this.callBackHandler.ignorableWhitespace(reader.Value.ToCharArray(), 0, reader.Value.Length);
							break;

						case System.Xml.XmlNodeType.ProcessingInstruction:
							if (this.callBackHandler != null)
								this.callBackHandler.processingInstruction(reader.Name, reader.Value);
							break;

						case System.Xml.XmlNodeType.Comment:
							if (this.lexical != null)
								this.lexical.comment(reader.Value.ToCharArray(), 0, reader.Value.Length);
							break;

						case System.Xml.XmlNodeType.CDATA:
							if (this.lexical != null)
							{
								lexical.startCDATA();
								if (this.callBackHandler != null)
									this.callBackHandler.characters(this.reader.Value.ToCharArray(), 0, this.reader.Value.ToCharArray().Length);
								lexical.endCDATA();
							}
							break;

						case System.Xml.XmlNodeType.DocumentType:
							if (this.lexical != null)
							{
								string lname = this.reader.Name;
								string systemId = null;
								if (this.reader.Reader.AttributeCount > 0)
									systemId = this.reader.Reader.GetAttribute(0);
								this.lexical.startDTD(lname, null, systemId);
								this.lexical.startEntity("[dtd]");
								this.lexical.endEntity("[dtd]");
								this.lexical.endDTD();
							}
							break;
					}
				}
				if (this.callBackHandler != null)
					this.callBackHandler.endDocument();
			}
			catch (System.Xml.XmlException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified file and process the events over the specified handler.
		/// </summary>
		/// <param name="filepath">The file to be used.</param>
		/// <param name="handler">The handler that manages the parser events.</param>
		public virtual void parse(System.IO.FileInfo filepath, XmlSaxContentHandler handler)
		{
			try
			{
				if (handler is XmlSaxDefaultHandler)
				{
					this.errorHandler = (XmlSaxDefaultHandler) handler;
					this.entityResolver =  (XmlSaxDefaultHandler) handler;
				}
				if (!(this is XmlSaxParserAdapter))
					this.callBackHandler = handler;
				else
				{
					if(this.callBackHandler == null)
						this.callBackHandler = handler;
				}
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(filepath.OpenRead());
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = filepath.FullName;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified file path and process the events over the specified handler.
		/// </summary>
		/// <param name="filepath">The path of the file to be used.</param>
		/// <param name="handler">The handler that manage the parser events.</param>
		public virtual void parse(string filepath, XmlSaxContentHandler handler)
		{
			try
			{
				if (handler is XmlSaxDefaultHandler)
				{
					this.errorHandler = (XmlSaxDefaultHandler) handler;
					this.entityResolver =  (XmlSaxDefaultHandler) handler;
				}
				if (!(this is XmlSaxParserAdapter))
					this.callBackHandler = handler;
				else
				{
					if(this.callBackHandler == null)
						this.callBackHandler = handler;
				}
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(filepath);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = filepath;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified stream and process the events over the specified handler.
		/// </summary>
		/// <param name="stream">The stream with the XML.</param>
		/// <param name="handler">The handler that manage the parser events.</param>
		public virtual void parse(System.IO.Stream stream, XmlSaxContentHandler handler)
		{
			try
			{
				if (handler is XmlSaxDefaultHandler)
				{
					this.errorHandler = (XmlSaxDefaultHandler) handler;
					this.entityResolver =  (XmlSaxDefaultHandler) handler;
				}
				if (!(this is XmlSaxParserAdapter))
					this.callBackHandler = handler;
				else
				{
					if(this.callBackHandler == null)
						this.callBackHandler = handler;
				}
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(stream);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = null;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified stream and process the events over the specified handler, and resolves the 
		/// entities with the specified URI.
		/// </summary>
		/// <param name="stream">The stream with the XML.</param>
		/// <param name="handler">The handler that manage the parser events.</param>
		/// <param name="URI">The namespace URI for resolve external etities.</param>
		public virtual void parse(System.IO.Stream stream, XmlSaxContentHandler handler, string URI)
		{
			try
			{
				if (handler is XmlSaxDefaultHandler)
				{
					this.errorHandler = (XmlSaxDefaultHandler) handler;
					this.entityResolver =  (XmlSaxDefaultHandler) handler;
				}
				if (!(this is XmlSaxParserAdapter))
					this.callBackHandler = handler;
				else
				{
					if(this.callBackHandler == null)
						this.callBackHandler = handler;
				}
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(URI, stream);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = null;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified 'XmlSourceSupport' instance and process the events over the specified handler, 
		/// and resolves the entities with the specified URI.
		/// </summary>
		/// <param name="source">The 'XmlSourceSupport' that contains the XML.</param>
		/// <param name="handler">The handler that manages the parser events.</param>
		public virtual void parse(XmlSourceSupport source, XmlSaxContentHandler handler)
		{
			if (source.Characters != null)
				parse(source.Characters.BaseStream, handler);
			else
			{
				if (source.Bytes != null)
					parse(source.Bytes, handler);
				else
				{
					if(source.Uri != null)
						parse(source.Uri, handler);
					else
						throw new System.Xml.XmlException("The XmlSource class can't be null");
				}
			}
		}

		/// <summary>
		/// Parses the specified file and process the events over previously specified handler.
		/// </summary>
		/// <param name="filepath">The file with the XML.</param>
		public virtual void parse(System.IO.FileInfo filepath)
		{
			try
			{
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(filepath.OpenRead());
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = filepath.FullName;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified file path and processes the events over previously specified handler.
		/// </summary>
		/// <param name="filepath">The path of the file with the XML.</param>
		public virtual void parse(string filepath)
		{
			try
			{
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(filepath);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = filepath;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified stream and process the events over previusly specified handler.
		/// </summary>
		/// <param name="stream">The stream with the XML.</param>
		public virtual void parse(System.IO.Stream stream)
		{
			try
			{
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(stream);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = null;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified stream and processes the events over previously specified handler, and resolves the 
		/// external entities with the specified URI.
		/// </summary>
		/// <param name="stream">The stream with the XML.</param>
		/// <param name="URI">The namespace URI for resolve external etities.</param>
		public virtual void parse(System.IO.Stream stream, string URI)
		{
			try
			{
				System.Xml.XmlTextReader tempTextReader = new System.Xml.XmlTextReader(URI, stream);
				System.Xml.XmlValidatingReader tempValidatingReader = new System.Xml.XmlValidatingReader(tempTextReader);
				parserFileName = null;
				tempValidatingReader.ValidationType = (this.isValidating) ? System.Xml.ValidationType.DTD : System.Xml.ValidationType.None;
				tempValidatingReader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandle);
				this.reader = tempValidatingReader;
				this.DoParsing();
			}
			catch (System.Xml.XmlException e)
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(e);
				throw e;
			}
		}

		/// <summary>
		/// Parses the specified 'XmlSourceSupport' and processes the events over the specified handler, and 
		/// resolves the entities with the specified URI.
		/// </summary>
		/// <param name="source">The 'XmlSourceSupport' instance with the XML.</param>
		public virtual void parse(XmlSourceSupport source)
		{
			if (source.Characters != null)
				parse(source.Characters.BaseStream);
			else
			{
				if (source.Bytes != null)
					parse(source.Bytes);
				else
				{
					if (source.Uri != null)
						parse(source.Uri);
					else
						throw new System.Xml.XmlException("The XmlSource class can't be null");
				}
			}
		}

		/// <summary>
		/// Manages all the exceptions that were thrown when the validation over XML fails.
		/// </summary>
		public void ValidationEventHandle(System.Object sender, System.Xml.Schema.ValidationEventArgs args)
		{
			System.Xml.Schema.XmlSchemaException tempException = args.Exception;
			if (args.Severity == System.Xml.Schema.XmlSeverityType.Warning)
			{
				if (this.errorHandler != null)
					this.errorHandler.warning(new System.Xml.XmlException(tempException.Message, tempException, tempException.LineNumber, tempException.LinePosition));
			}
			else
			{
				if (this.errorHandler != null)
					this.errorHandler.fatalError(new System.Xml.XmlException(tempException.Message, tempException, tempException.LineNumber, tempException.LinePosition));
			}
		}
				
		/// <summary>
		/// Assigns the object that will handle all the content events.
		/// </summary>
		/// <param name="handler">The object that handles the content events.</param>
		public virtual void setContentHandler(XmlSaxContentHandler handler)
		{
			if (handler != null)
				this.callBackHandler = handler;
			else
				throw new System.Xml.XmlException("Error assigning the Content handler: a null Content Handler was received");
		}

		/// <summary>
		/// Assigns the object that will handle all the error events. 
		/// </summary>
		/// <param name="handler">The object that handles the errors events.</param>
		public virtual void setErrorHandler(XmlSaxErrorHandler handler)
		{
			if (handler != null)
				this.errorHandler = handler;
			else
				throw new System.Xml.XmlException("Error assigning the Error handler: a null Error Handler was received");
		}

		/// <summary>
		/// Obtains the object that will handle all the content events.
		/// </summary>
		/// <returns>The object that handles the content events.</returns>
		public virtual XmlSaxContentHandler getContentHandler()
		{
			return this.callBackHandler;
		}

		/// <summary>
		/// Assigns the object that will handle all the error events. 
		/// </summary>
		/// <returns>The object that handles the error events.</returns>
		public virtual XmlSaxErrorHandler getErrorHandler()
		{
			return this.errorHandler;
		}

		/// <summary>
		/// Returns the current entity resolver.
		/// </summary>
		/// <returns>The current entity resolver, or null if none has been registered.</returns>
		public virtual XmlSaxEntityResolver getEntityResolver()
		{
			return this.entityResolver;
		}

		/// <summary>
		/// Allows an application to register an entity resolver.
		/// </summary>
		/// <param name="resolver">The entity resolver.</param>
		public virtual void setEntityResolver(XmlSaxEntityResolver resolver)
		{
			this.entityResolver = resolver;
		}
	}

	/*******************************/
	/// <summary>
	/// Manages the source XML document that will be transformed.
	/// </summary>
	public class GenericSourceSupport:BasicSourceSupport
	{
		/// <summary>
		/// Represents the type of the source.
		/// </summary>
		public enum TYPE {Uri, Stream, Reader, File}

		private string id;
		private string path;
		private System.IO.StreamReader reader;
		private System.IO.Stream stream;
		private TYPE type;

		/// <summary>
		/// Gets and sets the Id of the GenericSourceSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
				this.path = value;
				this.reader = null;
				this.stream = null;
				this.type = TYPE.Uri;
			}
		}

		/// <summary>
		/// Gets and sets the type of the instance.
		/// </summary>
		public virtual TYPE Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		/// <summary>
		/// Gets and sets the path of the GenericSourceSupport instance.
		/// </summary>
		public virtual string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;				
			}
		}

		/// <summary>
		/// Get and sets the stream of the instance.
		/// </summary>
		public virtual System.IO.Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{				
				this.stream = value;
				this.type = TYPE.Stream;
			}
		}

		/// <summary>
		/// Get and sets the stream of the instance.
		/// </summary>
		public virtual System.IO.StreamReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{				
				this.reader = value;
				this.type = TYPE.Reader;
			}
		}


		/// <summary>
		/// Creates a new instance from an URI.
		/// </summary>
		/// <param name="Uri"></param>
		public GenericSourceSupport(string Uri)
		{
			this.id = Uri;
			this.path = Uri;
			this.reader = null;
			this.stream = null;
			this.type = TYPE.Uri;
		}

		/// <summary>
		/// Creates a new instance form the specified StreamReader with the specified Id.
		/// </summary>
		/// <param name="reader">The StreamReader instance with the XML.</param>
		/// <param name="Id">The Id of the instance.</param>
		public GenericSourceSupport(System.IO.StreamReader Reader, string Id)
		{
			this.id = Id;
			this.path = "";
			this.reader = Reader;
			this.stream = reader.BaseStream;
			this.type = TYPE.Stream;
		}

		/// <summary>
		/// Creates a new instance from the specified StreamReader.
		/// </summary>
		/// <param name="reader">The StreamReader instance with the XML.</param>
		public GenericSourceSupport(System.IO.StreamReader Reader)
		{
			this.id = null;
			this.path = "";
			this.reader = Reader;
			this.stream = reader.BaseStream;
			this.type = TYPE.Stream;
		}

		/// <summary>
		/// Creates a new instance from the specified Stream with the specified Id.
		/// </summary>
		/// <param name="stream">The Stream instance with the XML.</param>
		/// <param name="Id">The Id of the instance.</param>
		public GenericSourceSupport(System.IO.Stream Stream, string Id)
		{
			this.id = Id;
			this.path = "";
			this.reader = null;
			this.stream = Stream;
			this.type = TYPE.Stream;
		}

		/// <summary>
		/// Creates a new instance from the specified Stream.
		/// </summary>
		/// <param name="stream">The Stream intance with the XML.</param>
		public GenericSourceSupport(System.IO.Stream Stream)
		{
			this.id = null;
			this.path = "";
			this.stream = Stream;
			this.reader = null;
			this.type = TYPE.Stream;
		}

		/// <summary>
		/// Creates a new instance from a FileInfo.
		/// </summary>
		/// <param name="file">The fileInfo instance with the XML.</param>
		public GenericSourceSupport(System.IO.FileInfo File)
		{
			this.id = File.FullName;
			this.path = File.FullName;
			this.reader = null;
			this.stream = null;
			this.type = TYPE.File;
		}

		/// <summary>
		/// Basic Constructor for the class.
		/// </summary>
		public GenericSourceSupport()
		{
			this.id = null;
			this.path = "";
			this.reader = null;
			this.stream = null;
		}
	}

	/*******************************/
	/// <summary>
	/// Manages the target where the result of the transformation will be saved.
	/// </summary>
	public class GenericResultSupport:BasicResultSupport
	{  
		/// <summary>
		/// Represents the type of the result.
		/// </summary>
		public enum TYPE {Uri, Stream, Writer, File, Null}

		private string id;
		private string name;
		private System.IO.StreamWriter writer;
		private System.IO.Stream stream;
		private TYPE type;
		
		/// <summary>
		/// Basic Constructor for the class.
		/// </summary>
		public GenericResultSupport()
		{
			id = null;
			name = "";
			writer = null;
			stream = null;
			type = TYPE.Null;
		}

		/// <summary>
		///  Gets and sets the Id of the GenericResultSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
				this.name = value;
				this.writer = null;
				this.stream = null;
				this.type = TYPE.Uri;
			}
		}

		/// <summary>
		/// Gets and sets the StreamWriter associated to the instance.
		/// </summary>
		public virtual System.IO.StreamWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.id = null;
				this.name = "";
				this.writer = value;
				this.type = TYPE.Writer;
			}
		}

		/// <summary>
		/// Gets and sets the Stream associated to the instance.
		/// </summary>
		public virtual System.IO.Stream Stream 
		{
			get 
			{
				return this.stream;
			}
			set
			{
				this.id = null;
				this.name = "";
				this.stream = value;
				this.type = TYPE.Stream;
			}
		}

		/// <summary>
		/// Gets and sets the type of the instance.
		/// </summary>
		public virtual TYPE Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		/// <summary>
		/// Creates a new instance from the specified Uri.
		/// </summary>
		/// <param name="Uri">The Uri to be used.</param>
		public GenericResultSupport(string Uri)
		{
			this.id = Uri;
			this.name = Uri;
			this.writer = null;
			this.stream = null;
			this.type = TYPE.Uri;
		}

		/// <summary>
		/// Creates a new instance from the specified StreamWriter.
		/// </summary>
		/// <param name="writer">The StreamWriter instance to be used.</param>
		public GenericResultSupport(System.IO.StreamWriter Writer)
		{
			this.id = null;
			this.name = "";
			this.writer = Writer;
			this.stream = null;
			this.type = TYPE.Writer;
		}

		/// <summary>
		/// Creates a new instance from the specified Stream.
		/// </summary>
		/// <param name="stream">The Stream instance to be used.</param>
		public GenericResultSupport(System.IO.Stream Stream)
		{
			this.id = null;
			this.name = "";
			this.writer = new System.IO.StreamWriter(Stream);
			this.stream = Stream;
			this.type = TYPE.Stream;
		}

		/// <summary>
		/// Creates a new instance from the specified FileInfo.
		/// </summary>
		/// <param name="file">The FileInfo instance to be used.</param>
		public GenericResultSupport(System.IO.FileInfo File)
		{
			this.id = File.FullName;
			this.name = File.FullName;
			this.writer = null;
			this.stream = null;
			this.type = TYPE.File;
		}
	}
	/*******************************/
	/// <summary>
	/// Manages the source XML document that will be transformed.
	/// </summary>
	public class DomSourceSupport:BasicSourceSupport
	{
		private string id;
		private System.Xml.XmlNode node;

		/// <summary>
		/// Gets and sets the Id of the DomSourceSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>
		/// Gets and sets the XmlNode instance associated to the DomSourceSupport.
		/// </summary>
		public virtual System.Xml.XmlNode Node
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		/// <summary>
		/// Creates a new instance from a XmlNode with the specified Id.
		/// </summary>
		/// <param name="node">The XmlNode instance with the XML.</param>
		/// <param name="Id">The Id of the instance.</param>
		public DomSourceSupport(System.Xml.XmlNode Node, string Id)
		{
			this.id = Id;
			this.node = Node;
		}

		/// <summary>
		/// Creates a new instance from a XmlNode instance.
		/// </summary>
		/// <param name="node">The XmlNode instance to be used.</param>
		public DomSourceSupport(System.Xml.XmlNode Node)
		{
			this.id = "";
			this.node = Node;
		}
	}


	/*******************************/
	/// <summary>
	/// Manages the target where the result of the tranformation will be saved.
	/// </summary>
	public class DomResultSupport:BasicResultSupport
	{
		private string id;
		private System.Xml.XmlNode node;

		/// <summary>
		///  Gets and sets the Id of the GenericResultSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>
		/// Gets and sets the Document for the GenericResultSupport instance.
		/// </summary>
		public virtual System.Xml.XmlNode Node
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		/// <summary>
		/// Creates a new instance of DomSourceSupport with the specified XmlDocument instance and the 
		/// specified Id.
		/// </summary>
		/// <param name="Document">The XmlDocument instance for the class.</param>
		/// <param name="Id">The Id for the DomSourceSupport.</param>
		public DomResultSupport(System.Xml.XmlNode Node, string Id)
		{
			this.id = Id;
			this.node = Node;
		}

		/// <summary>
		/// Creates a new instance of DomSourceSupport with the specified XmlDocument instance.
		/// </summary>
		/// <param name="Document">The XmlDocument instance for the class.</param>
		public DomResultSupport(System.Xml.XmlNode Node)
		{
			this.id = "";
			this.node = Node;
		}
	}


	/*******************************/
	/// <summary>
	/// Manages the source XML document that will be transformed.
	/// </summary>
	public class SaxSourceSupport:BasicSourceSupport
	{
		private string id;
		private XmlSourceSupport source;
		private XmlSAXDocumentManager reader;		

		/// <summary>
		/// Gets and sets the Id of the SaxSourceSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
				if (this.source == null)
					this.source = new XmlSourceSupport(this.id);
			}
		}

		/// <summary>
		/// Gets and sets the XmlSourceSupport of the SaxSourceSupport instance.
		/// </summary>
		public virtual XmlSourceSupport Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>
		/// Gets and sets the XmlSAXDocumentManager of the SaxSourceSupport instance.
		/// </summary>
		public virtual XmlSAXDocumentManager Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		/// <summary>
		/// Creates a new instance of SaxSourceSupport with the specified XmlSAXDocumentManager and 
		/// XmlSourceSupport.
		/// </summary>
		/// <param name="Reader">The XmlSAXDocumentManager used to parse the XML.</param>
		/// <param name="Source">The XmlSourceSupport with the XML.</param>
		public SaxSourceSupport(XmlSAXDocumentManager Reader, XmlSourceSupport Source)
		{
			this.id = (Source.Uri != null) ? Source.Uri : "";
			this.source = Source;
			this.reader = Reader;
		}

		/// <summary>
		/// Creates a new instance of SaxSourceSupport with the specified XmlSource support.
		/// </summary>
		/// <param name="Source">The XmlSourceSupport with the XML.</param>
		public SaxSourceSupport(XmlSourceSupport Source)
		{
			this.id = (Source.Uri != null) ? Source.Uri : "";
			this.source = Source;
			this.reader = new XmlSAXDocumentManager();
		}

		/// <summary>
		/// Creates an empty new instance of SaxSourceSupport.
		/// </summary>	
		public SaxSourceSupport()
		{
			this.id = "";		
			this.source = null;
			this.reader = null;
		}

		/// <summary>
		/// Creates a new instance of XmlSourceSupport from a BasicSourceSupport class.
		/// </summary>
		/// <param name="Source">The BasicSourceSupportClass to be used.</param>
		/// <returns>A new instance of XmlSourceSupport class.</returns>
		public static XmlSourceSupport FromGenericSource(BasicSourceSupport Source)
		{			
			XmlSourceSupport result = null;
			if(Source is GenericSourceSupport)
			{
				GenericSourceSupport temp_Source = (GenericSourceSupport)Source;
				if (temp_Source.Path != null)
					result = new XmlSourceSupport(temp_Source.Path);
				else
				{
					if (temp_Source.Reader != null)
						result = new XmlSourceSupport(temp_Source.Reader);
					else
						if (temp_Source.Stream != null)
						result = new XmlSourceSupport(temp_Source.Stream);
				}
			}			
			else
			{
				if(Source is SaxSourceSupport)
				{
					result = ((SaxSourceSupport)Source).Source;
				}

			}
			return result;
		}
	}


	/*******************************/
	/// <summary>
	/// Manages the target where the result of the transformation will be saved.
	/// </summary>
	public class SaxResultSupport:BasicResultSupport
	{
		private string id;
		private XmlSaxContentHandler handler;
		private XmlSaxLexicalHandler lexHandler;
		
		/// <summary>
		/// Basic Constructor for the class.
		/// </summary>
		public SaxResultSupport(){
			id = "";
			handler = null;
			lexHandler = null;
		}

		/// <summary>
		///  Gets and sets the Id of the SaxResultSupport instance.
		/// </summary>
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>
		///  Gets and sets the XmlSaxContentHandler of the SaxResultSupport instance.
		/// </summary>
		public virtual XmlSaxContentHandler Handler
		{
			get
			{
				return this.handler;
			}
			set
			{
				this.handler = value;
			}
		}

		/// <summary>
		///  Gets and sets the XmlSaxLexicalHandler of the SaxResultSupport instance.
		/// </summary>
		public virtual XmlSaxLexicalHandler LexHandler
		{
			get
			{
				return this.lexHandler;
			}
			set
			{
				this.lexHandler = value;
			}
		}

		/// <summary>
		/// Creates a new instance of SaxResultSupport, with the specified XmlSaxContentHandler.
		/// </summary>
		/// <param name="Handler">The XmlSaxContentHandler to be used.</param>
		public SaxResultSupport(XmlSaxContentHandler Handler)
		{
			this.handler = Handler;
		}
	}


	/*******************************/
	/// <summary>
	/// Supports the Errors thrown by the TransformerSupport class.
	/// </summary>
	public interface XsltExceptionManager
	{
		/// <summary>
		/// Manages when an exception was thrown in the Transform operation of the TranformerSupport class.
		/// </summary>
		/// <param name="Exception">The exception thrown by the TransformerSupport instance.</param>
		void Error(System.Xml.Xsl.XsltException Exception);

		/// <summary>
		/// This method is not supported only was created for compatibility.
		/// </summary>
		void FatalError(System.Xml.Xsl.XsltException Exception);

		/// <summary>
		/// This method is not supported only was created for compatibility.
		/// </summary>
		void Warning(System.Xml.Xsl.XsltException Exception);
	}


	/*******************************/
	/// <summary>
	/// Inteface that resolves the URI of external resources in transformations.
	/// </summary>
	public interface TransformerResolver
	{
		BasicSourceSupport ResolveURI(string hrefUri, string baseUri);		
	}

	/// <summary>
	/// Resolves the URI of external resources in transformations.
	/// </summary>
	public class DefaultTransformerResolver : System.Xml.XmlUrlResolver
	{
		private TransformerResolver uriResolver;

		public TransformerResolver UriResolver
		{
			get
			{
				return this.uriResolver;
			}
			set
			{
				this.uriResolver = value;
			}
		}
		
		private BasicSourceSupport style;
		public BasicSourceSupport Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}	
		

		/// <summary>
		/// Resolves URI of parent and calls user implementation method.
		/// </summary>
		public override System.Uri ResolveUri(System.Uri BaseUri, string relativeUri)
		{
			string baseUri = null;

			if( BaseUri != null )
				baseUri = BaseUri.AbsolutePath;
			else
				baseUri = this.style.Id;

			if( this.uriResolver != null )
			{
				BasicSourceSupport resolvedSource = this.uriResolver.ResolveURI( relativeUri, baseUri );
				System.Uri resolvedUri = null;

				if( BaseUri != null )
					resolvedUri = new System.Uri( BaseUri, resolvedSource.Id );
				else
					resolvedUri = new System.Xml.XmlUrlResolver().ResolveUri(null, resolvedSource.Id);

				return resolvedUri;
			}

			else
				return new System.Xml.XmlUrlResolver().ResolveUri( BaseUri, relativeUri );
		}
	}
	/*******************************/
	/// <summary>
	/// Supports the XSL transformations.
	/// </summary>
	public class TransformerSupport
	{
		private System.Xml.Xsl.XsltArgumentList Parameters = null;
		private TransformerResolver resolverFactory = null;
		private TransformerResolver resolverTransformer = null;		

		private System.Xml.XmlResolver DefaultResolverFactory=null;
		private System.Xml.XmlResolver DefaultResolverTransformer=null;

		private XsltExceptionManager errorListenerFactory = null;
		private XsltExceptionManager errorListenerTransformer = null;
		private System.Xml.Xsl.XslTransform Transformer = null;
		private BasicSourceSupport Stylesheet = null;

		/// <summary>
		/// The constructor of the support class.
		/// </summary>
		public TransformerSupport()
		{
			this.Transformer = new System.Xml.Xsl.XslTransform();			
			this.errorListenerFactory = new DefaultXsltExceptionManager();
			this.errorListenerTransformer = new DefaultXsltExceptionManager();
			this.ResolverFactory=null;
		}

		/// <summary>
		/// A static constructor for this class.
		/// </summary>
		/// <returns>A new instance of tranformer support.</returns>
		public static TransformerSupport NewInstance()
		{
			return new TransformerSupport();
		}

		/// <summary>
		/// Creates a new instance of TransformerSupport and sets it ResolverFactory and errorListenerFactory.
		/// </summary>
		/// <param name="transformer">Instance of TransformerSupport to get its attributes to create the new 
		/// instance.</param>
		/// <returns>The new instance.</returns>
		public static TransformerSupport NewTransform(TransformerSupport transformer)
		{
			TransformerSupport temp = new TransformerSupport();
			temp.ResolverFactory = transformer.ResolverFactory;			
			temp.errorListenerFactory = transformer.errorListenerFactory;
			return temp;
		}

		/// <summary>
		/// Clears the parameters asociated with the XslTransform instance.
		/// </summary>
		public void ClearArguments()
		{
			Parameters.Clear();
			Parameters = null;
		}		

		/// <summary>
		/// Gets and sets the class that manages the errors reported by the XslTransform.
		/// </summary>
		public XsltExceptionManager ErrorListenerFactory
		{
			get
			{
				return this.errorListenerFactory;
			}
			set
			{
				if(value != null)
					this.errorListenerFactory = value;
				else
					throw new System.ArgumentException("Error assigning the error listener: a null ErrorListener was received");
			}
		}
		
		/// <summary>
		/// Gets and sets the class that manages the errors reported by the XslTransform.
		/// </summary>
		public XsltExceptionManager ErrorListenerTransformer
		{
			get
			{
				return this.errorListenerTransformer;
			}
			set
			{
				if(value != null)
					this.errorListenerTransformer = value;
				else
					throw new System.ArgumentException("Error assigning the error listener: a null ErrorListener was received");
			}
		}

		/// <summary>
		/// Gets and sets the class that is used to resolve externs references.
		/// </summary>
		public TransformerResolver ResolverTransformer
		{
			get
			{
				return this.resolverTransformer;
			}
			set
			{
				this.resolverTransformer = value;
				this.DefaultResolverTransformer = new DefaultTransformerResolver();
				((DefaultTransformerResolver)DefaultResolverTransformer).UriResolver = this.resolverTransformer;				
			}
		}

		/// <summary>
		/// Gets and sets the class that is used to resolve externs references in load methods.
		/// </summary>
		public TransformerResolver ResolverFactory
		{
			get
			{
				return this.resolverFactory;
			}
			set
			{
				this.resolverFactory = value;
				this.DefaultResolverFactory = new DefaultTransformerResolver();
				((DefaultTransformerResolver)DefaultResolverFactory).UriResolver = this.resolverFactory;
				if(this.ResolverTransformer == null)
					this.ResolverTransformer = value;
			}
		}

		/// <summary>
		/// Loads the StyleSheet that will be used for the transformations operations.
		/// </summary>
		/// <param name="StyleSheet">A XPathDocument that contains the StyleSheet.</param>
		public void Load(System.Xml.XPath.XPathDocument StyleSheet)
		{
			try
			{
				Transformer.Load(StyleSheet, DefaultResolverFactory);
				this.Stylesheet = new GenericSourceSupport();
			}
			catch (System.Xml.XmlException exception)
			{
				throw new System.Xml.Xsl.XsltException(exception.Message, exception);
			}
			catch (System.Xml.Xsl.XsltException exception)
			{
				throw exception;
			}
		}

		/// <summary>
		/// Emulates the behavior of Templates creating a XPathDocument instance.
		/// </summary>
		/// <param name="Source">The source with the Xsl.</param>
		/// <returns>A XPathDocument instance that could be used as Templates.</returns>
		public System.Xml.XPath.XPathDocument ToXPathDocument(BasicSourceSupport Source)
		{
			System.Xml.XPath.XPathDocument Template = null;
			if (Source is GenericSourceSupport)
			{
				GenericSourceSupport Generic = (GenericSourceSupport)Source;
				switch(Generic.Type)
				{
					case GenericSourceSupport.TYPE.Uri:
					case GenericSourceSupport.TYPE.File:
						Template = new System.Xml.XPath.XPathDocument(Generic.Path);
						break;
					case GenericSourceSupport.TYPE.Stream:
						Template = new System.Xml.XPath.XPathDocument(Generic.Stream);
						break;
					case GenericSourceSupport.TYPE.Reader:
						Template = new System.Xml.XPath.XPathDocument(Generic.Reader);
						break;
					default:
						break;
				}
			}
			else
			{
				if (Source is DomSourceSupport)
				{
					string temp_String = ((DomSourceSupport) Source).Node.OuterXml;
					Template = new System.Xml.XPath.XPathDocument(new System.Xml.XmlTextReader(new System.IO.StringReader(temp_String)));
				}
				else
				{
					if (Source is SaxSourceSupport)
					{
						XmlSourceSupport XmlSource = ((SaxSourceSupport) Source).Source;
						if (XmlSource.Characters != null)
							Template = new System.Xml.XPath.XPathDocument(XmlSource.Characters);
						else
						{
							if (XmlSource.Bytes != null)
								Template = new System.Xml.XPath.XPathDocument(XmlSource.Bytes);
							else
							{
								if (XmlSource.Uri != null)
									Template = new System.Xml.XPath.XPathDocument(XmlSource.Uri);
							}
						}
					}
				}
			}
			return Template;
		}

		/// <summary>
		/// Loads the StyleSheet that will be used for the transformations operations.
		/// </summary>
		/// <param name="StyleSheet">A GenericSourceSupport that contains the StyleSheet.</param>
		public void Load(BasicSourceSupport StyleSheet)
		{
			((DefaultTransformerResolver)DefaultResolverFactory).Style = StyleSheet;
			if (StyleSheet is GenericSourceSupport)
			{
				GenericSourceSupport Generic = (GenericSourceSupport) StyleSheet;
				try
				{
					switch(Generic.Type)
					{
						case GenericSourceSupport.TYPE.Uri:
						case GenericSourceSupport.TYPE.File:
							Transformer.Load(Generic.Path,DefaultResolverFactory);
							break;						case GenericSourceSupport.TYPE.Stream:
							Transformer.Load(new System.Xml.XmlTextReader(Generic.Stream), DefaultResolverFactory);
							break;
						case GenericSourceSupport.TYPE.Reader:
							Transformer.Load(new System.Xml.XmlTextReader(Generic.Reader), DefaultResolverFactory);
							break;
						default:
							break;
					}
					this.Stylesheet = StyleSheet;
				}
				catch (System.Xml.XmlException exception)
				{
					if (this.ErrorListenerFactory == null)
						throw new System.Xml.Xsl.XsltException(exception.Message, exception);
					else
						this.ErrorListenerFactory.FatalError(new System.Xml.Xsl.XsltException(exception.Message, exception));
				}
				catch (System.Xml.Xsl.XsltException exception)
				{
					if (this.ErrorListenerFactory == null)
						throw exception;
					else
						this.ErrorListenerFactory.FatalError(exception);
				}
			}
			else
			{
				if(StyleSheet is SaxSourceSupport)
				{
					XmlSourceSupport Source = ((SaxSourceSupport) StyleSheet).Source;
					try
					{
						if (Source.Characters != null)
							Transformer.Load(new System.Xml.XmlTextReader(Source.Characters), DefaultResolverFactory);
						else
						{
							if(Source.Bytes != null)
								Transformer.Load(new System.Xml.XmlTextReader(Source.Bytes), DefaultResolverFactory);
							else
							{
								if(Source.Uri != null)
								{
									System.IO.FileStream tmp = new System.IO.FileStream(Source.Uri,System.IO.FileMode.Open,System.IO.FileAccess.Read);
									Transformer.Load(new System.Xml.XmlTextReader(tmp),DefaultResolverFactory);
									tmp.Close();
								}
							}
						}
						this.Stylesheet = StyleSheet;
					}
					catch (System.Xml.XmlException exception)
					{
						if (this.ErrorListenerFactory == null)
							throw new System.Xml.Xsl.XsltException(exception.Message, exception);
						else
							this.ErrorListenerFactory.FatalError(new System.Xml.Xsl.XsltException(exception.Message, exception));
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						if (this.ErrorListenerFactory == null)
							throw exception;
						else
							this.ErrorListenerFactory.FatalError(exception);
					}
				}
				else
				{
					if (StyleSheet is DomSourceSupport)
					{
						try
						{
							string temp_String = ((DomSourceSupport) StyleSheet).Node.OuterXml;
							Transformer.Load(new System.Xml.XmlTextReader(new System.IO.StringReader(temp_String)), DefaultResolverFactory);
							this.Stylesheet = StyleSheet;
						}
						catch (System.Xml.XmlException exception)
						{
							if (this.ErrorListenerFactory == null)
								throw new System.Xml.Xsl.XsltException(exception.Message, exception);
							else
								this.ErrorListenerFactory.FatalError(new System.Xml.Xsl.XsltException(exception.Message, exception));
						}
						catch (System.Xml.Xsl.XsltException exception)
						{
							if (this.ErrorListenerFactory == null)
								throw exception;
							else
								this.ErrorListenerFactory.FatalError(exception);
						}
					}
				}
			}
		}


		/// <summary>
		/// Returns the value of a parameter in the parameters list.
		/// </summary>
		/// <param name="ParamName">The Name of the parameter.</param>
		/// <returns>An object that contains the value of the specified paramenter.</returns>
		public System.Object GetParameter(string ParamName)
		{
			if (Parameters != null)
				return Parameters.GetParam(ParamName, "");
			else
				return null;
		}

		/// <summary>
		/// Introduces a new parameter to the parameters list or modify the value of a parameter previously 
		/// introduced.
		/// </summary>
		/// <param name="ParamName">The name of the parameter.</param>
		/// <param name="ParamValue">An object instance with the parameter value.</param>
		public void SetParameter(string ParamName, System.Object ParamValue)
		{
			if (Parameters == null)
				Parameters = new System.Xml.Xsl.XsltArgumentList();
			Parameters.AddParam(ParamName, "", ParamValue);
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(BasicSourceSupport Source, BasicResultSupport Result)
		{
			if (Source is GenericSourceSupport)
			{
				if(Result is GenericResultSupport)
					DoTransform((GenericSourceSupport) Source, (GenericResultSupport) Result);
				else
				{
					if (Result is DomResultSupport)
						DoTransform((GenericSourceSupport) Source, (DomResultSupport) Result);
					else
						if (Result is SaxResultSupport)
							DoTransform((GenericSourceSupport) Source, (SaxResultSupport) Result);
				}
			}
			else
			{
				if (Source is DomSourceSupport)
				{
					if (Result is GenericResultSupport)
						DoTransform((DomSourceSupport) Source, (GenericResultSupport) Result);
					else
					{
						if (Result is DomResultSupport)
							DoTransform((DomSourceSupport) Source, (DomResultSupport) Result);
						else
							if (Result is SaxResultSupport)
								DoTransform((DomSourceSupport)Source,(SaxResultSupport)Result);
					}
				}
				else
				{
					if (Source is SaxSourceSupport)
					{
						if (Result is GenericResultSupport)
							DoTransform((SaxSourceSupport) Source, (GenericResultSupport) Result);
						else
						{
							if (Result is DomResultSupport)
								DoTransform((SaxSourceSupport) Source, (DomResultSupport) Result);
							else
								if (Result is SaxResultSupport)
									DoTransform((SaxSourceSupport) Source, (SaxResultSupport) Result);
						}
					}
				}
			}
		}		

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(GenericSourceSupport Source, GenericResultSupport Result)
		{
			try
			{
				System.Xml.XmlDocument SourceDocument = new System.Xml.XmlDocument();
				switch(Source.Type)
				{
					case GenericSourceSupport.TYPE.Uri:
					case GenericSourceSupport.TYPE.File:
						SourceDocument.Load(Source.Path);
						break;
					case GenericSourceSupport.TYPE.Stream:
						SourceDocument.Load(Source.Stream);
						break;
					case GenericSourceSupport.TYPE.Reader:
						SourceDocument.Load(Source.Reader);
						break;
					default:
						SourceDocument = null;
						ErrorListenerTransformer.Error(new System.Xml.Xsl.XsltException("The Xml Source can't be null", null));
						break;
				}
				if (this.Stylesheet != null)
				{
					try
					{
						switch (Result.Type)
						{
							case GenericResultSupport.TYPE.Null:
								break;
							case GenericResultSupport.TYPE.Stream:
								Transformer.Transform(SourceDocument, Parameters, Result.Stream, DefaultResolverTransformer);
								break;
							case GenericResultSupport.TYPE.File:
							case GenericResultSupport.TYPE.Uri:
								System.IO.StreamWriter Temp_Writer = new System.IO.StreamWriter(Result.Id);
								Transformer.Transform(SourceDocument, Parameters,Temp_Writer, DefaultResolverTransformer);								
								Temp_Writer.Close();
								break;
							case GenericResultSupport.TYPE.Writer:
								Transformer.Transform(SourceDocument, Parameters, Result.Writer, DefaultResolverTransformer);
								Result.Writer.Close();
								break;
						}
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
				}
				else
					ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException("A Xsl stylesheet file must be defined before transform operation", null));
			}
			catch(System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}


		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(DomSourceSupport Source, GenericResultSupport Result)
		{
			if (this.Stylesheet != null)
			{
				try
				{
					switch (Result.Type)
					{
						case GenericResultSupport.TYPE.Null:
							break;
						case GenericResultSupport.TYPE.Stream:
							Transformer.Transform(Source.Node, Parameters, Result.Stream, DefaultResolverTransformer);
							break;
						case GenericResultSupport.TYPE.File:
						case GenericResultSupport.TYPE.Uri:
							System.IO.StreamWriter Temp_Writer = new System.IO.StreamWriter(Result.Id);
							Transformer.Transform(Source.Node, Parameters, Temp_Writer, DefaultResolverTransformer);
							Temp_Writer.Close();
							break;					
						case GenericResultSupport.TYPE.Writer:
							Transformer.Transform(Source.Node, Parameters, Result.Writer, DefaultResolverTransformer);
							Result.Writer.Close();
							break;					
					}
				}
				catch (System.Xml.Xsl.XsltException exception)
				{
					ErrorListenerTransformer.FatalError(exception);
				}
				catch (System.Exception e)
				{		
					if (this.ErrorListenerTransformer == null)
						throw new System.Xml.Xsl.XsltException(e.Message, e);
					else
						this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
				}
			}
			else
			{
				if (Source.Node is System.Xml.XmlDocument)
				{
					switch (Result.Type)
					{
						case GenericResultSupport.TYPE.Null:
							break;
						case GenericResultSupport.TYPE.Stream:
							((System.Xml.XmlDocument) Source.Node).Save(Result.Stream);
							Result.Stream.Close();							
							break;
						case GenericResultSupport.TYPE.File:
						case GenericResultSupport.TYPE.Uri:
							((System.Xml.XmlDocument) Source.Node).Save(Result.Id);
							break;
						case GenericResultSupport.TYPE.Writer:
							((System.Xml.XmlDocument) Source.Node).Save(Result.Writer);
							Result.Writer.Close();
							break;
					}
				}
				else
				{
					switch (Result.Type)
					{
						case GenericResultSupport.TYPE.Null:
							break;
						case GenericResultSupport.TYPE.Stream:
							System.IO.StreamWriter Writer = new System.IO.StreamWriter(Result.Stream);
							Writer.Write(Source.Node.OuterXml);							
							Result.Stream.Close();							
							break;
						case GenericResultSupport.TYPE.File:
						case GenericResultSupport.TYPE.Uri:
							System.IO.StreamWriter Temp_Writer = new System.IO.StreamWriter(Result.Id);
							Temp_Writer.Write(Source.Node.OuterXml);
							Temp_Writer.Close();
							break;
						case GenericResultSupport.TYPE.Writer:
							Result.Writer.Write(Source.Node.OuterXml);
							Result.Writer.Close();
							break;
					}
				}
			}
		}


		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(DomSourceSupport Source, DomResultSupport Result)
		{
			System.IO.MemoryStream tempStream = new System.IO.MemoryStream();			
			try
			{
				if (this.Stylesheet != null)
				{
					try
					{
						Transformer.Transform(Source.Node, Parameters, tempStream, DefaultResolverTransformer);
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.Error(exception);
					}
					tempStream.Position = 0;
					System.Xml.XmlDocument TempDocument = (System.Xml.XmlDocument)Result.Node.OwnerDocument;
					if(TempDocument == null)
					{
						TempDocument = (System.Xml.XmlDocument)Result.Node;
					}
					TempDocument.Load(tempStream);					
				}
				else
					ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException("A Xsl stylesheet file must be defined before transform operation", null));
			}
			catch (System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(GenericSourceSupport Source, DomResultSupport Result)
		{
			try
			{
				System.Xml.XmlDocument SourceDocument = new System.Xml.XmlDocument();
				switch(Source.Type)
				{
					case GenericSourceSupport.TYPE.Uri:
					case GenericSourceSupport.TYPE.File:
						SourceDocument.Load(Source.Path);
						break;
					case GenericSourceSupport.TYPE.Stream:
						SourceDocument.Load(Source.Stream);
						break;
					case GenericSourceSupport.TYPE.Reader:
						SourceDocument.Load(Source.Reader);
						break;
					default:
						SourceDocument = null;
						ErrorListenerTransformer.Error(new System.Xml.Xsl.XsltException("The Xml Source can't be null", null));
						break;
				}
				System.IO.MemoryStream tempStream = new System.IO.MemoryStream();				
				if (this.Stylesheet != null)
				{
					try
					{
						Transformer.Transform(SourceDocument, Parameters, tempStream, DefaultResolverTransformer);
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
					tempStream.Position = 0;
					System.Xml.XmlDocument TempDocument = (System.Xml.XmlDocument)Result.Node.OwnerDocument;
					if(TempDocument == null)
					{
						TempDocument = (System.Xml.XmlDocument)Result.Node;
					}
					TempDocument.Load(tempStream);
				}
				else
					ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException("A Xsl stylesheet file must be defined before transform operation", null));
			}
			catch(System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(SaxSourceSupport Source, SaxResultSupport Result)
		{
			try
			{
				System.Xml.XmlDocument SourceDocument = new System.Xml.XmlDocument();			
				System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
				XmlSAXDocumentManager tempManager = new XmlSAXDocumentManager();
				tempManager.setContentHandler(Result.Handler);

				if (Result.LexHandler != null)
					Source.Reader.setProperty("http://xml.org/sax/properties/lexical-handler", Result.LexHandler);

				if (Source.Source.Characters != null)
					SourceDocument.Load(Source.Source.Characters);
				else
				{
					if(Source.Source.Bytes != null)
						SourceDocument.Load(Source.Source.Bytes);
					else
					{
						if(Source.Source.Uri != null)
							SourceDocument.Load(Source.Source.Uri);
						else
							return;
					}
				}
				Transformer.Transform(SourceDocument, Parameters, tempStream, DefaultResolverTransformer);
				tempStream.Position = 0;
				tempManager.parse(tempStream);
			}
			catch (System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(SaxSourceSupport Source, DomResultSupport Result)
		{
			try
			{
				System.IO.MemoryStream tempStream = new System.IO.MemoryStream();				
				System.Xml.XmlDocument tempGSource = new System.Xml.XmlDocument();
				if (Source.Source.Characters != null)
					tempGSource.Load(Source.Source.Characters.BaseStream);
				else
				{
					if (Source.Source.Bytes != null)
						tempGSource.Load(Source.Source.Bytes);
					else
					{
						if (Source.Source.Uri != null)
							tempGSource.Load(Source.Source.Uri);
						else
							return;
					}
				}
				if (this.Stylesheet != null)
				{
					try
					{
						Transformer.Transform(tempGSource, Parameters, tempStream, DefaultResolverTransformer);
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
					tempStream.Position = 0;
					System.Xml.XmlDocument TempDocument = (System.Xml.XmlDocument)Result.Node.OwnerDocument;
					if(TempDocument == null)
					{
						TempDocument = (System.Xml.XmlDocument)Result.Node;
					}
					TempDocument.Load(tempStream);					
				}
				else
					ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException("A Xsl stylesheet file must be defined before transform operation", null));
			}
			catch (System.Exception e)
			{		
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(SaxSourceSupport Source, GenericResultSupport Result)
		{
			try
			{
				System.Xml.XmlDocument tempGSource = new System.Xml.XmlDocument();
				if (Source.Source.Characters != null)
					tempGSource.Load(Source.Source.Characters.BaseStream);
				else
				{
					if (Source.Source.Bytes != null)
						tempGSource.Load(Source.Source.Bytes);
					else
					{
						if (Source.Source.Uri != null)
							tempGSource.Load(Source.Source.Uri);
						else
							return;
					}
				}
				if (this.Stylesheet != null)
				{
					try
					{
						switch(Result.Type)
						{
							case GenericResultSupport.TYPE.Null:
								break;
							case GenericResultSupport.TYPE.Stream:
								Transformer.Transform(tempGSource, Parameters, Result.Stream, DefaultResolverTransformer);
								break;
							case GenericResultSupport.TYPE.File:
							case GenericResultSupport.TYPE.Uri:
								System.IO.StreamWriter Temp_Writer = new System.IO.StreamWriter(Result.Id);
								Transformer.Transform(tempGSource, Parameters, Temp_Writer, DefaultResolverTransformer);								
								Temp_Writer.Close();
								break;
							case GenericResultSupport.TYPE.Writer:
								Transformer.Transform(tempGSource, Parameters, Result.Writer, DefaultResolverTransformer);
								Result.Writer.Close();
								break;
						}
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
				}
				else
					ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException("A Xsl stylesheet file must be defined before transform operation", null));
			}
			catch (System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(GenericSourceSupport Source, SaxResultSupport Result)
		{
			try
			{
				XmlSAXDocumentManager tempManager = new XmlSAXDocumentManager();
				tempManager.setContentHandler(Result.Handler);
				if (Result.LexHandler != null)
					tempManager.setProperty("http://xml.org/sax/properties/lexical-handler", Result.LexHandler);
				if (this.Stylesheet != null)
				{
					System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
					System.Xml.XmlDocument SourceDocument = new System.Xml.XmlDocument();
					switch(Source.Type)
					{
						case GenericSourceSupport.TYPE.Uri:
						case GenericSourceSupport.TYPE.File:
							SourceDocument.Load(Source.Path);
							break;
						case GenericSourceSupport.TYPE.Stream:
							SourceDocument.Load(Source.Stream);
							break;
						case GenericSourceSupport.TYPE.Reader:
							SourceDocument.Load(Source.Reader);
							break;
						default:
							SourceDocument = null;
							ErrorListenerTransformer.Error(new System.Xml.Xsl.XsltException("The Xml Source can't be null", null));
							break;
					}
					try
					{
						Transformer.Transform(SourceDocument, Parameters, tempStream, DefaultResolverTransformer);
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
					tempStream.Position = 0;
					tempManager.parse(tempStream);

				}
				else
				{
					if (Source.Reader != null)
						tempManager.parse(Source.Reader.BaseStream);
					else
					{
						if (Source.Stream != null)
							tempManager.parse(Source.Stream);
						else
						{
							if (Source.Path != null)
								tempManager.parse(Source.Path);
							else
								return;
						}
					}
				}
			}
			catch (System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}


		/// <summary>
		/// Makes the tranformation from the specified source, to the specified target.
		/// </summary>
		/// <param name="Source">The XML source to be transformed.</param>
		/// <param name="Result">The result of the transformation.</param>
		public void DoTransform(DomSourceSupport Source, SaxResultSupport Result)
		{
			try
			{
				System.IO.MemoryStream tempStream;
				if (this.Stylesheet != null)
				{
					tempStream = new System.IO.MemoryStream();
					try
					{
						Transformer.Transform(Source.Node, Parameters, tempStream, DefaultResolverTransformer);
					}
					catch (System.Xml.Xsl.XsltException exception)
					{
						ErrorListenerTransformer.FatalError(exception);
					}
					tempStream.Position = 0;
				}
				else
				{
					char[] c = Source.Node.OuterXml.ToCharArray();
					byte[] x = new byte[c.Length];
					for (int i = 0; i < c.Length;i++)
						x[i] = System.Convert.ToByte(c[i]);
					tempStream = new System.IO.MemoryStream(x);
				}
				XmlSAXDocumentManager tempManager = new XmlSAXDocumentManager();
				tempManager.setContentHandler(Result.Handler);
				if (Result.LexHandler != null)
					tempManager.setProperty("http://xml.org/sax/properties/lexical-handler", Result.LexHandler);
				tempManager.parse(tempStream);
			}
			catch (System.Exception e)
			{
				if (this.ErrorListenerTransformer == null)
					throw new System.Xml.Xsl.XsltException(e.Message, e);
				else
					this.ErrorListenerTransformer.FatalError(new System.Xml.Xsl.XsltException(e.Message, e));
			}
		}

		/// <summary>
		/// This method indicates if a feature is supported by the current instance.
		/// </summary>
		/// <param name="feature">A string asociated to a feature.</param>
		/// <returns>True if the feature is supported otherwise false.</returns>
		public bool IsSupported(string feature)
		{
			bool result;
			switch(feature)
			{
				case "GENERICSOURCE":
				case "GENERICRESULT":
				case "DOMSOURCE":
				case "DOMRESULT":
				case "SAXSOURCE":
				case "SAXRESULT":
				case "TRANSFORMERHANDLER":
				case "XMLFILTERSUPPORT":
					result = true;
					break;
				default:
					result = false;
					break;
			}
			return result;
		}

		/// <summary>
		/// Represent the Default error handling for transform operations
		/// </summary>
		public class DefaultXsltExceptionManager : XsltExceptionManager
		{
			public System.IO.StreamWriter StandardError = null;

			/// <summary>
			/// Default constructor for the class.
			/// </summary>
			public DefaultXsltExceptionManager()
			{
				StandardError = new System.IO.StreamWriter(System.Console.OpenStandardError());
			}

			/// <summary>
			/// Manages when an exception was thrown in the Transform operation of the TranformerSupport class.
			/// </summary>
			/// <param name="exception">The exception thrown by the TransformerSupport instance.</param>
			public void Error(System.Xml.Xsl.XsltException exception)
			{
				StandardError.WriteLine(exception);
			}

			/// <summary>
			/// Manages when an exception was thrown in the Transform operation of the TranformerSupport class.
			/// </summary>
			/// <param name="exception">The exception thrown by the TransformerSupport instance.</param>
			public void FatalError(System.Xml.Xsl.XsltException exception)
			{
				StandardError.WriteLine(exception);
				throw exception;
			}

			/// <summary>
			/// Manages when an exception was thrown in the Transform operation of the TranformerSupport class.
			/// </summary>
			/// <param name="exception">The exception thrown by the TransformerSupport instance.</param>
			public void Warning(System.Xml.Xsl.XsltException exception)
			{
				StandardError.WriteLine(exception);
			}
		}
	}

	/*******************************/
	/// <summary>
	/// This class is created for emulates the SAX XMLFilter behaviors.
	/// </summary>
	public class XmlSaxXMLFilter : XmlSAXDocumentManager
	{
		/// <summary>
		/// Set the parent reader.
		/// </summary>
		/// <param name="parent">The parent reader.</param>
		public virtual void setParent(XmlSAXDocumentManager parent)
		{
		}

		/// <summary>
		/// Get the parent reader.
		/// </summary>
		/// <returns>The parent filter, or null if none has been set.</returns>
		public virtual XmlSAXDocumentManager getParent()
		{
			return null;
		}
	}


	/*******************************/
	/// <summary>
	/// Emulates XmlFilter behaviors over piped transformations.
	/// </summary>
	public class TransformXmlFilterSupport:XmlSaxXMLFilter
	{
		private TransformerSupport transformer;
		private BasicSourceSupport XslSource;
		private XmlSAXDocumentManager parent;


		/// <summary>
		/// Creates a new instance of TransformXmlFilterSupport from a BasicSourceSupport.
		/// </summary>
		/// <param name="source">The BasicSourceSupport instance to be used.</param>
		public TransformXmlFilterSupport(BasicSourceSupport source)
		{
			XslSource = source;
			transformer = new TransformerSupport();
			transformer.Load(source);
		}


		/// <summary>
		/// This method is used for Piped transformations using the XmlReaders instance.
		/// </summary>
		/// <param name="Source">The XmlSource with the XML document.</param>
		/// <param name="Filter">The TransformXmlFilterSupport for piped Transformations.</param>
		/// <returns>An XmlSourceSupport instance</returns>
		public virtual XmlSourceSupport Parse(XmlSourceSupport Source,TransformXmlFilterSupport Filter)
		{
			if(Filter.getParent() is TransformXmlFilterSupport)
			{			
				System.IO.MemoryStream tempStream = new System.IO.MemoryStream();			
				GenericResultSupport tempResult = new GenericResultSupport(tempStream);						
				SaxSourceSupport SaxSource = new SaxSourceSupport(Parse(Source,(TransformXmlFilterSupport)Filter.getParent()));			
				Filter.transformer.DoTransform(SaxSource,tempResult);						
				return new XmlSourceSupport(tempStream);
			}
			else
			{				
				Filter.getParent().parse(Source);
				System.IO.MemoryStream tempStream = new System.IO.MemoryStream();			
				GenericResultSupport tempResult = new GenericResultSupport(tempStream);
				SaxSourceSupport SaxSource = new SaxSourceSupport(Source);
				Filter.transformer.DoTransform(SaxSource,tempResult);
				Filter.parse(tempStream);
				tempStream.Position =0;
				return new XmlSourceSupport(tempStream);
			}
		}

		/// <summary>
		/// This method overrides the method parse of the XmlSaxXMLFilter class.
		/// </summary>
		/// <param name="filepath">A string with the file path of the XML source.</param>
		public override void parse(String filepath)
		{
			base.parse(Parse(new XmlSourceSupport(filepath),this));		
		}

		/// <summary>
		/// This method overrides the method parse of the XmlSaxXMLFilter class.
		/// </summary>
		/// <param name="source">The XmlSourceSupport instance with the Xml source.</param>
		public override void parse(XmlSourceSupport source)
		{
			base.parse(Parse(source,this));		
		}

		/// <summary>
		/// Overrides the SetParent method of the XmlSaxXMLFilter.
		/// </summary>
		/// <param name="parent">A XmlSAXDocumentManager instance that is parent of this instance.</param>
		public override void setParent(XmlSAXDocumentManager parent)
		{
			this.parent = parent;
		}

		/// <summary>
		/// Overrides the GetParent method of the XmlSaxXMLFilter.
		/// </summary>
		/// <returns>A XmlSAXDocumentManager instance that is parent of this instance.</returns>
		public override XmlSAXDocumentManager getParent()
		{
			return this.parent;
		}
	}

	/*******************************/
	/// <summary>
	/// Supports the basic desing of a Source for XML transformations.
	/// </summary>
	public interface BasicSourceSupport
	{
		/// <summary>
		/// Gets and sets the Id of the current instance.
		/// </summary>
		string Id 
		{
			get;
			set;
		}
	}


	/*******************************/
	/// <summary>
	/// Supports the basic desing of a Result for XML transformations.
	/// </summary>
	public interface BasicResultSupport
	{
		/// <summary>
		/// Gets and sets the Id of the current instance.
		/// </summary>
		string Id 
		{
			get;
			set;
		}
	}


/// <summary>
/// Contains conversion support elements such as classes, interfaces and static methods.
/// </summary>
public class SupportClass
{
	/// <summary>
	/// Converts an array of sbytes to an array of bytes
	/// </summary>
	/// <param name="sbyteArray">The array of sbytes to be converted</param>
	/// <returns>The new array of bytes</returns>
	public static byte[] ToByteArray(sbyte[] sbyteArray)
	{
		byte[] byteArray = null;

		if (sbyteArray != null)
		{
			byteArray = new byte[sbyteArray.Length];
			for(int index=0; index < sbyteArray.Length; index++)
				byteArray[index] = (byte) sbyteArray[index];
		}
		return byteArray;
	}

	/// <summary>
	/// Converts a string to an array of bytes
	/// </summary>
	/// <param name="sourceString">The string to be converted</param>
	/// <returns>The new array of bytes</returns>
	public static byte[] ToByteArray(string sourceString)
	{
		return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
	}

	/// <summary>
	/// Converts a array of object-type instances to a byte-type array.
	/// </summary>
	/// <param name="tempObjectArray">Array to convert.</param>
	/// <returns>An array of byte type elements.</returns>
	public static byte[] ToByteArray(System.Object[] tempObjectArray)
	{
		byte[] byteArray = null;
		if (tempObjectArray != null)
		{
			byteArray = new byte[tempObjectArray.Length];
			for (int index = 0; index < tempObjectArray.Length; index++)
				byteArray[index] = (byte)tempObjectArray[index];
		}
		return byteArray;
	}

	/*******************************/
	/// <summary>
	/// Write an array of bytes int the FileStream specified.
	/// </summary>
	/// <param name="FileStreamWrite">FileStream that must be updated.</param>
	/// <param name="Source">Array of bytes that must be written in the FileStream.</param>
	public static void WriteOutput(System.IO.FileStream FileStreamWrite, sbyte[] Source)
	{
		FileStreamWrite.Write(ToByteArray(Source), 0, Source.Length);
	}


	/*******************************/
	/// <summary>
	/// This class provides functionality not found in .NET collection-related interfaces.
	/// </summary>
	public class ICollectionSupport
	{
		/// <summary>
		/// Adds a new element to the specified collection.
		/// </summary>
		/// <param name="c">Collection where the new element will be added.</param>
		/// <param name="obj">Object to add.</param>
		/// <returns>true</returns>
		public static bool Add(System.Collections.ICollection c, System.Object obj)
		{
			bool added = false;
			//Reflection. Invoke either the "add" or "Add" method.
			System.Reflection.MethodInfo method;
			try
			{
				//Get the "add" method for proprietary classes
				method = c.GetType().GetMethod("Add");
				if (method == null)
					method = c.GetType().GetMethod("add");
				int index = (int) method.Invoke(c, new System.Object[] {obj});
				if (index >=0)	
					added = true;
			}
			catch (System.Exception e)
			{
				throw e;
			}
			return added;
		}

		/// <summary>
		/// Adds all of the elements of the "c" collection to the "target" collection.
		/// </summary>
		/// <param name="target">Collection where the new elements will be added.</param>
		/// <param name="c">Collection whose elements will be added.</param>
		/// <returns>Returns true if at least one element was added, false otherwise.</returns>
		public static bool AddAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(c).GetEnumerator();
			bool added = false;

			//Reflection. Invoke "addAll" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("addAll");

				if (method != null)
					added = (bool) method.Invoke(target, new System.Object[] {c});
				else
				{
					method = target.GetType().GetMethod("Add");
					while (e.MoveNext() == true)
					{
						bool tempBAdded =  (int) method.Invoke(target, new System.Object[] {e.Current}) >= 0;
						added = added ? added : tempBAdded;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return added;
		}

		/// <summary>
		/// Removes all the elements from the collection.
		/// </summary>
		/// <param name="c">The collection to remove elements.</param>
		public static void Clear(System.Collections.ICollection c)
		{
			//Reflection. Invoke "Clear" method or "clear" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Clear");

				if (method == null)
					method = c.GetType().GetMethod("clear");

				method.Invoke(c, new System.Object[] {});
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Determines whether the collection contains the specified element.
		/// </summary>
		/// <param name="c">The collection to check.</param>
		/// <param name="obj">The object to locate in the collection.</param>
		/// <returns>true if the element is in the collection.</returns>
		public static bool Contains(System.Collections.ICollection c, System.Object obj)
		{
			bool contains = false;

			//Reflection. Invoke "contains" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Contains");

				if (method == null)
					method = c.GetType().GetMethod("contains");

				contains = (bool)method.Invoke(c, new System.Object[] {obj});
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return contains;
		}

		/// <summary>
		/// Determines whether the collection contains all the elements in the specified collection.
		/// </summary>
		/// <param name="target">The collection to check.</param>
		/// <param name="c">Collection whose elements would be checked for containment.</param>
		/// <returns>true id the target collection contains all the elements of the specified collection.</returns>
		public static bool ContainsAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{						
			System.Collections.IEnumerator e =  c.GetEnumerator();

			bool contains = false;

			//Reflection. Invoke "containsAll" method for proprietary classes or "Contains" method for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("containsAll");

				if (method != null)
					contains = (bool)method.Invoke(target, new Object[] {c});
				else
				{					
					method = target.GetType().GetMethod("Contains");
					while (e.MoveNext() == true)
					{
						if ((contains = (bool)method.Invoke(target, new Object[] {e.Current})) == false)
							break;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return contains;
		}

		/// <summary>
		/// Removes the specified element from the collection.
		/// </summary>
		/// <param name="c">The collection where the element will be removed.</param>
		/// <param name="obj">The element to remove from the collection.</param>
		public static bool Remove(System.Collections.ICollection c, System.Object obj)
		{
			bool changed = false;

			//Reflection. Invoke "remove" method for proprietary classes or "Remove" method
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("remove");

				if (method != null)
					method.Invoke(c, new System.Object[] {obj});
				else
				{
					method = c.GetType().GetMethod("Contains");
					changed = (bool)method.Invoke(c, new System.Object[] {obj});
					method = c.GetType().GetMethod("Remove");
					method.Invoke(c, new System.Object[] {obj});
				}
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return changed;
		}

		/// <summary>
		/// Removes all the elements from the specified collection that are contained in the target collection.
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to remove from the target collection.</param>
		/// <returns>true</returns>
		public static bool RemoveAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.ArrayList al = ToArrayList(c);
			System.Collections.IEnumerator e = al.GetEnumerator();

			//Reflection. Invoke "removeAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("removeAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {al});
				else
				{
					method = target.GetType().GetMethod("Remove");
					System.Reflection.MethodInfo methodContains = target.GetType().GetMethod("Contains");

					while (e.MoveNext() == true)
					{
						while ((bool) methodContains.Invoke(target, new System.Object[] {e.Current}) == true)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return true;
		}

		/// <summary>
		/// Retains the elements in the target collection that are contained in the specified collection
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to be retained in the target collection.</param>
		/// <returns>true</returns>
		public static bool RetainAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(target).GetEnumerator();
			System.Collections.ArrayList al = new System.Collections.ArrayList(c);

			//Reflection. Invoke "retainAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("retainAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {c});
				else
				{
					method = c.GetType().GetMethod("Remove");

					while (e.MoveNext() == true)
					{
						if (al.Contains(e.Current) == false)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		/// Returns an array containing all the elements of the collection.
		/// </summary>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c)
		{	
			int index = 0;
			System.Object[] objects = new System.Object[c.Count];
			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objects[index++] = e.Current;

			return objects;
		}

		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <param name="objects">The array into which the elements of the collection will be stored.</param>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c, System.Object[] objects)
		{	
			int index = 0;

			System.Type type = objects.GetType().GetElementType();
			System.Object[] objs = (System.Object[]) Array.CreateInstance(type, c.Count );

			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objs[index++] = e.Current;

			//If objects is smaller than c then do not return the new array in the parameter
			if (objects.Length >= c.Count)
				objs.CopyTo(objects, 0);

			return objs;
		}

		/// <summary>
		/// Converts an ICollection instance to an ArrayList instance.
		/// </summary>
		/// <param name="c">The ICollection instance to be converted.</param>
		/// <returns>An ArrayList instance in which its elements are the elements of the ICollection instance.</returns>
		public static System.Collections.ArrayList ToArrayList(System.Collections.ICollection c)
		{
			System.Collections.ArrayList tempArrayList = new System.Collections.ArrayList();
			System.Collections.IEnumerator tempEnumerator = c.GetEnumerator();
			while (tempEnumerator.MoveNext())
				tempArrayList.Add(tempEnumerator.Current);
			return tempArrayList;
		}
	}


	/*******************************/
	/// <summary>
	/// Writes the exception stack trace to the received stream
	/// </summary>
	/// <param name="throwable">Exception to obtain information from</param>
	/// <param name="stream">Output sream used to write to</param>
	public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	/*******************************/
	/// <summary>
	/// Represents a collection ob objects that contains no duplicate elements.
	/// </summary>	
	public interface SetSupport : System.Collections.ICollection, System.Collections.IList
	{
		/// <summary>
		/// Adds a new element to the Collection if it is not already present.
		/// </summary>
		/// <param name="obj">The object to add to the collection.</param>
		/// <returns>Returns true if the object was added to the collection, otherwise false.</returns>
		new bool Add(System.Object obj);

		/// <summary>
		/// Adds all the elements of the specified collection to the Set.
		/// </summary>
		/// <param name="c">Collection of objects to add.</param>
		/// <returns>true</returns>
		bool AddAll(System.Collections.ICollection c);
	}


	/*******************************/
	/// <summary>
	/// SupportClass for the HashSet class.
	/// </summary>
	[Serializable]
	public class HashSetSupport : System.Collections.ArrayList, SetSupport
	{
		public HashSetSupport() : base()
		{	
		}

		public HashSetSupport(System.Collections.ICollection c) 
		{
			this.AddAll(c);
		}

		public HashSetSupport(int capacity) : base(capacity)
		{
		}

		/// <summary>
		/// Adds a new element to the ArrayList if it is not already present.
		/// </summary>		
		/// <param name="obj">Element to insert to the ArrayList.</param>
		/// <returns>Returns true if the new element was inserted, false otherwise.</returns>
		new public virtual bool Add(System.Object obj)
		{
			bool inserted;

			if ((inserted = this.Contains(obj)) == false)
			{
				base.Add(obj);
			}

			return !inserted;
		}

		/// <summary>
		/// Adds all the elements of the specified collection that are not present to the list.
		/// </summary>
		/// <param name="c">Collection where the new elements will be added</param>
		/// <returns>Returns true if at least one element was added, false otherwise.</returns>
		public bool AddAll(System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(c).GetEnumerator();
			bool added = false;

			while (e.MoveNext() == true)
			{
				if (this.Add(e.Current) == true)
					added = true;
			}

			return added;
		}
		
		/// <summary>
		/// Returns a copy of the HashSet instance.
		/// </summary>		
		/// <returns>Returns a shallow copy of the current HashSet.</returns>
		public override System.Object Clone()
		{
			return base.MemberwiseClone();
		}
	}


	/*******************************/
	/// <summary>
	/// Converts the specified collection to its string representation.
	/// </summary>
	/// <param name="c">The collection to convert to string.</param>
	/// <returns>A string representation of the specified collection.</returns>
	public static string CollectionToString(System.Collections.ICollection c)
	{
		System.Text.StringBuilder s = new System.Text.StringBuilder();
		
		if (c != null)
		{
		
			System.Collections.ArrayList l = new System.Collections.ArrayList(c);

			bool isDictionary = (c is System.Collections.BitArray || c is System.Collections.Hashtable || c is System.Collections.IDictionary || c is System.Collections.Specialized.NameValueCollection || (l.Count > 0 && l[0] is System.Collections.DictionaryEntry));
			for (int index = 0; index < l.Count; index++) 
			{
				if (l[index] == null)
					s.Append("null");
				else if (!isDictionary)
					s.Append(l[index]);
				else
				{
					isDictionary = true;
					if (c is System.Collections.Specialized.NameValueCollection)
						s.Append(((System.Collections.Specialized.NameValueCollection)c).GetKey (index));
					else
						s.Append(((System.Collections.DictionaryEntry) l[index]).Key);
					s.Append("=");
					if (c is System.Collections.Specialized.NameValueCollection)
						s.Append(((System.Collections.Specialized.NameValueCollection)c).GetValues(index)[0]);
					else
						s.Append(((System.Collections.DictionaryEntry) l[index]).Value);

				}
				if (index < l.Count - 1)
					s.Append(", ");
			}
			
			if(isDictionary)
			{
				if(c is System.Collections.ArrayList)
					isDictionary = false;
			}
			if (isDictionary)
			{
				s.Insert(0, "{");
				s.Append("}");
			}
			else 
			{
				s.Insert(0, "[");
				s.Append("]");
			}
		}
		else
			s.Insert(0, "null");
		return s.ToString();
	}

	/// <summary>
	/// Tests if the specified object is a collection and converts it to its string representation.
	/// </summary>
	/// <param name="obj">The object to convert to string</param>
	/// <returns>A string representation of the specified object.</returns>
	public static string CollectionToString(System.Object obj)
	{
		string result = "";

		if (obj != null)
		{
			if (obj is System.Collections.ICollection)
				result = CollectionToString((System.Collections.ICollection)obj);
			else
				result = obj.ToString();
		}
		else
			result = "null";

		return result;
	}
	/*******************************/
	/// <summary>
	/// Represents the methods to support some operations over files.
	/// </summary>
	public class FileSupport
	{
		/// <summary>
		/// Creates a new empty file with the specified pathname.
		/// </summary>
		/// <param name="path">The abstract pathname of the file</param>
		/// <returns>True if the file does not exist and was succesfully created</returns>
		public static bool CreateNewFile(System.IO.FileInfo path)
		{
			if (path.Exists)
			{
				return false;
			}
			else
			{
                System.IO.FileStream createdFile = path.Create();
                createdFile.Close();
				return true;
			}
		}

		/// <summary>
		/// Compares the specified object with the specified path
		/// </summary>
		/// <param name="path">An abstract pathname to compare with</param>
		/// <param name="file">An object to compare with the given pathname</param>
		/// <returns>A value indicating a lexicographically comparison of the parameters</returns>
		public static int CompareTo(System.IO.FileInfo path, System.Object file)
		{
			if( file is System.IO.FileInfo )
			{
				System.IO.FileInfo fileInfo = (System.IO.FileInfo)file;
				return path.FullName.CompareTo( fileInfo.FullName );
			}
			else
			{
				throw new System.InvalidCastException();
			}
		}

		/// <summary>
		/// Returns an array of abstract pathnames representing the files and directories of the specified path.
		/// </summary>
		/// <param name="path">The abstract pathname to list it childs.</param>
		/// <returns>An array of abstract pathnames childs of the path specified or null if the path is not a directory</returns>
		public static System.IO.FileInfo[] GetFiles(System.IO.FileInfo path)
		{
			if ( (path.Attributes & System.IO.FileAttributes.Directory) > 0 )
			{																 
				String[] fullpathnames = System.IO.Directory.GetFileSystemEntries(path.FullName);
				System.IO.FileInfo[] result = new System.IO.FileInfo[fullpathnames.Length];
				for(int i = 0; i < result.Length ; i++)
					result[i] = new System.IO.FileInfo(fullpathnames[i]);
				return result;
			}
			else return null;
		}

		/// <summary>
		/// Creates an instance of System.Uri class with the pech specified
		/// </summary>
		/// <param name="path">The abstract path name to create the Uri</param>
		/// <returns>A System.Uri instance constructed with the specified path</returns>
		public static System.Uri ToUri(System.IO.FileInfo path)
		{
			System.UriBuilder uri = new System.UriBuilder();
			uri.Path = path.FullName;
			uri.Host = String.Empty;
			uri.Scheme = System.Uri.UriSchemeFile;
			return uri.Uri;
		}

		/// <summary>
		/// Returns true if the file specified by the pathname is a hidden file.
		/// </summary>
		/// <param name="file">The abstract pathname of the file to test</param>
		/// <returns>True if the file is hidden, false otherwise</returns>
		public static bool IsHidden(System.IO.FileInfo file)
		{
			return ((file.Attributes & System.IO.FileAttributes.Hidden) > 0); 
		}

		/// <summary>
		/// Sets the read-only property of the file to true.
		/// </summary>
		/// <param name="file">The abstract path name of the file to modify</param>
		public static bool SetReadOnly(System.IO.FileInfo file)
		{
			try 
			{
				file.Attributes = file.Attributes | System.IO.FileAttributes.ReadOnly;
				return true;
			}
			catch (System.Exception exception)
			{
				String exceptionMessage = exception.Message;
				return false;
			}
		}

		/// <summary>
		/// Sets the last modified time of the specified file with the specified value.
		/// </summary>
		/// <param name="file">The file to change it last-modified time</param>
		/// <param name="date">Total number of miliseconds since January 1, 1970 (new last-modified time)</param>
		/// <returns>True if the operation succeeded, false otherwise</returns>
		public static bool SetLastModified(System.IO.FileInfo file, long date)
		{
			try 
			{
				long valueConstant = (new System.DateTime(1969, 12, 31, 18, 0, 0)).Ticks;
				file.LastWriteTime = new System.DateTime( (date * 10000L) + valueConstant );
				return true;
			}
			catch (System.Exception exception)
			{
				String exceptionMessage = exception.Message;
				return false;
			}
		}
	}
	/*******************************/
	/// <summary>
	/// Checks if the giving File instance is a directory or file, and returns his Length
	/// </summary>
	/// <param name="file">The File instance to check</param>
	/// <returns>The length of the file</returns>
	public static long FileLength(System.IO.FileInfo file)
	{
		if (file.Exists)
			return file.Length;
		else 
			return 0;
	}

	/*******************************/
	/// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
	/// <param name="sourceStream">The source Stream to read from.</param>
	/// <param name="target">Contains the array of characteres read from the source Stream.</param>
	/// <param name="start">The starting index of the target array.</param>
	/// <param name="count">The maximum number of characters to read from the source Stream.</param>
	/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
	public static System.Int32 ReadInput(System.IO.Stream sourceStream, sbyte[] target, int start, int count)
	{
		// Returns 0 bytes if not enough space in target
		if (target.Length == 0)
			return 0;

		byte[] receiver = new byte[target.Length];
		int bytesRead   = sourceStream.Read(receiver, start, count);

		// Returns -1 if EOF
		if (bytesRead == 0)	
			return -1;
                
		for(int i = start; i < start + bytesRead; i++)
			target[i] = (sbyte)receiver[i];
                
		return bytesRead;
	}

	/// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
	/// <param name="sourceTextReader">The source TextReader to read from</param>
	/// <param name="target">Contains the array of characteres read from the source TextReader.</param>
	/// <param name="start">The starting index of the target array.</param>
	/// <param name="count">The maximum number of characters to read from the source TextReader.</param>
	/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
	public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, sbyte[] target, int start, int count)
	{
		// Returns 0 bytes if not enough space in target
		if (target.Length == 0) return 0;

		char[] charArray = new char[target.Length];
		int bytesRead = sourceTextReader.Read(charArray, start, count);

		// Returns -1 if EOF
		if (bytesRead == 0) return -1;

		for(int index=start; index<start+bytesRead; index++)
			target[index] = (sbyte)charArray[index];

		return bytesRead;
	}

	/*******************************/
	/// <summary>
	/// Support class used to handle threads
	/// </summary>
	public class ThreadClass : IThreadRunnable
	{
		/// <summary>
		/// The instance of System.Threading.Thread
		/// </summary>
		private System.Threading.Thread threadField;
	      
		/// <summary>
		/// Initializes a new instance of the ThreadClass class
		/// </summary>
		public ThreadClass()
		{
			threadField = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
		}
	 
		/// <summary>
		/// Initializes a new instance of the Thread class.
		/// </summary>
		/// <param name="Name">The name of the thread</param>
		public ThreadClass(string Name)
		{
			threadField = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
			this.Name = Name;
		}
	      
		/// <summary>
		/// Initializes a new instance of the Thread class.
		/// </summary>
		/// <param name="Start">A ThreadStart delegate that references the methods to be invoked when this thread begins executing</param>
		public ThreadClass(System.Threading.ThreadStart Start)
		{
			threadField = new System.Threading.Thread(Start);
		}
	 
		/// <summary>
		/// Initializes a new instance of the Thread class.
		/// </summary>
		/// <param name="Start">A ThreadStart delegate that references the methods to be invoked when this thread begins executing</param>
		/// <param name="Name">The name of the thread</param>
		public ThreadClass(System.Threading.ThreadStart Start, string Name)
		{
			threadField = new System.Threading.Thread(Start);
			this.Name = Name;
		}
	      
		/// <summary>
		/// This method has no functionality unless the method is overridden
		/// </summary>
		public virtual void Run()
		{
		}
	      
		/// <summary>
		/// Causes the operating system to change the state of the current thread instance to ThreadState.Running
		/// </summary>
		public virtual void Start()
		{
			threadField.Start();
		}
	      
		/// <summary>
		/// Interrupts a thread that is in the WaitSleepJoin thread state
		/// </summary>
		public virtual void Interrupt()
		{
			threadField.Interrupt();
		}
	      
		/// <summary>
		/// Gets the current thread instance
		/// </summary>
		public System.Threading.Thread Instance
		{
			get
			{
				return threadField;
			}
			set
			{
				threadField = value;
			}
		}
	      
		/// <summary>
		/// Gets or sets the name of the thread
		/// </summary>
		public string Name
		{
			get
			{
				return threadField.Name;
			}
			set
			{
				if (threadField.Name == null)
					threadField.Name = value; 
			}
		}
	      
		/// <summary>
		/// Gets or sets a value indicating the scheduling priority of a thread
		/// </summary>
		public System.Threading.ThreadPriority Priority
		{
			get
			{
				return threadField.Priority;
			}
			set
			{
				threadField.Priority = value;
			}
		}
	      
		/// <summary>
		/// Gets a value indicating the execution status of the current thread
		/// </summary>
		public bool IsAlive
		{
			get
			{
				return threadField.IsAlive;
			}
		}
	      
		/// <summary>
		/// Gets or sets a value indicating whether or not a thread is a background thread.
		/// </summary>
		public bool IsBackground
		{
			get
			{
				return threadField.IsBackground;
			} 
			set
			{
				threadField.IsBackground = value;
			}
		}
	      
		/// <summary>
		/// Blocks the calling thread until a thread terminates
		/// </summary>
		public void Join()
		{
			threadField.Join();
		}
	      
		/// <summary>
		/// Blocks the calling thread until a thread terminates or the specified time elapses
		/// </summary>
		/// <param name="MiliSeconds">Time of wait in milliseconds</param>
		public void Join(long MiliSeconds)
		{
			lock(this)
			{
				threadField.Join(new System.TimeSpan(MiliSeconds * 10000));
			}
		}
	      
		/// <summary>
		/// Blocks the calling thread until a thread terminates or the specified time elapses
		/// </summary>
		/// <param name="MiliSeconds">Time of wait in milliseconds</param>
		/// <param name="NanoSeconds">Time of wait in nanoseconds</param>
		public void Join(long MiliSeconds, int NanoSeconds)
		{
			lock(this)
			{
				threadField.Join(new System.TimeSpan(MiliSeconds * 10000 + NanoSeconds * 100));
			}
		}
	      
		/// <summary>
		/// Resumes a thread that has been suspended
		/// </summary>
		public void Resume()
		{
			threadField.Resume();
		}
	      
		/// <summary>
		/// Raises a ThreadAbortException in the thread on which it is invoked, 
		/// to begin the process of terminating the thread. Calling this method 
		/// usually terminates the thread
		/// </summary>
		public void Abort()
		{
			threadField.Abort();
		}
	      
		/// <summary>
		/// Raises a ThreadAbortException in the thread on which it is invoked, 
		/// to begin the process of terminating the thread while also providing
		/// exception information about the thread termination. 
		/// Calling this method usually terminates the thread.
		/// </summary>
		/// <param name="stateInfo">An object that contains application-specific information, such as state, which can be used by the thread being aborted</param>
		public void Abort(System.Object stateInfo)
		{
			lock(this)
			{
				threadField.Abort(stateInfo);
			}
		}
	      
		/// <summary>
		/// Suspends the thread, if the thread is already suspended it has no effect
		/// </summary>
		public void Suspend()
		{
			threadField.Suspend();
		}
	      
		/// <summary>
		/// Obtain a String that represents the current Object
		/// </summary>
		/// <returns>A String that represents the current Object</returns>
		public override string ToString()
		{
			return "Thread[" + Name + "," + Priority.ToString() + "," + "" + "]";
		}
	     
		/// <summary>
		/// Gets the currently running thread
		/// </summary>
		/// <returns>The currently running thread</returns>
		public static ThreadClass Current()
		{
			ThreadClass CurrentThread = new ThreadClass();
			CurrentThread.Instance = System.Threading.Thread.CurrentThread;
			return CurrentThread;
		}
	}


	/*******************************/
	/// <summary>
	/// Reads the serialized fields written by the DefaultWriteObject method.
	/// </summary>
	/// <param name="info">SerializationInfo parameter from the special deserialization constructor.</param>
	/// <param name="context">StreamingContext parameter from the special deserialization constructor</param>
	/// <param name="instance">Object to deserialize.</param>
	public static void DefaultReadObject(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Object instance)
	{                       
		System.Type thisType = instance.GetType();
		System.Reflection.MemberInfo[] mi = System.Runtime.Serialization.FormatterServices.GetSerializableMembers(thisType, context);
		for (int i = 0 ; i < mi.Length; i++) 
		{
			System.Reflection.FieldInfo fi = (System.Reflection.FieldInfo) mi[i];
			fi.SetValue(instance, info.GetValue(fi.Name, fi.FieldType));
		}
	}
	/*******************************/
	/// <summary>
	/// Writes the serializable fields to the SerializationInfo object, which stores all the data needed to serialize the specified object object.
	/// </summary>
	/// <param name="info">SerializationInfo parameter from the GetObjectData method.</param>
	/// <param name="context">StreamingContext parameter from the GetObjectData method.</param>
	/// <param name="instance">Object to serialize.</param>
	public static void DefaultWriteObject(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Object instance)
	{                       
		System.Type thisType = instance.GetType();
		System.Reflection.MemberInfo[] mi = System.Runtime.Serialization.FormatterServices.GetSerializableMembers(thisType, context);
		for (int i = 0 ; i < mi.Length; i++) 
		{
			info.AddValue(mi[i].Name, ((System.Reflection.FieldInfo) mi[i]).GetValue(instance));
		}
	}


	/*******************************/
	/// <summary>
	/// SupportClass for the Stack class.
	/// </summary>
	public class StackSupport
	{
		/// <summary>
		/// Removes the element at the top of the stack and returns it.
		/// </summary>
		/// <param name="stack">The stack where the element at the top will be returned and removed.</param>
		/// <returns>The element at the top of the stack.</returns>
		public static System.Object Pop(System.Collections.ArrayList stack)
		{
			System.Object obj = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);

			return obj;
		}
	}


	/*******************************/
	public delegate void PropertyChangeEventHandler(System.Object sender, PropertyChangingEventArgs e);

	/// <summary>
	/// EventArgs for support to the contrained properties.
	/// </summary>
	public class PropertyChangingEventArgs : System.ComponentModel.PropertyChangedEventArgs
	{   
		private System.Object oldValue;
		private System.Object newValue;

		/// <summary>
		/// Initializes a new PropertyChangingEventArgs instance.
		/// </summary>
		/// <param name="propertyName">Property name that fire the event.</param>
		public PropertyChangingEventArgs(string propertyName) : base(propertyName)
		{
		}

		/// <summary>
		/// Initializes a new PropertyChangingEventArgs instance.
		/// </summary>
		/// <param name="propertyName">Property name that fire the event.</param>
		/// <param name="oldVal">Property value to be replaced.</param>
		/// <param name="newVal">Property value to be set.</param>
		public PropertyChangingEventArgs(string propertyName, System.Object oldVal, System.Object newVal) : base(propertyName)
		{
			this.oldValue = oldVal;
			this.newValue = newVal;
		}

		/// <summary>
		/// Gets or sets the old value of the event.
		/// </summary>
		public System.Object OldValue
		{
			get
			{
				return this.oldValue;
			}
			set
			{
				this.oldValue = value;
			}
		}
	        
		/// <summary>
		/// Gets or sets the new value of the event.
		/// </summary>
		public System.Object NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}
	}


	/*******************************/
	/// <summary>
	/// Performs an unsigned bitwise right shift with the specified number
	/// </summary>
	/// <param name="number">Number to operate on</param>
	/// <param name="bits">Ammount of bits to shift</param>
	/// <returns>The resulting number from the shift operation</returns>
	public static int URShift(int number, int bits)
	{
		if ( number >= 0)
			return number >> bits;
		else
			return (number >> bits) + (2 << ~bits);
	}

	/// <summary>
	/// Performs an unsigned bitwise right shift with the specified number
	/// </summary>
	/// <param name="number">Number to operate on</param>
	/// <param name="bits">Ammount of bits to shift</param>
	/// <returns>The resulting number from the shift operation</returns>
	public static int URShift(int number, long bits)
	{
		return URShift(number, (int)bits);
	}

	/// <summary>
	/// Performs an unsigned bitwise right shift with the specified number
	/// </summary>
	/// <param name="number">Number to operate on</param>
	/// <param name="bits">Ammount of bits to shift</param>
	/// <returns>The resulting number from the shift operation</returns>
	public static long URShift(long number, int bits)
	{
		if ( number >= 0)
			return number >> bits;
		else
			return (number >> bits) + (2L << ~bits);
	}

	/// <summary>
	/// Performs an unsigned bitwise right shift with the specified number
	/// </summary>
	/// <param name="number">Number to operate on</param>
	/// <param name="bits">Ammount of bits to shift</param>
	/// <returns>The resulting number from the shift operation</returns>
	public static long URShift(long number, long bits)
	{
		return URShift(number, (int)bits);
	}

	/*******************************/
	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static long Identity(long literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static ulong Identity(ulong literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static float Identity(float literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static double Identity(double literal)
	{
		return literal;
	}

	/*******************************/
	/// <summary>
	/// Receives a byte array and returns it transformed in an sbyte array
	/// </summary>
	/// <param name="byteArray">Byte array to process</param>
	/// <returns>The transformed array</returns>
	public static sbyte[] ToSByteArray(byte[] byteArray)
	{
		sbyte[] sbyteArray = null;
		if (byteArray != null)
		{
			sbyteArray = new sbyte[byteArray.Length];
			for(int index=0; index < byteArray.Length; index++)
				sbyteArray[index] = (sbyte) byteArray[index];
		}
		return sbyteArray;
	}

	/*******************************/
	/*******************************/
	/// <summary>
	/// Gives support functions to Http internet connections.
	/// </summary>
	public class URLConnectionSupport
	{
		/// <summary>
		/// Sets the request property for the specified key
		/// </summary>
		/// <param name="connection">Connection used to assign the property value</param>
		/// <param name="key">Property name to obtain the property value</param>
		/// <param name="keyValue">The value to associate with the specified property</param>
		public static void SetRequestProperty(System.Net.HttpWebRequest connection, string key, string keyValue) 
		{
			connection.Headers.Set(key,keyValue);
		}

		/// <summary>
		/// Gets the request property for the specified key
		/// </summary>
		/// <param name="connection">Connection used to obtain the property value</param>
		/// <param name="key">Property name to return it's property value</param>
		/// <returns>The value associated with the specified property</returns>
		public static string GetRequestProperty(System.Net.HttpWebRequest connection, string key) 
		{
			try
			{
				return connection.Headers.Get(key);
			}
			catch(System.Exception)
			{}
			return "";
		}

		/// <summary>
		/// Receives a key and returns it's default property value
		/// </summary>
		/// <param name="key">Key name to obtain the default request value</param>
		/// <returns>The default value associated with the property</returns>
		public static string GetDefaultRequestProperty(string key) 
		{
			return null;
		}

		/// <summary> 
		/// Gets the value of the "Content-Encoding" property from the collection of headers associated with the specified HttpWebRequest
		/// </summary>
		/// <param name="request">Instance of HttpWebRequest to get the headers from</param>
		/// <returns>The value of the "Content-Encoding" property if found, otherwise returns null</returns>
		public static string GetContentEncoding(System.Net.HttpWebRequest request)
		{
			try 
			{
				return request.GetResponse().Headers.Get("Content-Encoding");
			}
			catch(System.Exception)
			{}
			return null;
		}

		/// <summary>
		/// Gets the sending date of the resource referenced by the HttpRequest
		/// </summary>
		/// <param name="request">Instance of HttpWebRequest to get the date from</param>
		/// <returns>The sending date of the resource if found, otherwise 0</returns>
		public static long GetSendingDate(System.Net.HttpWebRequest request)
		{
			long headerDate;
			try
			{
				headerDate = System.DateTime.Parse(request.GetResponse().Headers.Get("Date")).Ticks;
			}
			catch(System.Exception)
			{
				headerDate = 0;
			}
			return headerDate;
		}

		/// <summary>
		/// Gets the key for the specified index from the KeysCollection of the specified HttpWebRequest's Headers property
		/// </summary>
		/// <param name="request">Instance HttpWebRequest to get the key from</param>
		/// <param name="indexField">Index of the field to get the corresponding key</param>
		/// <returns>The key for the specified index if found, otherwise null</returns>
		public static string GetHeaderFieldKey(System.Net.HttpWebRequest request, int indexField)
		{
			try
			{
				return request.GetResponse().Headers.Keys.Get(indexField);
			}
			catch(System.Exception)
			{}
			return null;
		}

		/// <summary>
		/// Gets the value of the "Last-Modified" property from the collection of headers associated with the specified HttWebRequest
		/// </summary>
		/// <param name="request">Instance of HttpWebRequest to get the headers from</param>
		/// <returns>The value of the "Last-Modified" property if found, otherwise returns null</returns>
		public static long GetLastModifiedHeaderField(System.Net.HttpWebRequest request)
		{
			long fieldHeaderDate;
			try
			{
				fieldHeaderDate = System.DateTime.Parse(request.GetResponse().Headers.Get("Last-Modified")).Ticks;
			}
			catch(System.Exception)
			{
				fieldHeaderDate = 0;
			}
			return fieldHeaderDate;
		}

		/// <summary>
		/// Gets the value of the named field parsed as date in milliseconds
		/// </summary>
		/// <param name="request">Instance of System.Net.HttpWebRequest to get the headers from</param>
		/// <param name="fieldName">Name of the header field</param>
		/// <param name="defaultValue">A default value to return if the value does not exist in the headers</param>
		/// <returns></returns>
		public static long GetHeaderFieldDate(System.Net.HttpWebRequest request, string fieldName, long defaultValue)
		{
			long fieldHeaderDate;
			try
			{
				fieldHeaderDate = System.DateTime.Parse(request.GetResponse().Headers.Get(fieldName)).Ticks;
			}
			catch(System.Exception)
			{
				fieldHeaderDate = defaultValue;
			}
			return fieldHeaderDate;
		}
	}

	/*******************************/
	/// <summary>
	/// The class performs token processing in strings
	/// </summary>
	public class Tokenizer: System.Collections.IEnumerator
	{
		/// Position over the string
		private long currentPos = 0;

		/// Include demiliters in the results.
		private bool includeDelims = false;

		/// Char representation of the String to tokenize.
		private char[] chars = null;
			
		//The tokenizer uses the default delimiter set: the space character, the tab character, the newline character, and the carriage-return character and the form-feed character
		private string delimiters = " \t\n\r\f";		

		/// <summary>
		/// Initializes a new class instance with a specified string to process
		/// </summary>
		/// <param name="source">String to tokenize</param>
		public Tokenizer(string source)
		{			
			this.chars = source.ToCharArray();
		}

		/// <summary>
		/// Initializes a new class instance with a specified string to process
		/// and the specified token delimiters to use
		/// </summary>
		/// <param name="source">String to tokenize</param>
		/// <param name="delimiters">String containing the delimiters</param>
		public Tokenizer(string source, string delimiters):this(source)
		{			
			this.delimiters = delimiters;
		}


		/// <summary>
		/// Initializes a new class instance with a specified string to process, the specified token 
		/// delimiters to use, and whether the delimiters must be included in the results.
		/// </summary>
		/// <param name="source">String to tokenize</param>
		/// <param name="delimiters">String containing the delimiters</param>
		/// <param name="includeDelims">Determines if delimiters are included in the results.</param>
		public Tokenizer(string source, string delimiters, bool includeDelims):this(source,delimiters)
		{
			this.includeDelims = includeDelims;
		}	


		/// <summary>
		/// Returns the next token from the token list
		/// </summary>
		/// <returns>The string value of the token</returns>
		public string NextToken()
		{				
			return NextToken(this.delimiters);
		}

		/// <summary>
		/// Returns the next token from the source string, using the provided
		/// token delimiters
		/// </summary>
		/// <param name="delimiters">String containing the delimiters to use</param>
		/// <returns>The string value of the token</returns>
		public string NextToken(string delimiters)
		{
			//According to documentation, the usage of the received delimiters should be temporary (only for this call).
			//However, it seems it is not true, so the following line is necessary.
			this.delimiters = delimiters;

			//at the end 
			if (this.currentPos == this.chars.Length)
				throw new System.ArgumentOutOfRangeException();
			//if over a delimiter and delimiters must be returned
			else if (   (System.Array.IndexOf(delimiters.ToCharArray(),chars[this.currentPos]) != -1)
				     && this.includeDelims )                	
				return "" + this.chars[this.currentPos++];
			//need to get the token wo delimiters.
			else
				return nextToken(delimiters.ToCharArray());
		}

		//Returns the nextToken wo delimiters
		private string nextToken(char[] delimiters)
		{
			string token="";
			long pos = this.currentPos;

			//skip possible delimiters
			while (System.Array.IndexOf(delimiters,this.chars[currentPos]) != -1)
				//The last one is a delimiter (i.e there is no more tokens)
				if (++this.currentPos == this.chars.Length)
				{
					this.currentPos = pos;
					throw new System.ArgumentOutOfRangeException();
				}
			
			//getting the token
			while (System.Array.IndexOf(delimiters,this.chars[this.currentPos]) == -1)
			{
				token+=this.chars[this.currentPos];
				//the last one is not a delimiter
				if (++this.currentPos == this.chars.Length)
					break;
			}
			return token;
		}

				
		/// <summary>
		/// Determines if there are more tokens to return from the source string
		/// </summary>
		/// <returns>True or false, depending if there are more tokens</returns>
		public bool HasMoreTokens()
		{
			//keeping the current pos
			long pos = this.currentPos;
			
			try
			{
				this.NextToken();
			}
			catch (System.ArgumentOutOfRangeException)
			{				
				return false;
			}
			finally
			{
				this.currentPos = pos;
			}
			return true;
		}

		/// <summary>
		/// Remaining tokens count
		/// </summary>
		public int Count
		{
			get
			{
				//keeping the current pos
				long pos = this.currentPos;
				int i = 0;
			
				try
				{
					while (true)
					{
						this.NextToken();
						i++;
					}
				}
				catch (System.ArgumentOutOfRangeException)
				{				
					this.currentPos = pos;
					return i;
				}
			}
		}

		/// <summary>
		///  Performs the same action as NextToken.
		/// </summary>
		public System.Object Current
		{
			get
			{
				return (Object) this.NextToken();
			}		
		}		
		
		/// <summary>
		//  Performs the same action as HasMoreTokens.
		/// </summary>
		/// <returns>True or false, depending if there are more tokens</returns>
		public bool MoveNext()
		{
			return this.HasMoreTokens();
		}
		
		/// <summary>
		/// Does nothing.
		/// </summary>
		public void  Reset()
		{
			;
		}			
	}
	/*******************************/
	/// <summary>
	/// Converts an array of sbytes to an array of chars
	/// </summary>
	/// <param name="sByteArray">The array of sbytes to convert</param>
	/// <returns>The new array of chars</returns>
	public static char[] ToCharArray(sbyte[] sByteArray) 
	{
		return System.Text.UTF8Encoding.UTF8.GetChars(ToByteArray(sByteArray));
	}

	/// <summary>
	/// Converts an array of bytes to an array of chars
	/// </summary>
	/// <param name="byteArray">The array of bytes to convert</param>
	/// <returns>The new array of chars</returns>
	public static char[] ToCharArray(byte[] byteArray) 
	{
		return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
	}

	/*******************************/
	/// <summary>
	/// The SplitterPanel its a panel with two controls separated by a movable splitter.
	/// </summary>
	public class SplitterPanelSupport : System.Windows.Forms.Panel
	{
		private System.Windows.Forms.Control firstControl;
		private System.Windows.Forms.Control secondControl;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.Orientation orientation;
		private int splitterSize;
		private int splitterLocation;
		private int lastSplitterLocation;

		//Default controls
		private System.Windows.Forms.Control defaultFirstControl;
		private System.Windows.Forms.Control defaultSecondControl;

		/// <summary>
		/// Creates a SplitterPanel with Horizontal orientation and two buttons as the default
		/// controls. The default size of the splitter is set to 5.
		/// </summary>
		public SplitterPanelSupport() : base()
		{
			System.Windows.Forms.Button button1 = new System.Windows.Forms.Button();
			System.Windows.Forms.Button button2 = new System.Windows.Forms.Button();
			button1.Text = "button1";
			button2.Text = "button2";
				
			this.lastSplitterLocation = -1;
			this.orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitterSize = 5;

			this.defaultFirstControl  = button1;
			this.defaultSecondControl = button2;
			this.firstControl  = this.defaultFirstControl;
			this.secondControl = this.defaultSecondControl;
			this.splitterLocation = this.firstControl.Size.Width;
			this.splitter = new System.Windows.Forms.Splitter();
			this.SuspendLayout();

			//
			// panel1
			//
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.firstControl);
			this.Controls.Add(this.secondControl);
				
			// 
			// firstControl
			// 
			this.firstControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.firstControl.Name = "firstControl";
			this.firstControl.TabIndex = 0;
				
			// 
			// secondControl
			//
			this.secondControl.Name = "secondControl";
			this.secondControl.TabIndex = 1;
			this.secondControl.Size = new System.Drawing.Size((this.Size.Width - this.firstControl.Size.Width) + this.splitterSize, this.Size.Height);
			this.secondControl.Location = new System.Drawing.Point((this.firstControl.Location.X + this.firstControl.Size.Width + this.splitterSize), 0);

			// 
			// splitter
			//			
			this.splitter.Name = "splitter";
			this.splitter.TabIndex = 2;
			this.splitter.TabStop = false;
			this.splitter.MinExtra = 10;
			this.splitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitter.Size = new System.Drawing.Size(this.splitterSize, this.Size.Height);
			this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(splitter_SplitterMoved);
				
			this.SizeChanged += new System.EventHandler(SplitterPanel_SizeChanged);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Creates a new SplitterPanelSupport with two buttons as default controls and the specified
		/// splitter orientation.
		/// </summary>
		/// <param name="newOrientation">The orientation of the SplitterPanel.</param>
		public SplitterPanelSupport(int newOrientation) : this()
		{
			this.SplitterOrientation = (System.Windows.Forms.Orientation) newOrientation;
		}

		/// <summary>
		/// Creates a new SplitterPanelSupport with the specified controls and orientation.
		/// </summary>
		/// <param name="newOrientation">The orientation of the SplitterPanel.</param>
		/// <param name="first">The first control of the panel, left-top control.</param>
		/// <param name="second">The second control of the panel, right-botton control.</param>
		public SplitterPanelSupport(int newOrientation, System.Windows.Forms.Control first, System.Windows.Forms.Control second) : this(newOrientation)
		{
			this.FirstControl  = first;
			this.SecondControl = second;
		}


		/// <summary>
		/// Creates a new SplitterPanelSupport with the specified controls and orientation.
		/// </summary>		
		/// <param name="first">The first control of the panel, left-top control.</param>
		/// <param name="second">The second control of the panel, right-botton control.</param>
		public SplitterPanelSupport(System.Windows.Forms.Control first, System.Windows.Forms.Control second) : this()
		{
			this.FirstControl  = first;
			this.SecondControl = second;
		}

		/// <summary>
		/// Adds a control to the SplitterPanel in the first available position.
		/// </summary>		
		/// <param name="control">The control to by added.</param>
		public void Add(System.Windows.Forms.Control control)
		{
			if(FirstControl == defaultFirstControl)
				FirstControl = control;
			else if(SecondControl == defaultSecondControl) 
				SecondControl = control;
		}

		/// <summary>
		/// Adds a control to the SplitterPanel in the specified position.
		/// </summary>		
		/// <param name="control">The control to by added.</param>
		/// <param name="position">The position to add the control in the SpliterPanel.</param>
		public void Add(System.Windows.Forms.Control control, SpliterPosition position)
		{
			if(position == SpliterPosition.First)
				FirstControl = control;
			else if(position == SpliterPosition.Second) 
				SecondControl = control;
		}

		/// <summary>
		/// Defines the possible positions of a control in a SpliterPanel.
		/// </summary>		
		public enum SpliterPosition
		{
			First,
			Second,
		}

		/// <summary>
		/// Gets the specified component.
		/// </summary>
		/// <param name="name">the name of the part of the component to get: "nw": first control, 
		/// "se": second control, "splitter": splitter.</param>
		/// <returns>returns the specified component.</returns>
		public virtual System.Windows.Forms.Control GetComponent(string name)
		{
			if (name == "nw")
				return this.FirstControl;
			else if (name == "se")
				return this.SecondControl;
			else if (name == "splitter")
				return this.splitter;
			else return null;
		}

		/// <summary>
		/// First control of the panel. When orientation is Horizontal then this is the left control
		/// when the orientation is Vertical then this is the top control.
		/// </summary>
		public virtual System.Windows.Forms.Control FirstControl
		{
			get
			{
				return this.firstControl;
			}
			set
			{
				this.Controls.Remove(this.firstControl);
				if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
					value.Dock = System.Windows.Forms.DockStyle.Left;
				else
					value.Dock = System.Windows.Forms.DockStyle.Top;
				value.Size = this.firstControl.Size;
				this.firstControl = value;
				this.Controls.Add(this.firstControl);
			}
		}

		/// <summary>
		/// The second control of the panel. Right control when the panel is Horizontal oriented and
		/// botton control when the SplitterPanel orientation is Vertical.
		/// </summary>
		public virtual System.Windows.Forms.Control SecondControl
		{
			get
			{
				return this.secondControl;
			}
			set
			{
				this.Controls.Remove(this.secondControl);
				value.Size = this.secondControl.Size;
				value.Location = this.secondControl.Location;
				this.secondControl = value;
				this.Controls.Add(this.secondControl);
			}
		}

		/// <summary>
		/// The orientation of the SplitterPanel. Specifies how the controls are aligned.
		/// Left to right using Horizontal orientation or top to botton with Vertical orientation.
		/// </summary>
		public virtual System.Windows.Forms.Orientation SplitterOrientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (value != this.orientation)
				{
					this.orientation = value;
					if (value == System.Windows.Forms.Orientation.Vertical)
					{
						int lastWidth = this.firstControl.Size.Width;
						this.firstControl.Dock = System.Windows.Forms.DockStyle.Top;
						this.firstControl.Size = new System.Drawing.Size(this.Width, lastWidth);
						this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
					}
					else
					{
						int lastHeight = this.firstControl.Size.Height;
						this.firstControl.Dock = System.Windows.Forms.DockStyle.Left;
						this.firstControl.Size = new System.Drawing.Size(lastHeight, this.Height);
						this.splitter.Dock = System.Windows.Forms.DockStyle.Left;
					}
					this.ResizeSecondControl();
				}
			}
		}

		/// <summary>
		/// Specifies the location of the Splitter in the panel.
		/// </summary>
		public virtual int SplitterLocation
		{
			get
			{
				return this.splitterLocation;
			}
			set
			{
				if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
					this.firstControl.Size = new System.Drawing.Size(value, this.Height);
				else
					this.firstControl.Size = new System.Drawing.Size(this.Width, value);					
				this.ResizeSecondControl();
				this.lastSplitterLocation = this.splitterLocation;
				this.splitterLocation = value;
			}
		}

		/// <summary>
		/// The last location of the splitter on the panel.
		/// </summary>
		public virtual int LastSplitterLocation
		{
			get
			{
				return this.lastSplitterLocation;
			}
			set
			{
				this.lastSplitterLocation = value;
			}
		}

		/// <summary>
		/// Specifies the size of the splitter divider.
		/// </summary>
		public virtual int SplitterSize
		{
			get
			{
				return this.splitterSize;
			}
			set
			{
				this.splitterSize = value;
				if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
					this.splitter.Size = new System.Drawing.Size(this.splitterSize, this.Size.Height);
				else
					this.splitter.Size = new System.Drawing.Size(this.Size.Width, this.splitterSize);
				this.ResizeSecondControl();
			}
		}

		/// <summary>
		/// The minimum location of the splitter on the panel.
		/// </summary>
		/// <returns>The minimum location value for the splitter.</returns>
		public virtual int GetMinimumLocation()
		{
			return this.splitter.MinSize;
		}

		/// <summary>
		/// The maximum location of the splitter on the panel.
		/// </summary>
		/// <returns>The maximum location value for the splitter.</returns>
		public virtual int GetMaximumLocation()
		{
			if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
				return this.Width - ( this.SplitterSize / 2 );
			else
				return this.Height - ( this.SplitterSize / 2 );
		}

		/// <summary>
		/// Adds a control to splitter panel.
		/// </summary>
		/// <param name="controlToAdd">The control to add.</param>
		/// <param name="dockStyle">The dock style for the control, left-top, or botton-right.</param>
		/// <param name="index">The index of the control in the panel control list.</param>
		protected virtual void AddControl(System.Windows.Forms.Control controlToAdd, System.Object dockStyle, int index)
		{
			if (dockStyle is string)
			{
				string dock = (string)dockStyle;
				if (dock == "botton" || dock == "right")
					this.SecondControl = controlToAdd;
				else if (dock == "top" || dock == "left")
					this.FirstControl  = controlToAdd;
				else
					throw new System.ArgumentException("Cannot add control: unknown constraint: " + dockStyle.ToString());
				this.Controls.SetChildIndex(controlToAdd, index);
			}
			else
				throw new System.ArgumentException("Cannot add control: unknown constraint: " + dockStyle.ToString());
		}

		/// <summary>
		/// Removes the specified control from the panel.
		/// </summary>
		/// <param name="controlToRemove">The control to remove.</param>
		public virtual void RemoveControl(System.Windows.Forms.Control controlToRemove)
		{
			if (this.Contains(controlToRemove))
			{
				this.Controls.Remove(controlToRemove);
				if (this.firstControl == controlToRemove)
					this.secondControl.Dock = System.Windows.Forms.DockStyle.Fill;
				else
					this.firstControl.Dock = System.Windows.Forms.DockStyle.Fill;
			}
		}

		/// <summary>
		/// Remove the control identified by the specified index.
		/// </summary>
		/// <param name="index">The index of the control to remove.</param>
		public virtual void RemoveControl(int index)
		{
			try 
			{
				this.Controls.RemoveAt(index);
				if (this.firstControl != null)
					if (this.Controls.Contains(this.firstControl))
						this.firstControl.Dock = System.Windows.Forms.DockStyle.Fill;
					else if (this.secondControl != null && (this.Controls.Contains(this.secondControl)))
						this.secondControl.Dock = System.Windows.Forms.DockStyle.Fill;
			} // Compatibility with the conversion assistant.
			catch (System.ArgumentOutOfRangeException)
			{
				throw new System.IndexOutOfRangeException("No such child: " + index);
			}
		}
			
		/// <summary>
		/// Changes the location of the splitter in the panel as a percentage of the panel's size.
		/// </summary>
		/// <param name="proportion">The percentage from 0.0 to 1.0.</param>
		public virtual void SetLocationProportional(double proportion)
		{
			if ((proportion > 0.0) && (proportion < 1.0))
				this.SplitterLocation = (int)((this.orientation == System.Windows.Forms.Orientation.Horizontal) ? (proportion * this.Width) : (proportion * this.Height));
			else
				throw new System.ArgumentException("Proportional location must be between 0.0 and 1.0");
		}

		private void splitter_SplitterMoved(System.Object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			this.lastSplitterLocation = this.splitterLocation;
			if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
				this.splitterLocation = this.firstControl.Width;
			else
				this.splitterLocation = this.firstControl.Height;
			this.ResizeSecondControl();
		}

		private void SplitterPanel_SizeChanged(System.Object sender, System.EventArgs e)
		{
			this.ResizeSecondControl();
		}

		private void ResizeSecondControl()
		{
			if (this.orientation == System.Windows.Forms.Orientation.Horizontal)
			{
				this.secondControl.Size = new System.Drawing.Size((this.Width - (this.firstControl.Size.Width + this.splitterSize)), this.Size.Height);
				this.secondControl.Location = new System.Drawing.Point((this.firstControl.Size.Width + this.splitterSize), 0);
			}
			else
			{
				this.secondControl.Size = new System.Drawing.Size(this.Size.Width, (this.Size.Height - (this.firstControl.Size.Height + this.splitterSize)));
				this.secondControl.Location = new System.Drawing.Point(0, (this.firstControl.Size.Height + this.splitterSize));
			}
		}
	}


	/*******************************/
	/// <summary>
	/// This class contains static methods to manage TreeViews.
	/// </summary>
	public class TreeSupport
	{
		/// <summary>
		/// Creates a new TreeView from the provided HashTable.
		/// </summary> 
		/// <param name="hashTable">HashTable</param>		
		/// <returns>Returns the created tree</returns>
		public static System.Windows.Forms.TreeView CreateTreeView(System.Collections.Hashtable hashTable)
		{
			System.Windows.Forms.TreeView tree = new System.Windows.Forms.TreeView();
			return SetTreeView(tree,hashTable);
		}

		/// <summary>
		/// Sets a TreeView with data from the provided HashTable.
		/// </summary> 
		/// <param name="treeView">Tree.</param>
		/// <param name="hashTable">HashTable.</param>
		/// <returns>Returns the set tree.</returns>		
		public static System.Windows.Forms.TreeView SetTreeView(System.Windows.Forms.TreeView treeView, System.Collections.Hashtable hashTable)
		{
			foreach (System.Collections.DictionaryEntry myEntry in hashTable)
			{				
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();

				if (myEntry.Value is System.Collections.ArrayList)
				{
					root.Text = "[";
					FillNode(root, (System.Collections.ArrayList)myEntry.Value);	
					root.Text = root.Text + "]";				
				}
				else if (myEntry.Value is System.Object[])
				{
					root.Text = "[";
					FillNode(root,(System.Object[])myEntry.Value);
					root.Text = root.Text + "]";
				}
				else if (myEntry.Value is System.Collections.Hashtable)
				{
					root.Text = "[";
					FillNode(root,(System.Collections.Hashtable)myEntry.Value);
					root.Text = root.Text + "]";
				}
				else
					root.Text = myEntry.ToString();

				treeView.Nodes.Add(root);					
			}
			return treeView;
		}
		

		/// <summary>
		/// Creates a new TreeView from the provided ArrayList.
		/// </summary> 
		/// <param name="arrayList">ArrayList.</param>		
		/// <returns>Returns the created tree.</returns>
		public static System.Windows.Forms.TreeView CreateTreeView(System.Collections.ArrayList arrayList)
		{
			System.Windows.Forms.TreeView tree = new System.Windows.Forms.TreeView();
			return SetTreeView(tree, arrayList);
		}

		/// <summary>
		/// Sets a TreeView with data from the provided ArrayList.
		/// </summary> 
		/// <param name="treeView">Tree.</param>
		/// <param name="arrayList">ArrayList.</param>
		/// <returns>Returns the set tree.</returns>
		public static System.Windows.Forms.TreeView SetTreeView(System.Windows.Forms.TreeView treeView, System.Collections.ArrayList arrayList)		
		{
			foreach (System.Object arrayEntry in arrayList)
			{
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();
		
				if (arrayEntry is System.Collections.ArrayList)
				{
					root.Text = "[";
					FillNode(root, (System.Collections.ArrayList)arrayEntry);
					root.Text = root.Text + "]";
				}
				else if (arrayEntry is System.Collections.Hashtable)
				{
					root.Text = "[";
					FillNode(root,(System.Collections.Hashtable)arrayEntry);
					root.Text = root.Text + "]";
				}
				else if (arrayEntry is System.Object[])	
				{
					root.Text = "[";
					FillNode(root,(System.Object[])arrayEntry);
					root.Text = root.Text + "]";
				}
				else
					root.Text = arrayEntry.ToString();
				
		
				treeView.Nodes.Add(root);					
			}
			return treeView;
		}		
		
		/// <summary>
		/// Creates a new TreeView from the provided Object Array.
		/// </summary> 
		/// <param name="objectArray">Object Array.</param>		
		/// <returns>Returns the created tree.</returns>
		public static System.Windows.Forms.TreeView CreateTreeView(System.Object[] objectArray)
		{
			System.Windows.Forms.TreeView tree = new System.Windows.Forms.TreeView();
			return SetTreeView(tree, objectArray);
		}

		/// <summary>
		/// Sets a TreeView with data from the provided Object Array.
		/// </summary> 
		/// <param name="treeView">Tree.</param>
		/// <param name="objectArray">Object Array.</param>
		/// <returns>Returns the created tree.</returns>
		public static System.Windows.Forms.TreeView SetTreeView(System.Windows.Forms.TreeView treeView, System.Object[] objectArray)
		{
			foreach (System.Object arrayEntry in objectArray)
			{		
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();

				if (arrayEntry is System.Collections.ArrayList)
				{
					root.Text = "[";
					FillNode(root,(System.Collections.ArrayList)arrayEntry);
					root.Text = root.Text + "]";
				}
				else if (arrayEntry is System.Collections.Hashtable)				
				{
					root.Text = "[";
					FillNode(root,(System.Collections.Hashtable)arrayEntry);						
					root.Text = root.Text + "]";		
				}
				else if (arrayEntry is System.Object[])
				{
					root.Text = "[";
					FillNode(root,(System.Object[])arrayEntry);					
					root.Text = root.Text + "]";
				}
				else
					root.Text = arrayEntry.ToString();

				treeView.Nodes.Add(root);
			}		
			return treeView;
		}		

		/// <summary>
		/// Creates a new TreeView with the provided TreeNode as root.
		/// </summary> 
		/// <param name="root">Root.</param>		
		/// <returns>Returns the created tree.</returns>
		public static System.Windows.Forms.TreeView CreateTreeView(System.Windows.Forms.TreeNode root)
		{
			System.Windows.Forms.TreeView tree = new System.Windows.Forms.TreeView();
			return SetTreeView(tree, root);
		}

		/// <summary>
		/// Sets a TreeView with the provided TreeNode as root.
		/// </summary>
		/// <param name="treeView">Tree</param>
		/// <param name="root">Root</param>
		/// <returns>Returns the created tree</returns>
		public static System.Windows.Forms.TreeView SetTreeView(System.Windows.Forms.TreeView treeView, System.Windows.Forms.TreeNode root)
		{
			if (root != null)
				treeView.Nodes.Add(root);
			return treeView;
		}
			
		/// <summary>
		/// Sets a TreeView with the provided TreeNode as root.
		/// </summary> 
		/// <param name="model">Root data model.</param>
		public static void SetModel(System.Windows.Forms.TreeView tree, System.Windows.Forms.TreeNode model)
		{
			tree.Nodes.Clear();
			tree.Nodes.Add(model);
		}
			
		/// <summary>
		/// Get the root TreeNode from a TreeView.
		/// </summary> 
		/// <param name="tree">Tree.</param>
		public static System.Windows.Forms.TreeNode GetModel(System.Windows.Forms.TreeView tree)
		{
			if (tree.Nodes.Count > 0 )
				return tree.Nodes[0];
			else
				return null;
		}

		/// <summary>
		/// Sets a TreeNode with data from the provided ArrayList instance.
		/// </summary> 
		/// <param name="treeNode">Node.</param>
		/// <param name="arrayList">ArrayList.</param>
		/// <returns>Returns the set node.</returns>
		public static System.Windows.Forms.TreeNode FillNode(System.Windows.Forms.TreeNode treeNode, System.Collections.ArrayList arrayList)
		{		
			foreach (System.Object arrayEntry in arrayList)
			{
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();				

				if (arrayEntry is System.Collections.ArrayList)
				{
					root.Text = "[";
					FillNode(root, (System.Collections.ArrayList)arrayEntry);
					root.Text = root.Text + "]";
					treeNode.Nodes.Add(root);
					treeNode.Text = treeNode.Text + ", " + root.Text;
				}
				else if (arrayEntry is System.Object[])
				{					
					root.Text = "[";
					FillNode(root,(System.Object[])arrayEntry);	
					root.Text = root.Text + "]";
					treeNode.Nodes.Add(root);	
					treeNode.Text = treeNode.Text + ", " + root.Text;
				}
				else if (arrayEntry is System.Collections.Hashtable)
				{
					root.Text = "[";
					FillNode(root,(System.Collections.Hashtable)arrayEntry);	
					root.Text = root.Text + "]";
					treeNode.Nodes.Add(root);	
					treeNode.Text = treeNode.Text + ", " + root.Text;
				}
				else
				{
					treeNode.Nodes.Add(arrayEntry.ToString());
					if (!(treeNode.Text.Equals("")))
					{
						if (treeNode.Text[treeNode.Text.Length-1].Equals('['))
							treeNode.Text = treeNode.Text + arrayEntry.ToString();
						else
							treeNode.Text = treeNode.Text + ", " + arrayEntry.ToString();
					}
					else
						treeNode.Text = treeNode.Text + ", " + arrayEntry.ToString();
				}
			}
			return treeNode;
		}
		

		/// <summary>
		/// Sets a TreeNode with data from the provided ArrayList.
		/// </summary> 
		/// <param name="treeNode">Node.</param>
		/// <param name="objectArray">Object Array.</param>
		/// <returns>Returns the set node.</returns>
		
		public static System.Windows.Forms.TreeNode FillNode(System.Windows.Forms.TreeNode treeNode, System.Object[] objectArray)
		{
			foreach (System.Object arrayEntry in objectArray)
			{
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();

				if (arrayEntry is System.Collections.ArrayList)
				{
					root.Text = "[";
					FillNode(root,(System.Collections.ArrayList)arrayEntry);									
					root.Text = root.Text + "]";
				}
				else if (arrayEntry is System.Collections.Hashtable)				
				{
					root.Text = "[";
					FillNode(root,(System.Collections.Hashtable)arrayEntry);
					root.Text = root.Text + "]";				
				}
				else
				{
					root.Nodes.Add(objectArray.ToString());
					root.Text = root.Text + ", " + objectArray.ToString();
				}
				treeNode.Nodes.Add(root);
				treeNode.Text = treeNode.Text + root.Text;
			}
			return treeNode;
		}
		
		/// <summary>		
		/// Sets a TreeNode with data from the provided Hashtable.		
		/// </summary> 		
		/// <param name="treeNode">Node.</param>		
		/// <param name="hashTable">Hash Table Object.</param>		
		/// <returns>Returns the set node.</returns>		
		public static System.Windows.Forms.TreeNode FillNode(System.Windows.Forms.TreeNode treeNode, System.Collections.Hashtable hashTable)
		{		
			foreach (System.Collections.DictionaryEntry myEntry in hashTable)
			{
				System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode();				

				if (myEntry.Value is System.Collections.ArrayList)
				{
					FillNode(root, (System.Collections.ArrayList)myEntry.Value);
					treeNode.Nodes.Add(root);
				}
				else if (myEntry.Value is System.Object[])
				{
					FillNode(root,(System.Object[])myEntry.Value);	
					treeNode.Nodes.Add(root);	
				}
				else
					treeNode.Nodes.Add(myEntry.Key.ToString());
			}
			return treeNode;
		}
	}
	/*******************************/
	public class FormSupport
	{
		/// <summary>
		/// Creates a Form instance and sets the property Text to the specified parameter.
		/// </summary>
		/// <param name="Text">Value for the Form property Text</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(string text)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Text = text;
			return tempForm;
		}

		/// <summary>
		/// Creates a Form instance and sets the property Text to the specified parameter.
		/// Adds the received control to the Form's Controls collection in the position 0.
		/// </summary>
		/// <param name="text">Value for the Form Text property.</param>
		/// <param name="control">Control to be added to the Controls collection.</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(string text, System.Windows.Forms.Control control )
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Text = text;
			tempForm.Controls.Add( control );	
			tempForm.Controls.SetChildIndex( control, 0 );
			return tempForm;
		}
		
		
		/// <summary>
		/// Creates a Form instance and sets the property Owner to the specified parameter.
		/// This also sets the FormBorderStyle and ShowInTaskbar properties.
		/// </summary>
		/// <param name="Owner">Value for the Form property Owner</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(System.Windows.Forms.Form owner)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Owner = owner;
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			tempForm.ShowInTaskbar = false;
			return tempForm;
		}


		/// <summary>
		/// Creates a Form instance and sets the property Owner to the specified parameter.
		/// Sets the FormBorderStyle and ShowInTaskbar properties.
		/// Also add the received Control to the Form's Controls collection in the position 0.
		/// </summary>
		/// <param name="owner">Value for the Form property Owner</param>
		/// <param name="header">Control to be added to the Form's Controls collection</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(System.Windows.Forms.Form owner, System.Windows.Forms.Control header)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Owner = owner;
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			tempForm.ShowInTaskbar = false;
			tempForm.Controls.Add( header );
			tempForm.Controls.SetChildIndex( header, 0 );
			return tempForm;
		}

		/// <summary>
		/// Creates a Form instance and sets the FormBorderStyle property.
		/// </summary>
		/// <param name="title">The title of the Form</param>
		/// <param name="resizable">Boolean value indicating if the Form is or not resizable</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(string title,bool resizable)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Text = title;
			if(resizable)
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.Sizable;
			else
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.FixedSingle;
			tempForm.TopLevel = false;
			tempForm.MaximizeBox = false;
			tempForm.MinimizeBox = false;
			return tempForm;
		}

		/// <summary>
		/// Creates a Form instance and sets the FormBorderStyle property.
		/// </summary>
		/// <param name="title">The title of the Form</param>
		/// <param name="resizable">Boolean value indicating if the Form is or not resizable</param>
		/// <param name="maximizable">Boolean value indicating if the Form displays the maximaze box</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(string title,bool resizable, bool maximizable)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Text = title;
			if(resizable)
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.Sizable;
			else
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.FixedSingle;
			tempForm.TopLevel = false;
			tempForm.MaximizeBox = maximizable;
			tempForm.MinimizeBox = false;
			return tempForm;
		}

		/// <summary>
		/// Creates a Form instance and sets the FormBorderStyle property.
		/// </summary>
		/// <param name="title">The title of the Form</param>
		/// <param name="resizable">Boolean value indicating if the Form is or not resizable</param>
		/// <param name="maximizable">Boolean value indicating if the Form displays the maximaze box</param>
		/// <param name="minimizable">Boolean value indicating if the Form displays the minimaze box</param>
		/// <returns>A new Form instance</returns>
		public static System.Windows.Forms.Form CreateForm(string title,bool resizable, bool maximizable, bool minimizable)
		{
			System.Windows.Forms.Form tempForm;
			tempForm = new System.Windows.Forms.Form();
			tempForm.Text = title;
			if(resizable)
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.Sizable;
			else
				tempForm.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.FixedSingle;
			tempForm.TopLevel = false;
			tempForm.MaximizeBox = maximizable;
			tempForm.MinimizeBox = minimizable;
			return tempForm;
		}

		/// <summary>
		/// Receives a Form instance and sets the property Owner to the specified parameter.
		/// This also sets the FormBorderStyle and ShowInTaskbar properties.
		/// </summary>
		/// <param name="formInstance">Form instance to be set</param>
		/// <param name="Owner">Value for the Form property Owner</param>
		public static void SetForm(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner)
		{
			formInstance.Owner = owner;
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			formInstance.ShowInTaskbar = false;
		}

		/// <summary>
		/// Receives a Form instance, sets the Text property and adds a Control.
		/// The received Control is added in the position 0 of the Form's Controls collection.
		/// </summary>
		/// <param name="formInstance">Form instance to be set</param>
		/// <param name="text">Value to be set to the Text property.</param>
		/// <param name="control">Control to add to the Controls collection.</param>
		public static void SetForm(System.Windows.Forms.Form formInstance, string text, System.Windows.Forms.Control control )
		{
			formInstance.Text = text;
			formInstance.Controls.Add( control );	
			formInstance.Controls.SetChildIndex( control, 0 );
		}
		
		/// <summary>
		/// Receives a Form instance and sets the property Owner to the specified parameter.
		/// Sets the FormBorderStyle and ShowInTaskbar properties.
		/// Also adds the received Control to the Form's Controls collection in position 0.
		/// </summary>
		/// <param name="formInstance">Form instance to be set</param>
		/// <param name="owner">Value for the Form property Owner</param>
		/// <param name="header">The Control to be added to the Controls collection.</param>
		public static void SetForm(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner, System.Windows.Forms.Control header)
		{
			formInstance.Owner = owner;
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			formInstance.ShowInTaskbar = false;
			formInstance.Controls.Add( header );
			formInstance.Controls.SetChildIndex( header, 0 );
		}
	}
	/*******************************/
	/// <summary>
	/// Method used to obtain the underlying type of an object to make the correct method call.
	/// </summary>
	/// <param name="tempObject">Object instance received.</param>
	/// <param name="method">Method name to invoke.</param>
	/// <param name="parameters">Object array containing the method parameters.</param>
	/// <returns>The return value of the method called with the proper parameters.</returns>
	public static System.Object InvokeMethodAsVirtual(System.Object tempObject, string method, System.Object[] parameters)
	{
		System.Reflection.MethodInfo methodInfo;
		System.Type type = tempObject.GetType();
		if (parameters != null)
		{
			System.Type[] types = new System.Type[parameters.Length];
			for (int index = 0; index < parameters.Length; index++)
				types[index] = parameters[index].GetType();
			methodInfo = type.GetMethod(method, types);
		}
		else methodInfo = type.GetMethod(method);
		try
		{
			return methodInfo.Invoke(tempObject, parameters);
		}
		catch (System.Exception exception)
		{
			throw exception.InnerException;
		}
	}

	/*******************************/
	/// <summary>
	/// Class used to store and retrieve an object command specified as a String.
	/// </summary>
	public class CommandManager
	{
		/// <summary>
		/// Private Hashtable used to store objects and their commands.
		/// </summary>
		private static System.Collections.Hashtable Commands = new System.Collections.Hashtable();

		/// <summary>
		/// Sets a command to the specified object.
		/// </summary>
		/// <param name="obj">The object that has the command.</param>
		/// <param name="cmd">The command for the object.</param>
		public static void SetCommand(System.Object obj, string cmd)
		{
			if (obj != null)
			{
				if (Commands.Contains(obj))
					Commands[obj] = cmd;
				else
					Commands.Add(obj, cmd);
			}
		}

		/// <summary>
		/// Gets a command associated with an object.
		/// </summary>
		/// <param name="obj">The object whose command is going to be retrieved.</param>
		/// <returns>The command of the specified object.</returns>
		public static string GetCommand(System.Object obj)
		{
			string result = "";
			if (obj != null)
				result = System.Convert.ToString(Commands[obj]);
			return result;
		}



		/// <summary>
		/// Checks if the Control contains a command, if it does not it sets the default
		/// </summary>
		/// <param name="button">The control whose command will be checked</param>
		public static void CheckCommand(System.Windows.Forms.ButtonBase button)
		{
			if (button != null)
			{
				if (GetCommand(button).Equals(""))
					SetCommand(button, button.Text);
			}
		}

		///// <summary>
		///// Checks if the Control contains a command, if it does not it sets the default
		///// </summary>
		///// <param name="button">The control whose command will be checked</param>
		//public static void CheckCommand(System.Windows.Forms.MenuItem menuItem)
		//{
		//	if (menuItem != null)
		//	{
		//		if (GetCommand(menuItem).Equals(""))
		//			SetCommand(menuItem, menuItem.Text);
		//	}
		//}

		/// <summary>
		/// Checks if the Control contains a command, if it does not it sets the default
		/// </summary>
		/// <param name="button">The control whose command will be checked</param>
		public static void CheckCommand(System.Windows.Forms.ComboBox comboBox)
		{
			if (comboBox != null)
			{
				if (GetCommand(comboBox).Equals(""))
					SetCommand(comboBox,"comboBoxChanged");
			}
		}

	}
	/*******************************/
	/// <summary>
	/// Selects a range of text in the text box. 
	/// </summary>
	/// <param name="textContainer">The current text box.</param>
	/// <param name="start">The position of the first character in the current text selection within the text box.</param>
	/// <param name="offset">The number of characters to select.</param>
	public static void SelectText(System.Windows.Forms.TextBoxBase textContainer, int start, int offset)
	{
		if (offset - start < 0)
			((System.Windows.Forms.TextBoxBase)textContainer).Select(start,0);
		else
			((System.Windows.Forms.TextBoxBase)textContainer).Select(start, offset - start);
	}


	/*******************************/
	/// <summary>
	/// Replaces the currently selected content with new content represented by the given string. 
	/// If there is no selection this amounts to an insert of the given text.
	/// If there is no replacement text this amounts to a removal of the current selection. 
	/// </summary>
	/// <param name="textbox">The Text Object to apply the changes</param>
	/// <param name="text">The content to replace the selection with</param>
	public static void ReplaceSelection(System.Windows.Forms.TextBoxBase textbox, string text)
	{
		if (textbox.SelectionLength > 0)
		{
			int SelStart = ((System.Windows.Forms.TextBoxBase)textbox).SelectionStart;			
			if (text.Equals(""))
				((System.Windows.Forms.TextBoxBase)textbox).Text = ((System.Windows.Forms.TextBoxBase)textbox).Text.Remove(SelStart, ((System.Windows.Forms.TextBoxBase)textbox).SelectionLength);
			else
				((System.Windows.Forms.TextBoxBase)textbox).Text = (((System.Windows.Forms.TextBoxBase)textbox).Text.Remove(SelStart, ((System.Windows.Forms.TextBoxBase)textbox).SelectionLength)).Insert(SelStart, text);
		}
		else
			((System.Windows.Forms.TextBoxBase)textbox).Text = text + ((System.Windows.Forms.TextBoxBase)textbox).Text;
	}
	/*******************************/
	/// <summary>
	/// Method used to obtain the underlying type of an object to make the correct property call.
	/// The method is used when setting values to a property.
	/// </summary>
	/// <param name="tempObject">Object instance received.</param>
	/// <param name="propertyName">Property name to work on.</param>
	/// <param name="newValue">Object containing the value to assing.</param>
	/// <returns>The return value of the property assignment.</returns>
	public static System.Object SetPropertyAsVirtual(System.Object tempObject, string propertyName, System.Object newValue)
	{
		System.Type type = tempObject.GetType();
		System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
		propertyInfo.SetValue(tempObject, newValue, null);
		try
		{
			return propertyInfo.GetValue(tempObject, null);
		}
		catch(Exception e)
		{
			throw e.InnerException;
		}
	}


	/*******************************/
	/// <summary>
	/// This method works as a handler for the Control.Layout event, it arranges the controls into the container 
	/// control in a left-to-right orientation.
	/// The location of each control will be calculated according the number of them in the container. 
	/// The corresponding alignment, the horizontal and vertical spacing between the inner controls are specified
	/// as an array of object values in the Tag property of the container.
	/// </summary>
	/// <param name="event_sender">The container control in which the controls will be relocated.</param>
	/// <param name="eventArgs">Data of the event.</param>
	public static void FlowLayoutResize(System.Object event_sender, System.Windows.Forms.LayoutEventArgs eventArgs)
	{
		System.Windows.Forms.Control container = (System.Windows.Forms.Control) event_sender;
		if (container.Tag is System.Array)
		{
			System.Object[] items = (System.Object[]) container.Tag;
			if (items.Length == 3)
			{
				container.SuspendLayout();

				int width = container.Width;
				int height = container.Height;
				if (!(container is System.Windows.Forms.ScrollableControl))
				{
					width = container.DisplayRectangle.Width;
					height = container.DisplayRectangle.Height;
				}
				else
					if (container is System.Windows.Forms.Form)
					{
						width = ((System.Windows.Forms.Form) container).ClientSize.Width;
						height = ((System.Windows.Forms.Form) container).ClientSize.Height;
					}
				System.Drawing.ContentAlignment alignment = (System.Drawing.ContentAlignment) items[0];
				int horizontal = (int) items[1];
				int vertical = (int) items[2];

				// Split controls in several rows
				System.Collections.ArrayList rows = new System.Collections.ArrayList();
				System.Collections.ArrayList list = new System.Collections.ArrayList();
				int tempWidth = 0;
				int tempHeight = 0;
				int totalHeight = 0;
				for (int index = 0; index < container.Controls.Count; index++)
				{
					if (tempHeight < container.Controls[index].Height)
						tempHeight = container.Controls[index].Height;

					list.Add(container.Controls[index]);

					if (index == 0) tempWidth = container.Controls[0].Width;

					if (index == container.Controls.Count - 1)
					{
						rows.Add(list);
						totalHeight += tempHeight + vertical;
					}
					else
					{
						tempWidth += horizontal + container.Controls[index + 1].Width;
						if (tempWidth >= width - horizontal * 2)
						{
							rows.Add(list);
							totalHeight += tempHeight + vertical;
							tempHeight = 0;
							list = new System.Collections.ArrayList();
							tempWidth = container.Controls[index + 1].Width;
						}
					}
				}
				totalHeight -= vertical;

				// Break out alignment coordinates
				int h = 0;
				int cx = 0;
				int cy = 0;
				if (((int) alignment & 0x00F) > 0)
				{
					h = (int) alignment;
					cy = 1;
				}
				if (((int) alignment & 0x0F0) > 0)
				{
					h = (int) alignment >> 4;
					cy = 2;
				}
				if (((int) alignment & 0xF00) > 0)
				{
					h = (int) alignment >> 8;
					cy = 3;
				}
				if (h == 1) cx = 1;
				if (h == 2) cx = 2;
				if (h == 4) cx = 3;

				int ypos = vertical;
				if (cy == 2) ypos = height / 2 - totalHeight / 2;
				if (cy == 3) ypos = height - totalHeight - vertical;
				foreach (System.Collections.ArrayList row in rows)
				{
					int maxHeight = PlaceControls(row, width, cx, ypos, horizontal);
					ypos += vertical + maxHeight;
				}
				container.ResumeLayout();
			}
		}
	}

	private static int PlaceControls(System.Collections.ArrayList controls, int width, int cx, int ypos, int horizontal)
	{
		int count = controls.Count;
		int controlsWidth = 0;
		int maxHeight = 0;
		foreach (System.Windows.Forms.Control control in controls)
		{
			controlsWidth += control.Width;
			if (maxHeight < control.Height) maxHeight = control.Height;
		}
		controlsWidth += horizontal * (count - 1);

		// Start x point
		int xpos = 0;
		if (cx == 1) xpos = horizontal; // Left
		if (cx == 2) xpos = width / 2 - controlsWidth / 2; // Center
		if (cx == 3) xpos = width - horizontal - controlsWidth; // Right

		// Place controls
		int x = xpos;
		foreach (System.Windows.Forms.Control control in controls)
		{
			int y = ypos + (maxHeight / 2) - control.Height / 2;
			control.Location = new System.Drawing.Point(x, y);
			x += control.Width + horizontal;
		}
		return maxHeight;
	}


	/*******************************/
	/// <summary>
	/// Support class for creation of Forms behaving like Dialog windows.
	/// </summary>
	public class DialogSupport
	{
		/// <summary>
		/// Creates a dialog Form.
		/// </summary>
		/// <returns>The new dialog Form instance.</returns>
		public static System.Windows.Forms.Form CreateDialog()
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			return tempForm;
		}

		/// <summary>
		/// Sets dialog like properties to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
		}

		/// <summary>
		/// Creates a dialog Form with an owner.
		/// </summary>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <returns>A new dialog Form.</returns>
		public static System.Windows.Forms.Form CreateDialog(System.Windows.Forms.Form owner)
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			tempForm.Owner = owner;
			return tempForm;
		}

		/// <summary>
		/// Sets dialog like properties and an owner to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
			formInstance.Owner = owner;
		}

		
		/// <summary>
		/// Creates a dialog Form with an owner and a text property.
		/// </summary>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <param name="text">The title text for the form.</param>
		/// <returns>The new dialog Form.</returns>
		public static System.Windows.Forms.Form CreateDialog(System.Windows.Forms.Form owner, string text)
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			tempForm.Owner = owner;
			tempForm.Text = text;
			return tempForm;
		}
				
		/// <summary>
		/// Sets dialog like properties, an owner and a text property to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <param name="text">The title text for the form.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner, string text)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
			formInstance.Owner = owner;
			formInstance.Text = text;
		}

			
		/// <summary>
		/// This method sets or unsets a resizable border style to a Form.
		/// </summary>
		/// <param name="formInstance">The form to be modified.</param>
		/// <param name="sizable">Boolean value to be set.</param>
		public static void SetSizable(System.Windows.Forms.Form formInstance, bool sizable)
		{
			if (sizable)
			{
				formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			}
			else
			{
				formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			}
		}
	}


	/*******************************/
	/// <summary>
	/// Recieves a form and an integer value representing the operation to perform when the closing 
	/// event is fired.
	/// </summary>
	/// <param name="form">The form that fire the event.</param>
	/// <param name="operation">The operation to do while the form is closing.</param>
	public static void CloseOperation(System.Windows.Forms.Form form, int operation)
	{
		switch (operation)
		{
			case 0:
				break;
			case 1:
				form.Hide();
				break;
			case 2:
				form.Dispose();
				break;
			case 3:
				form.Dispose();
				System.Windows.Forms.Application.Exit();
				break;
		}
	}


	/*******************************/
	/// <summary>
	/// Defines a method to handle basic events and convert the Action interface
	/// </summary>
	[Serializable]
	public abstract class ActionSupport
	{
		private System.Drawing.Image icon;
		private string description;

		/// <summary>
		/// Creates a new ActionSupport.
		/// </summary>		
		public ActionSupport()
		{
			this.description = null;	
			this.icon = null;		
		}


		/// <summary>
		/// Creates a new ActionSupport.
		/// </summary>
		/// <param name="description">The description for this Action</param>
		/// <param name="icon">The icon for this Action</param>
		public ActionSupport(string description, System.Drawing.Image icon)
		{
			this.description = description;
			this.icon = icon;
		}

		/// <summary>
		/// Creates a new ActionSupport
		/// </summary>
		/// <param name="description">The description for this Action</param>
		public ActionSupport(string description)
		{
			this.description = description;	
			this.icon = null;		
		}

		/// <summary>
		/// .NET version for the actionPerformed method.
		/// </summary>
		/// <param name="event_sender">The event raising object.</param>
		/// <param name="eventArgs">The arguments for the event.</param>
		public abstract void actionPerformed(System.Object event_sender, System.EventArgs eventArgs);

		/// <summary>
		/// The icon this Action provides for controls that use it.
		/// </summary>
		public System.Drawing.Image Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;                
			}
		}

		/// <summary>
		/// The text this Action provides for controls that use it.
		/// </summary>
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}
	}


	/*******************************/
	/// <summary>
	/// Give functions to obtain information of graphic elements
	/// </summary>
	public class GraphicsManager
	{
		//Instance of GDI+ drawing surfaces graphics hashtable
		static public GraphicsHashTable manager = new GraphicsHashTable();

		/// <summary>
		/// Creates a new Graphics object from the device context handle associated with the Graphics
		/// parameter
		/// </summary>
		/// <param name="oldGraphics">Graphics instance to obtain the parameter from</param>
		/// <returns>A new GDI+ drawing surface</returns>
		public static System.Drawing.Graphics CreateGraphics(System.Drawing.Graphics oldGraphics)
		{
			System.Drawing.Graphics createdGraphics;
			System.IntPtr hdc = oldGraphics.GetHdc();
			createdGraphics = System.Drawing.Graphics.FromHdc(hdc);
			oldGraphics.ReleaseHdc(hdc);
			return createdGraphics;
		}

		/// <summary>
		/// This method draws a Bezier curve.
		/// </summary>
		/// <param name="graphics">It receives the Graphics instance</param>
		/// <param name="array">An array of (x,y) pairs of coordinates used to draw the curve.</param>
		public static void Bezier(System.Drawing.Graphics graphics, int[] array)
		{
			System.Drawing.Pen pen;
			pen = GraphicsManager.manager.GetPen(graphics);
			try
			{
				graphics.DrawBezier(pen, array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
			}
			catch(System.IndexOutOfRangeException e)
			{
				throw new System.IndexOutOfRangeException(e.ToString());
			}
		}

		/// <summary>
		/// Gets the text size width and height from a given GDI+ drawing surface and a given font
		/// </summary>
		/// <param name="graphics">Drawing surface to use</param>
		/// <param name="graphicsFont">Font type to measure</param>
		/// <param name="text">String of text to measure</param>
		/// <returns>A point structure with both size dimentions; x for width and y for height</returns>
		public static System.Drawing.Point GetTextSize(System.Drawing.Graphics graphics, System.Drawing.Font graphicsFont, string text)
		{
			System.Drawing.Point textSize;
			System.Drawing.SizeF tempSizeF;
			tempSizeF = graphics.MeasureString(text, graphicsFont);
			textSize = new System.Drawing.Point();
			textSize.X = (int) tempSizeF.Width;
			textSize.Y = (int) tempSizeF.Height;
			return textSize;
		}

		/// <summary>
		/// Gets the text size width and height from a given GDI+ drawing surface and a given font
		/// </summary>
		/// <param name="graphics">Drawing surface to use</param>
		/// <param name="graphicsFont">Font type to measure</param>
		/// <param name="text">String of text to measure</param>
		/// <param name="width">Maximum width of the string</param>
		/// <param name="format">StringFormat object that represents formatting information, such as line spacing, for the string</param>
		/// <returns>A point structure with both size dimentions; x for width and y for height</returns>
		public static System.Drawing.Point GetTextSize(System.Drawing.Graphics graphics, System.Drawing.Font graphicsFont, string text, System.Int32 width, System.Drawing.StringFormat format)
		{
			System.Drawing.Point textSize;
			System.Drawing.SizeF tempSizeF;
			tempSizeF = graphics.MeasureString(text, graphicsFont, width, format);
			textSize = new System.Drawing.Point();
			textSize.X = (int) tempSizeF.Width;
			textSize.Y = (int) tempSizeF.Height;
			return textSize;
		}

		/// <summary>
		/// Gives functionality over a hashtable of GDI+ drawing surfaces
		/// </summary>
		public class GraphicsHashTable:System.Collections.Hashtable 
		{
			/// <summary>
			/// Gets the graphics object from the given control
			/// </summary>
			/// <param name="control">Control to obtain the graphics from</param>
			/// <returns>A graphics object with the control's characteristics</returns>
			public System.Drawing.Graphics GetGraphics(System.Windows.Forms.Control control)
			{
				System.Drawing.Graphics graphic;
				if (control.Visible == true)
				{
					graphic = control.CreateGraphics();
					SetColor(graphic, control.ForeColor);
					SetFont(graphic, control.Font);
				}
				else
				{
					graphic = null;
				}
				return graphic;
			}

			/// <summary>
			/// Sets the background color property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given background color.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="color">Background color to set</param>
			public void SetBackColor(System.Drawing.Graphics graphic, System.Drawing.Color color)
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).BackColor = color;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.BackColor = color;
					Add(graphic, tempProps);
				}
			}

			/// <summary>
			/// Gets the background color property to the given graphics object in the hashtable. If the element doesn't exist, then it returns White.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The background color of the graphic</returns>
			public System.Drawing.Color GetBackColor(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return System.Drawing.Color.White;
				else
					return ((GraphicsProperties) this[graphic]).BackColor;
			}

			/// <summary>
			/// Sets the text color property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given text color.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="color">Text color to set</param>
			public void SetTextColor(System.Drawing.Graphics graphic, System.Drawing.Color color)
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).TextColor = color;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.TextColor = color;
					Add(graphic, tempProps);
				}
			}

			/// <summary>
			/// Gets the text color property to the given graphics object in the hashtable. If the element doesn't exist, then it returns White.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The text color of the graphic</returns>
			public System.Drawing.Color GetTextColor(System.Drawing.Graphics graphic) 
			{
				if (this[graphic] == null)
					return System.Drawing.Color.White;
				else
					return ((GraphicsProperties) this[graphic]).TextColor;
			}

			/// <summary>
			/// Sets the GraphicBrush property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given GraphicBrush.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="brush">GraphicBrush to set</param>
			public void SetBrush(System.Drawing.Graphics graphic, System.Drawing.SolidBrush brush) 
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).GraphicBrush = brush;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.GraphicBrush = brush;
					Add(graphic, tempProps);
				}
			}
			
			/// <summary>
			/// Sets the GraphicBrush property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given GraphicBrush.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="brush">GraphicBrush to set</param>
			public void SetPaint(System.Drawing.Graphics graphic, System.Drawing.Brush brush) 
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).PaintBrush = brush;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.PaintBrush = brush;
					Add(graphic, tempProps);
				}
			}
			
			/// <summary>
			/// Sets the GraphicBrush property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given GraphicBrush.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="color">Color to set</param>
			public void SetPaint(System.Drawing.Graphics graphic, System.Drawing.Color color) 
			{
				System.Drawing.Brush brush = new System.Drawing.SolidBrush(color);
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).PaintBrush = brush;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.PaintBrush = brush;
					Add(graphic, tempProps);
				}
			}


			/// <summary>
			/// Gets the HatchBrush property to the given graphics object in the hashtable. If the element doesn't exist, then it returns Blank.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The HatchBrush setting of the graphic</returns>
			public System.Drawing.Drawing2D.HatchBrush GetBrush(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,System.Drawing.Color.Black,System.Drawing.Color.Black);
				else
					return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,((GraphicsProperties) this[graphic]).GraphicBrush.Color,((GraphicsProperties) this[graphic]).GraphicBrush.Color);
			}
			
			/// <summary>
			/// Gets the HatchBrush property to the given graphics object in the hashtable. If the element doesn't exist, then it returns Blank.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The Brush setting of the graphic</returns>
			public System.Drawing.Brush GetPaint(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,System.Drawing.Color.Black,System.Drawing.Color.Black);
				else
					return ((GraphicsProperties) this[graphic]).PaintBrush;
			}

			/// <summary>
			/// Sets the GraphicPen property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given Pen.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="pen">Pen to set</param>
			public void SetPen(System.Drawing.Graphics graphic, System.Drawing.Pen pen) 
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).GraphicPen = pen;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.GraphicPen = pen;
					Add(graphic, tempProps);
				}
			}

			/// <summary>
			/// Gets the GraphicPen property to the given graphics object in the hashtable. If the element doesn't exist, then it returns Black.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The GraphicPen setting of the graphic</returns>
			public System.Drawing.Pen GetPen(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return System.Drawing.Pens.Black;
				else
					return ((GraphicsProperties) this[graphic]).GraphicPen;
			}

			/// <summary>
			/// Sets the GraphicFont property to the given graphics object in the hashtable. If the element doesn't exist, then it adds the graphic element to the hashtable with the given Font.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="Font">Font to set</param>
			public void SetFont(System.Drawing.Graphics graphic, System.Drawing.Font font) 
			{
				if (this[graphic] != null)
					((GraphicsProperties) this[graphic]).GraphicFont = font;
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.GraphicFont = font;
					Add(graphic,tempProps);
				}
			}

			/// <summary>
			/// Gets the GraphicFont property to the given graphics object in the hashtable. If the element doesn't exist, then it returns Microsoft Sans Serif with size 8.25.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The GraphicFont setting of the graphic</returns>
			public System.Drawing.Font GetFont(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				else
					return ((GraphicsProperties) this[graphic]).GraphicFont;
			}

			/// <summary>
			/// Sets the color properties for a given Graphics object. If the element doesn't exist, then it adds the graphic element to the hashtable with the color properties set with the given value.
			/// </summary>
			/// <param name="graphic">Graphic element to search or add</param>
			/// <param name="color">Color value to set</param>
			public void SetColor(System.Drawing.Graphics graphic, System.Drawing.Color color) 
			{
				if (this[graphic] != null)
				{
					((GraphicsProperties) this[graphic]).GraphicPen.Color = color;
					((GraphicsProperties) this[graphic]).GraphicBrush.Color = color;
					((GraphicsProperties) this[graphic]).color = color;
				}
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.GraphicPen.Color = color;
					tempProps.GraphicBrush.Color = color;
					tempProps.color = color;
					Add(graphic,tempProps);
				}
			}

			/// <summary>
			/// Gets the color property to the given graphics object in the hashtable. If the element doesn't exist, then it returns Black.
			/// </summary>
			/// <param name="graphic">Graphic element to search</param>
			/// <returns>The color setting of the graphic</returns>
			public System.Drawing.Color GetColor(System.Drawing.Graphics graphic) 
			{
				if (this[graphic] == null)
					return System.Drawing.Color.Black;
				else
					return ((GraphicsProperties) this[graphic]).color;
			}

			/// <summary>
			/// This method gets the TextBackgroundColor of a Graphics instance
			/// </summary>
			/// <param name="graphic">The graphics instance</param>
			/// <returns>The color value in ARGB encoding</returns>
			public System.Drawing.Color GetTextBackgroundColor(System.Drawing.Graphics graphic)
			{
				if (this[graphic] == null)
					return System.Drawing.Color.Black;
				else 
				{ 
					return ((GraphicsProperties) this[graphic]).TextBackgroundColor;
				}
			}

			/// <summary>
			/// This method set the TextBackgroundColor of a Graphics instace
			/// </summary>
			/// <param name="graphic">The graphics instace</param>
			/// <param name="color">The System.Color to set the TextBackgroundColor</param>
			public void SetTextBackgroundColor(System.Drawing.Graphics graphic, System.Drawing.Color color) 
			{
				if (this[graphic] != null)
				{
					((GraphicsProperties) this[graphic]).TextBackgroundColor = color;								
				}
				else
				{
					GraphicsProperties tempProps = new GraphicsProperties();
					tempProps.TextBackgroundColor = color;				
					Add(graphic,tempProps);
				}
			}

			/// <summary>
			/// Structure to store properties from System.Drawing.Graphics objects
			/// </summary>
			class GraphicsProperties
			{
				public System.Drawing.Color TextBackgroundColor = System.Drawing.Color.Black;
				public System.Drawing.Color color = System.Drawing.Color.Black;
				public System.Drawing.Color BackColor = System.Drawing.Color.White;
				public System.Drawing.Color TextColor = System.Drawing.Color.Black;
				public System.Drawing.SolidBrush GraphicBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				public System.Drawing.Brush PaintBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				public System.Drawing.Pen   GraphicPen = new System.Drawing.Pen(System.Drawing.Color.Black);
				public System.Drawing.Font  GraphicFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			}
		}
	}

	/*******************************/
	/// <summary>
	/// This method returns an Array of System.Int32 containing the size of the non client area of a control.
	/// The non client area includes elements such as scroll bars, borders, title bars, and menus.
	/// </summary>
	/// <param name="control">The control from which to retrieve the values.</param>
	/// <returns>An Array of System.Int32 containing the width of each non client area border in the following order
	/// top, left, right and bottom.</returns>
	public static System.Int32[] GetInsets(System.Windows.Forms.Control control)
	{
		System.Int32[] returnValue = new System.Int32[4];

		returnValue[0] = (control.RectangleToScreen(control.ClientRectangle).Top - control.Bounds.Top);
		returnValue[1] = (control.RectangleToScreen(control.ClientRectangle).Left  - control.Bounds.Left);
		returnValue[2] = (control.Bounds.Right - control.RectangleToScreen(control.ClientRectangle).Right);
		returnValue[3] = (control.Bounds.Bottom - control.RectangleToScreen(control.ClientRectangle).Bottom);
		return returnValue;
	}


	/*******************************/
	/// <summary>
	/// Calculates the ascent of the font, using the GetCellAscent and GetEmHeight methods
	/// </summary>
	/// <param name="font">The Font instance used to obtain the Ascent</param>
	/// <returns>The ascent of the font</returns>
	public static int GetAscent(System.Drawing.Font font)
	{		
		System.Drawing.FontFamily fontFamily = font.FontFamily;
		int ascent = fontFamily.GetCellAscent(font.Style);
		int ascentPixel = (int)font.Size * ascent / fontFamily.GetEmHeight(font.Style);
		return ascentPixel;
	}

	/*******************************/
	/// <summary>
	/// Calculates the descent of the font, using the GetCellDescent and GetEmHeight
	/// </summary>
	/// <param name="font">The Font instance used to obtain the Descent</param>
	/// <returns>The Descent of the font </returns>
	public static int GetDescent(System.Drawing.Font font)
	{		
		System.Drawing.FontFamily fontFamily = font.FontFamily;
		int descent = fontFamily.GetCellDescent(font.Style);
		int descentPixel = (int) font.Size * descent / fontFamily.GetEmHeight(font.Style);
		return descentPixel;
	}

	/*******************************/
	/// <summary>
	/// Creates an AccessibleStates value based on an array of AccessibleStates.
	/// </summary>
	/// <param name="states">The base-array to create the new instance of AccessibleStates with.</param>
	/// <returns>The new instance of AccessibleStates that correspond to the bitwise representation of all
	/// the elements in the array given.</returns>
	public static System.Windows.Forms.AccessibleStates ConvertAccessibleStatesArray(System.Windows.Forms.AccessibleStates[] states)
	{
		System.Windows.Forms.AccessibleStates returnValue = new System.Windows.Forms.AccessibleStates();
		for (int index = 0; index < states.Length; index++)
			returnValue |= states[index];
		return returnValue;
	}


	/*******************************/
	/// <summary>
	/// Support class for the ListSelectionModel class.
	/// </summary>
	public class ListSelectionModelSupport : System.Windows.Forms.ListBox
	{

		/// <summary>
		/// Private field to store the first index argument from the most recent call to SetSelectionInterval(), AddSelectionInterval() or RemoveSelectionInterval().
		/// </summary>
		protected int anchor = -1;
		/// <summary>
		/// Private field to store the second index argument from the most recent call to SetSelectionInterval(), AddSelectionInterval() or RemoveSelectionInterval().
		/// </summary>
		protected int lead = -1;
		/// <summary>
		/// Private boolean field valueIsAdjusting. Included to provide functional equivalence.
		/// </summary>
		protected bool valueIsAdjusting = false;

		/// <summary>
		/// Default class constructor.
		/// </summary>
		public ListSelectionModelSupport() : base()
		{
		}


		/// <summary>
		/// Adds an interval to the selection.
		/// </summary>
		/// <param name="index0">Start of the interval.</param>
		/// <param name="index1">End of the interval.</param>
		public virtual void AddSelectionInterval(int index0, int index1) 
		{
			int start = System.Math.Min(index0,index1);
			int end = System.Math.Max(index0,index1);
			this.anchor = index0;
			this.lead = index1;
			if (start >= this.Items.Count) return;
			for (int i = start; i <= end; i++) 
			{
				this.SetSelected(i, true);
			}	
		}

		/// <summary>
		/// Clears the selection set.
		/// </summary>
		public virtual void ClearSelection() 
		{
			base.ClearSelected();
		}


		/// <summary>
		/// Return the first index argument from the most recent call to SetSelectionInterval(), AddSelectionInterval() or RemoveSelectionInterval().
		/// </summary>
		public virtual int GetAnchorSelectionIndex() 
		{
			return anchor;
		}


		/// <summary>
		/// Return the second index argument from the most recent call to SetSelectionInterval(), AddSelectionInterval() or RemoveSelectionInterval().
		/// </summary>
		public virtual int GetLeadSelectionIndex() 
		{
			return lead;
		}


		/// <summary>
		/// Returns the last selected index or -1 if the selection is empty.
		/// </summary>
		public virtual int GetMaxSelectionIndex() 
		{
			if (this.SelectedIndices.Count == 0)
				return -1;
			else
				return this.SelectedIndices[this.SelectedIndices.Count - 1];
		}


		/// <summary>
		/// Returns the first selected index or -1 if the selection is empty.
		/// </summary>
		public virtual int GetMinSelectionIndex() 
		{
			if (this.SelectedIndices.Count == 0)
				return -1;
			else
				return this.SelectedIndices[0];
		}

	
		/// <summary>
		/// Set the anchor selection index.
		/// </summary>
		/// <param name="index"></param>
		public virtual void SetAnchorSelectionIndex(int index) 
		{
			anchor = index;
		}


		/// <summary>
		/// Set the lead selection index.
		/// </summary>
		/// <param name="index"></param>
		public virtual void SetLeadSelectionIndex(int index) 
		{
			lead = index;
		}
	

		/// <summary>
		/// Remove the indices in the interval index0,index1 (inclusive).
		/// </summary>
		/// <param name="index0">Start of the interval.</param>
		/// <param name="index1">End of the interval.</param>
		public virtual void RemoveIndexInterval(int index0, int index1) 
		{
			int start = System.Math.Min(index0,index1);
			int end = System.Math.Max(index0,index1);
			if (start >= this.Items.Count) return;
			for (int i = start; i <= end; i++) 
			{
				this.SetSelected(i, false);
			}
		}


		/// <summary>
		/// Change the selection to be the set difference of the current selection and the indices between index0 and index1 inclusive.
		/// </summary>
		/// <param name="index0">Start of the interval.</param>
		/// <param name="index1">End of the interval.</param>
		public virtual void RemoveSelectionInterval(int index0, int index1) 
		{
			int start = System.Math.Min(index0,index1);
			int end = System.Math.Max(index0,index1);
			this.anchor = index0;
			this.lead = index1;
			if (start >= this.Items.Count) return;
			for (int i = start; i <= end; i++) 
			{
				this.SetSelected(i, false);
			}
		}


		/// <summary>
		/// Returns true if the value is undergoing a series of changes.
		/// </summary>
		public virtual bool GetValueIsAdjusting() 
		{
			return valueIsAdjusting;
		}


		/// <summary>
		/// This property is true if upcoming changes to the value of the model should be considered a single event.
		/// </summary>
		/// <param name="valueIsAdjusting"></param>
		public virtual void SetValueIsAdjusting(bool valueIsAdjusting) 
		{
			this.valueIsAdjusting = valueIsAdjusting;
		}


		/// <summary>
		/// Change the selection to be between index0 and index1 inclusive.
		/// </summary>
		/// <param name="index0">Start of the interval.</param>
		/// <param name="index1">End of the interval.</param>
		public virtual void SetSelectionInterval(int index0, int index1) 
		{
			this.ClearSelected();
			int start = System.Math.Min(index0,index1);
			int end = System.Math.Max(index0,index1);
			this.anchor = index0;
			this.lead = index1;
			if (start >= this.Items.Count) return;
			for (int i = start; i <= end; i++) 
			{
				this.SetSelected(i, false);
			}
		}

		/// <summary>
		/// Creates a shallow copy of the current Object.
		/// </summary>
		/// <returns>A shallow copy of the current Object.</returns>
		public virtual System.Object Clone()
		{
			return this.MemberwiseClone();
		}
	}

	/*******************************/
	/// <summary>
	/// Support Methods for FileDialog class. Note that several methods receive a DirectoryInfo object, but it won't be used in all cases.
	/// </summary>
	public class FileDialogSupport
	{
		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(System.IO.FileInfo path)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(System.IO.FileInfo path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates a OpenFileDialog open in a given path.
		/// </summary>		
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog()
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);			
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog (string path)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(string path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, string path, System.IO.DirectoryInfo directory)
		{
			fileDialog.InitialDirectory = path;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, System.IO.FileInfo path)
		{
			fileDialog.InitialDirectory = path.Directory.FullName;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, string path)
		{
			fileDialog.InitialDirectory = path;
		}

		///
		///  Use the following static methods to create instances of SaveFileDialog.
		///  By default, JFileChooser is converted as an OpenFileDialog, the following methods
		///  are provided to create file dialogs to save files.
		///	
		
		
		/// <summary>
		/// Creates a SaveFileDialog.
		/// </summary>		
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog()
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);			
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.FileInfo path)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.FileInfo path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates a SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog (string path)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(string path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}
	}
	/*******************************/
	/// <summary>
	/// This class uses a cryptographic Random Number Generator to provide support for
	/// strong pseudo-random number generation.
	/// </summary>
	[Serializable]
	public class SecureRandomSupport : System.Runtime.Serialization.ISerializable
	{
		private System.Security.Cryptography.RNGCryptoServiceProvider generator;

		//Serialization
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
		}

		protected SecureRandomSupport(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			this.generator = new System.Security.Cryptography.RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Initializes a new instance of the random number generator.
		/// </summary>
		public SecureRandomSupport()
		{
			this.generator = new System.Security.Cryptography.RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Initializes a new instance of the random number generator with the given seed.
		/// </summary>
		/// <param name="seed">The initial seed for the generator</param>
		public SecureRandomSupport(byte[] seed)
		{
			this.generator = new System.Security.Cryptography.RNGCryptoServiceProvider(seed);
		}

		/// <summary>
		/// Returns an array of bytes with a sequence of cryptographically strong random values.
		/// </summary>
		/// <param name="randomnumbersarray">The array of bytes to fill.</param>
		public sbyte[] NextBytes(byte[] randomnumbersarray)
		{
			this.generator.GetBytes(randomnumbersarray);
			return ToSByteArray(randomnumbersarray);
		}

		/// <summary>
		/// Returns the given number of seed bytes generated for the first running of a new instance 
		/// of the random number generator.
		/// </summary>
		/// <param name="numberOfBytes">Number of seed bytes to generate.</param>
		/// <returns>Seed bytes generated</returns>
		public static byte[] GetSeed(int numberOfBytes)
		{
			System.Security.Cryptography.RNGCryptoServiceProvider generatedSeed = new System.Security.Cryptography.RNGCryptoServiceProvider();
			byte[] seeds = new byte[numberOfBytes];
			generatedSeed.GetBytes(seeds);
			return seeds;
		}

		/// <summary>
		/// Returns the given number of seed bytes generated for the first running of a new instance 
		/// of the random number generator.
		/// </summary>
		/// <param name="numberOfBytes">Number of seed bytes to generate.</param>
		/// <returns>Seed bytes generated.</returns>
		public byte[] GenerateSeed(int numberOfBytes)
		{
			System.Security.Cryptography.RNGCryptoServiceProvider generatedSeed = new System.Security.Cryptography.RNGCryptoServiceProvider();
			byte[] seeds = new byte[numberOfBytes];
			generatedSeed.GetBytes(seeds);
			return seeds;
		}

		/// <summary>
		/// Creates a new instance of the random number generator with the seed provided by the user.
		/// </summary>
		/// <param name="newSeed">Seed to create a new random number generator.</param>
		public void SetSeed(byte[] newSeed)
		{
			this.generator = new System.Security.Cryptography.RNGCryptoServiceProvider(newSeed);
		}

		/// <summary>
		/// Creates a new instance of the random number generator with the seed provided by the user.
		/// </summary>
		/// <param name="newSeed">Seed to create a new random number generator.</param>
		public void SetSeed(long newSeed)
		{
			byte[] bytes = new byte[8];
			for (int index = 7; index > 0; index--)
			{
				bytes[index] = (byte) (newSeed - (long) ((newSeed >> 8) << 8));
				newSeed  = (long) (newSeed >> 8);
			}
			SetSeed(bytes);
		}
	}


	///*******************************/
	///// <summary>
	///// Action that will be executed when a toolbar button is clicked.
	///// </summary>
	///// <param name="event_sender">The object that fires the event.</param>
	///// <param name="event_args">An EventArgs that contains the event data.</param>
	//public static void ToolBarButtonClicked(System.Object event_sender, System.Windows.Forms.ToolBarButtonClickEventArgs event_args)
	//{
	//	System.Windows.Forms.Button button = (System.Windows.Forms.Button) event_args.Button.Tag;
	//	button.PerformClick();
	//}


	/*******************************/
/// <summary>
/// Contains methods to construct customized Buttons
/// </summary>
public class ButtonSupport
{
	/// <summary>
	/// Creates a popup style Button with an specific text.	
	/// </summary>
	/// <param name="label">The text associated with the Button</param>
	/// <returns>The new Button</returns>
	public static System.Windows.Forms.Button CreateButton(string label)
	{			
		System.Windows.Forms.Button tempButton = new System.Windows.Forms.Button();
		tempButton.Text = label;
		tempButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		return tempButton;
	}

	/// <summary>
	/// Sets the an specific text for the Button
	/// </summary>
	/// <param name="Button">The button to be set</param>
	/// <param name="label">The text associated with the Button</param>
	public static void SetButton(System.Windows.Forms.ButtonBase Button, string label)
	{
		Button.Text = label;
		Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
	}

	/// <summary>
	/// Creates a Button with an specific text and style.
	/// </summary>
	/// <param name="label">The text associated with the Button</param>
	/// <param name="style">The style of the Button</param>
	/// <returns>The new Button</returns>
	public static System.Windows.Forms.Button CreateButton(string label, int style)
	{
		System.Windows.Forms.Button tempButton = new System.Windows.Forms.Button();
		tempButton.Text = label;
		setStyle(tempButton,style);
		return tempButton;
	}

	/// <summary>
	/// Sets the specific Text and Style for the Button
	/// </summary>
	/// <param name="Button">The button to be set</param>
	/// <param name="label">The text associated with the Button</param>
	/// <param name="style">The style of the Button</param>
	public static void SetButton(System.Windows.Forms.ButtonBase Button, string label, int style)
	{
		Button.Text = label;
		setStyle(Button,style);
	}

	/// <summary>
	/// Creates a standard style Button that contains an specific text and/or image
	/// </summary>
	/// <param name="control">The control to be contained analized to get the text and/or image for the Button</param>
	/// <returns>The new Button</returns>
	public static System.Windows.Forms.Button CreateButton(System.Windows.Forms.Control control)
	{
		System.Windows.Forms.Button tempButton = new System.Windows.Forms.Button();
		if(control.GetType().FullName == "System.Windows.Forms.Label")
		{
			tempButton.Image = ((System.Windows.Forms.Label)control).Image;
			tempButton.Text = ((System.Windows.Forms.Label)control).Text;
			tempButton.ImageAlign = ((System.Windows.Forms.Label)control).ImageAlign;
			tempButton.TextAlign = ((System.Windows.Forms.Label)control).TextAlign;
		}
		else
		{
			if(control.GetType().FullName == "System.Windows.Forms.PictureBox")//Tentative to see maps of UIGraphic
			{
				tempButton.Image = ((System.Windows.Forms.PictureBox)control).Image;
				tempButton.ImageAlign = ((System.Windows.Forms.Label)control).ImageAlign;
			}else
				tempButton.Text = control.Text;
		}
		tempButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		return tempButton;
	}

	/// <summary>
	/// Sets an specific text and/or image to the Button
	/// </summary>
	/// <param name="Button">The button to be set</param>
	/// <param name="control">The control to be contained analized to get the text and/or image for the Button</param>
	public static void SetButton(System.Windows.Forms.ButtonBase Button,System.Windows.Forms.Control control)
	{
		if(control.GetType().FullName == "System.Windows.Forms.Label")
		{
			Button.Image = ((System.Windows.Forms.Label)control).Image;
			Button.Text = ((System.Windows.Forms.Label)control).Text;
			Button.ImageAlign = ((System.Windows.Forms.Label)control).ImageAlign;
			Button.TextAlign = ((System.Windows.Forms.Label)control).TextAlign;
		}
		else
		{
			if(control.GetType().FullName == "System.Windows.Forms.PictureBox")//Tentative to see maps of UIGraphic
			{
				Button.Image = ((System.Windows.Forms.PictureBox)control).Image;
				Button.ImageAlign = ((System.Windows.Forms.Label)control).ImageAlign;
			}
			else
				Button.Text = control.Text;
		}
		Button.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
	}

	/// <summary>
	/// Creates a Button with an specific control and style
	/// </summary>
	/// <param name="control">The control to be contained by the button</param>
	/// <param name="style">The style of the button</param>
	/// <returns>The new Button</returns>
	public static System.Windows.Forms.Button CreateButton(System.Windows.Forms.Control control, int style)
	{
		System.Windows.Forms.Button tempButton = CreateButton(control);
		setStyle(tempButton,style);
		return tempButton;
	}

	/// <summary>
	/// Sets an specific text and/or image to the Button
	/// </summary>
	/// <param name="Button">The button to be set</param>
	/// <param name="control">The control to be contained by the button</param>
	/// <param name="style">The style of the button</param>
	public static void SetButton(System.Windows.Forms.ButtonBase Button,System.Windows.Forms.Control control,int style)
	{
		SetButton(Button,control);
		setStyle(Button,style);
	}

	/// <summary>
	/// Sets the style of the Button
	/// </summary>
	/// <param name="Button">The Button that will change its style</param>
	/// <param name="style">The new style of the Button</param>
	/// <remarks> 
	/// If style is 0 then sets a popup style to the Button, otherwise sets a standard style to the Button.
	/// </remarks>
	public static void setStyle(System.Windows.Forms.ButtonBase Button, int style)
	{
		if (  (style == 0 ) || (style ==  67108864) || (style ==  33554432) ) 
			Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		else if ( (style == 2097152) || (style == 1048576) ||  (style == 16777216 ) )
				Button.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		else 
			throw new System.ArgumentException("illegal style: " + style);		
	}

	/// <summary>
	/// Selects the Button
	/// </summary>
	/// <param name="Button">The Button that will change its style</param>
	/// <param name="select">It determines if the button woll be selected</param>
	/// <remarks> 
	/// If select is true thebutton will be selected, otherwise not.
	/// </remarks>
	public static void setSelected(System.Windows.Forms.ButtonBase Button, bool select)
	{
		if (select)
			Button.Select();
	}

	/// <summary>
	/// Receives a Button instance and sets the Text and Image properties.
	/// </summary>
	/// <param name="buttonInstance">Button instance to be set.</param>
	/// <param name="buttonText">Value to be set to Text.</param>
	/// <param name="icon">Value to be set to Image.</param>
	public static void SetStandardButton (System.Windows.Forms.ButtonBase buttonInstance, string buttonText , System.Drawing.Image icon )
	{
		buttonInstance.Text = buttonText;
		buttonInstance.Image = icon;
	}

	/// <summary>
	/// Creates a Button with a given text.
	/// </summary>
	/// <param name="buttonText">The text to be displayed in the button.</param>
	/// <returns>The new created button with text</returns>
	public static System.Windows.Forms.Button CreateStandardButton (string buttonText)
	{
		System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
		newButton.Text = buttonText;
		return newButton;
	}

	/// <summary>
	/// Creates a Button with a given image.
	/// </summary>
	/// <param name="buttonImage">The image to be displayed in the button.</param>
	/// <returns>The new created button with an image</returns>
	public static System.Windows.Forms.Button CreateStandardButton (System.Drawing.Image buttonImage)
	{
		System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
		newButton.Image = buttonImage;
		return newButton;
	}

	/// <summary>
	/// Creates a Button with a given image and a text.
	/// </summary>
	/// <param name="buttonText">The text to be displayed in the button.</param>
	/// <param name="buttonImage">The image to be displayed in the button.</param>
	/// <returns>The new created button with text and image</returns>
	public static System.Windows.Forms.Button CreateStandardButton (string buttonText, System.Drawing.Image buttonImage)
	{
		System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
		newButton.Text = buttonText;
		newButton.Image = buttonImage;
		return newButton;
	}
}
	/*******************************/
	/// <summary>
	/// Contains methods to get an set a ToolTip
	/// </summary>
	public class ToolTipSupport
	{
		static System.Windows.Forms.ToolTip supportToolTip = new System.Windows.Forms.ToolTip();

		/// <summary>
		/// Get the ToolTip text for the specific control parameter.
		/// </summary>
		/// <param name="control">The control with the ToolTip</param>
		/// <returns>The ToolTip Text</returns>
		public static string getToolTipText(System.Windows.Forms.Control control)
		{
			return(supportToolTip.GetToolTip(control));
		}
		 
		/// <summary>
		/// Set the ToolTip text for the specific control parameter.
		/// </summary>
		/// <param name="control">The control to set the ToolTip</param>
		/// <param name="text">The text to show on the ToolTip</param>
		public static void setToolTipText(System.Windows.Forms.Control control, string text)
		{
			supportToolTip.SetToolTip(control,text);
		}
	}

	/*******************************/
	/// <summary>
	/// Provides functionality to perform character operations within a string
	/// </summary>
	public class StringIterator : System.Object
	{
		//Beginning position
		private int begin;
		//Ending position
		private int end;
		//Current position
		private int position;
		//String to operate on
		private string source;
		//Final character constant
		private const char DONE = '\uFFFF';

		/// <summary>
		/// Initializes a new object instance with the default values
		/// </summary>
		public StringIterator():this(""){}

		/// <summary>
		/// Initializes a new object instance with the specified string
		/// </summary>
		/// <param name="text">String to process</param>
		public StringIterator(string text):this(text,0){}

		/// <summary>
		/// Initializes a new object instance with the specified string
		/// starting processing in the given position
		/// </summary>
		/// <param name="text">String to process</param>
		/// <param name="pos">Starting position to work in</param>
		public StringIterator(string text, int pos):this(text,0,text.Length,pos){}

		/// <summary>
		/// Initializes a new object instance with the specified string,
		/// starting processing in the given position and within the
		/// specified limits
		/// </summary>
		/// <param name="text">String to process</param>
		/// <param name="begin">Lower limit to work in</param>
		/// <param name="end">Upper limit to work in</param>
		/// <param name="pos">Starting position to work in</param>
		public StringIterator(string text, int begin, int end, int pos)
		{
			if (text == null) throw new System.NullReferenceException();
			if ((begin < 0) || (begin > end) || (end > text.Length) || (begin > text.Length)) throw new System.ArgumentException("Invalid substring range");
			if ((pos < begin) || (pos > end)) throw new System.ArgumentException("Invalid position");

			this.source = text;
			this.begin = begin;
			this.end = end;
			this.position = pos;
		}

		/// <summary>
		/// The character value in the current position
		/// </summary>
		public virtual char Current
		{
			get
			{
				if (this.position < this.end) return this.source[this.position];
				else return DONE;
			}
		}

		/// <summary>
		/// Moves the position to the first element and returns it's value
		/// </summary>
		public virtual char First
		{
			get
			{
				this.position = this.begin;
				return this.source[this.position];
			}
		}

		/// <summary>
		/// Returns the lower limit of the iteration
		/// </summary>
		public virtual int BeginIndex
		{
			get
			{
				return this.begin;
			}
		}

		/// <summary>
		/// Returns the upper limit of the iteration
		/// </summary>
		public virtual int EndIndex
		{
			get
			{
				return this.end;
			}
		}

		/// <summary>
		/// Returns the current position
		/// </summary>
		/// <returns></returns>
		public virtual int GetIndex()
		{			
			return this.position;
		}

		/// <summary>
		/// Sets the current position index. It must be within the specified
		/// limits, or an argument exception will be thrown
		/// </summary>
		/// <param name="index">Position value to assign</param>
		/// <returns>The character value at the specified position</returns>
		public virtual char SetIndex(int index)
		{
			if (index == this.EndIndex)
				return DONE;
			else if (index < this.begin || index > this.end) 
					throw new System.ArgumentException("Invalid index");

			this.position = index;

			return this.source[this.position];
		}

		/// <summary>
		/// Moves the position to the last element and returns it's value
		/// </summary>
		public virtual char Last
		{
			get
			{
				this.position = this.end - 1;
				return this.source[this.position];
			}
		}

		/// <summary>
		/// Moves the position to the next element and returns it's value
		/// </summary>
		/// <returns>The new character</returns>
		public virtual char Next()
		{				
			if ((++this.position) < this.end)
			{					
				return this.source[this.position];
			}
			else return DONE;
		}

		/// <summary>
		/// Moves the position to the previous element and returns it's value
		/// </summary>
		/// <returns>The new character</returns>
		public virtual char Previous()
		{
			if ((this.position) > this.begin)
			{
				return this.source[--this.position];
			}
			else return DONE;
		}
	}

	/*******************************/
	/// <summary>
	/// Represents a segment of text for easy manipulation.  Allows iteration through the
	/// characters in the array.
	/// </summary>
	public class SegmentSupport : StringIterator
	{
		//Beginning position.
		public int Offset;

		//Ending position.
		public int Count;

		//String to operate on.
		public char[] Array;

		//Final character constant.
		public const char DONE = '\uFFFF';
	 
		//Current position.
		private int position;

		/// <summary>
		/// Initializes a new object instance with its default values.
		/// </summary>
		public SegmentSupport() : this(null, 0, 0)
		{
		}
	 
		/// <summary>
		/// Initializes a new object instance with an specified array of characters and
		/// data positions.
		/// </summary>
		/// <param name="chars">An array of characters that will be used to store data.</param>
		/// <param name="offset">The position from which to start reading data.</param>
		/// <param name="count">The amount of characters the Segment will hold.</param>
		public SegmentSupport(char[] chars, int offset, int count)
		{
			this.Array = chars;
			this.Offset = offset;
			this.Count = count;
			this.position = 0;
		}
	 
		/// <summary>
		/// The character value in the current position.
		/// </summary>
		public virtual new char Current
		{
			get
			{
				if (this.position < this.Offset + this.Count - 1)
					return this.Array[this.position];
				else return DONE;
			}
		}
	 
		/// <summary>
		/// Moves the position to the first element and returns its value.
		/// </summary>
		public virtual new char First
		{
			get
			{
				this.position = this.Offset;
				return this.Array[this.position];
			}
		}
	 
		/// <summary>
		/// Returns the lower limit of the iteration.
		/// </summary>
		public virtual new int BeginIndex
		{
			get
			{
				return this.Offset;
			}
		}
	 
		/// <summary>
		/// Returns the upper limit of the iteration.
		/// </summary>
		public virtual new int EndIndex
		{
			get
			{
				return this.Offset + this.Count;
			}
		}
	 
		/// <summary>
		/// Returns the current position.
		/// </summary>
		/// <returns>An integer value representing the current position in the array.</returns>
		public virtual new int GetIndex()
		{                 
			return this.position;
		}
	 
		/// <summary>
		/// Sets the current position index. It must be within the specified limits, or an argument 
		/// exception will be thrown.
		/// </summary>
		/// <param name="index">Position value to assign.</param>
		/// <returns>The character value at the specified position.</returns>
		public virtual new char SetIndex(int index)
		{
			if (index < this.Offset || index >= this.Offset + this.Count - 1)
				throw new System.ArgumentException("Invalid index");
			this.position = index;
			return this.Array[this.position];
		}
	 
		/// <summary>
		/// Moves the position to the last element and returns its value.
		/// </summary>
		public virtual new char Last
		{
			get
			{
				this.position = this.Offset + this.Count - 1;
				return this.Array[this.position];
			}
		}
	 
		/// <summary>
		/// Moves the position to the next element and returns its value.
		/// </summary>
		/// <returns>The next character.</returns>
		public virtual new char Next()
		{
			if ((++this.position) < this.Offset + this.Count)
				return this.Array[this.position];
			else return DONE;
		}
	 
		/// <summary>
		/// Moves the position to the previous element and returns its value.
		/// </summary>
		/// <returns>The previous character.</returns>
		public virtual new char Previous()
		{
			if ((this.position) > this.Offset)
				return this.Array[--this.position];
			else return DONE;
		}
	 
		/// <summary>
		/// The string representation of the internal character array.
		/// </summary>
		public override string ToString() 
		{
			return new string(this.Array).Substring(this.BeginIndex, this.Count);
		}

		/// <summary>
		/// Creates a shallow copy of the object.
		/// </summary>
		public new virtual System.Object MemberwiseClone() 
		{
			return base.MemberwiseClone();
		}
	}


	/*******************************/
	/// <summary>
	/// Retrieves the names of the fonts on the current context.
	/// </summary>
	/// <returns>A string array containing the names of the Fonts.</returns>
	public static string[] FontNames()
	{
		string[] fontArray;
		System.Drawing.FontFamily[] families = System.Drawing.FontFamily.Families;
		fontArray = new string[families.Length];
		for(int i = 0; i < families.Length; i++)
			fontArray[i] = families[i].Name;
		return fontArray;
	}

	/// <summary>
	/// Retrieves the name of the availables fonts for the specified culture.
	/// </summary>
	/// <param name="culture">The desired culture from which the fonts will be extracted.</param>
	/// <returns>A string array containing the names of the fonts.</returns>
	public static string[] FontNames(System.Globalization.CultureInfo culture)
	{
		string[] fontArray;
		System.Drawing.FontFamily[] families = System.Drawing.FontFamily.Families;
		fontArray = new string[families.Length];
		for(int i = 0; i < families.Length; i++)
			fontArray[i] = families[i].GetName(culture.LCID);
		return fontArray;
	}


	/*******************************/
	/// <summary>
	/// This class is used for storing colors in an array.  It can be used for constructing custom color palettes.
	/// </summary>
	public class IndexedColorArray
	{
		/// <summary>
		/// The array of color values.
		/// </summary>
		private int[] colorArray;
			
		/// <summary>
		/// The size of the array of color values.
		/// </summary>
		private int arraySize;
			
		/// <summary>
		/// Bitsize of color values.  Not Used.  Provided for compatibility.
		/// </summary>
		private int bitSize;

		/// <summary>
		/// Position in the array of the transparency value.
		/// </summary>
		private int transparentPixel;

		/// <summary>
		/// Size Property.
		/// <returns>The size of the array of color components.</returns>
		/// </summary>
		public int Size
		{
			get
			{
				return this.arraySize;
			}
		}

		/// <summary>
		/// TransparentPixel Property.
		/// </summary>
		/// <returns>The position of the transparency pixel within the array.</returns>
		public int TransparentPixel
		{ 
			get
			{
				return this.transparentPixel;
			}
		}

		/// <summary>
		/// Creates an indexed color array with arrays of color components.
		/// </summary>
		/// <param name="bitSize">BitSize of pixel values. (Not used. Supplied for compatibility.)</param>
		/// <param name="size">Size of the IndexedColorArray.</param>
		/// <param name="red">Array of red color components.</param>
		/// <param name="green">Array of green color components.</param>
		/// <param name="blue">Array of blue color components.</param>
		public IndexedColorArray(int bitSize, int size, byte[] red, byte[] green, byte[] blue)
		{
			this.bitSize = bitSize;
			SetValues(size, red, green, blue, null);
		}

		/// <summary>
		/// Creates an indexed color array with arrays of color components.
		/// </summary>
		/// <param name="bitSize">BitSize of pixel values. (Not used. Supplied for compatibility.)</param>
		/// <param name="size">Size of the IndexedColorArray.</param>
		/// <param name="red">Array of red color components.</param>
		/// <param name="green">Array of green color components.</param>
		/// <param name="blue">Array of blue color components.</param>
		/// <param name="transparencyPixel">Position in the array of the transparency pixel.</param>
		public IndexedColorArray(int bitSize, int size, byte[] red, byte[] green, byte[] blue, int transparencyPixel)
		{
			this.bitSize = bitSize;
			SetValues(size, red, green, blue, null);
			this.transparentPixel = transparencyPixel;
		}

		/// <summary>
		/// Creates an indexed color array with arrays of color components.
		/// </summary>
		/// <param name="bitSize">BitSize of pixel values. (Not used. Supplied for compatibility.)</param>
		/// <param name="size">Size of the IndexedColorArray.</param>
		/// <param name="red">Array of red color components.</param>
		/// <param name="green">Array of green color components.</param>
		/// <param name="blue">Array of blue color components.</param>
		/// <param name="alpha">Array of alpha values.</param>
		public IndexedColorArray(int bitSize, int size, byte[] red, byte[] green, byte[] blue, byte[] alpha)
		{
			this.bitSize = bitSize;
			SetValues(size, red, green, blue, alpha);
		}

		/// <summary>
		/// Creates an indexed color array with an array of color components.
		/// </summary>
		/// <param name="bitSize">BitSize of pixel values. (Not used. Supplied for compatibility.)</param>
		/// <param name="size">Size of the IndexedColorArray</param>
		/// <param name="colorMap">Array of color components</param>
		/// <param name="startPosition">Position in the array to start reading values.</param>
		/// <param name="hasAlpha">Boolean value indicating the presence of alpha values in the color components.</param>
		public IndexedColorArray(int bitSize, int size, byte[] colorMap, int startPosition,	bool hasAlpha) : this(bitSize, size, colorMap, startPosition, hasAlpha, -1)
		{
		}

		/// <summary>
		/// Creates an indexed color array with an array of color components.
		/// </summary>
		/// <param name="bitSize">BitSize of pixel values. (Not used. Supplied for compatibility.)</param>
		/// <param name="size">Size of the IndexedColorArray.</param>
		/// <param name="colorMap">Array of color components.</param>
		/// <param name="startPosition">Position in the array to start reading values.</param>
		/// <param name="hasAlpha">Boolean value indicating the presence of alpha values in the color components.</param>
		/// <param name=" transparencyPixel">Position within the array of the transparency pixel.</param>
		public IndexedColorArray(int bitSize, int size, byte[] colorMap, int startPosition, bool hasAlpha, int transparencyPixel) 
		{
			this.bitSize = bitSize;
			this.arraySize = size;
			int maxsize = size > 256 ? size : 256;
			this.colorArray = new int[maxsize];
			int start = startPosition;
			int alphaMask = 0xff;
			for (int index = 0; index < size; index++) 
			{
				this.colorArray[index] = ((colorMap[start++] & 0xff) << 16) | ((colorMap[start++] & 0xff) << 8) | (colorMap[start++] & 0xff);
				this.colorArray[index] |= (alphaMask << 24);
			}
			this.transparentPixel = transparencyPixel;
		}

		/// <summary>
		/// Stores color components. It converts byte array parameters to color components.</para>
		/// </summary>
		/// <param name="size">Size of the array of color components.</param>
		/// <param name="red">Array of red color components.</param>
		/// <param name="green">Array of green color components.</param>
		/// <param name="blue">Array of blue color components.</param>
		/// <param name="alpha">Array of alpha values.</param>
		/// <remarks> This method is private.</remarks>
		private void SetValues(int size, byte[] red, byte[] green, byte[] blue, byte[] alpha)
		{
			this.arraySize = size;
			int maxsize = this.arraySize > 256 ? this.arraySize : 256;
			this.colorArray = new int[maxsize];
			int alphaMask = 0xff;
			for (int index = 0; index < size; index++) 
				this.colorArray[index] = (alphaMask << 24) | ((red[index] & 0xff) << 16) | ((green[index] & 0xff) << 8) | (blue[index] & 0xff);
		}

		/// <summary>
		/// Returns the array of red color components in the byte array passed as parameter.
		/// </summary>
		/// <param name="red">The array to store the red color components.</param>
		/// <returns>The values are returned in the array passed as parameter.</returns>
		public void GetRedValues(byte[] red)
		{
			for (int index = 0; index < this.arraySize; index++) 
				red[index] = (byte) (this.colorArray[index] >> 16);
		}

		/// <summary>
		/// Returns the array of green color components in the byte array passed as parameter.
		/// </summary>
		/// <param name="green">The array to store the green color components.</param>
		/// <returns>The values are returned in the array passed as parameter.</returns>
		public void GetGreenValues(byte[] green)
		{
			for (int index = 0; index < this.arraySize; index++)
				green[index] = (byte) (this.colorArray[index] >> 8);
		}

		/// <summary>
		/// Returns the array of blue color components in the byte array passed as parameter.
		/// </summary>
		/// <param name="blue">The array to store the blue color components.</param>
		/// <returns>The values are returned in the array passed as parameter.</returns>
		public void GetBlueValues(byte[] blue)
		{
			for (int index = 0; index < this.arraySize; index++)
				blue[index] = (byte) this.colorArray[index];
		}

		/// <summary>
		/// Returns the array of alpha color components in the byte array passed as parameter.
		/// </summary>
		/// <param name="alpha">The array to store the blue color components.</param>
		/// <returns>The values are returned in the array passed as parameter.</returns>
		public void GetAlphaValues(byte[] alpha) 
		{
			for (int index = 0; index < this.arraySize; index++)
				alpha[index] = (byte) (this.colorArray[index] >> 24);
		}

		/// <summary>
		/// Returns the red color component of the pixel at location passed as parameter.
		/// </summary>
		/// <param name="pixel">Location of pixel to get the red color.</param>
		/// <returns>The red color component.</returns>
		public int GetRedFromPixel(int pixel) 
		{
			return (this.colorArray[pixel] >> 16) & 0xff;
		}

		/// <summary>
		/// Returns the green color component of the pixel at location passed as parameter.
		/// </summary>
		/// <param name="pixel">Location of pixel to get the red color.</param>
		/// <returns>The green color component.</returns>
		public int GetGreenFromPixel(int pixel)
		{
			return (this.colorArray[pixel] >> 8) & 0xff;
		}

		/// <summary>
		/// Returns the blue color component of the pixel at location passed as parameter.
		/// </summary>
		/// <param name="pixel">Location of pixel to get the blue color.</param>
		/// <returns>The blue color component.</returns>
		public int GetBlueFromPixel(int pixel)
		{
			return this.colorArray[pixel] & 0xff;
		}

		/// <summary>
		/// Returns the alpha color component of the pixel at location passed as parameter.
		/// </summary>
		/// <param name="pixel">Location of pixel to get the alpha componenet.</param>
		/// <returns>The alpha color component.</returns>
		public int GetAlphaFromPixel(int pixel)
		{
			return (this.colorArray[pixel] >> 24) & 0xff;
		}

		/// <summary>
		/// GetARGBFromPixel Method.
		/// </summary>
		/// <param name="pixel">Location of pixel to get color value.</param>
		/// <returns>The value of the color.</returns>
		public int GetARGBFromPixel(int pixel)
		{
			return this.colorArray[pixel];
		}
	}

	/*******************************/
	/// <summary>
	/// Creates a color dialog, sets the initial color of the dialog and returns the 
	/// selected user color.
	/// </summary>
	/// <param name="initialColor">The initial color of the color dialog.</param>
	/// <returns>The user selected color or a default color if the user does not select any.</returns>
	public static System.Drawing.Color ShowColorDialog(System.Drawing.Color initialColor)
	{
		System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog(); 
		dialog.Color = initialColor; 
		if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			return dialog.Color;
		else
			return new System.Drawing.Color();
	}


	/*******************************/
	/// <summary>
	/// Support for creation and modification of combo box elements
	/// </summary>
	public class ComboBoxSupport
	{
		/// <summary>
		/// Creates a new ComboBox control with the specified items.
		/// </summary>
		/// <param name="items">Items to insert into the combo box</param>
		/// <returns>A new combo box that contains the specified items</returns>
		public static System.Windows.Forms.ComboBox CreateComboBox( System.Object[] items )
		{
			System.Windows.Forms.ComboBox combo = new System.Windows.Forms.ComboBox();
			combo.Items.AddRange( items );
			combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			return combo;
		}

		/// <summary>
		/// Sets the items property of the specified combo with the items specified.
		/// </summary>
		/// <param name="combo">ComboBox to be modified</param>
		/// <param name="items">Items to insert into the combo box</param>
		public static void SetComboBox( System.Windows.Forms.ComboBox combo , System.Object[] items )
		{
			combo.Items.AddRange( items );
			combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		}

		/// <summary>
		/// Creates a new ComboBox control with the specified collection of items.
		/// </summary>
		/// <param name="items">Items to insert into the combo box</param>
		/// <returns>A new combo box that contains the specified items collection of items</returns>
		public static System.Windows.Forms.ComboBox CreateComboBox( System.Collections.ArrayList items )
		{
			return ComboBoxSupport.CreateComboBox( items.ToArray() );
		}

		/// <summary>
		/// Sets the items property of the specified combo with the items collection specified.
		/// </summary>
		/// <param name="combo">ComboBox to be modified</param>
		/// <param name="items">Collection of items to insert into the combo box</param>
		public static void SetComboBox( System.Windows.Forms.ComboBox combo , System.Collections.ArrayList items )
		{
			ComboBoxSupport.SetComboBox( combo, items.ToArray() );
		}

		/// <summary>
		/// Returns an array that contains the selected item of the specified combo box
		/// </summary>
		/// <param name="combo">The combo box from which the selected item is returned</param>
		/// <returns>An array that contains the selected item of the combo box</returns>
		public static System.Object[] GetSelectedObjects( System.Windows.Forms.ComboBox combo )
		{
			System.Object[] selectedObjects = new System.Object[1];
			selectedObjects[0] = combo.SelectedItem;
			return selectedObjects;
		}

		/// <summary>
		/// Returns a value indicating if the text portion of the specified combo box is editable
		/// </summary>
		/// <param name="combo">The combo box from to check</param>
		/// <returns>True if the text portion of the combo box is editable, false otherwise</returns>
		public static bool IsEditable( System.Windows.Forms.ComboBox combo )
		{
			return !( combo.DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDownList );
		}
		
		/// <summary>
		/// Create a TextBox object using the ComboBox object received as parameter.
		/// </summary>
		/// <param name="comboBox"></param>
		/// <returns></returns>
		public static System.Windows.Forms.TextBox GetEditComponent(System.Windows.Forms.ComboBox comboBox)
		{
			System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
			textBox.Text = comboBox.Text;
			return textBox;
		}
	}
	/*******************************/
	/// <summary>
	/// Provides overloaded methods to create and set values to an instance of System.Drawing.Pen.
	/// </summary>
	public class StrokeConsSupport
	{
		/// <summary>
		/// Creates an instance of System.Drawing.Pen with the default SolidBrush black.
		/// And then set the parameters into their corresponding properties.
		/// </summary>
		/// <param name="width">The width of the stroked line.</param>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <returns>A new instance with the values set.</returns>
		public static System.Drawing.Pen CreatePenInstance(float width, int cap, int join)
		{
			System.Drawing.Pen tempPen = new System.Drawing.Pen(System.Drawing.Brushes.Black,width);
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
			return tempPen;
		}

		/// <summary>
		/// Creates an instance of System.Drawing.Pen with the default SolidBrush black.
		/// And then set the parameters into their corresponding properties.
		/// </summary>
		/// <param name="width">The width of the stroked line.</param>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <param name="miterlimit">The limit of the line.</param>
		/// <returns>A new instance with the values set.</returns>
		public static System.Drawing.Pen CreatePenInstance(float width, int cap, int join, float miterlimit)
		{
			System.Drawing.Pen tempPen = new System.Drawing.Pen(System.Drawing.Brushes.Black,width);
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
			tempPen.MiterLimit = miterlimit;
			return tempPen;
		}

		/// <summary>
		/// Creates an instance of System.Drawing.Pen with the default SolidBrush black.
		/// And then set the parameters into their corresponding properties.
		/// </summary>
		/// <param name="width">The width of the stroked line.</param>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <param name="miterlimit">The limit of the line.</param>
		/// <param name="dashPattern">The array to use to make the dash.</param>
		/// <param name="dashOffset">The space between each dash.</param>
		/// <returns>A new instance with the values set.</returns>
		public static System.Drawing.Pen CreatePenInstance(float width, int cap, int join, float miterlimit,float[] dashPattern, float dashOffset)
		{
			System.Drawing.Pen tempPen = new System.Drawing.Pen(System.Drawing.Brushes.Black,width);
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
			tempPen.MiterLimit = miterlimit;
			tempPen.DashPattern = dashPattern;
			tempPen.DashOffset = dashOffset;
			return tempPen;
		}

		/// <summary>
		/// Sets a Pen instance with the corresponding DashCap and LineJoin values.
		/// </summary>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <returns>A new instance with the values set.</returns>
		public static void SetPen(System.Drawing.Pen tempPen, int cap, int join)
		{
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
		}

		/// <summary>
		/// Sets a Pen instance with the corresponding DashCap, LineJoin and MiterLimit values.
		/// </summary>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <param name="miterlimit">The limit of the line.</param>
		public static void SetPen(System.Drawing.Pen tempPen, int cap, int join, float miterlimit)
		{
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
			tempPen.MiterLimit = miterlimit;
		}

		/// <summary>
		/// Sets a Pen instance with the corresponding DashCap, LineJoin, MiterLimit, DashPattern and 
		/// DashOffset values.
		/// </summary>
		/// <param name="cap">The DashCap end of line style.</param>
		/// <param name="join">The LineJoin style.</param>
		/// <param name="miterlimit">The limit of the line.</param>
		/// <param name="dashPattern">The array to use to make the dash.</param>
		/// <param name="dashOffset">The space between each dash.</param>
		public static void SetPen(System.Drawing.Pen tempPen, float width, int cap, int join, float miterlimit, float[] dashPattern, float dashOffset)
		{
			tempPen.StartCap = (System.Drawing.Drawing2D.LineCap)  cap;
			tempPen.EndCap = (System.Drawing.Drawing2D.LineCap) cap;
			tempPen.LineJoin = (System.Drawing.Drawing2D.LineJoin)join;
			tempPen.MiterLimit = miterlimit;
			tempPen.DashPattern = dashPattern;
			tempPen.DashOffset = dashOffset;
		}
	}

	/*******************************/
	/// <summary>
	/// This class contains support methods to work with GraphicsPath and Arcs.
	/// </summary>
	public class Arc2DSupport
	{
		/// <summary>
		/// Specifies an OPEN arc type.
		/// </summary>
		public const int OPEN = 0;
		/// <summary>
		/// Specifies an CLOSED arc type.
		/// </summary>
		public const int CLOSED = 1;
		/// <summary>
		/// Specifies an PIE arc type.
		/// </summary>
		public const int PIE = 2;
		/// <summary>
		/// Creates a GraphicsPath object and adds an arc to it with the specified arc values and closure type.
		/// </summary>
		/// <param name="x">The x coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="start">The starting angle of the arc measured in degrees.</param>
		/// <param name="extent">The angular extent of the arc measured in degrees.</param>
		/// <param name="arcType">The closure type for the arc.</param>
		/// <returns>Returns a new GraphicsPath object that contains the arc path.</returns>
		public static System.Drawing.Drawing2D.GraphicsPath CreateArc2D(float x, float y, float height, float width, float start, float extent, int arcType)
		{
			System.Drawing.Drawing2D.GraphicsPath arc2DPath = new System.Drawing.Drawing2D.GraphicsPath();
			switch (arcType)
			{
				case OPEN:
					arc2DPath.AddArc(x, y, height, width, start * -1, extent * -1);
					break;
				case CLOSED:
					arc2DPath.AddArc(x, y, height, width, start * -1, extent * -1);
					arc2DPath.CloseFigure();
					break;
				case PIE:
					arc2DPath.AddPie(x, y, height, width, start * -1, extent * -1);
					break;
				default:
					break;
			}
			return arc2DPath;
		}
		/// <summary>
		/// Creates a GraphicsPath object and adds an arc to it with the specified arc values and closure type.
		/// </summary>
		/// <param name="ellipseBounds">A RectangleF structure that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
		/// <param name="start">The starting angle of the arc measured in degrees.</param>
		/// <param name="extent">The angular extent of the arc measured in degrees.</param>
		/// <param name="arcType">The closure type for the arc.</param>
		/// <returns>Returns a new GraphicsPath object that contains the arc path.</returns>
		public static System.Drawing.Drawing2D.GraphicsPath CreateArc2D(System.Drawing.RectangleF ellipseBounds, float start, float extent, int arcType)
		{
			System.Drawing.Drawing2D.GraphicsPath arc2DPath = new System.Drawing.Drawing2D.GraphicsPath();
			switch (arcType)
			{
				case OPEN:
					arc2DPath.AddArc(ellipseBounds, start * -1, extent * -1);
					break;
				case CLOSED:
					arc2DPath.AddArc(ellipseBounds, start * -1, extent * -1);
					arc2DPath.CloseFigure();
					break;
				case PIE:
					arc2DPath.AddPie(ellipseBounds.X, ellipseBounds.Y, ellipseBounds.Width, ellipseBounds.Height, start * -1, extent * -1);
					break;
				default:
					break;
			}

			return arc2DPath;
		}

		/// <summary>
		/// Resets the specified GraphicsPath object and adds an arc to it with the speficied values.
		/// </summary>
		/// <param name="arc2DPath">The GraphicsPath object to reset.</param>
		/// <param name="x">The x coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="start">The starting angle of the arc measured in degrees.</param>
		/// <param name="extent">The angular extent of the arc measured in degrees.</param>
		/// <param name="arcType">The closure type for the arc.</param>
		public static void SetArc(System.Drawing.Drawing2D.GraphicsPath arc2DPath, float x, float y, float height, float width, float start, float extent, int arcType)
		{
			arc2DPath.Reset();
			switch (arcType)
			{
				case OPEN:
					arc2DPath.AddArc(x, y, height, width, start * -1, extent * -1);
					break;
				case CLOSED:
					arc2DPath.AddArc(x, y, height, width, start * -1, extent * -1);
					arc2DPath.CloseFigure();
					break;
				case PIE:
					arc2DPath.AddPie(x, y, height, width, start * -1, extent * -1);
					break;
				default:
					break;
			}
		}
	}
	/*******************************/
	/// <summary>
	/// This class has static methods for manage CheckBox and RadioButton controls.
	/// </summary>
	public class CheckBoxSupport
	{

		/// <summary>
		/// Receives a CheckBox instance and sets the specific text and style.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="text">The text to set Text property.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, string text, int style)
		{
			checkBoxInstance.Text = text;			
			if (style == 2097152)
				checkBoxInstance.ThreeState = true;
		}

		/// <summary>
		/// Returns a new CheckBox instance with the specified text
		/// </summary>
		/// <param name="text">The text to create the CheckBox with</param>
		/// <returns>A new CheckBox instance</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text)
		{
			System.Windows.Forms.CheckBox tempCheck = new System.Windows.Forms.CheckBox();
			tempCheck.Text = text;
			return tempCheck;
		}

		/// <summary>
		/// Creates a CheckBox with a specified image.
		/// </summary>
		/// <param name="icon">The image to be used with the CheckBox.</param>
		/// <returns>A new CheckBox instance with an image.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(System.Drawing.Image icon)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Image = icon;
			return tempCheckBox;
		}

		/// <summary>
		/// Creates a CheckBox with a specified label and image.
		/// </summary>
		/// <param name="text">The text to be used as label.</param>
		/// <param name="icon">The image to be used with the CheckBox.</param>
		/// <returns>A new CheckBox instance with a label and an image.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text, System.Drawing.Image icon)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Text = text;
			tempCheckBox.Image = icon;
			return tempCheckBox;
		}

		/// <summary>
		/// Returns a new CheckBox instance with the specified text and state
		/// </summary>
		/// <param name="text">The text to create the CheckBox with</param>
		/// <param name="checkedStatus">Indicates if the Checkbox is checked or not</param>
		/// <returns>A new CheckBox instance</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text, bool checkedStatus)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Text = text;
			tempCheckBox.Checked = checkedStatus;
			return tempCheckBox;
		}

		/// <summary>
		/// Creates a CheckBox with a specified image and checked if checkedStatus is true.
		/// </summary>
		/// <param name="icon">The image to be used with the CheckBox.</param>
		/// <param name="checkedStatus">Value to be set to Checked property.</param>
		/// <returns>A new CheckBox instance.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(System.Drawing.Image icon, bool checkedStatus)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Image = icon;
			tempCheckBox.Checked = checkedStatus;
			return tempCheckBox;
		}

		/// <summary>
		/// Creates a CheckBox with label, image and checked if checkedStatus is true.
		/// </summary>
		/// <param name="text">The text to be used as label.</param>
		/// <param name="icon">The image to be used with the CheckBox.</param>
		/// <param name="checkedStatus">Value to be set to Checked property.</param>
		/// <returns>A new CheckBox instance.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text, System.Drawing.Image icon, bool checkedStatus)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Text = text;
			tempCheckBox.Image = icon;
			tempCheckBox.Checked = checkedStatus;
			return tempCheckBox;
		}

		/// <summary>
		/// Creates a CheckBox with a specific control.
		/// </summary>
		/// <param name="control">The control to be added to the CheckBox.</param>
		/// <returns>The new CheckBox with the specific control.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(System.Windows.Forms.Control control)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Controls.Add(control);
			control.Location = new System.Drawing.Point(0, 18);
			return tempCheckBox;
		}

		/// <summary>
		/// Creates a CheckBox with the specific control and style.
		/// </summary>
		/// <param name="control">The control to be added to the CheckBox.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		/// <returns>The new CheckBox with the specific control and style.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(System.Windows.Forms.Control control, int style)
		{
			System.Windows.Forms.CheckBox tempCheckBox = new System.Windows.Forms.CheckBox();
			tempCheckBox.Controls.Add(control);
			control.Location = new System.Drawing.Point(0, 18);
			if (style == 2097152)
				tempCheckBox.ThreeState = true;
			return tempCheckBox;
		}

		/// <summary>
		/// Returns a new RadioButton instance with the specified text in the specified panel.
		/// </summary>
		/// <param name="text">The text to create the RadioButton with.</param>
		/// <param name="checkedStatus">Indicates if the RadioButton is checked or not.</param>
		/// <param name="panel">The panel where the RadioButton will be placed.</param>
		/// <returns>A new RadioButton instance.</returns>
		/// <remarks>If the panel is null the third param is ignored</remarks>
		public static System.Windows.Forms.RadioButton CreateRadioButton(string text, bool checkedStatus, System.Windows.Forms.Panel panel)
		{
			System.Windows.Forms.RadioButton tempCheckBox = new System.Windows.Forms.RadioButton();
			tempCheckBox.Text = text;
			tempCheckBox.Checked= checkedStatus;
			if (panel != null)
				panel.Controls.Add(tempCheckBox);
			return tempCheckBox;
		}

		/// <summary>
		/// Receives a CheckBox instance and sets the Text and Image properties.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="text">Value to be set to Text property.</param>
		/// <param name="icon">Value to be set to Image property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, string text, System.Drawing.Image icon)
		{
			checkBoxInstance.Text = text;
			checkBoxInstance.Image = icon;
		}

		/// <summary>
		/// Receives a CheckBox instance and sets the Text and Checked properties.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set</param>
		/// <param name="text">Value to be set to Text</param>
		/// <param name="checkedStatus">Value to be set to Checked property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, string text, bool checkedStatus)
		{
			checkBoxInstance.Text = text;
			checkBoxInstance.Checked = checkedStatus;
		}

		/// <summary>
		/// Receives a CheckBox instance and sets the Image and Checked properties.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="icon">Value to be set to Image property.</param>
		/// <param name="checkedStatus">Value to be set to Checked property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, System.Drawing.Image icon, bool checkedStatus)
		{
			checkBoxInstance.Image = icon;
			checkBoxInstance.Checked = checkedStatus;
		}

		/// <summary>
		/// Receives a CheckBox instance and sets the Text, Image and Checked properties.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="text">Value to be set to Text property.</param>
		/// <param name="icon">Value to be set to Image property.</param>
		/// <param name="checkedStatus">Value to be set to Checked property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, string text, System.Drawing.Image icon, bool checkedStatus)
		{
			checkBoxInstance.Text = text;
			checkBoxInstance.Image = icon;
			checkBoxInstance.Checked = checkedStatus;
		}
		
		/// <summary>
		/// Receives a CheckBox instance and sets the control specified.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="control">The control assiciated with the CheckBox</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, System.Windows.Forms.Control control)
		{
			checkBoxInstance.Controls.Add(control);
			control.Location = new System.Drawing.Point(0, 18);
		}

		/// <summary>
		/// Receives a CheckBox instance and sets the specific control and style.
		/// </summary>
		/// <param name="checkBoxInstance">CheckBox instance to be set.</param>
		/// <param name="control">The control assiciated with the CheckBox.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		public static void SetCheckBox(System.Windows.Forms.CheckBox checkBoxInstance, System.Windows.Forms.Control control, int style)
		{
			checkBoxInstance.Controls.Add(control);
			control.Location = new System.Drawing.Point(0, 18);
			if (style == 2097152)
				checkBoxInstance.ThreeState = true;
		}

		/// <summary>
		/// Receives an instance of a RadioButton and sets its Text and Checked properties.
		/// </summary>
		/// <param name="RadioButtonInstance">The instance of RadioButton to be set.</param>
		/// <param name="text">The text to set Text property.</param>
		/// <param name="checkedStatus">Indicates if the RadioButton is checked or not.</param>
		public static void SetCheckbox(System.Windows.Forms.RadioButton radioButtonInstance, string text, bool checkedStatus)
		{
			radioButtonInstance.Text = text;
			radioButtonInstance.Checked = checkedStatus;
		}

		/// <summary>
		/// Receives an instance of a RadioButton and sets its Text and Checked properties and its containing panel
		/// </summary>
		/// <param name="CheckBoxInstance">The instance of RadioButton to be set</param>
		/// <param name="text">The text to set Text property</param>
		/// <param name="checkedStatus">Indicates if the RadioButton is checked or not</param>
		/// <param name="panel">The panel where the RadioButton will be placed</param>
		/// <remarks>If the panel is null the third param is ignored</remarks>
		public static void SetRadioButton(System.Windows.Forms.RadioButton radioButtonInstance, string text, bool checkedStatus, System.Windows.Forms.Panel panel)
		{
			radioButtonInstance.Text = text;
			radioButtonInstance.Checked = checkedStatus;
			if (panel != null)
				panel.Controls.Add(radioButtonInstance);
		}
		
		/// <summary>
		/// Creates a CheckBox with a specified text label and style.
		/// </summary>
		/// <param name="text">The text to be used as label.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		/// <returns>A new CheckBox instance.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text, int style)
		{
			System.Windows.Forms.CheckBox checkBox = new System.Windows.Forms.CheckBox();
			checkBox.Text = text;
			if (style == 2097152)
				checkBox.ThreeState = true;
			return checkBox;
		}
		
		/// <summary>
		/// Receives a CheckBox instance and sets the Text and ThreeState properties.
		/// </summary>
		/// <param name="checkBox">CheckBox instance to be set.</param>
		/// <param name="text">Value to be set to Text property.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		public static void setCheckBox(System.Windows.Forms.CheckBox checkBox, string text, int style)
		{
			checkBox.Text = text;
			if (style == 2097152)
				checkBox.ThreeState = true;
		}
		
		/// <summary>
		/// Creates a CheckBox with a specified text label, image and style.
		/// </summary>
		/// <param name="text">The text to be used as label.</param>
		/// <param name="icon">The image to be used with the CheckBox.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		/// <returns>A new CheckBox instance.</returns>
		public static System.Windows.Forms.CheckBox CreateCheckBox(string text, System.Drawing.Image icon, int style)
		{
			System.Windows.Forms.CheckBox checkBox = new System.Windows.Forms.CheckBox();
			checkBox.Text = text;
			checkBox.Image = icon;
			if (style == 2097152)
				checkBox.ThreeState = true;
			return checkBox;
		}
		
		/// <summary>
		/// Receives a CheckBox instance and sets the Text, Image and ThreeState properties.
		/// </summary>
		/// <param name="checkBox">CheckBox instance to be set.</param>
		/// <param name="text">Value to be set to Text property.</param>
		/// <param name="icon">Value to be set to Image property.</param>
		/// <param name="style">The style to be used to set the threeState property.</param>
		public static void setCheckBox(System.Windows.Forms.CheckBox checkBox, string text, System.Drawing.Image icon, int style)
		{
			checkBox.Text = text;
			checkBox.Image = icon;
			if (style == 2097152)
				checkBox.ThreeState = true;
		}
		
		/// <summary>
		/// The SetIndeterminate method is used to sets or clear the CheckState of the CheckBox component.
		/// </summary>
		/// <param name="indeterminate">The value to the Indeterminate state. If true, the state is set; otherwise, it is cleared.</param>
		/// <param name="checkBox">The CheckBox component to be modified.</param>
		/// <returns></returns>
		public static System.Windows.Forms.CheckBox SetIndeterminate(bool indeterminate, System.Windows.Forms.CheckBox checkBox)
		{
			if (indeterminate)
				checkBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			else if (checkBox.Checked)
				checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			else
				checkBox.CheckState = System.Windows.Forms.CheckState.Unchecked;
			return checkBox;
		}
	}

	/*******************************/
	/// <summary>
	/// This class supports basic functionality of the JOptionPane class.
	/// </summary>
	public class OptionPaneSupport 
	{
		/// <summary>
		/// This method finds the form which contains an specific control.
		/// </summary>
		/// <param name="control">The control which we need to find its form.</param>
		/// <returns>The form which contains the control</returns>
		public static System.Windows.Forms.Form GetFrameForComponent(System.Windows.Forms.Control control)
		{
			System.Windows.Forms.Form result = null;
			if (control == null)return null;
			if (control is System.Windows.Forms.Form)
				result = (System.Windows.Forms.Form)control;
			else if (control.Parent != null)
				result = GetFrameForComponent(control.Parent);
			return result;
		}

		/// <summary>
		/// This method finds the MDI container form which contains an specific control.
		/// </summary>
		/// <param name="control">The control which we need to find its MDI container form.</param>
		/// <returns>The MDI container form which contains the control.</returns>
		public static System.Windows.Forms.Form GetDesktopPaneForComponent(System.Windows.Forms.Control control)
		{
			System.Windows.Forms.Form result = null;
			if (control == null)return null;
			if (control.GetType().IsSubclassOf(typeof(System.Windows.Forms.Form)))
				if (((System.Windows.Forms.Form)control).IsMdiContainer)
					result = (System.Windows.Forms.Form)control;
				else if (((System.Windows.Forms.Form)control).IsMdiChild)
					result = GetDesktopPaneForComponent(((System.Windows.Forms.Form)control).MdiParent);
				else if (control.Parent != null)
					result = GetDesktopPaneForComponent(control.Parent);
			return result;
		}
		
		/// <summary>
		/// This method retrieves the message that is contained into the object that is sended by the user.
		/// </summary>
		/// <param name="control">The control which we need to find its form.</param>
		/// <returns>The form which contains the control</returns>
		public static string GetMessageForObject(System.Object message)
		{
			string result = "";
			if (message == null)
			  return result;
			else 
		 	  result = message.ToString();
			return result;
		}


		/// <summary>
		/// This method displays a dialog with a Yes, No, Cancel buttons and a question icon.
		/// </summary>
		/// <param name="parent">The component which will be the owner of the dialog.</param>
		/// <param name="message">The message to be displayed; if it isn't an String it displays the return value of the ToString() method of the object.</param>
		/// <returns>The integer value which represents the button pressed.</returns>
		public static int ShowConfirmDialog(System.Windows.Forms.Control parent, System.Object message)
		{
			return ShowConfirmDialog(parent, message,"Select an option...", (int)System.Windows.Forms.MessageBoxButtons.YesNoCancel,
				(int)System.Windows.Forms.MessageBoxIcon.Question);
		}

		/// <summary>
		/// This method displays a dialog with specific buttons and a question icon.
		/// </summary>
		/// <param name="parent">The component which will be the owner of the dialog.</param>
		/// <param name="message">The message to be displayed; if it isn't an String it displays the result value of the ToString() method of the object.</param>
		/// <param name="title">The title for the message dialog.</param>
		/// <param name="optiontype">The set of buttons to be displayed in the message box; defined by the MessageBoxButtons enumeration.</param>
		/// <returns>The integer value which represents the button pressed.</returns>
		public static int ShowConfirmDialog(System.Windows.Forms.Control parent, System.Object message,
			string title,int optiontype)
		{
			return ShowConfirmDialog(parent, message, title, optiontype, (int)System.Windows.Forms.MessageBoxIcon.Question);
		}

		/// <summary>
		/// This method displays a dialog with specific buttons and specific icon.
		/// </summary>
		/// <param name="parent">The component which will be the owner of the dialog.</param>
		/// <param name="message">The message to be displayed; if it isn't an String it displays the return value of the ToString() method of the object.</param>
		/// <param name="title">The title for the message dialog.</param>
		/// <param name="optiontype">The set of buttons to be displayed in the message box; defined by the MessageBoxButtons enumeration.</param>
		/// <param name="messagetype">The messagetype defines the icon to be displayed in the message box.</param>
		/// <returns>The integer value which represents the button pressed.</returns>
		public static int ShowConfirmDialog(System.Windows.Forms.Control parent, System.Object message,
			string title, int optiontype, int messagetype)
		{
			return (int)System.Windows.Forms.MessageBox.Show(GetFrameForComponent(parent), GetMessageForObject(message), title,
				(System.Windows.Forms.MessageBoxButtons)optiontype, (System.Windows.Forms.MessageBoxIcon)messagetype);
		}

		/// <summary>
		/// This method displays a simple MessageBox.
		/// </summary>
		/// <param name="parent">The component which will be the owner of the dialog.</param>
		/// <param name="message">The message to be displayed; if it isn't an String it displays result value of the ToString() method of the object.</param>
		public static void ShowMessageDialog(System.Windows.Forms.Control parent, System.Object message)
		{
			ShowMessageDialog(parent, message, "Message", (int)System.Windows.Forms.MessageBoxIcon.Information);
		}

		/// <summary>
		/// This method displays a simple MessageBox with a specific icon.
		/// </summary>
		/// <param name="parent">The component which will be the owner of the dialog.</param>
		/// <param name="message">The message to be displayed; if it isn't an String it displays result value of the ToString() method of the object.</param>
		/// <param name="title">The title for the message dialog.</param>
		/// <param name="messagetype">The messagetype defines the icon to be displayed in the message box.</param>
		public static void ShowMessageDialog(System.Windows.Forms.Control parent, System.Object message,
			string title, int messagetype)
		{
			System.Windows.Forms.MessageBox.Show(GetFrameForComponent(parent), GetMessageForObject(message), title,
				System.Windows.Forms.MessageBoxButtons.OK, (System.Windows.Forms.MessageBoxIcon)messagetype);
		}
	}


	/*******************************/
	/// <summary>
	/// Converts an angle in degrees to radians.
	/// </summary>
	/// <param name="angleInDegrees">Double value of angle in degrees to convert.</param>
	/// <returns>The value of the angle in radians.</returns>
	public static double DegreesToRadians(double angleInDegrees)
	{
		double valueRadians =  (2 * System.Math.PI) / 360;
		return angleInDegrees * valueRadians;
	}

	/*******************************/
	/// <summary>
	/// Converts an angle in radians to degrees.
	/// </summary>
	/// <param name="angleInRadians">Double value of angle in radians to convert.</param>
	/// <returns>The value of the angle in degrees.</returns>
	public static double RadiansToDegrees(double angleInRadians)
	{
		double valueDegrees = 360 / (2 * System.Math.PI) ;
		return angleInRadians * valueDegrees;
	}

	/*******************************/
	/// <summary>
	/// Creates a GraphicsPath from two Int Arrays with a specific number of points.
	/// </summary>
	/// <param name="xPoints">Int Array to set the X points of the GraphicsPath</param>
	/// <param name="yPoints">Int Array to set the Y points of the GraphicsPath</param>
	/// <param name="pointsNumber">Number of points to add to the GraphicsPath</param>
	/// <returns>A new GraphicsPath</returns>
	public static System.Drawing.Drawing2D.GraphicsPath CreateGraphicsPath(int[] xPoints, int[] yPoints, int pointsNumber)
	{
		System.Drawing.Drawing2D.GraphicsPath tempGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
		if (pointsNumber == 2)
			tempGraphicsPath.AddLine(xPoints[0], yPoints[0], xPoints[1], yPoints[1]);
		else
		{
			System.Drawing.Point[] tempPointArray = new System.Drawing.Point[pointsNumber];
			for (int index = 0; index < pointsNumber; index++)
				tempPointArray[index] = new System.Drawing.Point(xPoints[index], yPoints[index]);

			tempGraphicsPath.AddPolygon(tempPointArray);
		}
		return tempGraphicsPath;
	}

	/*******************************/
	/// <summary>
	/// Gets all X-axis points from the received graphics path
	/// </summary>
	/// <param name="path">Source graphics path</param>
	/// <returns>The array of X-axis values</returns>
	public static int[] GetXPoints(System.Drawing.Drawing2D.GraphicsPath path)
	{
		int[] tempIntArray = new int[path.PointCount];
		for (int index=0; index < path.PointCount; index++)
		{
			tempIntArray[index] = (int) path.PathPoints[index].X;
		}
		return tempIntArray;
	}

	/*******************************/
	/// <summary>
	/// Gets all Y-axis points from the received graphics path
	/// </summary>
	/// <param name="path">Source graphics path</param>
	/// <returns>The array of Y-axis values</returns>
	public static int[] GetYPoints(System.Drawing.Drawing2D.GraphicsPath path)
	{
		int[] tempIntArray = new int[path.PointCount];
		for (int index=0; index < path.PointCount; index++)
		{
			tempIntArray[index] = (int) path.PathPoints[index].Y;
		}
		return tempIntArray;
	}

	/*******************************/
	/// <summary>
	/// Adds the X and Y coordinates to the current graphics path.
	/// </summary>
	/// <param name="graphPath"> The current Graphics path</param>
	/// <param name="xCoordinate">The x coordinate to be added</param>
	/// <param name="yCoordinate">The y coordinate to be added</param>
	public static void AddPointToGraphicsPath(System.Drawing.Drawing2D.GraphicsPath graphPath, int x, int y)
	{
		System.Drawing.PointF[] tempPointArray = new System.Drawing.PointF[graphPath.PointCount + 1];
		byte[] tempPointTypeArray = new byte[graphPath.PointCount + 1];

		if (graphPath.PointCount == 0)
		{
			tempPointArray[0] = new System.Drawing.PointF(x, y);		
			System.Drawing.Drawing2D.GraphicsPath tempGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath(tempPointArray, new byte[]{(byte)System.Drawing.Drawing2D.PathPointType.Start});
			graphPath.AddPath(tempGraphicsPath, false);
		}
		else
		{
			graphPath.PathPoints.CopyTo(tempPointArray, 0);
			tempPointArray[graphPath.PointCount] = new System.Drawing.Point(x, y);
			
			graphPath.PathTypes.CopyTo(tempPointTypeArray, 0);
			tempPointTypeArray[graphPath.PointCount] = (byte) System.Drawing.Drawing2D.PathPointType.Line;

			System.Drawing.Drawing2D.GraphicsPath tempGraphics = new System.Drawing.Drawing2D.GraphicsPath(tempPointArray, tempPointTypeArray);
			graphPath.Reset();
			graphPath.AddPath(tempGraphics, false);
			graphPath.CloseFigure();
		}
	}
	/*******************************/
	/// <summary>
	/// This class provides some useful methods for calculate operations with components.
	/// </summary>
	public class SwingUtilsSupport
	{
		/// <summary>
		/// Calculates the intersection between two rectangles.
		/// </summary>
		/// <param name="X">The X coordinate of the first rectangle.</param>
		/// <param name="Y">The Y coordinate of the first rectangle.</param>
		/// <param name="width">The width of the first rectangle.</param>
		/// <param name="height">The height of the first rectangle.</param>
		/// <param name="rectangle">The second rectangle used to make the intersection.</param>
		/// <returns>The Rectangle results from the intersection operation.</returns>
		public static System.Drawing.Rectangle ComputeIntersection(int X, int Y, int width, int height, System.Drawing.Rectangle rectangle)
		{
			return System.Drawing.Rectangle.Intersect(new System.Drawing.Rectangle(X, Y, width, height), rectangle);
		}

		/// <summary>
		/// Calculates the union of two rectangles.
		/// </summary>
		/// <param name="X">The X coordinate of the first rectangle.</param>
		/// <param name="Y">The Y coordinate of the first rectangle.</param>
		/// <param name="width">The width of the first rectangle.</param>
		/// <param name="height">The height of the first rectangle.</param>
		/// <param name="rectangle">The second rectangle used to make the union.</param>
		/// <returns>The Rectangle results from the union operation.</returns>
		public static System.Drawing.Rectangle ComputeUnion(int X, int Y, int width, int height, System.Drawing.Rectangle rectangle)
		{
			return System.Drawing.Rectangle.Union(new System.Drawing.Rectangle(X, Y, width, height), rectangle);
		}

		/// <summary>
		/// Takes the Point coordinate from the screen and translate into component's coordinate.
		/// </summary>
		/// <param name="pointSource">The Point that represents the coordinates from the screen
		/// and will be translated to component's coordinates.</param>
		/// <param name="component">The component used to calculate the new coordinates of the Point.</param>
		public static void PointFromScreen(ref System.Drawing.Point pointSource, System.Windows.Forms.Control component)
		{			
			pointSource.X = (((System.Windows.Forms.Control)component).PointToClient(pointSource)).X;
			pointSource.Y = (((System.Windows.Forms.Control)component).PointToClient(pointSource)).Y;			
		}
		
		/// <summary>
		/// Takes the Point coordinate from the component and translates it into screen's coordinates.
		/// </summary>
		/// <param name="pointSource">The Point that represents the coordinates from the component
		/// and will be translated to screen's coordinates.</param>
		/// <param name="component">The component used to calculate the new coordinates of the point.</param>
		public static void PointToScreen(ref System.Drawing.Point pointSource, System.Windows.Forms.Control component)
		{
			pointSource.X = (((System.Windows.Forms.Control)component).PointToScreen(pointSource)).X;
			pointSource.Y = (((System.Windows.Forms.Control)component).PointToScreen(pointSource)).Y;
		}

		/// <summary>
		/// Calculates if the first rectangle contains the second one.
		/// </summary>
		/// <param name="rectangle1">The first rectangle.</param>
		/// <param name="rectangle2">The second rectangle.</param>
		/// <returns>True if the first rectangle contains the second, otherwise false.</returns>
		public static bool RectangleContains(System.Drawing.Rectangle rectangle1, System.Drawing.Rectangle rectangle2)
		{
			return (((rectangle1.X + rectangle1.Width) >= (rectangle2.X + rectangle2.Width)) &&
					((rectangle1.Y + rectangle1.Height) >= (rectangle2.Y + rectangle2.Height)));
		}

		/// <summary>
		/// Calculates if the MouseEvent was fired by the left mouse button.
		/// </summary>
		/// <param name="mouseEvent">The MouseEvent of origin.</param>
		/// <returns>True if the the MouseEvent was generated by the left mouse button, otherwise false.</returns>
		public static bool IsMouseLeft(System.Windows.Forms.MouseEventArgs mouseEvent)
		{
			return (mouseEvent.Button == System.Windows.Forms.MouseButtons.Left);
		}

		/// <summary>
		/// Calculates if the MouseEvent was fired by the right mouse button.
		/// </summary>
		/// <param name="mouseEvent">The MouseEvent of origin.</param>
		/// <returns>True if the the MouseEvent was generated by the right mouse button, otherwise false.</returns>
		public static bool IsMouseRight(System.Windows.Forms.MouseEventArgs mouseEvent)
		{
			return (mouseEvent.Button == System.Windows.Forms.MouseButtons.Right);
		}

		/// <summary>
		/// Calculates if the MouseEvent was fired by the middle mouse button.
		/// </summary>
		/// <param name="mouseEvent">The MouseEvent of origin.</param>
		/// <returns>True if the the MouseEvent was generated by the middle mouse button, otherwise false.</returns>
		public static bool IsMouseMiddle(System.Windows.Forms.MouseEventArgs mouseEvent)
		{
			return (mouseEvent.Button == System.Windows.Forms.MouseButtons.Middle);
		}
	}


	/*******************************/
	/// <summary>
	/// This class contains support methods to work with PointF struct.
	/// </summary>
	public class PointFSupport
	{
		/// <summary>
		/// Returns the distance between two specified points.
		/// </summary>
		/// <param name="point1">The first point.</param>
		/// <param name="point2">The second point.</param>
		/// <returns>Returns the distance between two specified points.</returns>
		public static double Distance(System.Drawing.PointF point1, System.Drawing.PointF point2)
		{
			return Distance(point1.X, point1.Y, point2.X, point2.Y);
		}

		/// <summary>
		/// Returns the distance between two specified points.
		/// </summary>
		/// <param name="point1">The first point.</param>
		/// <param name="x">The x-coordinate of the second point.</param>
		/// <param name="y">The y-coordinate of the second point.</param>
		/// <returns>Returns the distance between two specified points.</returns>
		public static double Distance(System.Drawing.PointF point1, float x, float y)
		{
			return Distance(point1.X, point1.Y, x, y);
		}

		/// <summary>
		/// Returns the distance between two specified points
		/// </summary>
		/// <param name="x1">The x-coordinate of the first point</param>
		/// <param name="y1">The y-coordinate of the first point</param>
		/// <param name="x2">The x-coordinate of the second point</param>
		/// <param name="y2">The y-coordinate of the second point</param>
		/// <returns>Returns the distance between two specified points</returns>
		public static double Distance(float x1, float y1, float x2, float y2)
		{
			//The Pythagorean Theorem: a^2 + b^2 = c^2
			float a = System.Math.Max(x1, x2) - System.Math.Min(x1, x2);
			float b = System.Math.Max(y1, y2) - System.Math.Min(y1, y2);
			double c = System.Math.Pow(a, 2) + System.Math.Pow(b, 2);
			return System.Math.Sqrt(c);
		}

		/// <summary>
		/// Returns the square distance between two specified points.
		/// </summary>
		/// <param name="point1">The first point.</param>
		/// <param name="point2">The second point.</param>
		/// <returns>Returns the square distance between two specified points.</returns>
		public static double DistanceSqrt(System.Drawing.PointF point1, System.Drawing.PointF point2)
		{
			return DistanceSqrt(point1.X, point1.Y, point2.X, point2.Y);
		}

		/// <summary>
		/// Returns the square distance between two specified points.
		/// </summary>
		/// <param name="point1">The first point.</param>
		/// <param name="x">The x-coordinate of the second point.</param>
		/// <param name="y">The y-coordinate of the second point.</param>
		/// <returns>Returns the square distance between two specified points.</returns>
		public static double DistanceSqrt(System.Drawing.PointF point1, float x, float y)
		{
			return DistanceSqrt(point1.X, point1.Y, x, y);
		}

		/// <summary>
		/// Returns the square distance between two specified points.
		/// </summary>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point.</param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point.</param>
		/// <returns>Returns the square distance between two specified points.</returns>
		public static double DistanceSqrt(float x1, float y1, float x2, float y2)
		{
			return System.Math.Pow(Distance(x1, y1, x2, y2), 2);
		}
	}


	/*******************************/
	/// <summary>
	/// This method works as a handler for the Control.Layout event, it arranges the controls into a container
	/// control in a rectangular grid (rows and columns).
	/// The size and location of each inner control will be calculated according the number of them in the 
	/// container.
	/// The number of columns, rows, horizontal and vertical spacing between the inner controls will are
	/// specified as array of object values in the Tag property of the container.
	/// If the number of rows and columns specified is not enought to allocate all the controls, the number of 
	/// columns will be increased in order to maintain the number of rows specified.
	/// </summary>
	/// <param name="event_sender">The container control in which the controls will be relocated.</param>
	/// <param name="eventArgs">Data of the event.</param>
	public static void GridLayoutResize(System.Object event_sender, System.Windows.Forms.LayoutEventArgs eventArgs)
	{
		System.Windows.Forms.Control container = (System.Windows.Forms.Control) event_sender;
		if ((container.Tag is System.Drawing.Rectangle) && (container.Controls.Count > 0))
		{
			System.Drawing.Rectangle tempRectangle = (System.Drawing.Rectangle) container.Tag;

			if ((tempRectangle.X <= 0) && (tempRectangle.Y <= 0))
				throw new System.Exception("Illegal column and row layout count specified");

			int horizontal = tempRectangle.Width;
			int vertical = tempRectangle.Height;
			int count = container.Controls.Count;

			int rows = (tempRectangle.X == 0) ? (int) System.Math.Ceiling((double) (count / tempRectangle.Y)) : tempRectangle.X;
			int columns = (tempRectangle.Y == 0) ? (int) System.Math.Ceiling((double) (count / tempRectangle.X)) : tempRectangle.Y;
			
			rows = (rows == 0) ? 1 : rows;
			columns = (columns == 0) ? 1 : columns;

			while (count > rows * columns) columns++;

			int width = (container.DisplayRectangle.Width - horizontal * (columns - 1)) / columns;
			int height = (container.DisplayRectangle.Height - vertical * (rows - 1)) / rows;
			
			int indexColumn = 0;
			int indexRow = 0;

			foreach (System.Windows.Forms.Control tempControl in container.Controls)
			{
				int xCoordinate = indexColumn++ * (width + horizontal);
				int yCoordinate = indexRow * (height + vertical);
				tempControl.Location = new System.Drawing.Point(xCoordinate, yCoordinate);
				tempControl.Width = width;
				tempControl.Height = height;
				if (indexColumn == columns)
				{
					indexColumn = 0;
					indexRow++;
				}
			}
		}
	}


	/*******************************/
		/// <summary>
		/// This method returns a 'Point' indicating the cursor's current location in the component's coordinates.
		/// </summary>
		/// <param name="DragEvent">The event to get its location.</param>
		/// <returns>A new 'Point' that represents the cursor location when the DragEvent was fired.</returns>
		public static System.Drawing.Point GetLocation(System.Windows.Forms.DragEventArgs DragEvent)
		{
			return new System.Drawing.Point(DragEvent.X, DragEvent.Y);
		}


	/*******************************/
	/// <summary>
	/// Support functions for the System.Drawing.Rectangle structure
	/// </summary>
	public class RectangleSupport
	{
		/// <summary>
		/// Changes the edges for the rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle to change</param>
		/// <param name="x">New x-coordinate of the upper-left corner</param>
		/// <param name="y">New y-coordinate of the upper-left corner</param>
		/// <param name="width">New width of the Rectangle structure</param>
		/// <param name="height">New height of the Rectangle structure</param>
		public static void ReshapeRectangle(ref System.Drawing.Rectangle rectangle, int x, int y, int width, int height)
		{
			rectangle.X = x;
			rectangle.Y = y;
			rectangle.Width = width;
			rectangle.Height = height;
		}

		/// <summary>
		/// Adds a point to the Rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle to change</param>
		/// <param name="newX">X-axis of the point to add</param>
		/// <param name="newY">Y-axis of the point to add</param>
		public static void AddXYToRectangle(ref System.Drawing.Rectangle rectangle, int newX, int newY)
		{
			int x = System.Math.Min(rectangle.X, newX);
			int y = System.Math.Min(rectangle.Y, newY);
			rectangle.Width = System.Math.Max(rectangle.X + rectangle.Width, newX) - x;
			rectangle.Height = System.Math.Max(rectangle.Y + rectangle.Height, newY) - y;
			rectangle.X = x;
			rectangle.Y = y;
		}

		/// <summary>
		/// Adds a the second rectangle to the first rectangle
		/// </summary>
		/// <param name="rectangle">Target rectangle</param>
		/// <param name="newRectangle">Rectangle to add</param>
		public static void AddRectangleToRectangle(ref System.Drawing.Rectangle rectangle, System.Drawing.Rectangle newRectangle)
		{
			int x = System.Math.Min(rectangle.X, newRectangle.X);
			int y = System.Math.Min(rectangle.Y, newRectangle.Y);
			rectangle.Width = System.Math.Max(rectangle.X + rectangle.Width, newRectangle.X + newRectangle.Width) - x;
			rectangle.Height = System.Math.Max(rectangle.Y + rectangle.Height, newRectangle.Y + newRectangle.Height) - y;
			rectangle.X = x;
			rectangle.Y = y;
		}

		/// <summary>
		/// Changes the edges for the first rectangle with the values of the second rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle to change</param>
		/// <param name="newRectangle">Rectangle from which to copy shape</param>
		public static void SetBoundsRectangle(ref System.Drawing.Rectangle rectangle, System.Drawing.Rectangle newRectangle)
		{
			ReshapeRectangle(ref rectangle, newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
		}

		/// <summary>
		/// Adds a point to the area covered by the rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle to resize</param>
		/// <param name="newPoint">Represents the ordered pair of integer x- and y-coordinates to add to the rectangle</param>
		public static void AddPointToRectangle(ref System.Drawing.Rectangle rectangle, System.Drawing.Point newPoint)
		{
			AddXYToRectangle(ref rectangle, newPoint.X, newPoint.Y);
		}
	}

	/*******************************/
	/// <summary>
	/// Checks if a file have write permissions
	/// </summary>
	/// <param name="file">The file instance to check</param>
	/// <returns>True if have write permissions otherwise false</returns>
public static bool FileCanWrite(System.IO.FileInfo file)
{
return (System.IO.File.GetAttributes(file.FullName) & System.IO.FileAttributes.ReadOnly) != System.IO.FileAttributes.ReadOnly;
}

	///*******************************/
	///// <summary>
	///// Adds a MenuItem to a ContextMenu
	///// </summary>
	///// <param name="menu">The MenuContext which the MenuItem will be added to</param>
	///// <param name="menuItem">The MenuItem to be added</param>
	///// <returns>The added MenuItem which has been added to the ContextMenu</returns>
	//public static  System.Windows.Forms.MenuItem AddMenuItem(System.Windows.Forms.ContextMenu menu, System.Windows.Forms.MenuItem menuItem)
	//{
	//	menu.MenuItems.Add(menuItem);
	//	return menuItem;
	//}

	/*******************************/
/// <summary>
/// Provides support for DateFormat
/// </summary>
public class DateTimeFormatManager
{
	static public DateTimeFormatHashTable manager = new DateTimeFormatHashTable();

	/// <summary>
	/// Hashtable class to provide functionality for dateformat properties
	/// </summary>
	public class DateTimeFormatHashTable :System.Collections.Hashtable 
	{
		/// <summary>
		/// Sets the format for datetime.
		/// </summary>
		/// <param name="format">DateTimeFormat instance to set the pattern</param>
		/// <param name="newPattern">A string with the pattern format</param>
		public void SetDateFormatPattern(System.Globalization.DateTimeFormatInfo format, string newPattern)
		{
			if (this[format] != null)
				((DateTimeFormatProperties) this[format]).DateFormatPattern = newPattern;
			else
			{
				DateTimeFormatProperties tempProps = new DateTimeFormatProperties();
				tempProps.DateFormatPattern  = newPattern;
				Add(format, tempProps);
			}
		}

		/// <summary>
		/// Gets the current format pattern of the DateTimeFormat instance
		/// </summary>
		/// <param name="format">The DateTimeFormat instance which the value will be obtained</param>
		/// <returns>The string representing the current datetimeformat pattern</returns>
		public string GetDateFormatPattern(System.Globalization.DateTimeFormatInfo format)
		{
			if (this[format] == null)
				return "d-MMM-yy";
			else
				return ((DateTimeFormatProperties) this[format]).DateFormatPattern;
		}
		
		/// <summary>
		/// Sets the datetimeformat pattern to the giving format
		/// </summary>
		/// <param name="format">The datetimeformat instance to set</param>
		/// <param name="newPattern">The new datetimeformat pattern</param>
		public void SetTimeFormatPattern(System.Globalization.DateTimeFormatInfo format, string newPattern)
		{
			if (this[format] != null)
				((DateTimeFormatProperties) this[format]).TimeFormatPattern = newPattern;
			else
			{
				DateTimeFormatProperties tempProps = new DateTimeFormatProperties();
				tempProps.TimeFormatPattern  = newPattern;
				Add(format, tempProps);
			}
		}

		/// <summary>
		/// Gets the current format pattern of the DateTimeFormat instance
		/// </summary>
		/// <param name="format">The DateTimeFormat instance which the value will be obtained</param>
		/// <returns>The string representing the current datetimeformat pattern</returns>
		public string GetTimeFormatPattern(System.Globalization.DateTimeFormatInfo format)
		{
			if (this[format] == null)
				return "h:mm:ss tt";
			else
				return ((DateTimeFormatProperties) this[format]).TimeFormatPattern;
		}

		/// <summary>
		/// Internal class to provides the DateFormat and TimeFormat pattern properties on .NET
		/// </summary>
		class DateTimeFormatProperties
		{
			public string DateFormatPattern = "d-MMM-yy";
			public string TimeFormatPattern = "h:mm:ss tt";
		}
	}	
}
	/*******************************/
	/// <summary>
	/// Gets the DateTimeFormat instance and date instance to obtain the date with the format passed
	/// </summary>
	/// <param name="format">The DateTimeFormat to obtain the time and date pattern</param>
	/// <param name="date">The date instance used to get the date</param>
	/// <returns>A string representing the date with the time and date patterns</returns>
	public static string FormatDateTime(System.Globalization.DateTimeFormatInfo format, System.DateTime date)
	{
		string timePattern = DateTimeFormatManager.manager.GetTimeFormatPattern(format);
		string datePattern = DateTimeFormatManager.manager.GetDateFormatPattern(format);
		return date.ToString(datePattern + " " + timePattern, format);            
	}

	/*******************************/
	/// <summary>
	/// Gets the DateTimeFormat instance using the culture passed as parameter and sets the pattern to the time or date depending of the value
	/// </summary>
	/// <param name="dateStyle">The desired date style.</param>
	/// <param name="timeStyle">The desired time style</param>
	/// <param name="culture">The CultureInfo instance used to obtain the DateTimeFormat</param>
	/// <returns>The DateTimeFomatInfo of the culture and with the desired date or time style</returns>
	public static System.Globalization.DateTimeFormatInfo GetDateTimeFormatInstance(int dateStyle, int timeStyle, System.Globalization.CultureInfo culture)
	{
		const int NULLPATERN = -1;
		const int PATERN_1 = 0;
		const int PATERN_2 = 1;
		const int PATERN_3 = 2;
		const int PATERN_4 = 3;
		System.Globalization.DateTimeFormatInfo format = culture.DateTimeFormat;
		 
		switch (timeStyle)
		{
			case NULLPATERN:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "");
				break;

			case PATERN_1:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, format.LongTimePattern);
				break;

			case PATERN_2:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm:ss tt zzz");
				break;

			case PATERN_3:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm:ss tt");
				break;

			case PATERN_4:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, format.ShortTimePattern);
				break;
		}

		switch (dateStyle)
		{
			case NULLPATERN:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "");
				break;

			case PATERN_1:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, format.LongDatePattern);
				break;

			case PATERN_2:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "MMMM d, yyy" );
				break;

			case PATERN_3:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "MMM d, yyy"  );
				break;

			case PATERN_4:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, format.ShortDatePattern);
				break;
		}

		return format;
	}

	/*******************************/
	/// <summary>
	/// This class contains static methods to manage tab controls.
	/// </summary>
	public class TabControlSupport
	{
		/// <summary>
		/// Create a new instance of TabControl and set the alignment property.
		/// </summary>
		/// <param name="alignment">The alignment property value.</param>
		/// <returns>New TabControl instance.</returns>
		public static System.Windows.Forms.TabControl CreateTabControl( System.Windows.Forms.TabAlignment alignment)
		{
			System.Windows.Forms.TabControl tabcontrol = new System.Windows.Forms.TabControl();
			tabcontrol.Alignment = alignment;
			return tabcontrol;
		}

		/// <summary>
		/// Set the alignment property to an instance of TabControl .
		/// </summary>
		/// <param name="tabcontrol">An instance of TabControl.</param>
		/// <param name="alignment">The alignment property value.</param>
		public static void SetTabControl( System.Windows.Forms.TabControl tabcontrol, System.Windows.Forms.TabAlignment alignment)
		{
			tabcontrol.Alignment = alignment;
		}

		/// <summary>
		/// Method to add TabPages into the TabControl object.
		/// </summary>
		/// <param name="tabControl">The TabControl to be modified.</param>
		/// <param name="component">A component to be added into the new TabControl.</param>
		public static System.Windows.Forms.Control AddTab(System.Windows.Forms.TabControl tabControl, System.Windows.Forms.Control component)
		{
			System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();
			tabPage.Controls.Add(component);
			tabControl.TabPages.Add(tabPage);
			return component;
		}
	
		/// <summary>
		/// Method to add TabPages into the TabControl object.
		/// </summary>
		/// <param name="tabControl">The TabControl to be modified.</param>
		/// <param name="TabLabel">The label for the new TabPage.</param>
		/// <param name="component">A component to be added into the new TabControl.</param>
		public static System.Windows.Forms.Control AddTab(System.Windows.Forms.TabControl tabControl, string tabLabel, System.Windows.Forms.Control component)
		{
			System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage(tabLabel);
			tabPage.Controls.Add(component);
			tabControl.TabPages.Add(tabPage);
			return component;
		}

		/// <summary>
		/// Method to add TabPages into the TabControl object.
		/// </summary>
		/// <param name="tabControl">The TabControl to be modified.</param>
		/// <param name="component">A component to be added into the new TabControl.</param>
		/// <param name="constraints">The object that should be displayed in the tab but won't because of limitations</param>		
		public static void AddTab(System.Windows.Forms.TabControl tabControl, System.Windows.Forms.Control component, System.Object constraints)
		{
			System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();
			if (constraints is string) 
			{
				tabPage.Text = (String)constraints;
			}
			tabPage.Controls.Add(component);
			tabControl.TabPages.Add(tabPage);
		}

		/// <summary>
		/// Method to add TabPages into the TabControl object.
		/// </summary>
		/// <param name="tabControl">The TabControl to be modified.</param>
		/// <param name="TabLabel">The label for the new TabPage.</param>
		/// <param name="constraints">The object that should be displayed in the tab but won't because of limitations</param>
		/// <param name="component">A component to be added into the new TabControl.</param>
		public static void AddTab(System.Windows.Forms.TabControl tabControl, string tabLabel, System.Object constraints, System.Windows.Forms.Control component)
		{
			System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage(tabLabel);
			tabPage.Controls.Add(component);
			tabControl.TabPages.Add(tabPage);
		}

		/// <summary>
		/// Method to add TabPages into the TabControl object.
		/// </summary>
		/// <param name="tabControl">The TabControl to be modified.</param>
		/// <param name="tabLabel">The label for the new TabPage.</param>
		/// <param name="image">Background image for the TabPage.</param>
		/// <param name="component">A component to be added into the new TabControl.</param>
		public static void AddTab(System.Windows.Forms.TabControl tabControl, string tabLabel, System.Drawing.Image image, System.Windows.Forms.Control component)
		{
			System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage(tabLabel);			
			tabPage.BackgroundImage = image;
			tabPage.Controls.Add(component);
			tabControl.TabPages.Add(tabPage);			
		}
	}


	/*******************************/
	/// <summary>
	/// This class manages array operations.
	/// </summary>
	public class ArraySupport
	{
		/// <summary>
		/// Compares the entire members of one array whith the other one.
		/// </summary>
		/// <param name="array1">The array to be compared.</param>
		/// <param name="array2">The array to be compared with.</param>
		/// <returns>True if both arrays are equals otherwise it returns false.</returns>
		/// <remarks>Two arrays are equal if they contains the same elements in the same order.</remarks>
		public static bool Equals(System.Array array1, System.Array array2)
		{
			bool result = false;
			if ((array1 == null) && (array2 == null))
				result = true;
			else if ((array1 != null) && (array2 != null))
			{
				if (array1.Length == array2.Length)
				{
					int length = array1.Length;
					result = true;
					for (int index = 0; index < length; index++)
					{
						if (!(array1.GetValue(index).Equals(array2.GetValue(index))))
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Fills the array with an specific value from an specific index to an specific index.
		/// </summary>
		/// <param name="array">The array to be filled.</param>
		/// <param name="fromindex">The first index to be filled.</param>
		/// <param name="toindex">The last index to be filled.</param>
		/// <param name="val">The value to fill the array with.</param>
		public static void Fill(System.Array array, System.Int32 fromindex, System.Int32 toindex, System.Object val)
		{
			System.Object Temp_Object = val;
			System.Type elementtype = array.GetType().GetElementType();
			if (elementtype != val.GetType())
				Temp_Object = System.Convert.ChangeType(val, elementtype);
			if (array.Length == 0)
				throw (new System.NullReferenceException());
			if (fromindex > toindex)
				throw (new System.ArgumentException());
			if ((fromindex < 0) || ((System.Array)array).Length < toindex)
				throw (new System.IndexOutOfRangeException());
			for (int index = (fromindex > 0) ? fromindex-- : fromindex; index < toindex; index++)
				array.SetValue(Temp_Object, index);
		}

		/// <summary>
		/// Fills the array with an specific value.
		/// </summary>
		/// <param name="array">The array to be filled.</param>
		/// <param name="val">The value to fill the array with.</param>
		public static void Fill(System.Array array, System.Object val)
		{
			Fill(array, 0, array.Length, val);
		}
	}


	/*******************************/
	/// <summary>
	/// Verifies if a value exist in a NameValueCollection.
	/// </summary>
	/// <param name="collection">The NameValueCollection to look in.</param>
	/// <param name="key">The key to look for.</param>
	/// <returns>If key exist in the NameValueCollection returns true, otherwise false.</returns>
	public static  bool ContainsKeySupport(System.Collections.Specialized.NameValueCollection collection, string key)
	{
		bool exists = false;
		if( collection != null)
		{
			string [] keys = collection.AllKeys;
			exists = !(System.Array.IndexOf(keys,key) == -1);
		}
		return exists;
	}
	/*******************************/
	/// <summary>
	/// This class manages different features for calendars.
	/// The different calendars are internally managed using a hashtable structure.
	/// </summary>
	public class CalendarManager
	{
		/// <summary>
		/// Field used to get or set the year.
		/// </summary>
		public const int YEAR = 1;

		/// <summary>
		/// Field used to get or set the month.
		/// </summary>
		public const int MONTH = 2;
		
		/// <summary>
		/// Field used to get or set the day of the month.
		/// </summary>
		public const int DATE = 5;
		
		/// <summary>
		/// Field used to get or set the hour of the morning or afternoon.
		/// </summary>
		public const int HOUR = 10;
		
		/// <summary>
		/// Field used to get or set the minute within the hour.
		/// </summary>
		public const int MINUTE = 12;
		
		/// <summary>
		/// Field used to get or set the second within the minute.
		/// </summary>
		public const int SECOND = 13;
		
		/// <summary>
		/// Field used to get or set the millisecond within the second.
		/// </summary>
		public const int MILLISECOND = 14;
		
		/// <summary>
		/// Field used to get or set the day of the year.
		/// </summary>
		public const int DAY_OF_YEAR = 4;
		
		/// <summary>
		/// Field used to get or set the day of the month.
		/// </summary>
		public const int DAY_OF_MONTH = 6;
		
		/// <summary>
		/// Field used to get or set the day of the week.
		/// </summary>
		public const int DAY_OF_WEEK = 7;
		
		/// <summary>
		/// Field used to get or set the hour of the day.
		/// </summary>
		public const int HOUR_OF_DAY = 11;
		
		/// <summary>
		/// Field used to get or set whether the HOUR is before or after noon.
		/// </summary>
		public const int AM_PM = 9;
		
		/// <summary>
		/// Field used to get or set the value of the AM_PM field which indicates the period of the day from midnight to just before noon.
		/// </summary>
		public const int AM = 0;
		
		/// <summary>
		/// Field used to get or set the value of the AM_PM field which indicates the period of the day from noon to just before midnight.
		/// </summary>
		public const int PM = 1;
		
		/// <summary>
		/// The hashtable that contains the calendars and its properties.
		/// </summary>
		static public CalendarHashTable manager = new CalendarHashTable();

		/// <summary>
		/// Internal class that inherits from HashTable to manage the different calendars.
		/// This structure will contain an instance of System.Globalization.Calendar that represents 
		/// a type of calendar and its properties (represented by an instance of CalendarProperties 
		/// class).
		/// </summary>
		public class CalendarHashTable:System.Collections.Hashtable 
		{
			/// <summary>
			/// Gets the calendar current date and time.
			/// </summary>
			/// <param name="calendar">The calendar to get its current date and time.</param>
			/// <returns>A System.DateTime value that indicates the current date and time for the 
			/// calendar given.</returns>
			public System.DateTime GetDateTime(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
					return ((CalendarProperties) this[calendar]).dateTime;
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					return this.GetDateTime(calendar);
				}
			}

			/// <summary>
			/// Sets the specified System.DateTime value to the specified calendar.
			/// </summary>
			/// <param name="calendar">The calendar to set its date.</param>
			/// <param name="date">The System.DateTime value to set to the calendar.</param>
			public void SetDateTime(System.Globalization.Calendar calendar, System.DateTime date)
			{
				if (this[calendar] != null)
				{
					((CalendarProperties) this[calendar]).dateTime = date;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = date;
					this.Add(calendar, tempProps);
				}
			}

			/// <summary>
			/// Sets the corresponding field in an specified calendar with the value given.
			/// If the specified calendar does not have exist in the hash table, it creates a 
			/// new instance of the calendar with the current date and time and then assings it 
			/// the new specified value.
			/// </summary>
			/// <param name="calendar">The calendar to set its date or time.</param>
			/// <param name="field">One of the fields that composes a date/time.</param>
			/// <param name="fieldValue">The value to be set.</param>
			public void Set(System.Globalization.Calendar calendar, int field, int fieldValue)
			{
				if (this[calendar] != null)
				{
					System.DateTime tempDate = ((CalendarProperties) this[calendar]).dateTime;
					switch (field)
					{
						case CalendarManager.DATE:
							tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
							break;
						case CalendarManager.HOUR:
							tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
							break;
						case CalendarManager.MILLISECOND:
							tempDate = tempDate.AddMilliseconds(fieldValue - tempDate.Millisecond);
							break;
						case CalendarManager.MINUTE:
							tempDate = tempDate.AddMinutes(fieldValue - tempDate.Minute);
							break;
						case CalendarManager.MONTH:
							//Month value is 0-based. e.g., 0 for January
							tempDate = tempDate.AddMonths((fieldValue + 1) - tempDate.Month);
							break;
						case CalendarManager.SECOND:
							tempDate = tempDate.AddSeconds(fieldValue - tempDate.Second);
							break;
						case CalendarManager.YEAR:
							tempDate = tempDate.AddYears(fieldValue - tempDate.Year);
							break;
						case CalendarManager.DAY_OF_MONTH:
							tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
							break;
						case CalendarManager.DAY_OF_WEEK:
							tempDate = tempDate.AddDays((fieldValue - 1) - (int)tempDate.DayOfWeek);
							break;
						case CalendarManager.DAY_OF_YEAR:
							tempDate = tempDate.AddDays(fieldValue - tempDate.DayOfYear);
							break;
						case CalendarManager.HOUR_OF_DAY:
							tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
							break;

						default:
							break;
					}
					((CalendarProperties) this[calendar]).dateTime = tempDate;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, field, fieldValue);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) and hour (hour and minute) 
			/// to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			/// <param name="hour">Integer value that represent the hour.</param>
			/// <param name="minute">Integer value that represent the minutes.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
					this.Set(calendar, CalendarManager.HOUR, hour);
					this.Set(calendar, CalendarManager.MINUTE, minute);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day, hour, minute);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) and hour (hour, minute and second) 
			/// to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			/// <param name="hour">Integer value that represent the hour.</param>
			/// <param name="minute">Integer value that represent the minutes.</param>
			/// <param name="second">Integer value that represent the seconds.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute, int second)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
					this.Set(calendar, CalendarManager.HOUR, hour);
					this.Set(calendar, CalendarManager.MINUTE, minute);
					this.Set(calendar, CalendarManager.SECOND, second);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day, hour, minute, second);
				}
			}

			/// <summary>
			/// Gets the value represented by the field specified.
			/// </summary>
			/// <param name="calendar">The calendar to get its date or time.</param>
			/// <param name="field">One of the field that composes a date/time.</param>
			/// <returns>The integer value for the field given.</returns>
			public int Get(System.Globalization.Calendar calendar, int field)
			{
				if (this[calendar] != null)
				{
					int tempHour;
					switch (field)
					{
						case CalendarManager.DATE:
							return ((CalendarProperties) this[calendar]).dateTime.Day;
						case CalendarManager.HOUR:
							tempHour = ((CalendarProperties) this[calendar]).dateTime.Hour;
							return tempHour > 12 ? tempHour - 12 : tempHour;
						case CalendarManager.MILLISECOND:
							return ((CalendarProperties) this[calendar]).dateTime.Millisecond;
						case CalendarManager.MINUTE:
							return ((CalendarProperties) this[calendar]).dateTime.Minute;
						case CalendarManager.MONTH:
							//Month value is 0-based. e.g., 0 for January
							return ((CalendarProperties) this[calendar]).dateTime.Month - 1;
						case CalendarManager.SECOND:
							return ((CalendarProperties) this[calendar]).dateTime.Second;
						case CalendarManager.YEAR:
							return ((CalendarProperties) this[calendar]).dateTime.Year;
						case CalendarManager.DAY_OF_MONTH:
							return ((CalendarProperties) this[calendar]).dateTime.Day;
						case CalendarManager.DAY_OF_YEAR:							
							return (int)(((CalendarProperties) this[calendar]).dateTime.DayOfYear);
						case CalendarManager.DAY_OF_WEEK:
							return (int)(((CalendarProperties) this[calendar]).dateTime.DayOfWeek) + 1;
						case CalendarManager.HOUR_OF_DAY:
							return ((CalendarProperties) this[calendar]).dateTime.Hour;
						case CalendarManager.AM_PM:
							tempHour = ((CalendarProperties) this[calendar]).dateTime.Hour;
							return tempHour > 12 ? CalendarManager.PM : CalendarManager.AM;

						default:
							return 0;
					}
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					return this.Get(calendar, field);
				}
			}

			/// <summary>
			/// Sets the time in the specified calendar with the long value.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="milliseconds">A long value that indicates the milliseconds to be set to 
			/// the hour for the calendar.</param>
			public void SetTimeInMilliseconds(System.Globalization.Calendar calendar, long milliseconds)
			{
				if (this[calendar] != null)
				{
					((CalendarProperties) this[calendar]).dateTime = new System.DateTime(milliseconds);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = new System.DateTime(System.TimeSpan.TicksPerMillisecond * milliseconds);
					this.Add(calendar, tempProps);
				}
			}
				
			/// <summary>
			/// Gets what the first day of the week is; e.g., Sunday in US, Monday in France.
			/// </summary>
			/// <param name="calendar">The calendar to get its first day of the week.</param>
			/// <returns>A System.DayOfWeek value indicating the first day of the week.</returns>
			public System.DayOfWeek GetFirstDayOfWeek(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
				{
					if (((CalendarProperties)this[calendar]).dateTimeFormat == null)
					{
						((CalendarProperties)this[calendar]).dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
						((CalendarProperties)this[calendar]).dateTimeFormat.FirstDayOfWeek = System.DayOfWeek.Sunday;
					}
					return ((CalendarProperties) this[calendar]).dateTimeFormat.FirstDayOfWeek;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
					tempProps.dateTimeFormat.FirstDayOfWeek = System.DayOfWeek.Sunday;
					this.Add(calendar, tempProps);
					return this.GetFirstDayOfWeek(calendar);
				}
			}

			/// <summary>
			/// Sets what the first day of the week is; e.g., Sunday in US, Monday in France.
			/// </summary>
			/// <param name="calendar">The calendar to set its first day of the week.</param>
			/// <param name="firstDayOfWeek">A System.DayOfWeek value indicating the first day of the week
			/// to be set.</param>
			public void SetFirstDayOfWeek(System.Globalization.Calendar calendar, System.DayOfWeek  firstDayOfWeek)
			{
				if (this[calendar] != null)
				{
					if (((CalendarProperties)this[calendar]).dateTimeFormat == null)
						((CalendarProperties)this[calendar]).dateTimeFormat = new System.Globalization.DateTimeFormatInfo();

					((CalendarProperties) this[calendar]).dateTimeFormat.FirstDayOfWeek = firstDayOfWeek;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
					this.Add(calendar, tempProps);
					this.SetFirstDayOfWeek(calendar, firstDayOfWeek);
				}
			}

			/// <summary>
			/// Removes the specified calendar from the hash table.
			/// </summary>
			/// <param name="calendar">The calendar to be removed.</param>
			public void Clear(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
					this.Remove(calendar);
			}

			/// <summary>
			/// Removes the specified field from the calendar given.
			/// If the field does not exists in the calendar, the calendar is removed from the table.
			/// </summary>
			/// <param name="calendar">The calendar to remove the value from.</param>
			/// <param name="field">The field to be removed from the calendar.</param>
			public void Clear(System.Globalization.Calendar calendar, int field)
			{
				if (this[calendar] != null)
					this.Set(calendar, field, 0);
			}

			/// <summary>
			/// Internal class that represents the properties of a calendar instance.
			/// </summary>
			class CalendarProperties
			{
				/// <summary>
				/// The date and time of a calendar.
				/// </summary>
				public System.DateTime dateTime;
				
				/// <summary>
				/// The format for the date and time in a calendar.
				/// </summary>
				public System.Globalization.DateTimeFormatInfo dateTimeFormat;
			}
		}
	}
	/*******************************/
	/// <summary>
	/// This class gives support for creation of management of System.Data.DataTable.
	/// </summary>
	public class DataTableSupport
	{
		/// <summary>
		/// Creates a new System.Data.DataTable with the specified number of columns and rows.
		/// </summary>
		/// <param name="rows">Number of rows.</param>
		/// <param name="columns">Number of columns.</param>
		/// <returns>A System.Data.DataTable with the number of columns and rows specified, containing null values.</returns>
		public static System.Data.DataTable CreateDataTable(int rows, int columns)
		{
			if ((rows >= 0) && (columns >= 0))
			{
				System.Data.DataTable table = new System.Data.DataTable();
				System.Object[] emptyRow = new System.Object[columns];
				while (columns > table.Columns.Count)
					table.Columns.Add();
				while (rows > table.Rows.Count)
					table.Rows.Add(emptyRow);
				return table;
			}
			else
				throw (new System.ArgumentException("Illegal Capacity " + rows + " or " + columns));
		}

		/// <summary>
		/// Sets a System.Data.DataTable with a specific number of columns and rows.
		/// </summary>
		/// <param name="table">System.Data.DataTable instance to be set.</param>
		/// <param name="rows">Number of rows.</param>
		/// <param name="columns">Number of columns.</param>
		/// <returns>A System.Data.DataTable with the number of columns and rows specified, containing null values.</returns>
		public static void SetDataTable(System.Data.DataTable table, int rows, int columns)
		{
			if (table != null)
			{
				if ((rows >= 0) && (columns >= 0))
				{
					System.Object[] emptyRow = new System.Object[columns];
					while (columns > table.Columns.Count)
						table.Columns.Add();
					while (rows > table.Rows.Count)
						table.Rows.Add(emptyRow);
				}
				else
					throw (new System.ArgumentException("Illegal Capacity " + rows + " or " + columns));
			}
		}

		/// <summary>
		/// Creates a System.Data.DataTable with the specified column names and the specified amount of rows.
		/// </summary>
		/// <param name="columnNames">System.Collections.ArrayList containing the names of the columns to add to the DataTable.</param>
		/// <param name="rows">Number of rows.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing null values.</returns>
		public static System.Data.DataTable CreateDataTable(System.Collections.ArrayList columnNames, int rows) 
		{
			return CreateDataTable(columnNames.ToArray(), rows);
		}

		/// <summary>
		/// Sets a System.Data.DataTable with the specified column names and number of rows.
		/// </summary>
		/// <param name="table">System.Data.DataTable instance to be set.</param>
		/// <param name="columnNames">System.Collections.ArrayList containing the names of the columns to add to the DataTable.</param>
		/// <param name="rows">Number of rows.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing null values.</returns>
		public static void SetDataTable(System.Data.DataTable table, System.Collections.ArrayList columnNames, int rows)
		{
			SetDataTable(table, columnNames.ToArray(), rows);
		}

		/// <summary>
		/// Creates a System.Data.DataTable with the specified column names and number of rows.
		/// </summary>
		/// <param name="columnNames">System.Object array containing the names of the columns to add to the DataTable.</param>
		/// <param name="rows">Number of rows.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing null values.</returns>
		public static System.Data.DataTable CreateDataTable(System.Object[] columnNames, int rows)
		{
			if (rows >= 0)
			{
				System.Data.DataTable table = new System.Data.DataTable();
				System.Object[] emptyRow = new System.Object[columnNames.Length];
				foreach (System.Object columnName in columnNames)
					table.Columns.Add((string) columnName);
				while (rows > table.Rows.Count)
					table.Rows.Add(emptyRow);
				return table;
			}
			else
				throw (new System.ArgumentException("Illegal Capacity " + rows));
		}

		/// <summary>
		/// Sets a System.Data.DataTable with the specified column names and number of rows.
		/// </summary>
		/// <param name="table">System.Data.DataTable instance to be set.</param>
		/// <param name="columnNames">System.Object array containing the names of the columns to add to the DataTable.</param>
		/// <param name="rows">Number of rows.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing null values.</returns>
		public static void SetDataTable(System.Data.DataTable table, System.Object[] columnNames, int rows)
		{
			if (table != null)
			{
				if (rows >= 0)
				{
					System.Object[] emptyRow = new System.Object[columnNames.Length];
					foreach (System.Object columnName in columnNames)
						table.Columns.Add((string) columnName);
					while (rows > table.Rows.Count)
						table.Rows.Add(emptyRow);
				}
				else
					throw (new System.ArgumentException("Illegal Capacity " + rows));
			}
		}

		/// <summary>
		/// Creates a System.Data.DataTable with the specified data, column names and number of rows.
		/// </summary>
		/// <param name="data">System.Collections.ArrayList containing the data to add to the DataTable.</param>
		/// <param name="columnNames">System.Collections.ArrayList containing the column names to add to the DataTable.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing the specified values.</returns>
		public static System.Data.DataTable CreateDataTable(System.Collections.ArrayList data, System.Collections.ArrayList columnNames)
		{
			System.Data.DataTable table = new System.Data.DataTable();
			SetDataVector(table, data, columnNames);
			return table;
		}

		/// <summary>
		/// Sets a System.Data.DataTable with the specified data, column names and number of rows.
		/// </summary>
		/// <param name="table">System.Data.DataTable instance to be set.</param>
		/// <param name="data">System.Collections.ArrayList containing the data to add to the DataTable.</param>
		/// <param name="columnNames">System.Collections.ArrayList containing the column names to add to the DataTable.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing the specified values.</returns>
		public static void SetDataTable(System.Data.DataTable table, System.Collections.ArrayList data, System.Collections.ArrayList columnNames)
		{
			if (table != null)
				SetDataVector(table, data, columnNames);
		}

		/// <summary>
		/// Creates a System.Data.DataTable with the specified data, column names and number of rows.
		/// </summary>
		/// <param name="data">System.Object[][] containing the data to add to the DataTable, the first index is the data's row, and the second one its column.</param>
		/// <param name="columnNames">System.Object[] containing the column names to add to the DataTable.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing the specified values.</returns>
		public static System.Data.DataTable CreateDataTable(System.Object[][] data, System.Object[] columnNames)
		{
			System.Data.DataTable table = new System.Data.DataTable();
			SetDataVector(table, data, columnNames);
			return table;
		}

		/// <summary>
		/// Sets a System.Data.DataTable with the specified data, column names and number of rows.
		/// </summary>
		/// <param name="table">System.Data.DataTable instance to set</param>
		/// <param name="data">System.Object[][] containing the data to add to the DataTable, the first index is the data's row, and the second one its column.</param>
		/// <param name="columnNames">System.Object[] containing the column names to add to the DataTable.</param>
		/// <returns>A System.Data.DataTable containing the rows and columns specified, containing the specified values.</returns>
		public static void SetDataTable(System.Data.DataTable table, System.Object[][] data, System.Object[] columnNames)
		{
			if (table != null)
				SetDataVector(table, data, columnNames);
		}

		/// <summary>
		/// Sets the amount of rows to the specified value.
		/// </summary>
		/// <param name="table">The DataTable instance to modify.</param>
		/// <param name="rows">The new amount of rows for the DataTable.</param>
		public static void SetRowCount(System.Data.DataTable table, int rows)
		{
			if (table !=  null)
			{
				if (rows >= 0)
				{
					if (rows > table.Rows.Count)
					{
						System.Object[] emptyRow = new System.Object[table.Columns.Count];
						while (rows > table.Rows.Count)
							table.Rows.Add(emptyRow);
					} 
					else if (rows < table.Rows.Count)
						while (rows < table.Rows.Count)
							table.Rows.RemoveAt(table.Rows.Count);
				}
				else
					throw (new System.ArgumentException("Illegal Capacity " + rows));
			}
		}

		/// <summary>
		/// Sets the amount of columns to the specified value.
		/// </summary>
		/// <param name="table">The DataTable instance to modify.</param>
		/// <param name="columns">The new amount of columns for the DataTable.</param>
		public static void SetColumnCount(System.Data.DataTable table, int columns)
		{
			if (table != null)
			{
				if (columns >= 0)
				{
					if (columns > table.Columns.Count)
						while (columns > table.Columns.Count)
							table.Columns.Add();
					else if (columns < table.Columns.Count)
						while (columns < table.Columns.Count)
							table.Columns.RemoveAt(table.Columns.Count);
				}
				else
					throw (new System.ArgumentException("Illegal Capacity " + columns));
			}
		}

		/// <summary>
		/// Sets the column's names to the specified values.
		/// </summary>
		/// <param name="table">The DataTable instance to be modified.</param>
		/// <param name="newIdentifiers">A System.Object[] containing the values that should be applied to column names.</param>
		public static void SetColumnIdentifiers(System.Data.DataTable table, System.Object[] newIdentifiers)
		{
			int columns = newIdentifiers.Length;
			if (table != null)
			{
				if (columns > table.Columns.Count)
					while (columns > table.Columns.Count)
						table.Columns.Add();
				else if (columns < table.Columns.Count)
					while (columns < table.Columns.Count)
						table.Columns.RemoveAt(table.Columns.Count);
				for (int index = 0; index < table.Columns.Count; index++)
					table.Columns[index].ColumnName = (string) newIdentifiers[index];
			}
		}

		/// <summary>
		/// Sets the column's names to the specified values.
		/// </summary>
		/// <param name="table">The DataTable instance to be modified.</param>
		/// <param name="newIdentifiers">A System.Collections.ArrayList containing the values that should be applied to column names.</param>
		public static void SetColumnIdentifiers(System.Data.DataTable table, System.Collections.ArrayList newIdentifiers)
		{
			SetColumnIdentifiers(table, newIdentifiers.ToArray());
		}

		/// <summary>
		/// Sets the specified data to the corresponding columns in the table specified.
		/// </summary>
		/// <param name="table">The DataTable instance to be modified.</param>
		/// <param name="newData">System.Object[][] containing the data to add to the DataTable, the first index is the data's row, and the second one its column.</param>
		/// <param name="columnNames">A System.Object[] containing the names of the columns.</param>
		public static void SetDataVector(System.Data.DataTable table, System.Object[][] newData, System.Object[] columnNames)
		{
			if (table != null)
			{
				int rows = newData.Length;
				SetRowCount(table, rows);
				SetColumnIdentifiers(table, columnNames);
				for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
					for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
						table.Rows[rowIndex][columnIndex] = newData[rowIndex][columnIndex];
			}
		}

		/// <summary>
		/// Sets the specified data to the corresponding columns in the table specified.
		/// </summary>
		/// <param name="table">The DataTable instance to be modified.</param>
		/// <param name="newData">System.Collections.ArrayList containing the data to add to the DataTable.</param>
		/// <param name="columnNames">A System.Collections.ArrayList containing the names of the columns.</param>
		public static void SetDataVector(System.Data.DataTable table, System.Collections.ArrayList newData, System.Collections.ArrayList columnNames)
		{
			SetColumnIdentifiers(table, columnNames);
			System.Object[][] data = new System.Object[newData.Count][];
			for (int index = 0; index < newData.Count; index++)
			{
				data[index] = new System.Object[table.Columns.Count];
				((System.Collections.ArrayList) newData[index]).CopyTo(data[index]);
			}
			SetDataVector(table, data, columnNames.ToArray());
		}
	}


	/*******************************/
	/// <summary>
	/// Provides functionality not found in .NET map-related interfaces.
	/// </summary>
	public class MapSupport
	{
		/// <summary>
		/// Determines whether the SortedList contains a specific value.
		/// </summary>
		/// <param name="d">The dictionary to check for the value.</param>
		/// <param name="obj">The object to locate in the SortedList.</param>
		/// <returns>Returns true if the value is contained in the SortedList, false otherwise.</returns>
		public static bool ContainsValue(System.Collections.IDictionary d, System.Object obj)
		{
			bool contained = false;
			System.Type type = d.GetType();

			//Classes that implement the SortedList class
			if (type == System.Type.GetType("System.Collections.SortedList"))
			{
				contained = (bool) ((System.Collections.SortedList) d).ContainsValue(obj);
			}
			//Classes that implement the Hashtable class
			else if (type == System.Type.GetType("System.Collections.Hashtable"))
			{
				contained = (bool) ((System.Collections.Hashtable) d).ContainsValue(obj);
			}
			else 
			{
				//Reflection. Invoke "containsValue" method for proprietary classes
				try
				{
					System.Reflection.MethodInfo method = type.GetMethod("containsValue");
					contained = (bool) method.Invoke(d, new Object[] {obj});
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					throw e;
				}
				catch (System.Exception e)
				{
					throw e;
				}
			}

			return contained;
		}
		
		
		/// <summary>
		/// Determines whether the NameValueCollection contains a specific value.
		/// </summary>
		/// <param name="d">The dictionary to check for the value.</param>
		/// <param name="obj">The object to locate in the SortedList.</param>
		/// <returns>Returns true if the value is contained in the NameValueCollection, false otherwise.</returns>
		public static bool ContainsValue(System.Collections.Specialized.NameValueCollection d, System.Object obj)
		{
			bool contained = false;
			System.Type type = d.GetType();

			for (int i = 0; i < d.Count && !contained ; i++)
			{
				string [] values = d.GetValues(i);
				if (values != null) 
				{
					foreach (string val in values)
					{
						if (val.Equals(obj))
						{
							contained = true;
							break;
						}
					}
				}
			}
			return contained;
		}		

		/// <summary>
		/// Copies all the elements of d to target.
		/// </summary>
		/// <param name="target">Collection where d elements will be copied.</param>
		/// <param name="d">Elements to copy to the target collection.</param>
		public static void PutAll(System.Collections.IDictionary target, System.Collections.IDictionary d)
		{
			if(d != null)
			{
					System.Collections.ArrayList keys = new System.Collections.ArrayList(d.Keys);
				System.Collections.ArrayList values = new System.Collections.ArrayList(d.Values);

				for (int i=0; i < keys.Count; i++)
					target[keys[i]] = values[i];
			}
		}
		
		/// <summary>
		/// Returns a portion of the list whose keys are less than the limit object parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The end element of the portion to extract.</param>
		/// <returns>The portion of the collection whose elements are less than the limit object parameter.</returns>
		public static System.Collections.SortedList HeadMap(System.Collections.SortedList l, System.Object limit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			for (int i=0; i < l.Count; i++)
			{
				if (comparer.Compare(l.GetKey(i), limit) >= 0)
					break;

				newList.Add(l.GetKey(i), l[l.GetKey(i)]);
			}

			return newList;
		}

		/// <summary>
		/// Returns a portion of the list whose keys are greater that the lowerLimit parameter less than the upperLimit parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The start element of the portion to extract.</param>
		/// <param name="limit">The end element of the portion to extract.</param>
		/// <returns>The portion of the collection.</returns>
		public static System.Collections.SortedList SubMap(System.Collections.SortedList list, System.Object lowerLimit, System.Object upperLimit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			if (list != null)
			{
				if ((list.Count > 0)&&(!(lowerLimit.Equals(upperLimit))))
				{
					int index = 0;
					while (comparer.Compare(list.GetKey(index), lowerLimit) < 0)
						index++;

					for (; index < list.Count; index++)
					{
						if (comparer.Compare(list.GetKey(index), upperLimit) >= 0)
							break;

						newList.Add(list.GetKey(index), list[list.GetKey(index)]);
					}
				}
			}

			return newList;
		}

		/// <summary>
		/// Returns a portion of the list whose keys are greater than the limit object parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The start element of the portion to extract.</param>
		/// <returns>The portion of the collection whose elements are greater than the limit object parameter.</returns>
		public static System.Collections.SortedList TailMap(System.Collections.SortedList list, System.Object limit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			if (list != null)
			{
				if (list.Count > 0)
				{
					int index = 0;
					while (comparer.Compare(list.GetKey(index), limit) < 0)
						index++;

					for (; index < list.Count; index++)
						newList.Add(list.GetKey(index), list[list.GetKey(index)]);
				}
			}

			return newList;
		}
	}


	/*******************************/
	/// <summary>
	/// Summary description for EqualsSupport.
	/// </summary>
	public class EqualsSupport
	{
		/// <summary>
		/// Determines whether two Collections instances are equal.
		/// </summary>
		/// <param name="source">The first Collections to compare. </param>
		/// <param name="target">The second Collections to compare. </param>
		/// <returns>Return true if the first collection is the same instance as the second collection, otherwise returns false.</returns>
		public static bool Equals(System.Collections.ICollection source, System.Collections.ICollection target )
		{
			bool equal = true;

			System.Collections.ArrayList sourceInterfaces = new System.Collections.ArrayList(source.GetType().GetInterfaces());
			System.Collections.ArrayList targetInterfaces = new System.Collections.ArrayList(target.GetType().GetInterfaces());

			if (sourceInterfaces.Contains(System.Type.GetType("SupportClass+SetSupport")) && 
				!targetInterfaces.Contains(System.Type.GetType("SupportClass+SetSupport")))
				equal = false;
			else if (targetInterfaces.Contains(System.Type.GetType("SupportClass+SetSupport")) && 
				!sourceInterfaces.Contains(System.Type.GetType("SupportClass+SetSupport")))
				equal = false;

			if (equal)
			{
				System.Collections.IEnumerator sourceEnumerator = ReverseStack(source);
				System.Collections.IEnumerator targetEnumerator = ReverseStack(target);

				if (source.Count != target.Count)
					equal = false;

				while(sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
					if (!sourceEnumerator.Current.Equals(targetEnumerator.Current))
						equal = false;
			}

			return equal;
		}

		/// <summary>
		/// Determines if a Collection is equal to the Object.
		/// </summary>
		/// <param name="source">The first Collections to compare.</param>
		/// <param name="target">The Object to compare.</param>
		/// <returns>Return true if the first collection contains the same values of the second Object, otherwise returns false.</returns>
		public static bool Equals(System.Collections.ICollection source, System.Object target)
		{
			return (target is System.Collections.ICollection) ? Equals(source, (System.Collections.ICollection) target) : false;
		}

		/// <summary>
		/// Determines if a IDictionaryEnumerator is equal to the Object.
		/// </summary>
		/// <param name="source">The first IDictionaryEnumerator to compare.</param>
		/// <param name="target">The second Object to compare.</param>
		/// <returns>Return true if the first IDictionaryEnumerator contains the same values of the second Object, otherwise returns false.</returns>
		public static bool Equals(System.Collections.IDictionaryEnumerator source, System.Object target)
		{
			return (target is System.Collections.IDictionaryEnumerator) ? Equals(source, (System.Collections.IDictionaryEnumerator) target) : false;
		}

		/// <summary>
		/// Determines if a IDictionary is equal to the Object.
		/// </summary>
		/// <param name="source">The first IDictionary to compare.</param>
		/// <param name="target">The second Object to compare.</param>
		/// <returns>Return true if the first IDictionary contains the same values of the second Object, otherwise returns false.</returns>
		public static bool Equals(System.Collections.IDictionary source, System.Object target)
		{
			return (target is System.Collections.IDictionary)? Equals(source, (System.Collections.IDictionary) target) : false;
		}

		/// <summary>
		/// Determines whether two IDictionaryEnumerator instances are equals.
		/// </summary>
		/// <param name="source">The first IDictionaryEnumerator to compare.</param>
		/// <param name="target">The second IDictionaryEnumerator to compare.</param>
		/// <returns>Return true if the first IDictionaryEnumerator contains the same values as the second IDictionaryEnumerator, otherwise return false.</returns>
		public static bool Equals(System.Collections.IDictionaryEnumerator source, System.Collections.IDictionaryEnumerator target )
		{
			while (source.MoveNext() && target.MoveNext())
				if (source.Key.Equals(target.Key))
					if (source.Value.Equals(target.Value))
						return true;
			return false;
		}

		/// <summary>
		/// Reverses the Stack Collection received.
		/// </summary>
		/// <param name="collection">The collection to reverse.</param>
		/// <returns>The collection received in reverse order if it was a System.Collections.Stack type, otherwise it does 
		/// nothing to the collection.</returns>
		public static System.Collections.IEnumerator ReverseStack(System.Collections.ICollection collection)
		{
			if ((collection.GetType()) == (typeof(System.Collections.Stack)))
			{
				System.Collections.ArrayList collectionStack = new System.Collections.ArrayList(collection);
				collectionStack.Reverse();
				return collectionStack.GetEnumerator();
			}
			else
				return collection.GetEnumerator();
		}

		/// <summary>
		/// Determines whether two IDictionary instances are equal.
		/// </summary>
		/// <param name="source">The first Collection to compare.</param>
		/// <param name="target">The second Collection to compare.</param>
		/// <returns>Return true if the first collection is the same instance as the second collection, otherwise return false.</returns>
		public static bool Equals(System.Collections.IDictionary source, System.Collections.IDictionary target)
		{
			System.Collections.Hashtable targetAux = new System.Collections.Hashtable(target);

			if (source.Count == targetAux.Count)
			{
				System.Collections.IEnumerator sourceEnum = source.Keys.GetEnumerator();
				while (sourceEnum.MoveNext())
					if (targetAux.Contains(sourceEnum.Current))
						targetAux.Remove(sourceEnum.Current);
					else
						return false;
			}
			else
				return false;
			if (targetAux.Count == 0)
				return true;
			else
				return false;
		}
	}


	/*******************************/
	/// <summary>
	/// Retrieves a FileInfo array with of all selected files in the dialog box.
	/// </summary>
	/// <param name="files">String array with the selected file names in the dialog box.</param>
	/// <returns>A FileInfo array with the selected files.</returns>
	public static System.IO.FileInfo[] GetSelectedFiles(string[] files)
	{
		System.IO.FileInfo[] filesSelected = new System.IO.FileInfo[files.Length];
		for (int index = 0; index < files.Length; index++)
			filesSelected[index] = new System.IO.FileInfo(files[index]);
		return filesSelected;
	}


	/*******************************/
	/// <summary>
	/// Gets the selected items in a ListBox instance.
	/// </summary>
	/// <param name="listbox">A listbox to get its selected items.</param>
	/// <returns>An object array with the selected items.</returns>
	public static System.Object[] GetSelectedItems(System.Windows.Forms.ListBox listbox)
	{
		System.Object[] selectedvalues = new System.Object[listbox.SelectedItems.Count];
		listbox.SelectedItems.CopyTo(selectedvalues, 0);
		return selectedvalues;
	}


	/*******************************/
	/// <summary>
	/// This class contains support methods to work with GraphicsPath and Ellipses.
	/// </summary>
	public class Ellipse2DSupport
	{
		/// <summary>
		/// Creates a object and adds an ellipse to it.
		/// </summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
		/// <returns>Returns a GraphicsPath object containing an ellipse.</returns>
		public static System.Drawing.Drawing2D.GraphicsPath CreateEllipsePath(float x, float y, float width, float height)
		{
			System.Drawing.Drawing2D.GraphicsPath ellipsePath = new System.Drawing.Drawing2D.GraphicsPath();
			ellipsePath.AddEllipse(x, y, width, height);
			return ellipsePath;
		}

		/// <summary>
		/// Resets the x-coordinate of the ellipse path contained in the specified GraphicsPath object.
		/// </summary>
		/// <param name="ellipsePath">The GraphicsPath object that will be set.</param>
		/// <param name="x">The new x-coordinate.</param>
		public static void SetX(System.Drawing.Drawing2D.GraphicsPath ellipsePath, float x)
		{
			System.Drawing.RectangleF rectangle = ellipsePath.GetBounds();
			rectangle.X = x;
			ellipsePath.Reset();
			ellipsePath.AddEllipse(rectangle);
		}

		/// <summary>
		/// Resets the y-coordinate of the ellipse path contained in the specified GraphicsPath object.
		/// </summary>
		/// <param name="ellipsePath">The GraphicsPath object that will be set.</param>
		/// <param name="y">The new y-coordinate.</param>
		public static void SetY(System.Drawing.Drawing2D.GraphicsPath ellipsePath, float y)
		{
			System.Drawing.RectangleF rectangle = ellipsePath.GetBounds();
			rectangle.Y = y;
			ellipsePath.Reset();
			ellipsePath.AddEllipse(rectangle);
		}

		/// <summary>
		/// Resets the width of the ellipse path contained in the specified GraphicsPath object.
		/// </summary>
		/// <param name="ellipsePath">The GraphicsPath object that will be set.</param>
		/// <param name="width">The new width.</param>
		public static void SetWidth(System.Drawing.Drawing2D.GraphicsPath ellipsePath, float width)
		{
			System.Drawing.RectangleF rectangle = ellipsePath.GetBounds();
			rectangle.Width = width;
			ellipsePath.Reset();
			ellipsePath.AddEllipse(rectangle);
		}

		/// <summary>
		/// Resets the height of the ellipse path contained in the specified GraphicsPath object.
		/// </summary>
		/// <param name="ellipsePath">The GraphicsPath object that will be set.</param>
		/// <param name="height">The new height.</param>
		public static void SetHeight(System.Drawing.Drawing2D.GraphicsPath ellipsePath, float height)
		{
			System.Drawing.RectangleF rectangle = ellipsePath.GetBounds();
			rectangle.Height = height;
			ellipsePath.Reset();
			ellipsePath.AddEllipse(rectangle);
		}
	}


	/*******************************/
	/// <summary>
	/// This class contains methods that supports list management operations in 
	/// ListBox.ObjectCollection instances.
	/// </summary>
	public class ListBoxObjectCollectionSupport
	{
		/// <summary>
		/// Gets the index of the first occurence of the specified element after the specified index.
		/// </summary>
		/// <param name="items">The collection of objects.</param>
		/// <param name="element">The element to search.</param>
		/// <param name="index">First index of the searching.</param>
		/// <returns>The index where the element was found or -1 if it wasn't.</returns>
		public static int IndexOf(System.Windows.Forms.ListBox.ObjectCollection items, System.Object element, int index)
		{
			for (int itemIndex = index; itemIndex < items.Count; itemIndex++)
				if (items[itemIndex] == element)
					return itemIndex;
			return -1;
		}

		/// <summary>
		/// Returns the last element of the collection of objects specified.
		/// </summary>
		/// <param name="items">The collection of objects.</param>
		/// <returns>The last item of the collection.</returns>
		public static System.Object LastElement(System.Windows.Forms.ListBox.ObjectCollection items)
		{
			if (items.Count == 0)
				throw new System.ArgumentOutOfRangeException();
			return items[items.Count - 1];
		}

		/// <summary>
		/// Gets the last index before the specified index of the specified element.
		/// </summary>
		/// <param name="items">The collection of objects.</param>
		/// <param name="element">The element to search.</param>
		/// <param name="index">Last index of the search.</param>
		/// <returns>Last index before the specified index of the element.</returns>
		public static int LastIndexOf(System.Windows.Forms.ListBox.ObjectCollection items, System.Object element, int index)
		{
			for (int itemIndex = index; itemIndex >= 0; itemIndex--)
				if (items[itemIndex] == element)
					return itemIndex;
			return -1;
		}

		/// <summary>
		/// Gets the index of the last occurrence of the specified element.
		/// </summary>
		/// <param name="items">The collection of elements.</param>
		/// <param name="element">The element to search.</param>
		/// <returns>Index of the last ocurrence of the element.</returns>
		public static int LastIndexOf(System.Windows.Forms.ListBox.ObjectCollection items, System.Object element)
		{
			for (int itemIndex = items.Count; itemIndex >= 0; itemIndex--)
				if (items[itemIndex] == element)
					return itemIndex;
			return -1;
		}

		/// <summary>
		/// Deletes specified range of elements in the specified collection of items.
		/// </summary>
		/// <param name="items">The collection of objects.</param>
		/// <param name="fromIndex">Minimum index of the range.</param>
		/// <param name="toIndex">Maximum index of the range.</param>
		public static void RemoveRange(System.Windows.Forms.ListBox.ObjectCollection items, int fromIndex, int toIndex)
		{
			for (int itemIndex = toIndex; itemIndex >= fromIndex; itemIndex--)
			{
				if (itemIndex >= items.Count)
					throw new System.IndexOutOfRangeException(itemIndex + " >= " + items.Count);
				else if (itemIndex < 0)
					throw new System.IndexOutOfRangeException("Array index out of range: " + itemIndex);
				else
					items.RemoveAt(itemIndex);
			}
		}

		/// <summary>
		/// Gets an array representation of the specified collection of objects.
		/// </summary>
		/// <param name="items">The collection of objects.</param>
		/// <returns>An array with all the elements in the collection.</returns>
		public static System.Object[] ToArray(System.Windows.Forms.ListBox.ObjectCollection items)
		{
			System.Object[] result = new System.Object[items.Count];
			items.CopyTo(result, 0);
			return result;
		}
	}


	/*******************************/
	/// <summary>
	/// Inserts a row in the DataTable.
	/// </summary>
	/// <param name="table">The DataTable to insert the row into.</param>
	/// <param name="index">The row index of the row to be inserted.</param>
	/// <param name="vector">Data of the row being added.</param>
	public static void InsertRow(System.Data.DataTable table, int index, System.Collections.ArrayList vector)
	{
		System.Data.DataRow row = ((System.Data.DataTable)table).NewRow();
		row.ItemArray = ((System.Collections.ArrayList)vector).ToArray();
		table.Rows.InsertAt(row, index);
	}

	/// <summary>
	/// Inserts a row in the DataTable.
	/// </summary>
	/// <param name="table">The DataTable to insert the row into.</param>
	/// <param name="index">The row index of the row to be inserted.</param>
	/// <param name="vector">Data of the row being added.</param>
	public static void InsertRow(System.Data.DataTable table, int index, System.Object[] vector)
	{
		System.Data.DataRow row = ((System.Data.DataTable)table).NewRow();
		row.ItemArray = vector;
		table.Rows.InsertAt(row, index);
	}


	///*******************************/
	///// <summary>
	///// This class manages different operations with a DataGrid objects.
	///// </summary>
	//public class DataGridSupport
	//{
	//	/// <summary>
	//	/// Creates a new DataGrid instance and sets its data and columns name.
	//	/// </summary>
	//	/// <param name="data">A dimensional array with data.</param>
	//	/// <param name="columnNames">An array with columns name.</param>
	//	/// <returns>A DataGrid instance initialized with the values suplied.</returns>
	//	public static System.Windows.Forms.DataGrid CreateDataGrid(System.Object[][] data, System.Object[] columnNames)
	//	{	
	//		System.Windows.Forms.DataGrid grid = new System.Windows.Forms.DataGrid();
	//		System.Data.DataTable tempTable  = new System.Data.DataTable();
	//		foreach (System.Object columnName in columnNames)
	//			tempTable.Columns.Add((string)columnName);
	//		for (int index = 0; index < data.GetLongLength(0); index++)
	//			tempTable.Rows.Add(data[index]);
	//		grid.DataSource = tempTable;
	//		return grid;
	//	}

	//	/// <summary>
	//	/// Creates a new DataGrid instances and sets its data and columns' name.
	//	/// </summary>
	//	/// <param name="data">An ArrayList containing the data.</param>
	//	/// <param name="columnNames">An array with columns name.</param>
	//	/// <returns>A DataGrid instance initialized with the values suplied.</returns>
	//	public static System.Windows.Forms.DataGrid CreateDataGrid(System.Collections.ArrayList data, System.Collections.ArrayList columnNames)
	//	{	
	//		System.Windows.Forms.DataGrid grid = new System.Windows.Forms.DataGrid();
	//		System.Data.DataTable tempTable  = new System.Data.DataTable();
	//		foreach (System.Object columnName in columnNames)
	//			tempTable.Columns.Add((string)columnName);
	//		for (int index = 0; index < data.Count; index++) 
	//			tempTable.Rows.Add(((System.Collections.ArrayList)data[index]).ToArray());
	//		grid.DataSource = tempTable;
	//		return grid;
	//	}

	//	/// <summary>
	//	/// Creates a new DataGrid instances with the specified number of empty rows and columns.
	//	/// </summary>
	//	/// <param name="rows">Amount of rows to be added to the grid.</param>
	//	/// <param name="cols">Amount of columns to be added to the grid.</param>
	//	/// <returns>A DataGrid instance initialized with the values suplied.</returns>
	//	public static System.Windows.Forms.DataGrid CreateDataGrid(int rows, int columns)
	//	{	
	//		System.Windows.Forms.DataGrid grid = new System.Windows.Forms.DataGrid();
	//		System.Data.DataTable tempTable  = new System.Data.DataTable();
	//		for (int indexColumns = 0; indexColumns < columns; indexColumns++)
	//			tempTable.Columns.Add(GetColumnLabel(indexColumns));
	//		System.Object[] emptyRow = new System.Object[columns];
	//		for (int indexRows = 0 ; indexRows < rows; indexRows++)
	//			tempTable.Rows.Add(emptyRow);
	//		grid.DataSource = tempTable;
	//		return grid;
	//	}

	//	/// <summary>
	//	/// Returns a label column name, based on ABC sequence.
	//	/// </summary>
	//	/// <param name="numberCol">Number of column to be converted to ABC format.</param>
	//	private static string GetColumnLabel(int numberCol)
	//	{
	//		string labelCol = "";
	//		int division = numberCol / 26; //A = 1, B = 2, ..., Z = 26
	//		int remainder = numberCol % 26;
	//		for (int index = 0; index < division; index++) 
	//			labelCol = labelCol + System.Convert.ToChar(65);
	//		labelCol = labelCol + System.Convert.ToChar(65 + remainder); //65 = 'A', 66 = 'B', ..., 90 = 'Z'
	//		return labelCol;
	//	}

	//	/// <summary>
	//	/// Sets the specified columns to the DataGrid instace.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action to.</param>
	//	/// <param name="columnArray">A array of DataGridColumnStyle that represent the columns to be 
	//	/// set to the DataGrid.</param>
	//	public static void SetColumns(System.Windows.Forms.DataGrid dataGrid, System.Windows.Forms.DataGridColumnStyle[] columnArray)
	//	{
	//		System.Windows.Forms.DataGridTableStyle tableStyle = new System.Windows.Forms.DataGridTableStyle();
	//		if (dataGrid.DataSource != null)
	//			tableStyle.MappingName = ((System.Data.DataTable)dataGrid.DataSource).TableName;
	//		if (columnArray == null)
	//			tableStyle.GridColumnStyles.Clear();
	//		else
	//			tableStyle.GridColumnStyles.AddRange(columnArray);
	//		dataGrid.TableStyles.Clear();
	//		dataGrid.TableStyles.Add(tableStyle);
	//	}

	//	/// <summary>
	//	/// Gets the columns of a DataGrid instance.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action to.</param>
	//	/// <returns>An array of DataGridTextBoxColumn values that represent the columns in the 
	//	/// DataGrid.</returns>
	//	public static System.Windows.Forms.DataGridTextBoxColumn[] GetColumns(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//		{
	//			System.Windows.Forms.DataGridTextBoxColumn[] columnArray = new System.Windows.Forms.DataGridTextBoxColumn[dataGrid.TableStyles[0].GridColumnStyles.Count];
	//			dataGrid.TableStyles[0].GridColumnStyles.CopyTo(columnArray, 0);
	//			return columnArray;
	//		}
	//		else
	//			return null;
	//	}

	//	/// <summary>
	//	/// Gets the index of the specified column in a DataGrid instance.
	//	/// </summary>
	//	/// <param name="dataGridColumn">A DataGridColumnStyle that represents the element to get 
	//	/// its index.</param>
	//	/// <returns>An integer value that represents the index of the column, or -1 whether it  
	//	/// was not found.</returns>
	//	public static int GetColumnIndex(System.Windows.Forms.DataGridColumnStyle dataGridColumn)
	//	{
	//		if (dataGridColumn.DataGridTableStyle != null)
	//			return dataGridColumn.DataGridTableStyle.GridColumnStyles.IndexOf(dataGridColumn);
	//		else
	//			return -1;
	//	}

	//	/// <summary>
	//	/// Adds the column specified to a DataGrid instance.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action to.</param>
	//	/// <param name="column">The column to be added.</param>
	//	/// <returns>The column that was added to the DataGrid.</returns>
	//	public static System.Windows.Forms.DataGridColumnStyle AddColumn(System.Windows.Forms.DataGrid dataGrid, System.Windows.Forms.DataGridColumnStyle column)
	//	{
	//		if (dataGrid.TableStyles != null)
	//		{
	//			dataGrid.TableStyles[0].GridColumnStyles.Add(column);
	//			return column;
	//		}
	//		else
	//			return null;
	//	}

	//	/// <summary>
	//	/// Creates a new column with the specified name and then adds it to a DataGrid instance.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action to.</param>
	//	/// <param name="mappingName">A string value that represents the name for the new column.</param>
	//	/// <returns>The new column created and added into the DataGrid.</returns>
	//	public static System.Windows.Forms.DataGridColumnStyle AddColumn(System.Windows.Forms.DataGrid dataGrid, string mappingName)
	//	{
	//		if (dataGrid.TableStyles != null)
	//		{
	//			System.Windows.Forms.DataGridTextBoxColumn newColumn = new System.Windows.Forms.DataGridTextBoxColumn();
	//			newColumn.MappingName = mappingName;
	//			dataGrid.TableStyles[0].GridColumnStyles.Add(newColumn);
	//			return newColumn;
	//		}
	//		else
	//			return null;
	//	}

	//	/// <summary>
	//	/// Clears the collection of the columns of the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	public static void ClearColumns(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].GridColumnStyles.Clear();
	//	}

	//	/// <summary>
	//	/// Gets the specified column.
	//	/// It throws an ArgumentException if the index is invalid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="columnIndex">A integer value that represents the index of the column to return.</param>
	//	/// <returns>The column in the specified index.</returns>
	//	public static System.Windows.Forms.DataGridColumnStyle GetColumn(System.Windows.Forms.DataGrid dataGrid, int columnIndex)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			if ((dataGrid.TableStyles[0].GridColumnStyles.Count != 0) && 
	//				(columnIndex < dataGrid.TableStyles[0].GridColumnStyles.Count))
	//				return dataGrid.TableStyles[0].GridColumnStyles[columnIndex];
	//		throw new System.ArgumentException("Invalid displayIndex");
	//	}

	//	/// <summary>
	//	/// Gets a value indicating the number of columns in the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>An integer value indicating the total of columns in the DataGrid.</returns>
	//	public static int GetColumnCount(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].GridColumnStyles.Count;
	//		else
	//			return 0;
	//	}

	//	/// <summary>
	//	/// Gets a value that indicates whether the headers of the columns are visible.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True if the headers are visible, false otherwise.</returns>
	//	public static bool GetColumnHeadersVisible(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].ColumnHeadersVisible;
	//		else
	//			return dataGrid.ColumnHeadersVisible;
	//	}

	//	/// <summary>
	//	/// Sets a value to indicate the visibility of the headers of the columns in the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="headersVisible">A boolean value to indicate that the headers are visible: 
	//	/// true to show them, false otherwise.</param>
	//	public static void SetColumnHeadersVisible(System.Windows.Forms.DataGrid dataGrid, bool headersVisible)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].ColumnHeadersVisible = headersVisible;
	//		else
	//			dataGrid.ColumnHeadersVisible = headersVisible;
	//	}

	//	/// <summary>
	//	/// Gets the value of the current cell in the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A string value that represents the value of the current cell in the DataGrid object.</returns>
	//	public static string GetCurrentCellValue(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid != null)
	//			return dataGrid[dataGrid.CurrentCell.RowNumber, dataGrid.CurrentCell.ColumnNumber].ToString();
	//		else
	//			return null;
	//	}

	//	/// <summary>
	//	/// Sets a value to the current cell in the DataGrid object.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="newValue">A string value that represents the new value for the current cell.</param>
	//	public static void SetCurrentCellValue(System.Windows.Forms.DataGrid dataGrid, string newValue)
	//	{
	//		if (dataGrid != null)
	//			dataGrid[dataGrid.CurrentCell.RowNumber,dataGrid.CurrentCell.ColumnNumber] = newValue;
	//	}

	//	/// <summary>
	//	/// Gets the current column in the DataGrid object.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A DataGridColumnStyle value that represents the current column.</returns>
	//	public static System.Windows.Forms.DataGridColumnStyle GetCurrentColumn(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].GridColumnStyles[dataGrid.CurrentCell.ColumnNumber];
	//		else
	//			return null;
	//	}

	//	/// <summary>
	//	/// Sets the values of the column specified.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="column">An integer value that represents the index of the column to set.</param>
	//	public static void SetCurrentColumn(System.Windows.Forms.DataGrid dataGrid, int column)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.CurrentCell = new System.Windows.Forms.DataGridCell(dataGrid.CurrentCell.RowNumber, column);
	//	}

	//	/// <summary>
	//	/// Sets the values of the column specified.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="column">The column to be set.</param>
	//	public static void SetCurrentColumn(System.Windows.Forms.DataGrid dataGrid, System.Windows.Forms.DataGridColumnStyle column)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.CurrentCell = new System.Windows.Forms.DataGridCell(dataGrid.CurrentCell.RowNumber, column.DataGridTableStyle.GridColumnStyles.IndexOf(column));
	//	}

	//	/// <summary>
	//	/// Gets a value indicating the style of grid lines.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A DataGridLineStyle value that indicates the style of grid lines.</returns>
	//	public static System.Windows.Forms.DataGridLineStyle GetGridLineStyle(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].GridLineStyle;
	//		else
	//			return dataGrid.GridLineStyle;
	//	}

	//	/// <summary>
	//	/// Sets the style of the grid lines.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="gridLineStyle">A DataGridLineStyle value that represents the style to be set.</param>
	//	public static void SetGridLineStyle(System.Windows.Forms.DataGrid dataGrid, System.Windows.Forms.DataGridLineStyle gridLineStyle)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].GridLineStyle = gridLineStyle;
	//		else
	//			dataGrid.GridLineStyle = gridLineStyle;
	//	}

	//	/// <summary>
	//	/// Removes the specified column of the DataGrid object.
	//	/// It throws an ArgumentException if the specified index is invalid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="columnIndex">A integer value that represents the index of the column to remove.</param>
	//	public static void RemoveColumn(System.Windows.Forms.DataGrid dataGrid, int columnIndex)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			if ((dataGrid.TableStyles[0].GridColumnStyles.Count != 0) && 
	//				(columnIndex < dataGrid.TableStyles[0].GridColumnStyles.Count))
	//			{
	//				dataGrid.TableStyles[0].GridColumnStyles.RemoveAt(columnIndex);
	//				return;
	//			}
	//		throw new System.ArgumentException("Invalid displayIndex");
	//	}

	//	/// <summary>
	//	/// Clears the content of the cells in the selection.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	public static void ClearSelection(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.DataSource != null)
	//		{
	//			for (int index = 0; index < ((System.Data.DataTable)dataGrid.DataSource).Rows.Count; index++)
	//				if (dataGrid.IsSelected(index)) dataGrid.UnSelect(index);
	//		}
	//	}

	//	/// <summary>
	//	/// Gets a value that indicates whether the headers of the rows are visible.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True if the headers are visible, false otherwise.</returns>
	//	public static bool GetRowHeaders(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].RowHeadersVisible;
	//		else
	//			return dataGrid.RowHeadersVisible;
	//	}

	//	/// <summary>
	//	/// Sets a value to indicate the visibility of the headers of the rows in the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="visible">A boolean value to indicate that the headers are visible: 
	//	/// true to show them, false otherwise.</param>
	//	public static void SetRowHeaders(System.Windows.Forms.DataGrid dataGrid, bool visible)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].RowHeadersVisible = visible;
	//		else
	//			dataGrid.RowHeadersVisible = visible;
	//	}

	//	/// <summary>
	//	/// Gets an integer value that indicates the number of rows in the DataGrid.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>An integer value that indicates the number of rows in the DataGrid object.</returns>
	//	public static int GetRowCount(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.DataSource != null)
	//			return ((System.Data.DataTable)dataGrid.DataSource).Rows.Count;
	//		else
	//			return 0;
	//	}

	//	/// <summary>
	//	/// Gets a value indication whether the cells are visible.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True is the cells are visible, false otherwise.</returns>
	//	public static bool CellVisible(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		System.Drawing.Rectangle currentCell = dataGrid.GetCurrentCellBounds();
	//		if ((currentCell.X + currentCell.Width > dataGrid.DisplayRectangle.Width) || 
	//			(currentCell.Y + currentCell.Height > dataGrid.DisplayRectangle.Height) || 
	//			(currentCell.Y < 0))
	//			return false;
	//		else
	//			return true;
	//	}

	//	/// <summary>
	//	/// Gets the height of the current row.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>An integer value that represents the height of the current row.</returns>
	//	public static int GetCurrentRowHeight(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		return dataGrid.GetCellBounds(dataGrid.CurrentCell.RowNumber, dataGrid.CurrentCell.ColumnNumber).Height;
	//	}

	//	/// <summary>
	//	/// Gets the color of the background of the current selection.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A Color value that represents the background color of the current selection.</returns>
	//	public static System.Drawing.Color GetSelectedBackColor(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].SelectionBackColor;
	//		else
	//			return dataGrid.SelectionBackColor;
	//	}

	//	/// <summary>
	//	/// Sets the color of the background of the current selection.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="backColor">The Color value to be set.</param>
	//	public static void SetSelectedBackColor(System.Windows.Forms.DataGrid dataGrid, System.Drawing.Color backColor)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].SelectionBackColor = backColor;
	//		else
	//			dataGrid.SelectionBackColor = backColor;
	//	}

	//	/// <summary>
	//	/// Gets the color of the foreground of the current selection.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A Color value that represents the foreground color of the selection.</returns>
	//	public static System.Drawing.Color GetSelectedForeColor(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].SelectionForeColor;				
	//		else
	//			return dataGrid.SelectionForeColor;
	//	}

	//	/// <summary>
	//	/// Sets the color of the foreground of the current selection.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="foreColor">The Color to be set.</param>
	//	public static void SetSelectedForeColor(System.Windows.Forms.DataGrid dataGrid, System.Drawing.Color foreColor)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].SelectionForeColor = foreColor;
	//		else
	//			dataGrid.SelectionForeColor = foreColor;
	//	}

	//	/// <summary>
	//	/// Gets the font of the headers of the DataGrid object.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>A Font object that represents the font of the headers of the DataGrid object.</returns>
	//	public static System.Drawing.Font GetHeaderFont(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			return dataGrid.TableStyles[0].HeaderFont;
	//		else
	//			return dataGrid.HeaderFont;
	//	}

	//	/// <summary>
	//	/// Sets the font for the headers of the DataGrid object.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="font">The Font to be set.</param>
	//	public static void SetHeaderFont(System.Windows.Forms.DataGrid dataGrid, System.Drawing.Font font)
	//	{
	//		if (dataGrid.TableStyles != null)
	//			dataGrid.TableStyles[0].HeaderFont = font;
	//		else
	//			dataGrid.HeaderFont = font;
	//	}

	//	/// <summary>
	//	/// Indicates whether the new rows can be added using the AddNew method.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True it is allowed, false otherwise.</returns>
	//	public static bool GetAllowNew(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.DataSource != null)
	//			return ((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowNew;
	//		else
	//			return false;
	//	}

	//	/// <summary>
	//	/// Sets the AllowNew property to indicate that new rows can be added using the AddNew method.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="allowNew">The new value for the property.</param>
	//	public static void SetAllowNew(System.Windows.Forms.DataGrid dataGrid, bool allowNew)
	//	{
	//		if (dataGrid.DataSource != null)
	//			((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowNew = allowNew;
	//	}

	//	/// <summary>
	//	/// Gets a value indicating whether deletes are allowed.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True if deletes are allowed, false otherwise.</returns>
	//	public static bool GetAllowDelete(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.DataSource != null)
	//			return ((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowDelete;
	//		else
	//			return false;
	//	}

	//	/// <summary>
	//	/// Sets the AllowDelete property to indicate whether deletes are allowed.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="allowDelete">The value to be set to the property.</param>
	//	public static void SetAllowDelete(System.Windows.Forms.DataGrid dataGrid, bool allowDelete)
	//	{
	//		if (dataGrid.DataSource != null)
	//			((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowDelete = allowDelete;
	//	}

	//	/// <summary>
	//	/// Gets a value indicating whether edition is allowed.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <returns>True if the edition is allowed, false otherwise.</returns>
	//	public static bool GetAllowEdit(System.Windows.Forms.DataGrid dataGrid)
	//	{
	//		if (dataGrid.DataSource != null)
	//			return ((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowEdit;
	//		else
	//			return false;
	//	}

	//	/// <summary>
	//	/// Sets the AllowEdit property to indicate whether edition is allowed.
	//	/// </summary>
	//	/// <param name="dataGrid">A DataGrid object to perform the action.</param>
	//	/// <param name="allowEdit">The value to be set to the property.</param>
	//	public static void SetAllowEdit(System.Windows.Forms.DataGrid dataGrid, bool allowEdit)
	//	{
	//		if (dataGrid.DataSource != null)
	//			((System.Data.DataTable)dataGrid.DataSource).DefaultView.AllowEdit = allowEdit;
	//	}
	//}


	/*******************************/
	/// <summary>
	/// Support methods for working with Image Filters
	/// </summary>
	public class ImageSupport
	{
		/// <summary>
		/// Filters an Image depending on the filter received
		/// </summary>
		/// <param name="image">The image to be filtered</param>
		/// <param name="filter">The object containing the filter</param>
		/// <returns>The filtered Image</returns>
		public static System.Drawing.Image FilterImage(System.Drawing.Image image, System.Object filter)
		{
			return (System.Drawing.Image) FilterImage((System.Drawing.Bitmap) image, filter);
		}

		/// <summary>
		/// Filters an Image depending on the filter received
		/// </summary>
		/// <param name="bitmap">The image to be filtered</param>
		/// <param name="filter">The object containing the filter</param>
		/// <returns>The filtered Image</returns>
		public static System.Drawing.Bitmap FilterImage(System.Drawing.Bitmap bitmap, System.Object filter)
		{
			if (filter is System.Drawing.Rectangle)
			{
				System.Drawing.Rectangle rectangleFilter = (System.Drawing.Rectangle) filter;
				if (rectangleFilter.Width > bitmap.Width)
					rectangleFilter.Width = bitmap.Width;
				if (rectangleFilter.Height > bitmap.Height)
					rectangleFilter.Height = bitmap.Height;
				return bitmap.Clone(rectangleFilter, bitmap.PixelFormat);
			}
			else if(filter is System.Drawing.Size)
			{
				return new System.Drawing.Bitmap(bitmap, (System.Drawing.Size) filter);
			}
			else
				return (System.Drawing.Bitmap) bitmap.Clone();
		}

		/// <summary>
		/// Changes the dimensions of resizing filters.
		/// </summary>
		/// <param name="filter">The object containing the filter.</param>
		/// <param name="width">The width to be assigned to the filter</param>
		/// <param name="height">The height to be assigned to the filter</param>
		/// <returns></returns>
		public static System.Object ChangeFilterDimensions(ref System.Object filter, int width, int height)
		{
			if (filter is System.Drawing.Rectangle)
			{
				System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle) filter;
				rectangle.Size = new System.Drawing.Size(width, height);
				filter = (System.Object) rectangle;
			}
			else if (filter is System.Drawing.Size)
			{
				filter = new System.Drawing.Size(width, height); 
			}
			return filter;
		}
	}
	/*******************************/
	/// <summary>
	/// This method obtains the codes of the countries defined in ISO 639-1.
	/// </summary>
	/// <returns>Returns an array that contain the languages codes.</returns>
	public static string[] GetLanguages()
	{
		System.Collections.ArrayList Languages = new System.Collections.ArrayList();
		System.Globalization.CultureInfo[] Cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.NeutralCultures);
		string[] ArrayLanguages;

		for (int i = 0; i < Cultures.Length; i++)
		{
			string Language = Cultures[i].TwoLetterISOLanguageName;

			if (!Languages.Contains(Language))
				Languages.Add(Language);
		}

		ArrayLanguages = new string[Languages.Count];
		ArrayLanguages = (string[])Languages.ToArray(Type.GetType("string"));
		return ArrayLanguages;
	}


	/*******************************/
	/// <summary>
	/// Returns the two-letter regions/countries code for all the specific cultures installed in the Windows system.
	/// </summary>
	/// <returns>Returns an array of string values that contains the regions/countries codes.</returns>
	public static string[] GetRegionsCodes()
	{
		System.Collections.ArrayList Regions = new System.Collections.ArrayList();
		System.Globalization.CultureInfo[] Cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);
		foreach (System.Globalization.CultureInfo Culture in Cultures)
		{
			System.Globalization.RegionInfo Region = new System.Globalization.RegionInfo(Culture.LCID);
			string Code = Region.TwoLetterISORegionName;
			if (!(Regions.Contains(Code)))
				Regions.Add(Code);
		}
		Regions.Sort();
		string[] ArrayRegions = new string[Regions.Count];
		Regions.CopyTo(ArrayRegions);
		return ArrayRegions;
	}


	///*******************************/
	///// <summary>
	///// Gets the number of the row that is being edited.
	///// </summary>
	///// <param name="DataGridTable">The current datagrid being edited.</param>
	///// <returns>The number of the row currently being edited or -1 if no row is being edited.</returns>
	//public static int GetEditingRow(System.Windows.Forms.DataGrid DataGridTable)
	//{
	//	if (((System.Data.DataTable) DataGridTable.DataSource).DefaultView[DataGridTable.CurrentCell.RowNumber].IsEdit)
	//		return DataGridTable.CurrentCell.RowNumber;
	//	else
	//		return -1 ;
	//}


	///*******************************/
	///// <summary>
	///// Gets the number of the column that is being edited.
	///// </summary>
	///// <param name="DataGridTable">The current datagrid being edited.</param>
	///// <returns>The number of the column currently being edited or -1 if no column is being edited.</returns>
	//public static int GetEditingColumn(System.Windows.Forms.DataGrid DataGridTable)
	//{
	//	if (((System.Data.DataTable) DataGridTable.DataSource).DefaultView[DataGridTable.CurrentCell.RowNumber].IsEdit)
	//		return DataGridTable.CurrentCell.ColumnNumber;
	//	else
	//		return -1;
	//}


	/*******************************/
	/// <summary>
	/// Deserializes an object, or an entire graph of connected objects, and returns the object intance
	/// </summary>
	/// <param name="binaryReader">Reader instance used to read the object</param>
	/// <returns>The object instance</returns>
	public static System.Object Deserialize(System.IO.BinaryReader binaryReader)
	{
		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		return formatter.Deserialize(binaryReader.BaseStream);
	}

	/*******************************/
	/// <summary>
	/// Writes an object to the specified Stream
	/// </summary>
	/// <param name="stream">The target Stream</param>
	/// <param name="objectToSend">The object to be sent</param>
	public static void Serialize(System.IO.Stream stream, System.Object objectToSend)
	{
		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		formatter.Serialize(stream, objectToSend);
	}

	/// <summary>
	/// Writes an object to the specified BinaryWriter
	/// </summary>
	/// <param name="stream">The target BinaryWriter</param>
	/// <param name="objectToSend">The object to be sent</param>
	public static void Serialize(System.IO.BinaryWriter binaryWriter, System.Object objectToSend)
	{
		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		formatter.Serialize(binaryWriter.BaseStream, objectToSend);
	}

	/*******************************/
	/// <summary>
	/// Gets the current System properties.
	/// </summary>
	/// <returns>The current system properties.</returns>
	public static System.Collections.Specialized.NameValueCollection GetProperties()
	{
		System.Collections.Specialized.NameValueCollection properties = new System.Collections.Specialized.NameValueCollection();
		System.Collections.ArrayList keys = new System.Collections.ArrayList(System.Environment.GetEnvironmentVariables().Keys);
		System.Collections.ArrayList values = new System.Collections.ArrayList(System.Environment.GetEnvironmentVariables().Values);
		for (int count = 0; count < keys.Count; count++)
			properties.Add(keys[count].ToString(), values[count].ToString());
		return properties;
	}


	/*******************************/
	//Provides access to a static System.Random class instance
	static public System.Random Random = new System.Random();

	/*******************************/
	/// <summary>
	/// Provides support functions to create read-write random acces files and write functions
	/// </summary>
	public class RandomAccessFileSupport
	{
		/// <summary>
		/// Creates a new random acces stream with read-write or read rights
		/// </summary>
		/// <param name="fileName">A relative or absolute path for the file to open</param>
		/// <param name="mode">Mode to open the file in</param>
		/// <returns>The new System.IO.FileStream</returns>
		public static System.IO.FileStream CreateRandomAccessFile(string fileName, string mode) 
		{
			System.IO.FileStream newFile = null;

			if (mode.CompareTo("rw") == 0)
				newFile =  new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite); 
			else if (mode.CompareTo("r") == 0 )
				newFile =  new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read); 
			else
				throw new System.ArgumentException();

			return newFile;
		}

		/// <summary>
		/// Creates a new random acces stream with read-write or read rights
		/// </summary>
		/// <param name="fileName">File infomation for the file to open</param>
		/// <param name="mode">Mode to open the file in</param>
		/// <returns>The new System.IO.FileStream</returns>
		public static System.IO.FileStream CreateRandomAccessFile(System.IO.FileInfo fileName, string mode)
		{
			return CreateRandomAccessFile(fileName.FullName, mode);
		}

		/// <summary>
		/// Writes the data to the specified file stream
		/// </summary>
		/// <param name="data">Data to write</param>
		/// <param name="fileStream">File to write to</param>
		public static void WriteBytes(string data,System.IO.FileStream fileStream)
		{
			int index = 0;
			int length = data.Length;

			while(index < length)
				fileStream.WriteByte((byte)data[index++]);	
		}

		/// <summary>
		/// Writes the received string to the file stream
		/// </summary>
		/// <param name="data">String of information to write</param>
		/// <param name="fileStream">File to write to</param>
		public static void WriteChars(string data,System.IO.FileStream fileStream)
		{
			WriteBytes(data, fileStream);	
		}

		/// <summary>
		/// Writes the received data to the file stream
		/// </summary>
		/// <param name="sByteArray">Data to write</param>
		/// <param name="fileStream">File to write to</param>
		public static void WriteRandomFile(sbyte[] sByteArray,System.IO.FileStream fileStream)
		{
			byte[] byteArray = ToByteArray(sByteArray);
			fileStream.Write(byteArray, 0, byteArray.Length);
		}
	}

	/*******************************/
	///// <summary>
	///// This class contains static methods for management of style sheets.
	///// </summary>
	//public class StyleSheetSupport
	//{
	//	///<summary>
	//	///Adds a set of rules to the sheet. The rules are expected to be in valid CSS format.
	//	///</summary>
	//	///<param name="styleSheet">The StyleSheet to be modified.</param>
	//	///<param name="rule">The string value with valid CSS format.</param>
	//	public static void AddStyleSheetRule(MSHTML.IHTMLStyleSheet styleSheet, string rule)
	//	{
	//		string selectorName = null;
	//		string ruleString =  null;
	//		selectorName = rule.Substring(0, rule.IndexOf("{") - 1).Trim();
	//		ruleString = rule.Substring(rule.IndexOf("{")).Trim();
	//		styleSheet.addRule(selectorName, ruleString, 0);
	//	}
 
	//	///<summary>
	//	///Returns the rule from the rules of StyleSheet which selectorText is same to "selector" parameter.
	//	///</summary>
	//	///<param name="styleSheet">The StyleSheet to search the rule.</param>
	//	///<param name="name">The string value with selector of searched rule.</param>
	//	///<returns>The rule from the StyleSheet rules searched, or null value if rule is not found.</returns> 
	//	public static MSHTML.IHTMLStyleSheetRule GetStyleSheetRule(MSHTML.IHTMLStyleSheet styleSheet, string selector)
	//	{
	//		MSHTML.IHTMLStyleSheetRule tempRule = null;
	//		bool foundRule = false;
	//		int idxRule = 0;
	//		while (!foundRule && idxRule < styleSheet.rules.length)
	//		{
	//			tempRule = styleSheet.rules.item(idxRule);
	//			if (tempRule.selectorText.Equals(selector))
	//				foundRule = true;
	//			idxRule++;
	//		}
	//		return tempRule;
	//	}

	//	///<summary>
	//	///Remove a StyleSheet from the StyleSheet's collection of StyleSheet which is equals to "styleSheetSearched" parameter.
	//	///</summary>
	//	///<param name="styleSheetReceiver">The StyleSheet receiver to search the StyleSheet to delete.</param>
	//	///<param name="styleSheetSearched">The StyleSheet object to delete.</param>
	//	public static void RemoveStyleSheet(MSHTML.IHTMLStyleSheet styleSheetReceiver, MSHTML.IHTMLStyleSheet styleSheetSearched)
	//	{
	//		bool foundSheet = false;
	//		System.Object idxSheet = 0;
	//		while (!foundSheet && (int)idxSheet < styleSheetReceiver.imports.length)
	//		{
	//			if (styleSheetReceiver.imports.item(ref idxSheet).Equals(styleSheetSearched))
	//			{
	//				styleSheetReceiver.removeImport((int)idxSheet);
	//				foundSheet = true;
	//			}
	//			idxSheet = (int)idxSheet + 1;
	//		}
	//	}
	//}


	/*******************************/
	/// <summary>
	/// Method used to obtain the underlying type of an object to make the correct property call.
	/// The method is used when getting values from a property.
	/// </summary>
	/// <param name="tempObject">Object instance received</param>
	/// <param name="propertyName">Property name to obtain value</param>
	/// <returns>The return value of the property</returns>
	public static System.Object GetPropertyAsVirtual(System.Object tempObject, string propertyName)
	{
		System.Type type = tempObject.GetType();
		System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
		try
		{
			return propertyInfo.GetValue(tempObject, null);
		}
		catch(Exception e)
		{
			throw e.InnerException;
		}
	}


	/*******************************/
	/// <summary>
	/// Shows a dialog object.
	/// </summary>
	/// <param name="dialog">Dialog to be shown.</param>
	/// <param name="visible">Indicates if the dialog should be shown.</param>
	public static void ShowDialog(System.Windows.Forms.FileDialog dialog, bool visible)
	{
		if (visible)
			dialog.ShowDialog();
	}


	/*******************************/
	/// <summary>
	/// Creates a new positive random number 
	/// </summary>
	/// <param name="random">The last random obtained</param>
	/// <returns>Returns a new positive random number</returns>
	public static long NextLong(System.Random random)
	{
		long temporaryLong = random.Next();
		temporaryLong = (temporaryLong << 32)+ random.Next();
		if (random.Next(-1,1) < 0)
			return -temporaryLong;
		else
			return temporaryLong;
	}
	/*******************************/
	/// <summary>
	/// This method returns the bounding Rectangle of the raster image.
	/// </summary>
	/// <param name="bitmap">The image contained into the rectangle area.</param>
	public static System.Drawing.RectangleF RasterBoundsSupport(System.Drawing.Bitmap bitmap)
	{
		System.Drawing.GraphicsUnit unit = System.Drawing.GraphicsUnit.Pixel;
		return bitmap.GetBounds(ref unit);
	}


	/*******************************/
	/// <summary>
	/// Provides functionality to capture pixels from an image
	/// </summary>
	public class PixelCapturer
	{
		//Image to work with
		private System.Drawing.Bitmap bitmap;
		//Dimenstion of the image and frame to work in
		private int originX, originY, width, height, offset, scanWidth;
		//Structure to maintin the captured pixels
		private int[] intPixelBuffer;

		/// <summary>
		/// Initializes a new instance of the object
		/// </summary>
		/// <param name="image">Image to capture from</param>
		/// <param name="x">X-axis starting point for the upper left corner of the capture frame</param>
		/// <param name="y">Y-axis starting point for the upper left corner of the capture form</param>
		/// <param name="width">Picture width</param>
		/// <param name="height">Picture height</param>
		/// <param name="array">Array to use when storing the captured pixels</param>
		/// <param name="offset">Offset of the frame to capture</param>
		/// <param name="scanWidth">Width of the capture frame</param>
		public PixelCapturer ( System.Drawing.Image image, int x, int y, int width, int height, int[] array, int offset, int scanWidth )
		{
			this.bitmap = (System.Drawing.Bitmap)image;
			this.originX = x;
			this.originY = y;
			this.width = width;
			this.height = height;
			this.offset = offset;
			this.scanWidth = scanWidth;
			this.intPixelBuffer = array;
		}

		/// <summary>
		/// Initializes a new instance of the object
		/// </summary>
		/// <param name="image">Image to capture from</param>
		/// <param name="x">X-axis starting point for the upper left corner of the capture frame</param>
		/// <param name="y">Y-axis starting point for the upper left corner of the capture form</param>
		/// <param name="width">Picture width</param>
		/// <param name="height">Picture height</param>
		/// <param name="useRGB">Determines if RGB pixel encoding is used</param>
		public PixelCapturer(System.Drawing.Image image, int x, int y, int width, int height, bool useRGB)
			: this (image, x, y, width, height, new int[width*height], 0, width){}

		/// <summary>
		/// Captures the defined frames pixels
		/// </summary>
		/// <returns>True if the capture was successfull; false if an error occured</returns>
		public bool CapturePixels()
		{
			try
			{
				for (int coordinateY = this.originY; coordinateY < this.height; coordinateY++)
				{
					for (int coordinateX = this.originX; coordinateX < this.scanWidth; coordinateX++)
					{
						intPixelBuffer [ ( this.scanWidth * (coordinateY - this.originY) )  + ( coordinateX - this.originX ) + this.offset ] = 
							( bitmap.GetPixel( coordinateX, coordinateY ) ).ToArgb();
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Captures the defined frames pixels
		/// </summary>
		/// <param name="timeout">Timeout time, specified in seconds</param>
		/// <returns>True if the capture was successfull; false if an error occured</returns>
		public bool CapturePixels(long timeout)
		{
			try
			{
				this.CapturePixels();
				return true;
			}
			catch
			{
				return false;
			}
		} 

		/// <summary>
		/// Gets the width of the image to process
		/// </summary>
		public int Width
		{
			get
			{
				if ( this.width <= 0 )
					return -1;
				else
					return this.width;
			}
		}

		/// <summary>
		/// Gets the height of the image to process
		/// </summary>
		public int Height
		{
			get
			{
				if ( this.height <= 0 )
					return -1;
				else
					return this.height;
			}
		}

		/// <summary>
		/// Resizes the image to process
		/// </summary>
		/// <param name="width">New width</param>
		/// <param name="height">New height</param>
		public void Dimensions(int width, int height)
		{
			this.width  = width;
			this.height = height;
		}

		/// <summary>
		/// Gets the capture pixels
		/// </summary>
		public int[] Pixels
		{
			get
			{
				return this.intPixelBuffer;
			}
		}

		/// <summary>
		/// Get the color representation structure
		/// </summary>
		/// <returns>An instance of an ARGB color representation</returns>
		public System.Drawing.Color GetColor ()
		{
			return new System.Drawing.Color();
		}
	}

	/*******************************/
	/// <summary>
	/// Creates a System.Drawing.Rectangle with the giving parameters
	/// </summary>
	/// <param name="x1">The x coordinate</param>
	/// <param name="y1">The y coordinate</param>
	/// <param name="x2">The x final coordinate</param>
	/// <param name="y2">The y final coordinate</param>
	/// <returns>The new rectangle</returns>
	public static System.Drawing.Rectangle CreateRectangle(int x1, int y1, int x2, int y2)
	{
		System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(x1, y1, x2-x1, y2-y1);
		return rectangle;
	}

	/*******************************/
/// <summary>
/// Provides Color functionality for ColorModel
/// </summary>
public class ColorSupport
{
	/// <summary>
	/// Gets the Alpha color from the giving color
	/// </summary>
	/// <param name="color">The color to use</param>
	/// <returns>The Alpha color</returns>
	public static int GetAlphaFromColor(int color)
	{
		System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color);
		return newColor.A;
	}

	/// <summary>
	/// Gets the Red color from the giving color
	/// </summary>
	/// <param name="color">The color to use</param>
	/// <returns>The Red color</returns>
	public static int GetRedFromColor(int color)
	{
		System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color);
		return newColor.R;
	}

	/// <summary>
	/// Gets the Green color from the giving color
	/// </summary>
	/// <param name="color">The color to use</param>
	/// <returns>The Green color</returns>
	public static int GetGreenFromColor(int color)
	{
		System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color);
		return newColor.G;
	}

	/// <summary>
	/// Gets the Blue color from the giving color
	/// </summary>
	/// <param name="color">The color to use</param>
	/// <returns>The Blue color</returns>
	public static int GetBlueFromColor(int color)
	{
		System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color);
		return newColor.B;
	}

	/// <summary>
	/// Gets the RGB color from the giving color
	/// </summary>
	/// <param name="color">The color to use</param>
	/// <returns>The RGB value color</returns>
	public static int GetRGBFromColor(int color)
	{
		System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color);
		return newColor.ToArgb();
	}

	/// <summary>
	/// Returns the default bitsize of the pixel
	/// </summary>
	/// <returns>The default for .NET is 32</returns>
	public static int GetColorPixelSize()
	{
		return 32;
	}

	/// <summary>
	/// Returns the default bitsize of the pixel
	/// </summary>
	public static int PixelBits
	{
		get
		{
			return 32;
		}
	}

	/// <summary>
	/// Creates a new intance of System.Drawing.Color
	/// </summary>
	/// <returns>A new instance with de default values for System.Drawing.Color</returns>
	public static System.Drawing.Color GetRGBDefault()
	{
		return new System.Drawing.Color();
	}

	/// <summary>
	/// Creates the mask for Alpha color
	/// </summary>
	/// <returns>The int value of the mask for alpha color</returns>
	public static uint GetAlphaMask()
	{
		return (0xFF000000);
	}

	/// <summary>
	/// Creates the mask for Red color
	/// </summary>
	/// <returns>The int value of the mask for red color</returns>
	public static int GetRedMask()
	{
		return (0x00FF0000);
	}

	/// <summary>
	/// Creates the mask for Green color
	/// </summary>
	/// <returns>The int value of the mask for green color</returns>
	public static int GetGreenMask()
	{
		return (0x0000FF00);
	}

	/// <summary>
	/// Creates the mask for Blue color
	/// </summary>
	/// <returns>The int value of the mask for blue color</returns>
	public static int GetBlueMask()
	{
		return (0x000000FF);
	}
}
	/*******************************/
	/// <summary>
	/// Provides functionality to read and unread from a Stream.
	/// </summary>
	public class BackInputStream : System.IO.BinaryReader
	{
		private byte[] buffer;
		private int position = 1;

		/// <summary>
		/// Creates a BackInputStream with the specified stream and size for the buffer.
		/// </summary>
		/// <param name="streamReader">The stream to use.</param>
		/// <param name="size">The specific size of the buffer.</param>
		public BackInputStream(System.IO.Stream streamReader, System.Int32 size) : base(streamReader)
		{
			this.buffer = new byte[size];
			this.position = size;
		}

		/// <summary>
		/// Creates a BackInputStream with the specified stream.
		/// </summary>
		/// <param name="streamReader">The stream to use.</param>
		public BackInputStream(System.IO.Stream streamReader) : base(streamReader)
		{
			this.buffer = new byte[this.position];
		}

		/// <summary>
		/// Checks if this stream support mark and reset methods.
		/// </summary>
		/// <returns>Always false, these methods aren't supported.</returns>
		public bool MarkSupported()
		{	
			return false;
		}

		/// <summary>
		/// Reads the next bytes in the stream.
		/// </summary>
		/// <returns>The next byte readed</returns>
		public override int Read()
		{
			if (position >= 0 && position < buffer.Length)
				return (int) this.buffer[position++];
			return base.Read();
		}

		/// <summary>
		/// Reads the amount of bytes specified from the stream.
		/// </summary>
		/// <param name="array">The buffer to read data into.</param>
		/// <param name="index">The beginning point to read.</param>
		/// <param name="count">The number of characters to read.</param>
		/// <returns>The number of characters read into buffer.</returns>
		public virtual int Read(sbyte[] array, int index, int count)
		{
			int byteCount = 0;
			int readLimit = count + index;
			byte[] aux = ToByteArray(array);

			for (byteCount = 0; position < buffer.Length && index < readLimit; byteCount++)
				aux[index++] = buffer[position++];

			if (index < readLimit)
				byteCount += base.Read(aux, index, readLimit - index);

			for(int i = 0; i < aux.Length;i++)
				array[i] = (sbyte)aux[i];

			return byteCount;
		}

		/// <summary>
		/// Unreads a byte from the stream.
		/// </summary>
		/// <param name="element">The value to be unread.</param>
		public void UnRead(int element)
		{
			this.position--;
			if (position >= 0)
				this.buffer[this.position] = (byte) element;
		}

		/// <summary>
		/// Unreads an amount of bytes from the stream.
		/// </summary>
		/// <param name="array">The byte array to be unread.</param>
		/// <param name="index">The beginning index to unread.</param>
		/// <param name="count">The number of bytes to be unread.</param>
		public void UnRead(byte[] array, int index, int count)
		{
			this.Move(array, index, count);
		}

		/// <summary>
		/// Unreads an array of bytes from the stream.
		/// </summary>
		/// <param name="array">The byte array to be unread.</param>
		public void UnRead(byte[] array)
		{
			this.Move(array, 0, array.Length - 1);
		}

		/// <summary>
		/// Skips the specified number of bytes from the underlying stream.
		/// </summary>
		/// <param name="numberOfBytes">The number of bytes to be skipped.</param>
		/// <returns>The number of bytes actually skipped</returns>
		public long Skip(long numberOfBytes)
		{
			return this.BaseStream.Seek(numberOfBytes, System.IO.SeekOrigin.Current) - this.BaseStream.Position;
		}

		/// <summary>
		/// Moves data from the array to the buffer field.
		/// </summary>
		/// <param name="array">The array of bytes to be unread.</param>
		/// <param name="index">The beginning index to unread.</param>
		/// <param name="count">The amount of bytes to be unread.</param>
		private void Move(byte[] array, int index, int count)
		{
			for (int arrayPosition = index + count;  arrayPosition >= index; arrayPosition--)
				this.UnRead(array[arrayPosition]);
		}
	}


	/*******************************/
	/// <summary>
	/// This class provides support for creating Forms and sets its visual properties.
	/// </summary>
	public class WindowSupport
	{
		/// <summary>
		/// Creates a new Form with the passed parameter as its owner, no BorderStyle
		/// and which does not display itself in the taskbar.
		/// </summary>
		/// <param name="owner">The owner of the new Form.</param>
		/// <returns>The created Form.</returns>
		public static System.Windows.Forms.Form CreateWindow(System.Windows.Forms.Form owner)
		{
			System.Windows.Forms.Form window = new System.Windows.Forms.Form();
			window.Owner = owner;
			window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			window.ShowInTaskbar = false;
			return window;
		}

		/// <summary>
		/// Modifies an existing Form, setting its BorderStyle to None, its Owner to 
		/// the recieved parameter and its ShowInTaskbar attribute to false.
		/// </summary>
		/// <param name="window">The Form to be modified.</param>
		/// <param name="owner">The owner of the modified Form.</param>
		public static void SetWindow(System.Windows.Forms.Form window, System.Windows.Forms.Form owner)
		{
			window.Owner = owner;
			window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			window.ShowInTaskbar = false;
		}

		/// <summary>
		/// Creates a new Form with no BorderStyle and which does not display itself in the taskbar.
		/// </summary>
		/// <returns>The created Form.</returns>
		public static System.Windows.Forms.Form CreateWindow()
		{
			System.Windows.Forms.Form window = new System.Windows.Forms.Form();
			window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			window.ShowInTaskbar = false;
			return window;
		}

		/// <summary>
		/// Modifies an existing Form, setting its BorderStyle to None and its ShowInTaskbar attribute to false.
		/// </summary>
		/// <param name="window">The Form to be modified.</param>
		public static void SetWindow(System.Windows.Forms.Form window)
		{
			window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			window.ShowInTaskbar = false;
		}
	}


	/*******************************/
	/// <summary>
	/// Contains methods to construct ProgressBar objects
	/// </summary>
	public class ProgressBarSupport
	{ 
		/// <summary>
		/// Creates a ProgressBar with the specified range and an initial position of 0.
		/// </summary>
		/// <param name="maxRange">The maximum range value of the control. The control's range is from 0 to range.</param>
		/// <returns>The new ProgressBar</returns>
		public static System.Windows.Forms.ProgressBar CreateProgress(int maxRange)
		{
			System.Windows.Forms.ProgressBar tempProgress = new System.Windows.Forms.ProgressBar();
			tempProgress.Maximum = maxRange;
			return tempProgress;
		}

		/// <summary>
		/// Creates a ProgressBar with the specified range and initial position.
		/// </summary>
		/// <param name="minRange">The minimum range value of the ProgressBar.</param>
		/// <param name="maxRange">The maximum range value of the ProgressBar.</param>
		/// <returns>The new ProgressBar</returns>
		public static System.Windows.Forms.ProgressBar CreateProgress(int minRange, int maxRange)
		{
			System.Windows.Forms.ProgressBar tempProgress = CreateProgress(maxRange);
			tempProgress.Minimum = minRange;
			return tempProgress;
		}
	}

}
