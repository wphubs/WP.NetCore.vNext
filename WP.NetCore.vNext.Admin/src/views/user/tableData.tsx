import { FormProps, FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';

export function getUserBasicColumns(): BasicColumn[] {
  return [
    {
      title: 'ID',
      dataIndex: 'id',
      edit: true,
    },
    {
      title: '帐号',
      dataIndex: 'account',
      edit: true,
    },
    {
      title: '头像',
      dataIndex: 'avatar',
      slots: { customRender: 'avatar' },
    },
    {
      title: '名称',
      dataIndex: 'name',
    },
    {
      title: '性别',
      dataIndex: 'sex',
      edit: true,
      editComponent: 'Select',
      editComponentProps: {
        options: [
          {
            label: '男',
            value: '1',
          },
          {
            label: '女',
            value: '2',
          },
        ],
      },
      width: 200,
    },
  ];
}
