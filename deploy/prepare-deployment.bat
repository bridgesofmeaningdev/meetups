REM run this script from a visual studio 2022 command line
REM intended to execute this script from the parent directory

cd ./src/Pvk.Meetups.Web/

REM dotnet ef migrations bundle --verbose --self-contained --force --no-build --target-runtime "ubuntu.20.04-x64" -o ../../deploy/dbupdate.exe
dotnet ef migrations script --verbose --idempotent --no-build -o ../../deploy/dbmigrate.psql

cd ../
dotnet publish -v normal --self-contained true --runtime "ubuntu.20.04-x64" -p:PublishSingleFile=true -o ../deploy/ubuntu/