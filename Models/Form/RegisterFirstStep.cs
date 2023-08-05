using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第一部分，验证身份的表单模型
	/// </summary>
	public class RegisterFirstStep
	{
		[Required(ErrorMessage ="学号不能为空")]
		[RegularExpression(@"^[0-9]{11}",ErrorMessage ="学号应为11位数字")]
		public string StudentNumber { get; set; }
		[Required(ErrorMessage = "生日不能为空")]
		public DateTime Brithday { get; set; }
		[Required(ErrorMessage = "身份选择不能为空")]
		public string RoleId { get; set; }
	}
}
