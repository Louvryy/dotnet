#!/bin/bash

#start SQL Server, start the script to create the DB, start the app and Execute Continous
/opt/mssql/bin/sqlservr & /scripts/setup-db.sh & tail -f /dev/null