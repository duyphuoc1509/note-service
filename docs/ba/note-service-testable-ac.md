# Testable Acceptance Criteria — note-service extraction

## AC-EXT-01 — Service independence
**Given** `note-service` source is built and configured  
**When** operator starts `note-service` without `NotesCool.Api`  
**Then** `note-service` starts successfully and serves health/API endpoints independently.

## AC-EXT-02 — JWT validation works
**Given** a JWT issued by current Identity service with valid issuer/audience/signature  
**When** client calls `GET /api/v1/notes`  
**Then** `note-service` returns `200 OK` or valid business response, not `401` due to token validation failure.

## AC-EXT-03 — Unauthorized request blocked
**Given** no bearer token or invalid token  
**When** client calls any protected Notes endpoint  
**Then** `note-service` returns `401 Unauthorized` and does not expose note data.

## AC-EXT-04 — Ownership enforced
**Given** User A owns Note X and User B is authenticated  
**When** User B requests `GET/PATCH/DELETE /api/v1/notes/{id-of-note-x}`  
**Then** system returns `404` or `403` per chosen policy and does not expose Note X data.

## AC-EXT-05 — Create note contract
**Given** authenticated user submits valid payload to `POST /api/v1/notes`  
**When** request is processed  
**Then** system returns `201 Created` with envelope:
```json
{ "data": { "id": "...", "title": "...", "content": "...", "isFavorite": false, "isArchived": false, "createdAt": "...", "updatedAt": null } }
```

## AC-EXT-06 — List notes contract
**Given** authenticated user has notes  
**When** user calls `GET /api/v1/notes?page=1&pageSize=20`  
**Then** system returns `200 OK` with envelope:
```json
{
  "data": {
    "items": [],
    "page": { "number": 1, "size": 20, "totalItems": 0, "totalPages": 0 }
  }
}
```

## AC-EXT-07 — Default list excludes archived
**Given** authenticated user has active and archived notes  
**When** user calls default list endpoint without archived filter  
**Then** response includes only active notes.

## AC-EXT-08 — Detail contract
**Given** authenticated owner requests an existing active note  
**When** client calls `GET /api/v1/notes/{id}`  
**Then** system returns `200 OK` with correct note DTO in `data` envelope.

## AC-EXT-09 — Update contract
**Given** authenticated owner submits valid patch to `PATCH /api/v1/notes/{id}`  
**When** request is processed  
**Then** system returns `200 OK`, updated fields persist, `updatedAt` changes.

## AC-EXT-10 — Delete means soft archive
**Given** authenticated owner deletes an active note  
**When** client calls `DELETE /api/v1/notes/{id}`  
**Then** system returns `204 No Content`, note remains in DB as archived, note disappears from default list.

## AC-EXT-11 — Favorite toggle works
**Given** authenticated owner has an active note  
**When** client calls `PATCH /api/v1/notes/{id}/favorite`  
**Then** system returns `200 OK` with updated `isFavorite` state.

## AC-EXT-12 — Validation errors standardized
**Given** authenticated user sends invalid payload (empty title, title > 200)  
**When** request is processed  
**Then** system returns validation error in standard error envelope with stable `code`, human-readable `message`, `traceId`.

## AC-EXT-13 — DB isolation
**Given** deployment config is applied  
**When** service connects to database  
**Then** `note-service` uses `notes_db` and does not read/write Notes data in monolith DB.

## AC-EXT-14 — Gateway routing readiness
**Given** gateway service routes `/api/v1/notes/**` to `note-service`  
**When** request reaches `note-service`  
**Then** response contract is preserved and service does not depend on gateway-specific code.

## AC-EXT-15 — Monolith cleanup complete
**Given** extraction is complete  
**When** `NotesCool.Api` source is reviewed  
**Then** monolith no longer hosts Notes endpoints or Notes module wiring for public Notes API.

## AC-EXT-16 — Test baseline exists
**Given** repository CI/test commands are run  
**When** unit and integration tests execute  
**Then** tests covering create/list/detail/update/archive/ownership/auth pass.

## AC-EXT-17 — Container build works
**Given** Docker build is run in `note-service`  
**When** image build completes  
**Then** image is created successfully and container can start with env-based config.
