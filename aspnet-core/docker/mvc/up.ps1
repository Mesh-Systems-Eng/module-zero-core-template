############################################################################################################################################################################
##
## If running a local SQL Server database, the IP Address is needed rather than the name unless set in the Host file.
## Additionally, if running a local database in a container, the port, if not 1433, will need to be specified along with
## a database user and password. The following can be used to obtain the localhost's IP Address that is used as the
## database server that is then set as an environment variable used by the container.
##
## The docker-compose.yml file will need updated uncommenting the following:
##     # ConnectionStrings__Default: "${App_DB_ConnStr}"
##
## The following is not needed if the database is set in the appsettings.json:
##  * referencing a remote database
##  * the local database is referred to via IP address
##  * the local database is referred to by name that is set in the Host file associated with the local IP address
##
############################################################################################################################################################################
#
## This obtains the first IP when the machine is connected.
# $PrimaryIP = (Get-NetRoute -DestinationPrefix 0.0.0.0/0 | Get-NetIPConfiguration).IPv4Address.IPAddress | Select-Object -First 1
# echo "Primary IP Address: $PrimaryIP"
#
# $DatabaseName = "AbpProjectNameDb"
#
# $DatabasePort = "1433"
# $DatabaseUser = ""
# $DatabasePassword = ""
#
# $env:App_DB_ConnStr = "Server=$PrimaryIP, $DatabasePort; Database=$DatabaseName; User Id=$DatabaseUser;Password=$DatabasePassword; TrustServerCertificate=True;"
# echo "Database ConnectionString: $env:App_DB_ConnStr"
#
############################################################################################################################################################################

echo "Starting the container..."
docker-compose up -d
