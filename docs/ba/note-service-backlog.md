# Backlog — note-service extraction

## Git workflow
- Base branch: `dev`
- All work via PR into `dev`
- Do not merge to `main`

---

## BE-01 — Scaffold note-service repo + solution structure
**Owner:** BE  
**Labels:** backend, phase-1, priority-high

### Deliverables
- `NoteService.sln`
- Projects: `NoteService.Api`, `NoteService.Domain`, `NoteService.Application`, `NoteService.Infrastructure`, `NoteService.Contracts`, `NoteService.Shared`
- Test projects: `NoteService.UnitTests`, `NoteService.IntegrationTests`
- Basic health endpoint

### Acceptance
- `dotnet build` succeeds
- API starts on port 8080
- Health endpoint responds 200

---

## BE-02 — Copy lean shared code
**Owner:** BE  
**Labels:** backend, phase-2, priority-high

### Scope
Copy only:
- `Entity.cs`
- `PagedResult.cs`
- `ApiException.cs`
- `ICurrentUser.cs`

### Acceptance
- Namespaces updated to `NoteService.Shared.*`
- No dependency/reference to `NotesCool.Shared`

---

## BE-03 — Port Notes domain/application/contracts/infrastructure
**Owner:** BE  
**Labels:** backend, phase-2, priority-high

### Source → Target
- `NotesCool.Notes/Domain/Note.cs` → `NoteService.Domain`
- `NotesCool.Notes/Application/NotesService.cs` → `NoteService.Application`
- `NotesCool.Notes/Contracts/NoteDtos.cs` → `NoteService.Contracts`
- `NotesCool.Notes/Infrastructure/NotesDbContext.cs` → `NoteService.Infrastructure`

### Acceptance
- Domain rules preserved
- Build passes
- No plugin architecture

---

## BE-04 — Configure `notes_db` + EF migrations
**Owner:** BE  
**Labels:** backend, database, phase-3, priority-high

### Requirements
- Same Postgres server, separate DB: `notes_db`
- Env-based connection string
- Initial migration creates `notes` table

### Acceptance
- AC-EXT-13 passes

---

## BE-05 — Configure JWT authentication middleware
**Owner:** BE  
**Labels:** backend, security, phase-3, priority-high

### Requirements
- Validate JWT from Identity service
- Do not issue tokens
- Read JWT issuer/audience/key from env/appsettings
- Notes endpoints protected

### Acceptance
- AC-EXT-02, AC-EXT-03 pass

---

## BE-06 — Map `/api/v1/notes` endpoints + envelope
**Owner:** BE  
**Labels:** backend, api, phase-4, priority-high

### Endpoints
```text
POST   /api/v1/notes
GET    /api/v1/notes
GET    /api/v1/notes/{id}
PATCH  /api/v1/notes/{id}
DELETE /api/v1/notes/{id}
PATCH  /api/v1/notes/{id}/favorite
```

### Acceptance
- AC-EXT-05 through AC-EXT-12 pass

---

## BE-07 — Standard error envelope
**Owner:** BE  
**Labels:** backend, api, phase-4, priority-medium

### Error format
```json
{
  "error": {
    "code": "note_not_found",
    "message": "Note was not found.",
    "traceId": "00-abc123"
  }
}
```

### Acceptance
- AC-EXT-12 passes

---

## BE-08 — Dockerfile + compose readiness
**Owner:** BE  
**Labels:** backend, devops, phase-5, priority-medium

### Requirements
- Multi-stage Dockerfile
- Port 8080
- Health check
- Env vars for DB/JWT

### Acceptance
- AC-EXT-17 passes

---

## BE-09 — Gateway readiness, no Kong implementation here
**Owner:** BE  
**Labels:** backend, integration, phase-5, priority-medium

### Description
Ensure service can be routed by external Kong gateway service. Do not implement Kong config in this repo.

### Acceptance
- Paths stable under `/api/v1/notes/**`
- `strip_path: false` compatible
- Service has no dependency on gateway-specific code
- AC-EXT-14 passes once gateway team configures route

---

## BE-10 — Remove Notes module from NotesCool monolith
**Owner:** BE  
**Repo:** `NotesCool`  
**Labels:** backend, cleanup, phase-6, priority-medium

### Acceptance
- AC-EXT-15 passes
- Auth/Tasks/Reminders still work

---

## QC-01 — Test plan for note-service extraction
**Owner:** QC  
**Labels:** qa, testing, priority-high

### Scope
Validate all AC in `docs/ba/note-service-testable-ac.md`.

### Acceptance
- AC-EXT-16 passes
- Test evidence covers auth, CRUD, ownership, archive, DB isolation, Docker readiness
