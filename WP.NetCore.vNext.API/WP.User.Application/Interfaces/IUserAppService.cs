﻿namespace WP.User.Application.Interfaces;

public interface IUserAppService
{
    Task CreateUserAsync(UserCreateOrUpdateDto userCreateOrUpdate);

}
