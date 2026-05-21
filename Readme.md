# RequestService

## Запуск postgreSQL в docker
```bash
docker compose up -d --build
```
## Применить миграции:
```
dotnet ef database update --project RequestService.Infrastructure --startup-project RequestService.API
```