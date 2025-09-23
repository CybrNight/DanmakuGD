using Godot;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using static Godot.HttpRequest;

namespace BulletMLLib;
/// <summary>
/// Class describing a BulletML Pattern
/// </summary>
public partial class BulletPattern {
	#region Members

	/// <summary>
	/// Root node of the BulletML XML tree
	/// </summary>
	public BulletMLNode RootNode { get; private set; }

    /// <summary>
    /// Returns the file name of the XML source file
    /// Only set by calling Parse
    /// </summary>
    /// <value>The filename.</value>
    public string Filename { get; private set; }

	/// <summary>
	/// the orientation of this bullet pattern: horizontal or veritcal
	/// this is read in from the xml
	/// </summary>
	/// <value>The orientation.</value>
	public EPatternType Orientation { get; private set; } = EPatternType.none;

	public IBulletManager BulletManager { get; set; }

	#endregion //Members

	#region Methods

	/// <summary>
	/// Initializes a new instance of the <see cref="BulletMLLib.BulletPattern"/> class.
	/// </summary>
	public BulletPattern() {
		//BulletManager = manager;
		RootNode = null;
	}

	/// <summary>
	/// convert a string to a pattern type enum
	/// </summary>
	/// <returns>The type to name.</returns>
	/// <param name="str">String.</param>
	private static EPatternType StringToPatternType(string str) {

        // Parse the input string to EPatternType
        var status = Enum.TryParse<EPatternType>(str, out var type);
		if (!status){
			throw new ArgumentException($"{str} is not a valid {nameof(EPatternType)}! Aborting parse!");
		}

		return type;
	}

	/// <summary>
	/// Parses new BulletML XML into this <see cref="BulletPattern"/>
	/// </summary>
	/// <param name="xmlFileName">Xml file name.</param>
	public void ParseXML(string xmlFileName) {
		//grab that filename 
		Filename = xmlFileName;

		try {
#if NETFX_CORE
					XmlReaderSettings settings = new XmlReaderSettings();
					settings.DtdProcessing = DtdProcessing.Ignore;
#else
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ValidationType = ValidationType.None;
			settings.DtdProcessing = DtdProcessing.Parse;
			settings.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);
#endif

			using(XmlReader reader = XmlReader.Create(xmlFileName, settings)) {
				//Open the file.
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(reader);
				ReadXmlDocument(xmlDoc);
			}
		} catch(Exception ex) {
			//an error ocurred reading in the tree
			throw new Exception("Error reading \"" + xmlFileName + "\"", ex);
		}

		//validate that the bullet nodes are all valid
		try {
			RootNode.ValidateNode();
		} catch(Exception ex) {
			//an error ocurred reading in the tree
			throw new Exception("Error reading \"" + xmlFileName + "\"", ex);
		}
	}

	private void ReadXmlDocument(XmlDocument xmlDoc) {
		XmlNode rootXmlNode = xmlDoc.DocumentElement;

		//make sure it is actually an xml node
		if(rootXmlNode.NodeType == XmlNodeType.Element) {
			//eat up the name of that xml node
			string strElementName = rootXmlNode.Name;
			if("bulletml" != strElementName) {
				//The first node HAS to be bulletml
				throw new Exception("Error reading \"" + Filename + "\": XML root node needs to be \"bulletml\", found \"" + strElementName + "\" instead");
			}

			//Create the root node of the bulletml tree
			RootNode = new BulletMLNode(ENodeName.bulletml, BulletManager);

			//Read in the whole bulletml tree
			RootNode.Parse(rootXmlNode, null, BulletManager);

			//Find what kind of pattern this is: horizontal or vertical
			XmlNamedNodeMap mapAttributes = rootXmlNode.Attributes;
			for(int i = 0; i < mapAttributes.Count; i++) {
				//will only have the name attribute
				string strName = mapAttributes.Item(i).Name;
				string strValue = mapAttributes.Item(i).Value;
				if("type" == strName) {
					//if  this is a top level node, "type" will be veritcal or horizontal
					Orientation = StringToPatternType(strValue);
				}
			}
		}
	}

#if !NETFX_CORE
	/// <summary>
	/// delegate method that gets called when a validation error occurs
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="args">Arguments.</param>
	public static void MyValidationEventHandler(object sender, ValidationEventArgs args) {
		throw new XmlSchemaException("Error validating bulletml document: " + args.Message,
									 args.Exception,
									 args.Exception.LineNumber,
									 args.Exception.LinePosition);
	}
#endif

	#endregion //Methods
}
