import { FormProps, FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';

export const getAdvanceSchema = (itemNumber = 9): FormSchema[] => {
  const arr: any = [];
  for (let index = 0; index < itemNumber; index++) {
    arr.push({
      field: `field${index}`,
      label: `字段${index}`,
      labelWidth: 80,
      component: 'Input',
      colProps: {
        xl: 6,
        xxl: 6,
      },
    });
  }
  return arr;
};
export function getFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      ...getAdvanceSchema(9),
      {
        field: `field11`,
        label: `Slot示例`,
        component: 'Select',
        slot: 'custom',
        colProps: {
          xl: 6,
          xxl: 6,
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
      // edit: true,
    },
    {
      title: '头像',
      dataIndex: 'avatar',
      slots: { customRender: 'avatar' },
    },
    {
      title: '帐号',
      dataIndex: 'account',
      // edit: true,
    },

    {
      title: '名称',
      dataIndex: 'name',
    },
    {
      title: '性别',
      dataIndex: 'sex',
      // edit: true,
      slots: { customRender: 'sex' },
      // editComponent: 'Select',
      // editComponentProps: {
      //   options: [
      //     {
      //       label: '男',
      //       value: '1',
      //     },
      //     {
      //       label: '女',
      //       value: '2',
      //     },
      //   ],
      // },
      width: 200,
    },
  ];
}
