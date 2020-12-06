//#region Imports
const { get } = require("mongoose");
const productCollection = require("../collection/productCollection");
const ResultMessage = require("../models/responseMessage");
const Helper = require("./helper");
//#endregion

//#region Private variables
let lastId = 0;
const SAVE_TRANSACTION = "SAVE_TRANSACTION";
const DELETE_TRANSACTION = "DELETE_TRANSACTION";
// TO-DO: Bunları tüm uygulamalar kullanacak. Kapsayan bir JSON da tutalım.
const EXTERNAL_ERR = "EXTERNAL_ERR";
const INTERNAL_ERR = "INTERNAL_ERR";
//#endregion

//#region Private methods
const getLastIdInDb = async () => {
  let lastIdInDb = await productCollection
    .findOne({})
    .sort({ id: -1 })
    .select("id");

  return lastIdInDb["id"];
};

const getFilterForGetListByQuery = (product) => {
  let queryFilter = {};

  for (let prop in product) {
    if (!Helper.HasDefaultValue(product[prop])) {
      queryFilter[prop] = product[prop];
    }
  }

  return queryFilter;
};

const setTransactionResponseAsSuccessful = (
  transactionResponse,
  id,
  transactionType
) => {
  transactionResponse.isSuccess = true;
  transactionResponse.message = `${id} ${
    transactionType === SAVE_TRANSACTION ? "saved !" : "deleted !"
  }`;
};

const getNextId = async () => {
  if (lastId === 0) {
    lastId = (await getLastIdInDb()) + 1;
  } else {
    lastId = lastId + 1;
  }

  return lastId;
};

const insertProduct = async (newDoc) => {
  let insertResult = await productCollection.create(newDoc);

  if (insertResult._id === undefined || insertResult._id === null) {
    throw new Error(EXTERNAL_ERR);
  }

  return {
    isSucces: true,
    insertedProduct: insertResult._doc["id"],
  };
};

const updateProduct = async (doc) => {
  let updateResult = await productCollection.updateOne({ id: doc.id }, doc);

  if (updateResult.nModified !== 1 || updateResult.ok !== 1) {
    throw new Error(EXTERNAL_ERR);
  }

  return {
    isSucces: true,
    updatedProduct: doc.id,
  };
};
//#endregion

//#region Public methods
const GetById = async (call, callback) => {
  let getByIdProcessResult = new ResultMessage(true);
  let product = null;

  try {
    product = await productCollection.findOne({ id: call.request.id });
  } catch {
    getByIdProcessResult.isSuccess = false;
    getByIdProcessResult.message = EXTERNAL_ERR;
  }

  callback(null, {
    serviceProcessResult: getByIdProcessResult,
    product: product,
  });
};

const GetListByQuery = async (call, callback) => {
  let getListByQueryProcessResult = new ResultMessage(true);
  let products = null;

  let queryFilter = getFilterForGetListByQuery(call.request.query);

  try {
    products = await productCollection.find(queryFilter);
  } catch {
    getListByQueryProcessResult.isSuccess = false;
    getListByQueryProcessResult.message = EXTERNAL_ERR;
  }

  callback(null, {
    serviceProcessResult: getListByQueryProcessResult,
    products: products,
  });
};

const Save = async (call, callback) => {
  let saveResponse = new ResultMessage(false, INTERNAL_ERR);

  try {
    if (call.request.id <= 0) {
      call.request.id = await getNextId();

      let insertResult = await insertProduct(call.request);

      setTransactionResponseAsSuccessful(
        saveResponse,
        insertResult.insertedProduct,
        SAVE_TRANSACTION
      );
    } else {
      let updateResult = await updateProduct(call.request);

      setTransactionResponseAsSuccessful(
        saveResponse,
        updateResult.updatedProduct,
        SAVE_TRANSACTION
      );
    }
  } catch {
    saveResponse.message = EXTERNAL_ERR;
  }
  callback(null, { serviceProcessResult: saveResponse });
};

const Delete = async (call, callback) => {
  let deleteResponse = new ResultMessage(false, INTERNAL_ERR);

  try {
    let deleteResult = await productCollection.deleteOne({
      id: call.request.id,
    });

    if (deleteResult.deletedCount === 1 || deleteResult.ok === 1) {
      setTransactionResponseAsSuccessful(
        deleteResponse,
        call.request.id,
        DELETE_TRANSACTION
      );
    }
  } catch {
    deleteResponse.message = EXTERNAL_ERR;
  }

  callback(null, {
    serviceProcessResult: deleteResponse,
  });
};
//#endregion

module.exports = { GetById, GetListByQuery, Save, Delete };
