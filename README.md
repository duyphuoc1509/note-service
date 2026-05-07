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

3. Cấu hình Notes database riêng (`notes_db`):
   - Ưu tiên dùng `ConnectionStrings:NotesConnection`
   - Hoặc cấu hình qua env:
     - `DATABASE_INTERNAL_HOST`
     - `DATABASE_INTERNAL_PORT`
     - `DATABASE_INTERNAL_USER`
     - `DATABASE_INTERNAL_PASSWORD`
   - Service sẽ luôn ép tên database là `notes_db` khi build connection string từ env để tránh đọc/ghi nhầm monolith DB.
   - Có thể tham khảo `appsettings.example.json`.

4. Tạo migration / apply schema:
   ```
   dotnet ef database update --project src/NoteService.Infrastructure/NoteService.Infrastructure.csproj --context NotesDbContext
   ```

## Chạy ứng dụng

Để chạy test hiện có:

```
dotnet test
```

Để khởi tạo migration mới:

```
dotnet ef migrations add <MigrationName> --project src/NoteService.Infrastructure/NoteService.Infrastructure.csproj --context NotesDbContext --output-dir Persistence/Migrations
```


## Công nghệ sử dụng

- .NET Core / ASP.NET Core
- Database: Postgres SQL / Entity Framework Core
- Authentication: JWT

## Đóng góp

Chào mừng đóng góp! Vui lòng tạo issue hoặc pull request.

## Giấy phép

MIT License
