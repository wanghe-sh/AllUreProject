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
					required: '请选择角色'
				},
				'lastname': {
					required: '请填写姓氏'
				},
                'company': {
		            required: '请填写公司'
                },
                'password': {
                    pattern: '密码至少需要8个字符,1大写字母和1个数字或特殊字符',
                    required: '请填写密码'
                },
                'repassword': {
                    required: '请填写密码确认'
                }
			}
		}
	}
})