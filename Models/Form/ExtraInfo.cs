using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第三部分，填写基本信息的表单模型
	/// </summary>
	public class ExtraInfo
	{
		[StringLength(maximumLength:15,MinimumLength =6)]
		public string? PhoneNumber { get; set; }=string.Empty;
		[EmailAddress]
		public string? Email { get; set; } = string.Empty;
        [MaxLength(255)]
		public string? Description { get; set; } = string.Empty;
    }
}
