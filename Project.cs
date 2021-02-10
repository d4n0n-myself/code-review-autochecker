using System.Xml.Serialization;

namespace CodeReviewAutochecker
{
    public class Project
    {
        [XmlElement(ElementName = "Issue")] public Issue[] Issue { get; set; }

        [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }
    }
}