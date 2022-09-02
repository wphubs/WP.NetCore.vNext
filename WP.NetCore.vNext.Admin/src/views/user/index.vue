<template>
  <div class="p-4">
    <a-button type="primary" @click="openModal">Primary Button</a-button>

    <BasicTable @register="registerTable">
      <template #avatar="{ text: tags }">
        <a-avatar :src="tags" />
      </template>
    </BasicTable>
    <UserModal @register="registerUserModal"></UserModal>
    {{ list }}
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref, reactive, onMounted } from 'vue';
  import { BasicTable, useTable } from '/@/components/Table';
  import { getUserBasicColumns } from './tableData';
  import { getUserList } from '/@/api/sys/user';
  import { SysUserModel } from '/@/api/sys/model/userModel';
  import UserModal from './components/UserModal.vue';
  import { useModal } from '/@/components/Modal';
  export default defineComponent({
    components: { BasicTable,UserModal },
    methods: {
      clickAdd() {
        console.log('clickAdd');
      },
    },
    setup() {
      const list: any = reactive({ data: [] });
      onMounted(async () => {
        list.data = await getUserList();
        console.log('onMounted:' + JSON.stringify(list));
      });
      const [registerTable, { setLoading }] = useTable({
        api: getUserList,
        columns: getUserBasicColumns(),
      });
      const [registerUserModal, { openModal,setModalProps }] = useModal();
      function changeLoading() {
        setLoading(true);
        setTimeout(() => {
          setLoading(false);
        }, 1000);
      }
      return {
        registerUserModal,
        openModal,
        list,
        registerTable,
        changeLoading,
      };
    },
  });
</script>
