<template>
  <BasicModal  @register="register" v-bind="$attrs" title="新增用户" :helpMessage="['新增用户']">
    <BasicForm
    :labelWidth="80"
    :schemas="schemas"
    :actionColOptions="{ span: 12 }"
    :showResetButton="false"
    :showSubmitButton="false"
  />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, FormSchema } from '/@/components/Form';
  import { CollapseContainer } from '/@/components/Container';
  import { useMessage } from '/@/hooks/web/useMessage';
  const schemas: FormSchema[] = [
    {
      field: 'field',
      component: 'Input',
      label: '帐号',
      defaultValue: '1',
      componentProps: {
        placeholder: '自定义placeholder',
        onChange: (e) => {
          console.log(e);
        },
      },
    },

        {
      field: 'field2',
      component: 'InputPassword',
      label: '密码',
      defaultValue: '1',
      componentProps: {
        placeholder: '自定义placeholder',
        onChange: (e) => {
          console.log(e);
        },
      },
    },
  ];

  export default defineComponent({
    components: { BasicModal,BasicForm,CollapseContainer  },
    setup() {
      const [register, { closeModal, setModalProps }] = useModalInner();
      const { createMessage } = useMessage();
      return {
        schemas,
        handleSubmit: (values: any) => {
          createMessage.success('click search,values:' + JSON.stringify(values));
        },
        register,
        closeModal,
        setModalProps: () => {
          setModalProps({ title: 'Modal New Title' });
        },
      };
    },
  });
</script>