import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const dashboard: AppRouteModule = {
  path: '/dashboard',
  name: 'Dashboard',
  component: LAYOUT,
  redirect: '/dashboard/index',

  meta: {
    orderNo: 1,
    hideChildrenInMenu: true,
    icon: 'ion:grid-outline',
    title: t('routes.dashboard.dashboard'),
  },
  children: [
    {
      path: 'index',
      name: 'Workbench',
      component: () => import('/@/views/dashboard/index.vue'),
      meta: {
        title: t('routes.dashboard.dashboard'),
      },
    },
  ],
};

export default dashboard;
