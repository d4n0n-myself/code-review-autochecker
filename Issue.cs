using System.Xml.Serialization;

namespace CodeReviewAutochecker
{
    public class Issue
    {
        [XmlAttribute(AttributeName = "TypeId")]
        public string TypeId { get; set; }

        [XmlAttribute(AttributeName = "File")] 
        public string File { get; set; }

        [XmlAttribute(AttributeName = "Offset")]
        public string Offset { get; set; }

        [XmlAttribute(AttributeName = "Message")]
        public string Message { get; set; }

        [XmlAttribute(AttributeName = "Line")] 
        public string Line { get; set; }
    }
}