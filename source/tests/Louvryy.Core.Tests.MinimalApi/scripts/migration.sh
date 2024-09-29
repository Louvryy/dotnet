#!/bin/bash

# Colors for better formatting
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # No color

if [ $# -ne 1 ]; then
    echo "Uso: $0 NomeDaMigracao"
    exit 1
fi

migrationName="$1"
migrationDirectory="Data/Migrations"

timestamp=$(date +'%Y%m%d%H%M%S')
migrationFileName="${timestamp}_${migrationName}.cs"

echo "using FluentMigrator;

namespace Louvryy.Core.Tests.MinimalApi.Data.Migrations;

[Migration($timestamp)]
public class $migrationName : Migration
{
    public const string TableName = \"TableName\";

    public override void Up()
    {
        //
    }

    public override void Down()
    {
        //
    }
}" > "$migrationDirectory/$migrationFileName"

echo -e ""
echo -e "${BLUE}MIGRATION CREATED${NC}"
echo -e "${GREEN}Success:${NC} Migration '$migrationName' has been created in '${BLUE}$migrationDirectory/$migrationFileName${NC}'."
