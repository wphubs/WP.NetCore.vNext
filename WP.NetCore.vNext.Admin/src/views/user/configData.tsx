import { FormProps, FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';
import { getRoleList } from '/@/api/sys/role';
export function getFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      {
        field: `name`,
        label: `姓名`,
        labelWidth: 100,
        component: 'Input',
        colProps: {
          xl: 6,
          xxl: 4,
        },
      },
      {
        field: `account`,
        label: `帐号`,
        labelWidth: 100,
        component: 'Input',
        colProps: {
          xl: 6,
          xxl: 4,
        },
      },
    ],
  };
}

export function getUserBasicColumns(): BasicColumn[] {
  return [
    {
      title: 'ID',
      dataIndex: 'id',
    },
    {
      title: '头像',
      dataIndex: 'avatar',
      slots: { customRender: 'avatar' },
    },
    {
      title: '帐号',
      dataIndex: 'account',
    },

    {
      title: '名称',
      dataIndex: 'name',
    },
    {
      title: '性别',
      dataIndex: 'sex',
      slots: { customRender: 'sex' },
      width: 200,
    },
    {
      title: '角色',
      dataIndex: 'roles',
      slots: { customRender: 'roles' },
      width: 200,
    },
    {
      title: '创建时间',
      dataIndex: 'createTime',
    },
    {
      title: '修改时间',
      dataIndex: 'modifyTime',
    },
  ];
}

export const schemas: FormSchema[] = [
  {
    field: 'name',
    component: 'Input',
    label: '姓名',
    colProps: {
      span: 24,
    },
    defaultValue: '',
    rules: [{ required: true, message: '请填写姓名', trigger: 'blur' }],
    componentProps: {
      placeholder: '输入姓名',
    },
  },
  {
    field: 'account',
    component: 'Input',
    label: '帐号',
    defaultValue: '',
    rules: [{ required: true, message: '请填写帐号', trigger: 'blur' }],
    componentProps: {
      placeholder: '输入帐号',
    },
  },
  {
    field: 'password',
    component: 'InputPassword',
    label: '密码',
    defaultValue: '',
    rules: [{ required: true, message: '请填写密码', trigger: 'blur' }],
    componentProps: {
      placeholder: '输入密码',
    },
  },
  {
    field: 'sex',
    component: 'Select',
    label: '性别',
    componentProps: {
      // mode: 'multiple',
      options: [
        {
          label: '男',
          value: 1,
          key: 1,
        },
        {
          label: '女',
          value: 2,
          key: 2,
        },
      ],
    },
  },
  {
    field: 'roles',
    component: 'ApiSelect',
    label: '角色',
    colProps: {
      span: 24,
    },
    required: true,
    componentProps: {
      api: getRoleList,
      params: { PageIndex: 1, PageSize: 999 },
      labelField: 'name',
      valueField: 'id',
      resultField: 'items',
      mode: 'tags',
    },
  },
];
