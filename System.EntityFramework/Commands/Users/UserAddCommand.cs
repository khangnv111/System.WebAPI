using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.Model.Admin;
using System.Models.ViewModel;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;
using System.Utils;
using System.Utils.Account;

namespace System.EntityFramework.Commands.Users
{
    public class UserAddCommand
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly IMapper mapper;
        public UserAddCommand(IUserReponsitory userReponsitory, IMapper mapper)
        {
            this.userReponsitory = userReponsitory;
            this.mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveUser user, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)
                   || string.IsNullOrWhiteSpace(user.RePassword) || user.GroupId <= 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Check account char
                var textError = string.Empty;
                if (!AccountVerify.CheckAccount(user.UserName, ref textError))
                {
                    return new OkObjectResult(new DataResponse(textError));
                }

                // Check password
                if (!AccountVerify.CheckPassword(user.Password, user.RePassword, ref textError))
                {
                    return new OkObjectResult(new DataResponse(textError));
                }

                // Check account exists
                var userData = await userReponsitory.GetUserByNameAsync(user.UserName).ConfigureAwait(false);
                if (userData != null)
                {
                    return new OkObjectResult(new DataResponse("Tài khoản đã tồn tại"));
                }

                // Create new user
                var userModel = new User()
                {
                    UserName = user.UserName,
                    Password = Common.MD5String(user.Password),
                    Email = user.Email,
                    FullName = user.FullName,
                    GroupId = user.GroupId,
                    Status = (int)UserStatus.Active
                };

                userModel = await userReponsitory.AddUserAsync(userModel, cancellationToken).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(userModel));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }

        }
    }
}
