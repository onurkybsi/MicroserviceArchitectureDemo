//#region Imports
const productCollection = require("../collection/productCollection");
const ResultMessage = require("../models/responseMessage");
//#endregion

const getAllProducts = async () => await productCollection.find();
const getSuccessfulSaveMessage = (id) => `${id} saved !`;

async function GetById(call, callback) {
  let product = await productCollection.findOne({ id: call.request.id });

  callback(null, { product: product });
}

async function Save(call, callback) {
  let saveResponse = new ResultMessage();

  let updatedPersonFilter = { id: call.request.id };
  let currentProduct = call.request;
  let saveOptions = { upsert: true, new: true, setDefaultsOnInsert: true };

  let savedProduct = await productCollection.findOneAndUpdate(
    updatedPersonFilter,
    currentProduct,
    saveOptions
  );

  if (savedProduct.id > 0) {
    saveResponse.isSuccess = true;
    saveResponse.message = getSuccessfulSaveMessage(savedProduct.id);
  }

  callback(null, { saveResponse });
}

//  To be developed
async function InsertMany(insertedList) {
  await productCollection.insertMany(insertedList);
}

module.exports = { GetById, Save };
