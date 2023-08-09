using AntDesign;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	public class AddAnnouncement
	{
		public Guid UserId { get; set; }
        public string Title { get; set; }
		public string SubTitle { get; set; }
		public string HtmlText { get; set; }
		public string Icon { get; set; } = string.Empty;
	}
}
