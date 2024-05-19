### сделать миграцию:

1. встаем в корень
2. пишем в терминале: dotnet ef migrations add <название миграции> --project IotRemoteLab.Persistence -s IotRemoteLab.API --output-dir PostgresMigrations
3. готово

### обновить бд:
1. если чото в базе не хочет ставится, можно удалить volume в докере и перезапустить контейнер (удалить - запустить)
2. встаем в корень пишем: dotnet ef database update -p .\IotRemoteLab.API\
3. если не работает, пишем тому, кто писал последнюю миграцию, если у него все ок делаем первый пункт
4. готово


### поднять базу данных:

1. если нет на компе, скачиваем docker desktop
2. пишем docker compose up в корне
3. готово