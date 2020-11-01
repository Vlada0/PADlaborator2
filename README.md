# PAD-lab2 
## Overview
### Used technologies
- .NET Core 3 + C#
- MongoDB as the distributed database
- Redis as the distributed cache
- Postman as the testing tool

### Implementation
- Data Warehouse (API + DB node)
- Distributed MongoDB with 2 replica sets
- Proxy Middleware
- IPFilter Middleware for API project
- Load balancing (Proxy level)
- Caching using Redis (Proxy level)

### This repository contains two .NET Core projects:

- PADLab2_1part (Picture API)

- Proxy

  The first project is an API, which is responsible for interacting with the distributed database. This project contains some CRUD operations. 
  Data for this app is stored and distributed across a MongoDB cluster (a set of NoSQL DB nodes). 
  Description of API you can find in paragraph 1.6 of Report.

  The second project is responsible for requests forwarding, caching and load balancing.

### Installation
- Download Redis 3.0.504 https://github.com/microsoftarchive/redis/releases
- Download MongoDB https://www.mongodb.com/try/download/community
- Client for MongoDB https://www.mongodb.com/try/download/compass

### Usage
#### MongoDB replication
  For the initial configuration of MongoDB, after installation, you need to create an empty "data" folder and run the following command (in command line) in this folder:
```
  "C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe" --dbpath="data" --port 50002 --replSet "ReplicationName"
```

  Next, you need to open the mongo shell and run the following command:
```
  "C:\Program Files\MongoDB\Server\4.4\bin\mongo.exe" --port 50002 
```
  Then you must complete the following points in mongo shell:
```yaml
  rsconf = {
    _id: "ReplicationName",
    members: [
    {
      _id: 0,
      host: "localhost:50002"
    },
    {
      _id: 1,
      host: "localhost:50003"
    }
    ]
  }
```
and:

```yaml
rs.initiate(rsconf)
```
#### Redis
After downloading Redis, you need to run the redis-server.exe file. To test the connection open the redis-cli.exe file and execute the ping command. 
The answer should be PONG. 
The port to which the client connects is 6379.

#### Run the Project
To run the project you can use Visual Studio or .NET Core CLI. 
