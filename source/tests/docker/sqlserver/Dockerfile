FROM mcr.microsoft.com/mssql/server:2022-latest

# USER root
USER root

# Create app directory
RUN mkdir -p /scripts
WORKDIR /scripts

# Bundle app source
COPY . /scripts

# Grant permissions for the import-data script to be executable
RUN chmod +x /scripts/setup-db.sh

EXPOSE 1433

# USER mssql
ENTRYPOINT /bin/sh ./entrypoint.sh