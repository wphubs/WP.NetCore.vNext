/**
 * @description: Login interface parameters
 */
export interface LoginParams {
  account: string;
  password: string;
}

export interface RoleInfo {
  roleName: string;
  value: string;
}

/**
 * @description: Login interface return value
 */
export interface LoginResultModel {
  userId: string | number;
  token: string;
  role: RoleInfo;
}

/**
 * @description: Get user information return value
 */
export interface GetUserInfoModel {
  id: string;
  roles: RoleInfo[];
  // 用户id
  userId: string | number;
  // 用户名
  account: string;
  // 真实名字
  name: string;
  // 头像
  avatar: string;
  // 介绍
  desc?: string;
}

export interface SysUserModel {
  id: string;
  account: string;
  avatar: string;
  deptId: string;
  name: string;
  sex: number;
  createBy: string;
  createTime: string;
  modifyBy: string;
  modifyTime: string;
}

export interface SysUserCreateOrUpdateModel {
  account: string;
  deptId: string;
  name: string;
  sex: number;
}
