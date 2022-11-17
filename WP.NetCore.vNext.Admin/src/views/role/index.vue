<template>
  <div class="p-4">
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增角色</a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑',
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
            {
              label: '删除',
              icon: 'ic:outline-delete-outline',
              color: 'error',
              popConfirm: {
                title: '确认要删除吗',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <RoleModal @register="registerRoleModal" @success="handleSuccess" />
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref, reactive, onMounted } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getUserBasicColumns, getFormConfig } from './configData';
  import { getRoleList, deleteRole } from '/@/api/sys/role';
  import { SysUserModel } from '/@/api/sys/model/userModel';
  import RoleModal from './components/RoleModal.vue';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    components: { BasicTable, RoleModal, TableAction },
    setup() {
      const { createMessage } = useMessage();
      const [registerTable, { setLoading, reload, getForm }] = useTable({
        api: getRoleList,
        columns: getUserBasicColumns(),
        useSearchForm: true,
        formConfig: getFormConfig(),
        actionColumn: {
          width: 160,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });
      const [registerRoleModal, { openModal, setModalProps }] = useModal();
      function changeLoading() {
        setLoading(true);
        setTimeout(() => {
          setLoading(false);
        }, 1000);
      }
      function handleSuccess() {
        reload();
      }
      function getFormValues() {
        console.log(getForm().getFieldsValue());
      }

      async function handleDelete(user: SysUserModel) {
        await deleteRole('/' + user.id);
        createMessage.success('删除成功');
        reload();
      }
      function handleEdit(user: SysUserModel) {
        openModal(true, {
          user,
          isUpdate: true,
        });
      }
      function handleCreate() {
        openModal(true, {
          isUpdate: false,
        });
      }
      return {
        getFormValues,
        handleCreate,
        handleSuccess,
        registerRoleModal,
        openModal,
        registerTable,
        changeLoading,
        handleDelete,
        handleEdit,
      };
    },
  });
</script>
