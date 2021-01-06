//#region Imports
const grpc = require("grpc");
const protoLoader = require("@grpc/proto-loader");

const mongoose = require("mongoose");
require("dotenv").config({
  path: `${__dirname}/${
    process.env.ENVIRONMENT === undefined ? "dev" : process.env.ENVIRONMENT
  }.env`,
});

const productService = require("./service/productService");
const LogService = require("./service/logService");
//#endregion

const Logger = LogService.CreateLogger({
  elasticUrl: process.env.ELASTICSEARCH_URL,
  appName: process.env.APP_NAME,
});

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
    useCreateIndex: true,
    useNewUrlParser: true,
    useUnifiedTopology: true,
    useFindAndModify: false,
  });
};

const registerServices = (server) => {
  const grpcPackage = grpc.loadPackageDefinition(packageDefinition).service;

  server.addService(grpcPackage.ProductService.service, productService);
};

async function main() {
  Logger.info(`ProductService running on: ${process.env.ENVIRONMENT}`, {});

  await connectToMongo(process.env.PRODUCTDB_CONNECTION_STRING);

  const server = new grpc.Server();
  registerServices(server);

  server.bind(process.env.SERVICE_URL, grpc.ServerCredentials.createInsecure());
  server.start();
}

main();
