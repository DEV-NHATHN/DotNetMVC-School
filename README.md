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
```
dotnet aspnet-codegenerator area Product
```  

_ViewStart và _ViewImports

# Url.Action()
# Url.ActionLink()
# Url.RouteUrl()
# Url.Link()

asp-area
asp-controller
asp-action
asp-route-[paramName]
asp-route

"Microsoft.EntityFrameworkCore.Query": "Information",
"Microsoft.EntityFrameworkCore.Database.Command": "Information"


<div class="tooltip">
  Tên sản phẩm quá dài làm mất layout của trang
  <span class="tooltiptext">Tên sản phẩm quá dài làm mất layout của trang</span>
</div>
