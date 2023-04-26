using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text;

namespace AppMVC.Menu
{
    public class AdminSidebarService
    {
        private readonly IUrlHelper UrlHelper;
        public List<SidebarItem> Items { get; set; } = new List<SidebarItem>();


        public AdminSidebarService(IUrlHelperFactory factory, IActionContextAccessor action)
        {
            UrlHelper = factory.GetUrlHelper(action.ActionContext);
            // Khoi tao cac muc sidebar

            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });
            Items.Add(new SidebarItem() { Type = SidebarItemType.Heading, Title = "Quản lý chung" });

            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Controller = "DbManage",
                Action = "Index",
                Area = "Database",
                Title = "Quản lý Database",
                AwesomeIcon = "fas fa-database"
            });
            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });

            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Title = "Phân quyền & thành viên",
                AwesomeIcon = "far fa-folder",
                collapseID = "role",
                Items = new List<SidebarItem>() {
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Role",
                                Action = "Index",
                                Area = "Identity",
                                Title = "Các vai trò (role)"
                        },
                         new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Role",
                                Action = "Create",
                                Area = "Identity",
                                Title = "Tạo role mới"
                        },
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "User",
                                Action = "Index",
                                Area = "Identity",
                                Title = "Danh sách thành viên"
                        },
                    },
            });
            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });

            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Title = "Quản lý trường",
                AwesomeIcon = "far fa-folder",
                collapseID = "school",
                Items = new List<SidebarItem>() {
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "School",
                                Action = "Index",
                                Area = "SchoolManagement",
                                Title = "Danh sách trường"
                        },
                         new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "School",
                                Action = "Create",
                                Area = "SchoolManagement",
                                Title = "Tạo trường"
                        }
                    },
            });
            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });
            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Title = "Quản lý khoa",
                AwesomeIcon = "far fa-folder",
                collapseID = "department",
                Items = new List<SidebarItem>() {
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Department",
                                Action = "Index",
                                Area = "SchoolManagement",
                                Title = "Danh sách khoa"
                        },
                         new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Department",
                                Action = "Create",
                                Area = "SchoolManagement",
                                Title = "Tạo khoa"
                        }
                    },
            });

            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });
            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Title = "Quản lý lớp",
                AwesomeIcon = "far fa-folder",
                collapseID = "class",
                Items = new List<SidebarItem>() {
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Class",
                                Action = "Index",
                                Area = "SchoolManagement",
                                Title = "Danh sách lớp"
                        },
                         new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Class",
                                Action = "Create",
                                Area = "SchoolManagement",
                                Title = "Tạo lớp"
                        }
                    },
            });

            Items.Add(new SidebarItem() { Type = SidebarItemType.Divider });
            Items.Add(new SidebarItem()
            {
                Type = SidebarItemType.NavItem,
                Title = "Quản lý sinh viên",
                AwesomeIcon = "far fa-folder",
                collapseID = "student",
                Items = new List<SidebarItem>() {
                        new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Student",
                                Action = "Index",
                                Area = "SchoolManagement",
                                Title = "Danh sách sinh viên"
                        },
                         new SidebarItem() {
                                Type = SidebarItemType.NavItem,
                                Controller = "Student",
                                Action = "Create",
                                Area = "SchoolManagement",
                                Title = "Tạo sinh viên"
                        }
                    },
            });
        }


        public string renderHtml()
        {
            var html = new StringBuilder();

            foreach (var item in Items)
            {
                html.Append(item.RenderHtml(UrlHelper));
            }
            return html.ToString();
        }

        public void SetActive(string Controller, string Action, string Area)
        {
            foreach (var item in Items)
            {
                if (item.Controller == Controller && item.Action == Action && item.Area == Area)
                {
                    item.IsActive = true;
                    return;
                }
                else
                {
                    if (item.Items != null)
                    {
                        foreach (var childItem in item.Items)
                        {
                            if (childItem.Controller == Controller && childItem.Action == Action && childItem.Area == Area)
                            {
                                childItem.IsActive = true;
                                item.IsActive = true;
                                return;

                            }
                        }
                    }
                }
            }
        }
    }
}