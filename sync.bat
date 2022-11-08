RD /Q /S BlazorDiffusion.Client\Pages
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\Pages BlazorDiffusion.Client\Pages
DEL BlazorDiffusion.Client\Pages\*.cshtml

RD /Q /S BlazorDiffusion.Client\Shared
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\Shared BlazorDiffusion.Client\Shared

RD /Q /S BlazorDiffusion.Client\UI
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion\UI BlazorDiffusion.Client\UI

COPY ..\BlazorDiffusion\BlazorDiffusion\tailwind.* BlazorDiffusion.Client\

RD /Q /S BlazorDiffusion\App_Data
MD BlazorDiffusion\App_Data

RD /Q /S BlazorDiffusion\proto
MD BlazorDiffusion\proto

MOVE BlazorDiffusion\Program.cs .
COPY ..\BlazorDiffusion\BlazorDiffusion\*.cs BlazorDiffusion\
DEL TrackingCircuitHandler.cs
MOVE Program.cs BlazorDiffusion\

COPY ..\BlazorDiffusion\BlazorDiffusion\appsettings.* BlazorDiffusion\
COPY ..\BlazorDiffusion\BlazorDiffusion\Migrations\*.cs BlazorDiffusion\Migrations\
COPY ..\BlazorDiffusion\BlazorDiffusion\App_Data\*.sqlite BlazorDiffusion\App_Data\
COPY ..\BlazorDiffusion\BlazorDiffusion\proto\*.* BlazorDiffusion\proto\

RD /Q /S BlazorDiffusion.ServiceInterface
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion.ServiceInterface BlazorDiffusion.ServiceInterface

RD /Q /S BlazorDiffusion.ServiceModel
XCOPY /Y /E /H /C /I ..\BlazorDiffusion\BlazorDiffusion.ServiceModel BlazorDiffusion.ServiceModel

