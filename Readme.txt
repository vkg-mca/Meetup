Scaffold-DbContext "Data Source=.\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f


<TargetFrameworks>netstandard2.0;netcoreapp2.2</TargetFrameworks>

dotnet tool install -g --version 2.2.0-* --add-source https://dotnet.myget.org/F/dotnet-core/api/v3/index.json dotnet-httprepl

dotnet tool list -g

dotnet httprepl
set base https://api.github.com
help
cls
get
get emojis
type in browser https://assets-cdn.github.com/images/icons/emoji/unicode/1f4a4.png?v8

set base http://localhost:3987
get api/values

Add httprepl from C:\Users\user\.dotnet\tools\dotnet-httprepl.exe
browse with HTTP REPL
get api/values
ui

setting a default editor for creating post data for http repl
pref set editor.command.default "C:\Users\user\AppData\Local\Programs\Microsoft VS Code\code.exe"

change segment to api/meetupdetails
cd api/meetupdetails

construct the port request
1. copy the post contents from swagger post test
2. enter in http repl => post -h Content-Type:application/json 
3. chnage the contents and press ctrl+s to save the data
4. close VS code
