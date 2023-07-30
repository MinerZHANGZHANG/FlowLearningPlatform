using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第一部分，验证身份的表单模型
	/// </summary>
	public class IdentityVal
	{
		[Required,StringLength(maximumLength:11,MinimumLength =11)]
		public string StudentNumber { get; set; }
		[Required]
		public DateTime Brithday { get; set; }
		[Required]
		public string RoleId { get; set; }
	}
}
