using AntDesign;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	public class AddAnnouncement
	{
		public Guid UserId { get; set; }
		[Required(ErrorMessage ="公告标题不能为空")]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "公告内容不能为空")]
        public string HtmlText { get; set; }
		public string Icon { get; set; } = string.Empty;
	}
}
