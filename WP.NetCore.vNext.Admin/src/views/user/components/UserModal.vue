<template>
  <BasicModal
    @register="register"
    @ok="customSubmitFunc"
    v-bind="$attrs"
    :title="getTitle"
    :helpMessage="['新增用户']"
  >
  <!-- :showOkBtn="false"
  :showCancelBtn="false" -->

    <BasicForm @register="registerForm" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent,ref,unref,computed } from 'vue';
  import { createUser } from '/@/api/sys/user'
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form';
  import { CollapseContainer } from '/@/components/Container';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    components: { BasicModal, BasicForm, CollapseContainer },
    setup(_, { emit }) {
      const schemas: FormSchema[] = [
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
            value:1,
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
      ];
     
      const [registerForm, { validate, setFieldsValue, setProps,resetFields }] = useForm({
        showActionButtonGroup: false,
        labelCol: {
          span: 4,
        },
        wrapperCol: {
          span: 22,
        },
        schemas: schemas,
        actionColOptions: {
          offset: 8,
          span: 18,
        },
        submitButtonOptions: {
          text: '提交',
        },
        submitFunc: customSubmitFunc,
      });
      const isUpdate = ref(true);
      const rowId = ref('');
     
      const [register, { closeModal, setModalProps }] = useModalInner(async (data) => {
        resetFields();
        setModalProps({ confirmLoading: false });
        isUpdate.value = !!data?.isUpdate;

        if (unref(isUpdate)) {
          // rowId.value = data.record.id;
          console.log('data:'+JSON.stringify(data.user))
          setFieldsValue({
            ...data.user,
          });
        }

      });
      const getTitle = computed(() => (!unref(isUpdate) ? '新增账号' : '编辑账号'));

      const { createMessage } = useMessage();
    
  

      async function customSubmitFunc() {
        try {
          const values = await validate();
          setModalProps({ confirmLoading: true });

          await createUser(values);

          // setTimeout(() => {
             setModalProps({ confirmLoading: false });
          //   console.log(values);
          emit('success');
            createMessage.success('提交成功！');
             closeModal();
          // }, 2000);
        } catch (error) {
          createMessage.success('提交失败！');
          setModalProps({ confirmLoading: false });
        }
      }

      return {
        getTitle,
        register,
        registerForm,
        customSubmitFunc,
        closeModal,
        setModalProps: () => {
          setModalProps({ title: 'Modal New Title' });
        },
      };
    },
  });
</script>
