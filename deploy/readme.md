
When configuring the PVK Meetups server, after installing postgres 14 or later, use the psql scripts in this directory to create, configure, and migrate the PVKMeetupDb before running the application.

1. sudo -u postgres psql -d postgres -f ./pvkmeetups-db-create.psql 
2. sudo -u postgres psql -d PvkMeetupDb -f ./pvkmeetups-db-configure.psql
3. sudo -u postgres psql -d PvkMeetupDb -f ./dbmigrate.psql
