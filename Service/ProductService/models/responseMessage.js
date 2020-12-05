function ResponseMessage(isSuccess, message) {
  this.isSuccess = isSuccess === undefined ? false : isSuccess;
  this.message = message === undefined ? "" : message;
}

module.exports = ResponseMessage;
