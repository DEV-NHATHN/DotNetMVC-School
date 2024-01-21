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

- Action trong controller là public
- Trả về IActionResult
- Các dịch vụ inject vào controller qua hàm tạo

## Truyền dữ liệu sang View

- Model
- ViewData
- ViewBag
- TempData

## Areas

- Là tên dùng để routing
- Là cấu trúc thư mục chứa M.V.C
- Thiết lập Area cho controller bằng `[Area("AreaName")]`
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

1/ C# cơ bản
1.1/ Cơ bản về lập trình OOP?
OOP tức là lập trình hướng đối tượng, nó gồm 4 tính chất

    Tính đóng gói (Encapsulation): Là cách để che dấu những tính chất xử lý bên trong của đối tượng, những đối tượng khác không thể tác động trực tiếp làm thay đổi trạng thái chỉ có thể tác động thông qua các method public của đối tượng đó.
    Tính kế thừa (Inheritance): Là kỹ thuật cho phép kế thừa lại những tính năng mà một đối tượng cha đã có, giúp tránh việc code lặp dư thừa mà chỉ xử lý công việc tương tự.
    Tính đa hình (Polymorphism ): Là một đối tượng thuộc các lớp khác nhau có thể hiểu cùng một thông điệp theo cách khác nhau.
    Tính trừu tượng (Abstraction): Là phương pháp trừu tượng hóa định nghĩa lên những hành động, tính chất của loại đối tượng nào đó cần phải có.

    1.2/ Hãy so sánh giữa interface và abstract?
    Giống nhau:

    Abstract class và interface đều không thể khởi tạo đối tượng bên trong được.
    Abstract class và interface đều có thể khai báo các phương thức nhưng không thực hiện chúng.
    Abstract class và interface đều bao gồm các phương thức abstract.
    Abstract class và interface đều được thực thi từ các class con hay còn gọi kế thừa, dẫn xuất.
    Abstract class và interface đều có thể kế thừa từ nhiều interface.

    Khác nhau:
    	Abstract Class : Cho phép khai báo field
    					Các phương thức có thể có thân hàm hoặc không có thân hàm
    					Class dẫn xuất chỉ kế thừa được từ 1 abstract class và nhiều interface.
    					Có chứa constructor
    					Các phương thức có từ khóa access modifier


    	Interface: 	Không cho phép
    				Chỉ khai báo không có thân hàm
    				Class triển khai có thể triển khai nhiều interface.
    				Không có
    				Không có
    1.3/ override, overload khác gì nhau:
    	– Sử dụng override khi ở lớp cha có một phương thức thực hiện công việc A, và lớp con khi kế thừa phương thức này nó muốn làm khác đi công việc A đó thì chúng ta sẽ dùng override.

    	– Sử dụng overload khi chúng ta muốn chỉ sử dụng một tên method cho nhiều xử lý có ý nghĩa tương tự nhau trong cùng một class, để dễ gợi nhớ, tránh trường hợp đặt nhiều tên method khác nhau mà chúng lại có chung xử lý.

    1.4/ Phân biệt string với string builder
    1.5/ extenstion menthod
    	Chúng ta tạo 1 extenstion class, class này phải là static class, method cũng phải static, params đầu tiên truyền vào là class cần extention, với từ khóa this.
    1.5/ E có biết về extension menthod không.
    	Lớp chứa extension method phải là static
    	Extension method cũng phải là một phương thức static
    	Tham số đầu tiên của extension method xác định kiểu của đối tượng sẽ mở rộng được sử dụng với từ khóa this, và tham số đầu tiên này sẽ được bỏ qua khi extension method được gọi.
    1.6/ phân biệt Queue và Stack khách gì nhau:
    1.7/ E đã làm việc với generic bao giờ chưa
    1.8/ e có biết về design parten không:
    	Singleton đảm bảo chỉ duy nhất một thể hiện (instance) được tạo ra và nó sẽ cung cấp cho bạn một method để có thể truy xuất được thể hiện duy nhất đó mọi lúc mọi nơi trong chương trình.
    	private constructor để hạn chế truy cập từ class bên ngoài.
    	Đặt private static final variable đảm bảo biến chỉ được khởi tạo trong class.
    	Có một method public static để return instance được khởi tạo ở trên.

