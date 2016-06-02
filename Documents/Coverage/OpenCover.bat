rem NUnit�̃C���X�g�[����
set nunit_home=C:\Program Files (x86)\NUnit.org

rem OpenCover�̃C���X�g�[����
set opencover_home=C:\Users\602293.JPN\AppData\Local\Apps\OpenCover

rem ReportGenerator�̃C���X�g�[����
set reportgen_dir=C:\Work\Tools\ReportGenerator_2.4.5.0\bin

rem �p�X�̐ݒ�
set path=%path%;%opencover_home%;%reportgen_dir%\

rem ���s����e�X�g�̃A�Z���u��
set target_test=STG_Test.dll

rem ���s����e�X�g�̃A�Z���u���̊i�[��
set target_dir=C:\Work\Others\CS_Study\OOP_TDD_Study\STG\STG\Development\Source\STG_Test\bin\Test

rem OpenCover�̎��s
OpenCover.Console -register:user -target:"%nunit_home%\nunit-console\nunit3-console.exe" -targetargs:"%target_test%"  -targetdir:"%target_dir%" -output:Result\result.xml -mergebyhash

rem ���|�[�g�̐���
ReportGenerator "Result\result.xml" Result\html
pause