import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const role: AppRouteModule = {
  path: '/role',
  name: 'Role',
  component: LAYOUT,
  redirect: '/role/index',
  meta: {
    hideChildrenInMenu: true,
    icon: 'simple-icons:about-dot-me',
    title: t('routes.role.role'),
    orderNo: 4,
  },
  children: [
    {
      path: 'index',
      name: 'RolePage',
      component: () => import('/@/views/role/index.vue'),
      meta: {
        title: t('routes.role.role'),
        icon: 'simple-icons:about-dot-me',
      },
    },
  ],
};

export default role;
