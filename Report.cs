using System.Xml.Serialization;

namespace CodeReviewAutochecker
{
    public class Report
    {
        [XmlAttribute(AttributeName = "ToolsVersion")]
        public string ToolsVersion { get; set; }

        public Information Information { get; set; }

        public IssueType[] IssueTypes { get; set; }

        public Project[] Issues { get; set; }
    }
}