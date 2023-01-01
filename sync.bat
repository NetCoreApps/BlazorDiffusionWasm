RD /Q /S BlazorDiffusion.Client\Pages
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\Pages BlazorDiffusion.Client\Pages
DEL BlazorDiffusion.Client\Pages\*.cshtml

RD /Q /S BlazorDiffusion.Client\Shared
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\Shared BlazorDiffusion.Client\Shared

RD /Q /S BlazorDiffusion.Client\UI
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\UI BlazorDiffusion.Client\UI

RD /Q /S BlazorDiffusion.Client\wwwroot\img
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\wwwroot\img BlazorDiffusion.Client\wwwroot\img

COPY ..\BlazorDiffusion\BlazorDiffusion\wwwroot\js\*.* BlazorDiffusion.Client\wwwroot\js\
COPY ..\BlazorDiffusion\BlazorDiffusion\wwwroot\_index.html BlazorDiffusion\
REM powershell -Command "(Get-Content ..\BlazorDiffusion\BlazorDiffusion\wwwroot\_index.html) -replace 'blazor.server.js', 'blazor.webassembly.js' | Out-File -encoding ASCII BlazorDiffusion\_index.html"

COPY ..\BlazorDiffusion\BlazorDiffusion\tailwind.* BlazorDiffusion.Client\

RD /Q /S BlazorDiffusion\App_Data
MD BlazorDiffusion\App_Data

RD /Q /S BlazorDiffusion\proto
MD BlazorDiffusion\proto

MOVE BlazorDiffusion\Program.cs .
COPY ..\BlazorDiffusion\BlazorDiffusion\*.cs BlazorDiffusion\
DEL TrackingCircuitHandler.cs
MOVE Program.cs BlazorDiffusion\

REM COPY ..\BlazorDiffusion\BlazorDiffusion\appsettings.* BlazorDiffusion\
COPY ..\BlazorDiffusion\BlazorDiffusion\Migrations\*.cs BlazorDiffusion\Migrations\
COPY ..\BlazorDiffusion\BlazorDiffusion\proto\*.* BlazorDiffusion\proto\

RD /Q /S BlazorDiffusion\App_Data
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\App_Data BlazorDiffusion\App_Data

MOVE BlazorDiffusion.ServiceInterface\BlazorDiffusion.ServiceInterface.csproj .
RD /Q /S BlazorDiffusion.ServiceInterface
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion.ServiceInterface BlazorDiffusion.ServiceInterface
MOVE BlazorDiffusion.ServiceInterface.csproj BlazorDiffusion.ServiceInterface\

MOVE BlazorDiffusion.ServiceModel\BlazorDiffusion.ServiceModel.csproj .
RD /Q /S BlazorDiffusion.ServiceModel
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion.ServiceModel BlazorDiffusion.ServiceModel
MOVE BlazorDiffusion.ServiceModel.csproj BlazorDiffusion.ServiceModel\

MOVE BlazorDiffusion.Tests\BlazorDiffusion.Tests.csproj .
MOVE BlazorDiffusion.Tests\appsettings.json .
RD /Q /S BlazorDiffusion.Tests
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion.Tests BlazorDiffusion.Tests
MOVE BlazorDiffusion.Tests.csproj BlazorDiffusion.Tests\
MOVE appsettings.json BlazorDiffusion.Tests\

