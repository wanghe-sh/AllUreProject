define(['angular', 'jquery'], function(angular) {
	return {
		'user': {
			'input_valid': {
				'email': {
					pattern: '请填写email',
					required: '请输入正确的email格式'
				},
				'status': {
					required: '请选择有无效',
				},
				'gender': {
					required: '请选择称谓'
				},
				'roles': {
					required: ' 请选择角色'
				},
				'lastname': {
					required: ' 请填写姓氏'
				}
			}
		}
	}
})