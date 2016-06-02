rem NUnitのインストール先
set nunit_home=C:\Program Files (x86)\NUnit.org

rem OpenCoverのインストール先
set opencover_home=C:\Users\602293.JPN\AppData\Local\Apps\OpenCover

rem ReportGeneratorのインストール先
set reportgen_dir=C:\Work\Tools\ReportGenerator_2.4.5.0\bin

rem パスの設定
set path=%path%;%opencover_home%;%reportgen_dir%\

rem 実行するテストのアセンブリ
set target_test=STG_Test.dll

rem 実行するテストのアセンブリの格納先
set target_dir=C:\Work\Others\CS_Study\OOP_TDD_Study\STG\STG\Development\Source\STG_Test\bin\Test

rem OpenCoverの実行
OpenCover.Console -register:user -target:"%nunit_home%\nunit-console\nunit3-console.exe" -targetargs:"%target_test%"  -targetdir:"%target_dir%" -output:Result\result.xml -mergebyhash

rem レポートの生成
ReportGenerator "Result\result.xml" Result\html

rem レポートの表示
start Result\html\index.htm