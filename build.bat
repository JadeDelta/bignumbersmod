@echo off
setlocal

set CSC_PATH=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe
set OUT=GameScaling.dll
set SRC=src\GameScaling.cs

set REFS=refs\mscorlib.dll;refs\System.dll;refs\System.Core.dll;refs\netstandard.dll;refs\UnityEngine.dll;refs\UnityEngine.CoreModule.dll;refs\Assembly-CSharp.dll;refs\BepInEx.dll;refs\0Harmony.dll;refs\PhotonUnityNetworking.dll


"%CSC_PATH%" /target:library /out:%OUT% /nostdlib+ /noconfig /reference:%REFS% %SRC%

if exist %OUT% (
    echo.
    echo ✅ Build successful! Output: %OUT%
) else (
    echo.
    echo ❌ Build failed.
)

pause
