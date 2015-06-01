@echo off

@echo %~dp0

"%~dp0\gacutil" -u DevExpress.XtraEditors.v10.2
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v10.2\10.2.4.0__b88d1754d700e49a
copy "%~dp0\DevExpress.XtraEditors.v10.2.dll" %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v10.2\10.2.4.0__b88d1754d700e49a


"%~dp0\gacutil" -u DevExpress.XtraNavBar.v10.2
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraNavBar.v10.2\10.2.4.0__b88d1754d700e49a
copy "%~dp0\DevExpress.XtraNavBar.v10.2.dll" %windir%\assembly\GAC_MSIL\DevExpress.XtraNavBar.v10.2\10.2.4.0__b88d1754d700e49a


"%~dp0\gacutil" -u DevExpress.Data.v10.2
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Data.v10.2\10.2.4.0__b88d1754d700e49a
copy "%~dp0\DevExpress.Data.v10.2.dll" %windir%\assembly\GAC_MSIL\DevExpress.Data.v10.2\10.2.4.0__b88d1754d700e49a

"%~dp0\gacutil" -u DevExpress.Utils.v10.2
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Utils.v10.2\10.2.4.0__b88d1754d700e49a
copy "%~dp0\DevExpress.Utils.v10.2.dll" %windir%\assembly\GAC_MSIL\DevExpress.Utils.v10.2\10.2.4.0__b88d1754d700e49a

"%~dp0\gacutil" -u DevExpress.CodeRush.Common
mkdir %windir%\assembly\GAC_MSIL\DevExpress.CodeRush.Common\10.2.4.0__35c9f04b7764aa3d
copy "%~dp0\DevExpress.CodeRush.Common.dll" %windir%\assembly\GAC_MSIL\DevExpress.CodeRush.Common\10.2.4.0__35c9f04b7764aa3d
REM copy "%~dp0\DevExpress.CodeRush.Common.dll" "C:\Program Files\DevExpress 2010.2\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll"
if "[%ProgramFiles(x86)%]" == "[]" (copy "%~dp0\DevExpress.CodeRush.Common.dll" "%ProgramFiles%\DevExpress 2010.2\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll") else (copy "%~dp0\DevExpress.CodeRush.Common.dll" "%ProgramFiles(x86)%\DevExpress 2010.2\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll")

echo 'OK'
