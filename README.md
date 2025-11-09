# web_shop

Glavne tehnologije:
- .NET 8
- Microsoft SQL Server (najbolje 2022)

Startup projekt:
- GrbusWebShop.WebApi

Swagger UI: https://localhost:44336/swagger/index.html

SQL baza: unutar foldera \Grbus.WebShop\Grbus.WebShop.Infrastructure\Migrations su dvije skripte
- CreateDatabase.sql (treba prepraviti putanju za .mdf i .ldf datoteke, linije 8 i 10)
- Migrations.sql (migracijska skripta za kreiranje tablica)

Lokalno logiranje
- U \Grbus.WebShop\GrbusWebShop\WebApi je NLog.config u kojemu se mogu podesiti putanje na lokalnom računalu za log datoteke

Authentikacija: OAuth2
- Azure Entra ID (client credentials flow) - za API pozive
- Google (Authorization Code Flow, registrirano za https://localhost:44336). Trebalo bi još napraviti web frontend da bi radilo kako treba. Da se vidi kako bi izgledalo može se za sad u browseru otići na https://localhost:44336/api/products i browser bi se trebao preusmjeriti na accounts.google.com
