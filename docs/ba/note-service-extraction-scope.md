# BA Scope — Extract Notes API to note-service

## Objective
Tách phân hệ Notes API từ `NotesCool` modulith sang repo `note-service` để tiến tới microservice architecture.

## In Scope
- `note-service` repo độc lập.
- `.NET 8` API service.
- Copy lean shared code, không dùng private NuGet.
- DB riêng `notes_db`, cùng PostgreSQL server.
- Auth: validate JWT từ Identity hiện tại, không issue token.
- API chuẩn `/api/v1/notes`.
- Expose upstream-ready API để service Kong riêng route vào.
- Standard response envelope.
- Unit/integration tests baseline.

## Out of Scope
- Tasks service extraction.
- Reminders service extraction.
- FE redesign.
- Kong gateway implementation/config repo.
- Cross-service linked notes/tasks behavior.
- Data migration unless product confirms required.

## Assumptions
- Identity vẫn do `NotesCool`/Identity service quản lý.
- JWT signing config có thể share an toàn qua env/secret.
- `note-service` bắt đầu từ Notes module hiện có.
- DB mới có thể start fresh nếu chưa có yêu cầu migrate data.
- Kong nằm service/repo khác; repo này không sở hữu gateway impl.

## User Flow
1. User login qua auth service.
2. User nhận JWT.
3. Client gọi endpoint Notes qua gateway.
4. Gateway route request tới `note-service`.
5. `note-service` validate JWT.
6. `note-service` lấy `ownerId` từ claims.
7. CRUD note chỉ trong phạm vi owner.

## Acceptance Criteria
See `docs/ba/note-service-testable-ac.md`.

## Risks
- JWT config lệch → toàn bộ Notes API fail auth.
- Gateway route sai → 502/404 dù service đúng.
- Response envelope đổi → FE cần update mapping.
- DB tách mới → cần quyết định migrate hay start fresh.

## Open Questions
- Data migration từ DB cũ sang `notes_db` có cần không?
- Key sharing: env var thuần hay secret manager?
- Cutover hard switch hay feature flag?
