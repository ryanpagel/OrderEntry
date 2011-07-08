*****
7/7/2011
Upgrade Notes:
1. All server data must be under one path including:
	-Pending Files direcotry (Pending Files)
	-Sales Items directory

[ServerDataRootDir]/Pending Files
[ServerDataRootDir]/QBSalesItems/SalesItems.xml

2. The config file will need to be updated on all clients to no longer point at the PendingFiles dir but rather the ServerDataRootDir.


