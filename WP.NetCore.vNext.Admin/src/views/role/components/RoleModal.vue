<template>
  <BasicModal
    destroyOnClose
    @register="register"
    @ok="customSubmitFunc"
    v-bind="$attrs"
    :title="getTitle"
    :helpMessage="['新增用户']"
  >
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent, ref, unref, computed } from 'vue';
  import { createRole, updateRole } from '/@/api/sys/role';

  import { schemas } from '../configData';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form';
  import { CollapseContainer } from '/@/components/Container';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    components: { BasicModal, BasicForm, CollapseContainer },
    setup(_, { emit }) {
      const [
        registerForm,
        { validate, setFieldsValue, setProps, resetFields, removeSchemaByFiled },
      ] = useForm({
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
          text: '保存',
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
          var tempData = JSON.parse(JSON.stringify(data.user));
          rowId.value = tempData.id;
          setFieldsValue({
            ...tempData,
          });
        }
      });
      const getTitle = computed(() => (!unref(isUpdate) ? '新增角色' : '编辑角色'));

      const { createMessage } = useMessage();

      async function customSubmitFunc() {
        try {
          const values = await validate();
          setModalProps({ confirmLoading: true });
          if (!unref(isUpdate)) {
            await createRole(values);
          } else {
            await updateRole(rowId.value, values);
          }
          setModalProps({ confirmLoading: false });
          emit('success');
          createMessage.success('保存成功！');
          closeModal();
        } catch (error) {
          // createMessage.error('保存失败！');
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
