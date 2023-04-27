Set up:
1. cd into sql-server-docker::: docker-compose up -d
   127.0.0.1,1433
   sa
   Password123
2. Init db + Seed data ::: https://localhost:3000/database-manage/Index
3. Login admin:
   admin
   admin123


dotnet --list-sdks
dotnet --list-runtimes
dotnet ef migrations remove
docker-compose up -d

Visual Studio:
add-migration AddCategory
update-database
Remove-Migration

## Action Result
- ViewResult : Trả về View
- PartialViewResult : Trả về PartialView
- RedirectResult : Chuyển hướng Redirect
- RedirectToRouteResult : Chuyển hướng RedirectToRoute/RedirectToAction
- ContentResult : Trả về Content
- JsonResult : Trả về Json
- JavaScriptResult : Trả về JavaScript
- FileResult : Trả về File
- EmptyResult : Trả về Empty
## Controller

-   Action trong controller là public
-   Trả về IActionResult
-   Các dịch vụ inject vào controller qua hàm tạo

## Truyền dữ liệu sang View

-   Model
-   ViewData
-   ViewBag
-   TempData

## Areas
- Là tên dùng để routing
- Là cấu trúc thư mục chứa M.V.C
- Thiết lập Area cho controller bằng ```[Area("AreaName")]```
- Tạo cấu trúc thư mục

# Url.Action()
# Url.ActionLink()
# Url.RouteUrl()
# Url.Link()

asp-area
asp-controller
asp-action
asp-route-[paramName]
asp-route

<div class="tooltip">
  Tên sản phẩm quá dài làm mất layout của trang
  <span class="tooltiptext">Tên sản phẩm quá dài làm mất layout của trang</span>
</div>

@using AppMVC.Menu
@inject AdminSidebarService _AdminSidebarService

@{
    _AdminSidebarService.SetActive("Class", "Index", "SchoolManagement");
}

@section Sidebar
    {
    @Html.Raw(_AdminSidebarService.renderHtml())
}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "product",
                 areaName: "ProductManage",
                 pattern: "/{controller}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                 name: "first",
                        pattern: "{url:regex(^view.*product$)}/{id:range(1,5)}",
                        defaults: new { controller = "First", action = "ViewProduct" });
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });