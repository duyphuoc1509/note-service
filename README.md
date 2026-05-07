# Note Service

## Mô tả

Đây là service backend cho ứng dụng ghi chép nhật ký hàng ngày (Note Service). Service này cung cấp các API để quản lý các ghi chú, nhật ký cá nhân, cho phép người dùng tạo, đọc, cập nhật và xóa các mục nhật ký.

## Tính năng

- Tạo nhật ký mới
- Xem danh sách nhật ký
- Chỉnh sửa nhật ký
- Xóa nhật ký
- Tìm kiếm và lọc nhật ký theo ngày, từ khóa

## Cài đặt

1. Clone repository:
   ```
   git clone https://github.com/duyphuoc1509/note-service.git
   cd note-service
   ```

2. Cài đặt dependencies:
   ```
   dotnet restore
   ```

3. Cấu hình biến môi trường (nếu cần):
   Tạo file `appsettings.json` hoặc sử dụng biến môi trường cho database connection, JWT secret, v.v.

## Chạy ứng dụng

Để chạy service:

```
dotnet run --project src/NoteService.Api/NoteService.Api.csproj
```

Health check:

```
GET /health
```

## Cấu trúc solution

- `src/NoteService.Api` — ASP.NET Core API host
- `src/NoteService.Domain` — domain entities/business rules
- `src/NoteService.Application` — application/use-case layer
- `src/NoteService.Infrastructure` — infrastructure integrations
- `src/NoteService.Contracts` — API DTOs/contracts
- `src/NoteService.Shared` — shared primitives/utilities
- `tests/NoteService.UnitTests` — unit tests
- `tests/NoteService.IntegrationTests` — integration tests

## Kiểm tra build/test

```
dotnet build NoteService.sln
dotnet test NoteService.sln
```

## Công nghệ sử dụng

- .NET Core / ASP.NET Core
- Database: Postgres SQL / Entity Framework Core
- Authentication: JWT

## Đóng góp

Chào mừng đóng góp! Vui lòng tạo issue hoặc pull request.

## Giấy phép

MIT License
