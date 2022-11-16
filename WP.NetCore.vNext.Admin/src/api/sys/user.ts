import { defHttp } from '/@/utils/http/axios';
import {
  LoginParams,
  LoginResultModel,
  GetUserInfoModel,
  SysUserModel,
  SysUserCreateOrUpdateModel,
} from './model/userModel';

import { ErrorMessageMode } from '/#/axios';

enum Api {
  Login = '/Account',
  Logout = '/logout',
  GetUserInfo = '/user/getUserInfo',
  user = '/user',
  GetPermCode = '/getPermCode',
}

/**
 * @description: user login api
 */
export function loginApi(params: LoginParams, mode: ErrorMessageMode = 'modal') {
  return defHttp.post<LoginResultModel>(
    {
      url: Api.Login,
      params,
    },
    {
      errorMessageMode: mode,
    },
  );
}

export function createUser(params: SysUserCreateOrUpdateModel) {
  return defHttp.post<any>({ url: Api.user, params });
}
export function updateUser(id: string, params: SysUserCreateOrUpdateModel) {
  return defHttp.put<any>({ url: `${Api.user}/${id}`, params });
}

export function getUserList(params) {
  return defHttp.get<SysUserModel>({ url: Api.user, params }, { errorMessageMode: 'none' });
}

export function deleteUser(params) {
  return defHttp.delete<any>({ url: Api.user, params }, { errorMessageMode: 'none' });
}

/**
 * @description: getUserInfo
 */
export function getUserInfo() {
  return defHttp.get<GetUserInfoModel>({ url: Api.GetUserInfo }, { errorMessageMode: 'none' });
}

export function getPermCode() {
  return defHttp.get<string[]>({ url: Api.GetPermCode });
}

export function doLogout() {
  return defHttp.get({ url: Api.Logout });
}
