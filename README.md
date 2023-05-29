Olá!

Siga as instruções abaixo para garantir que o projeto rode localmente:

Ajuste a conecctionString do arquivo "appSettings.json" da API. Utilize uma instancia de MS SQL server local ou remoto.
ex: "ApplicationDb": "Server=(localdb)\\MSSQLLocalDB;Database=ClockInClockOut_Dev;Trust Server Certificate=true;MultiSubnetFailover=False;Trust Server Certificate=true;" // <- Use your cnx string here!

Após alterar a cnx string, defina o projeto "ClockInClockOut_API" como projeto default (botão direito do mouse e selecione a opção "set as default project").

De play no projeto.

Após iniciar o projeto, a interface do projeto swagger ira aparecer, faça a autenticação com o usuário "seed", usuario "test" senha "test123". O response do endpoint de autenticação irá retornar um token, utilize esse token nas demais requisições.


