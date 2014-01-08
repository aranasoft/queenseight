@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

:: ----------------------
:: KUDU Deployment Script
:: Version: 0.1.5
:: ----------------------

:: Prerequisites
:: -------------

:: Verify node.js installed
where node 2>nul >nul
IF %ERRORLEVEL% NEQ 0 (
  echo Missing node.js executable, please install node.js, if already installed make sure it can be reached from current environment.
  goto error
)

:: Setup
:: -----

setlocal enabledelayedexpansion

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT_SOURCE (
  SET DEPLOYMENT_SOURCE=%~dp0%.
)

IF NOT DEFINED DEPLOYMENT_TARGET (
  SET DEPLOYMENT_TARGET=%ARTIFACTS%\wwwroot
)

IF NOT DEFINED NEXT_MANIFEST_PATH (
  SET NEXT_MANIFEST_PATH=%ARTIFACTS%\manifest

  IF NOT DEFINED PREVIOUS_MANIFEST_PATH (
    SET PREVIOUS_MANIFEST_PATH=%ARTIFACTS%\manifest
  )
)

IF NOT DEFINED KUDU_SYNC_CMD (
  :: Install kudu sync
  echo Installing Kudu Sync
  call npm install kudusync -g --silent
  IF !ERRORLEVEL! NEQ 0 goto error

  :: Locally just running "kuduSync" would also work
  SET KUDU_SYNC_CMD=node "%appdata%\npm\node_modules\kuduSync\bin\kuduSync"
)
IF NOT DEFINED DEPLOYMENT_TEMP (
  SET DEPLOYMENT_TEMP=%temp%\___deployTemp%random%
  SET CLEAN_LOCAL_DEPLOYMENT_TEMP=true
)

IF DEFINED CLEAN_LOCAL_DEPLOYMENT_TEMP (
  IF EXIST "%DEPLOYMENT_TEMP%" rd /s /q "%DEPLOYMENT_TEMP%"
  mkdir "%DEPLOYMENT_TEMP%"
)

IF NOT DEFINED MSBUILD_PATH (
  SET MSBUILD_PATH=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
)

IF NOT DEFINED NUGET_EXE (
  SET NUGET_EXE=%DEPLOYMENT_SOURCE%\.nuget\nuget.exe
)

goto Deployment

:: Utility Functions
:: -----------------

:SelectNodeVersion

IF DEFINED KUDU_SELECT_NODE_VERSION_CMD (
  :: The following are done only on Windows Azure Websites environment
  call %KUDU_SELECT_NODE_VERSION_CMD% "%DEPLOYMENT_SOURCE%" "%DEPLOYMENT_TARGET%" "%DEPLOYMENT_TEMP%"
  IF !ERRORLEVEL! NEQ 0 goto error

  IF EXIST "%DEPLOYMENT_TEMP%\__nodeVersion.tmp" (
    SET /p NODE_EXE=<"%DEPLOYMENT_TEMP%\__nodeVersion.tmp"
    IF !ERRORLEVEL! NEQ 0 goto error
  )
  
  IF EXIST "%DEPLOYMENT_TEMP%\__npmVersion.tmp" (
    SET /p NPM_JS_PATH=<"%DEPLOYMENT_TEMP%\__npmVersion.tmp"
    IF !ERRORLEVEL! NEQ 0 goto error
  )

  IF NOT DEFINED NODE_EXE (
    SET NODE_EXE=node
  )

  SET NPM_CMD="!NODE_EXE!" "!NPM_JS_PATH!"
) ELSE (
  SET NPM_CMD=npm
  SET NODE_EXE=node
)

goto :EOF

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:: Deployment
:: ----------

:Deployment
echo Handling .NET Web Application deployment.

ECHO 1. Restore NuGet packages
IF /I "QueensEight.sln" NEQ "" (
  call "%NUGET_EXE%" restore "%DEPLOYMENT_SOURCE%\QueensEight.sln"
  IF !ERRORLEVEL! NEQ 0 goto error
)

ECHO 2. Build to the temporary path
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  %MSBUILD_PATH% "%DEPLOYMENT_SOURCE%\API\API.csproj" /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%DEPLOYMENT_TEMP%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%DEPLOYMENT_SOURCE%\" %SCM_BUILD_ARGS%
) ELSE (
  %MSBUILD_PATH% "%DEPLOYMENT_SOURCE%\API\API.csproj" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%DEPLOYMENT_SOURCE%\" %SCM_BUILD_ARGS%
)

IF !ERRORLEVEL! NEQ 0 goto error

echo Handling node.js deployment.

pushd Web

ECHO 1. Select node version
call :SelectNodeVersion

ECHO 2. Install npm packages
IF EXIST "package.json" (
  call !NPM_CMD! install
  IF !ERRORLEVEL! NEQ 0 goto error
)

ECHO 3. Install bower packages
IF EXIST "bower.json" (
  call !NPM_CMD! install bower
  IF !ERRORLEVEL! NEQ 0 goto error
  call .\node_modules\.bin\bower install
  IF !ERRORLEVEL! NEQ 0 goto error
)

ECHO 4. Run Lineman Tasks
IF EXIST "Gruntfile.js" (
  call !NPM_CMD! install lineman
  IF !ERRORLEVEL! NEQ 0 goto error
  call .\node_modules\.bin\lineman clean build
  IF !ERRORLEVEL! NEQ 0 goto error
)

popd

ECHO 5. KuduSync
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call %KUDU_SYNC_CMD% -v 50 -f "%DEPLOYMENT_SOURCE%\Web\dist" -t "%DEPLOYMENT_TEMP%" -n "%DEPLOYMENT_SOURCE%\Web\Generated\manifest" -p "%DEPLOYMENT_SOURCE%\Web\Generated\manifest" -i ".git;.hg;.deployment;deploy.cmd"
  IF !ERRORLEVEL! NEQ 0 goto error
)

:: KuduSync
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call %KUDU_SYNC_CMD% -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
  IF !ERRORLEVEL! NEQ 0 goto error
)


::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

:: Post deployment stub
call %POST_DEPLOYMENT_ACTION%
IF !ERRORLEVEL! NEQ 0 goto error

goto end

:error
echo An error has occurred during web site deployment.
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
echo Finished successfully.
