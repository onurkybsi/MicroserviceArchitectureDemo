const winston = require("winston");
const { ElasticsearchTransport } = require("winston-elasticsearch");

const esTransportOpts = {
  level: "info",
  clientOpts: { node: "http://localhost:9200" },
  indexSuffixPattern: "product-service",
};

const esTransport = new ElasticsearchTransport(esTransportOpts);

const logger = winston.createLogger({
  transports: [esTransport],
});

// Compulsory error handling
logger.on("error", (error) => {
  console.error("Error caught", error);
});
esTransport.on("warning", (error) => {
  console.error("Error caught", error);
});

module.exports = {
  Logger: logger,
};
