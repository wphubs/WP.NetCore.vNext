import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const user: AppRouteModule = {
  path: '/user',
  name: 'User',
  component: LAYOUT,
  redirect: '/user/index',
  meta: {
    hideChildrenInMenu: true,
    icon: 'simple-icons:about-dot-me',
    title: t('routes.dashboard.about'),
  },
  children: [
    {
      path: 'index',
      name: 'UserPage',
      component: () => import('/@/views/user/index.vue'),
      meta: {
        title: t('routes.dashboard.about'),
        icon: 'simple-icons:about-dot-me',
      },
    },
  ],
};

export default user;
