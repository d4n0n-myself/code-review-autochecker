using System.Xml.Serialization;

namespace CodeReviewAutochecker
{
    public class IssueType
    {
        [XmlAttribute(AttributeName = "Id")] 
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "Category")]
        public string Category { get; set; }

        [XmlAttribute(AttributeName = "CategoryId")]
        public string CategoryId { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "Severity")]
        public string Severity { get; set; }

        [XmlAttribute(AttributeName = "WikiUrl")]
        public string WikiUrl { get; set; }
    }
}