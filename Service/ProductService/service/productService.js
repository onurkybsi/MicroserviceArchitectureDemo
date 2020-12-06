//#region Imports
const { get } = require("mongoose");
const productCollection = require("../collection/productCollection");
const ResultMessage = require("../models/responseMessage");
const Helper = require("./helper");
//#endregion

//#region Private variables
let lastId = 0;
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

const setSaveResponseAsSuccessful = (saveResponse, id) => {
  saveResponse.isSuccess = true;
  saveResponse.message = `${id} saved !`;
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

      setSaveResponseAsSuccessful(saveResponse, insertResult.insertedProduct);
    } else {
      let updateResult = await updateProduct(call.request);

      setSaveResponseAsSuccessful(saveResponse, updateResult.updatedProduct);
    }
  } catch {
    saveResponse.message = EXTERNAL_ERR;
  }
  callback(null, { serviceProcessResult: saveResponse });
};
//#endregion

module.exports = { GetById, GetListByQuery, Save };
