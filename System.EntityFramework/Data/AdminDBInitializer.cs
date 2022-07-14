using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.Model.Admin;
using System.Text;
using System.Threading.Tasks;
using System.Utils;

namespace System.EntityFramework.Data
{
    public static class AdminDBInitializer
    {
        public static void SeedDataAdmin(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                    new Group
                    {
                        GroupId = 1,
                        GroupName = "Admin",
                        Description = "Full chức năng",
                        Status = 1,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    Password = Common.MD5String("123123"),
                    FullName = "admin",
                    GroupId = 1,
                    Status = 1,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    PermissionId = 1,
                    PermissionTitle = "Dashboard",
                    PermissionKey = "dashboard",
                    PermissionUrl = "/dashboard",
                    Status = 1,
                    Position = 1,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                }
            );

            modelBuilder.Entity<GroupPermission>().HasData(
                new GroupPermission
                {
                    Id = 1,
                    PermissionId = 1,
                    GroupId = 1,
                    IsDelete = true,
                    IsInsert = true,
                    IsUpdate = true,
                    IsView = true,
                }
            );
        }
    }
}