2/ những câu hỏi .net/
2.1/ .net core khác gì .net mvc
2.2/Linq thì có gì
cần lấy ra số lượng phần tử trong linq thì dùng hàm gì
nếu cần join trong linq thì dùng gì
nếu bảng công việc có 2 trường id người thực hiện và id người hỗ trợ thì e sẽ select thế nào để lấy ra tên người thực hiện và tên người hỗ trợ
2.3/làm thế nào để truyền dữ liệu từ controller đến view
2.4/làm thế nào để truyền view đến controller
2.5/e dùng gì để xác thực 1 api
2.6/JSON Web Token (JWT) là gì
JWT là một phương tiện đại diện cho các yêu cầu chuyển giao giữa hai bên Client – Server , các thông tin trong chuỗi JWT được định dạng bằng JSON . Trong đó chuỗi Token phải có 3 phần là header , phần payload và phần signature được ngăn bằng dấu “.”
3/ những câu hỏi Entity Framework
EF thì e dùng loại nào:
3.1/ đã sử dụng migration trong entity chưa,
Database First: Đầu tiên là nếu bạn đã có một CSDL hoặc muốn thiết kế CSDL trước khi làm các phần khác của ứng dụng.
Hướng tiếp cận này sẽ giúp sinh ra model từ cơ sở dữ liệu có sẵn
Code First: Thứ hai là bạn muốn tập trung vào các domain class rồi mới tạo CSDL từ các domain class đó.
Đây là hướng tiếp cận thứ hai xuất hiện từ EF4. Hướng tiếp cận này cho phép bạn sử dụng một giao diện đồ họa để thiết kế model trước. Sau đó trình thiết kế này sẽ sinh ra các lớp model tương ứng cũng như mã SQL để tạo cơ sở dữ liệu. Nói cách khác, bạn xuất phát từ một giao diện thiết kế riêng biệt. EF sẽ giúp bạn sinh ra cả ba thành phần của EDM.
Model First: Thứ ba là bạn muốn thiết kế schema của CSDL trên visual designer rồi mới tạo CSDL và các class.
Với vai trò lập trình viên, bạn không cần quan tâm đến SQL hay giao diện thiết kế đồ họa nữa. Tất cả những gì cần làm là xây dựng các lớp model (domain classes) theo đúng kiểu hướng đối tượng mà bạn quen thuộc nhất. Entity Framework sẽ làm tất cả những gì còn lại để bạn có một cơ sở dữ liệu phù hợp. Nói theo kiểu khác, bạn tự mình code Conceptual Model, EF sẽ giúp bạn sinh ra Mapping và Storage Model.
4/ DI:
DI là gì sử dụng như thế nào
ở .net core có sử dụng di không, sử dụng ntn
làm thế nào để e add thêm 1 service vào trong trương trình
giảm sự phụ thuộc giữa cá module, dễ dàng hơn trong việc thay đổi các module, bảo trì và testing
dạng thông dụng nhất để dùng di vào .net core là gì: contructor injection: các dependency sẽ được container truyền vào một class thông qua constructor của class đó, đây là cách thông dụng nhất
AddScoped
(lifetime) Service DI trong .net core
Transient: Một thể hiện mới luôn được tạo, mỗi khi được yêu cầu.
public HomeController(ITransientService transientService1,
ITransientService transientService2)
{
\_transientService1 = transientService1;
\_transientService2 = transientService2;
}
=> LUÔN TRẢ VỀ 2 CÁI KHÁC NHAU
Scoped: Tạo một thể hiện mới cho tất cả các scope (Mỗi request là một scope). Trong scope thì service được dùng lại
Singleton: Service được tạo chỉ một lần duy nhất.

    SOILD:
    Single Responsibility Principle – Nguyên lý Đơn Trách Nhiệm.
    Open/Closed Principle : Có thể thoải mái mở rộng 1 module, nhưng hạn chế sửa đổi bên trong module đó (open for extension but closed for modification).

    Interface Segregation Principle: Thay vì dùng 1 interface lớn, ta nên tách thành nhiều interface nhỏ, với nhiều mục đích cụ thể
    Dependency Inversion Principle:
    	1. Các module cấp cao không nên phụ thuộc vào các module cấp thấp. Cả 2 nên phụ thuộc vào abstraction.
    	2. Interface (abstraction) không nên phụ thuộc vào chi tiết, mà ngược lại. (Các class giao tiếp với nhau thông qua interface, không phải thông qua implementation.)

git,svn
1.1/e đã làm việc với git chưa
1.2/git khác gì svn( nếu làm việc với svn)
1.3/nếu gặp trường hợp conflic code thì e sẽ giải quyết thế nào
1.4/làm thế nào để lấy 1 commit của nhánh A cho nhánh B: cherrypick
1.5/nếu sinh ra file k cần: log,image thì e sẽ làm gì để k phải xóa từng file khi commit
1.6/merger khác gì rebase
1.7/làm thể nào để chỉnh sửa 1 commit đã được commit:
lên local, reset thì sẽ k tạo commit mới
lên remote
1.8/đang làm việc với nhánh A, người ta cần e phải sang fix bug gấp ở tag B thì sẽ giải quyết thế nào.
sql turning
1.1/ thiết kế db:
1 Giảng viên thì có thể cùng lúc tham gia Giảng dạy cho 1 hoặc nhiều Lớp học.
1 Lớp học thì có thể có 1 hoặc nhiều Giảng viên giảng dạy.
1.2/
where khác gì having
1.3/
dùng view bao giờ chưa. dùng trong trường hợp nào
điểm mạnh là gì:
điểm yếu là gì:
1.4/
giả sử cho 1 bảng học sinh có trường tuổi làm thế nào để lấy ra học sinh lớn tuổi thứ 2 LIMIT VÀ OFFSET
1.5/
Select distinct \* from User
1.6/
e biết khái niệm index trong sql không, khi nào dùng, 1 table có thể có nhiều index hay chỉ có 1 index
1.7/
đã bao giờ dùng store procedure chưa, viết T-SQL chưa,
CREATE PROCEDURE ShowNumber @Num int
AS
DECLARE @cnt INT = 0;
WHILE @cnt < @Num
BEGIN
PRINT @cnt;
SET @cnt = @cnt + 1;
END;

    	GO;

    	drop PROCEDURE ShowNumber;
    	go
    	exec ShowNumber @Num=5;

    1.8/ khi nào thì dùng transection:
    	dùng cho 2 bảng trở lên
    1.9/ pivot table sql

sercurity
các lỗi bảo mật owasp
sql injection một ví dụ cơ bản ' or 1=1; --
Cross Site Scripting (XSS) : <script>alert(‘XSS’)</script>
CSRF la gì
Hash password theo những thuật toán nào
Hash rồi thì mk đã an toàn chưa
Nâng cao:
Message Queue
Docker
AWS

@using AppMVC.Menu
@inject AdminSidebarService \_AdminSidebarService

@{
\_AdminSidebarService.SetActive("Class", "Index", "SchoolManagement");
}

@section Sidebar
{
@Html.Raw(\_AdminSidebarService.renderHtml())
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
