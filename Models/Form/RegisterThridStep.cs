using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第三部分，填写基本信息的表单模型
	/// </summary>
	public class RegisterThridStep
	{
		//[Phone(ErrorMessage ="请输入正确格式的电话号码")]
		public string? PhoneNumber { get; set; }=string.Empty;
		//[EmailAddress(ErrorMessage = "请输入正确格式的邮箱")]
		public string? Email { get; set; } = string.Empty;
        [StringLength(255,ErrorMessage ="自我介绍不能超过255个字符")]
		public string? Description { get; set; } = string.Empty;
    }
}
