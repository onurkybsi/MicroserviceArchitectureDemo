const grpc = require("grpc");
var protoLoader = require("@grpc/proto-loader");

const packageDefinition = protoLoader.loadSync(
  "../../ServiceConnectionBases/protos/connectioncheck.proto",
  {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true,
  }
);

const connectionCheck = grpc.loadPackageDefinition(packageDefinition)
  .connectioncheck;

function sayHello(call, callback) {
  console.log(call.request.name);
  callback(null, { message: "Hello from ProductService to ApiGateway !" });
}

function main() {
  const server = new grpc.Server();
  server.addService(connectionCheck.Greet.service, { sayHello: sayHello });
  server.bind("127.0.0.1:30051", grpc.ServerCredentials.createInsecure());
  server.start();
}

main();
