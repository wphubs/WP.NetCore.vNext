import { FormProps, FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';
import { getRoleList } from '/@/api/sys/role';
export function getFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      {
        field: `name`,
        label: `名称`,
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
      title: '角色名称',
      dataIndex: 'name',
    },
    {
      title: '描述',
      dataIndex: 'desc',
    },

    {
      title: '排序',
      dataIndex: 'sort',
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
    label: '名称',
    colProps: {
      span: 24,
    },
    defaultValue: '',
    rules: [{ required: true, message: '请填写角色名称', trigger: 'blur' }],
    componentProps: {
      placeholder: '输入角色名称',
    },
  },
  {
    field: 'desc',
    component: 'Input',
    label: '描述',
    defaultValue: '',
    colProps: {
      span: 24,
    },
  },
  {
    field: 'sort',
    component: 'InputNumber',
    label: '排序',
    colProps: {
      span: 24,
    },
  },
];
