const winston = require("winston");
const { ElasticsearchTransport } = require("winston-elasticsearch");
const modelValidator = require("models-validator");

const LoggerSettings = modelValidator.createModel("LoggerSettings", {
  elasticUrl: "string",
  appName: "string",
});

const CONSOLE_TRANSPORT = new winston.transports.Stream({
  level: "info",
  stream: process.stderr,
});

const getTransports = (loggerSettings) => [
  CONSOLE_TRANSPORT,
  new ElasticsearchTransport(getElasticTransportOpts(loggerSettings)).on(
    "warning",
    (error) => {
      console.error("Error caught", error);
    }
  ),
];

const getElasticTransportOpts = (loggerSettings) => {
  return {
    level: "info",
    clientOpts: { node: loggerSettings.elasticUrl },
    index: `${loggerSettings.appName}-logs`,
  };
};

let _logger = undefined;
const CreateLogger = (loggerSettings) => {
  if (_logger === undefined) {
    validateLoggerSettings(loggerSettings);

    let transports = getTransports(loggerSettings);

    _logger = winston.createLogger({
      transports: transports,
    });
    configureLogger(_logger);
  }

  return _logger;
};

const validateLoggerSettings = (loggerSettings) => {
  let validationResult = LoggerSettings.validate(loggerSettings, false, true);

  if (!validationResult.isValid) {
    throw Error(loggerSettings.errorMessage);
  }
};

const configureLogger = (logger) => {
  logger.on("error", (error) => {
    console.error("Error caught", error);
  });
};

module.exports = {
  CreateLogger: CreateLogger,
};
