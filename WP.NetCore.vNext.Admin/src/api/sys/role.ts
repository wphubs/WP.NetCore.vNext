import { defHttp } from '/@/utils/http/axios';
import { RoleModel } from './model/roleModel';
enum Api {
  role = '/role',
}

export function getRoleList(params) {
  return defHttp.get<RoleModel>({ url: Api.role, params });
}

export function createRole(params: RoleModel) {
  return defHttp.post<any>({ url: Api.role, params });
}
export function updateRole(id: string, params: RoleModel) {
  return defHttp.put<any>({ url: `${Api.role}/${id}`, params });
}

export function deleteRole(params) {
  return defHttp.delete<any>({ url: Api.role, params });
}
