//#region Imports
const grpc = require("grpc");
const protoLoader = require("@grpc/proto-loader");

const mongoose = require("mongoose");
require("dotenv").config({
  path: `${__dirname}/${process.env.ENVIRONMENT}.env`,
});

const productService = require("./service/productService");
//#endregion

const packageDefinition = protoLoader.loadSync(
  `${__dirname}/${process.env.PROTO_FILE_PATH}`,
  {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true,
  }
);

const connectToMongo = async (dbUrl) => {
  await mongoose.connect(dbUrl, {
    useNewUrlParser: true,
    useUnifiedTopology: true,
  });
};

const registerServices = (server) => {
  const grpcPackage = grpc.loadPackageDefinition(packageDefinition).product;

  server.addService(grpcPackage.ProductService.service, productService);
};

async function main() {
  console.log(`ProductService running on: ${process.env.ENVIRONMENT}`);
  
  await connectToMongo(process.env.PRODUCTDB_CONNECTION_STRING);

  // const server = new grpc.Server();
  // registerServices(server);

  // server.bind(process.env.SERVICE_URL, grpc.ServerCredentials.createInsecure());
  // server.start();
}

main();