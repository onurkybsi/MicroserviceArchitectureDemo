//#region Imports
const productCollection = require("../collection/productCollection");
const ResultMessage = require("../models/responseMessage");
//#endregion

const getAllProducts = async () => await productCollection.find();
let nextProductId = productCollection
  .findOne({ id: 1 })
  .sort('-LAST_MOD')
  .exec(function (err, doc) {
    return doc.last_mod;
  });
const getSuccessfulSaveMessage = (id) => `${id} saved !`;

async function GetById(call, callback) {
  let product = await productCollection.findOne({ id: call.request.id });

  callback(null, { product: product });
}

async function Save(call, callback) {
  let saveResponse = new ResultMessage();
  console.log(nextProductId);

  let updatedPersonFilter = { id: call.request.id };
  let currentProduct = call.request;
  let saveOptions = { upsert: true, new: true, setDefaultsOnInsert: true };

  let saveProductResult = await productCollection.findOneAndUpdate(
    updatedPersonFilter,
    { ...currentProduct, $inc: { id: 1 } },
    saveOptions
  );

  if (saveProductResult._doc.id > 0) {
    saveResponse.isSuccess = true;
    saveResponse.message = getSuccessfulSaveMessage(saveProductResult._doc.id);
  }

  callback(null, { saveResponse });
}

//  To be developed
async function InsertMany(insertedList) {
  await productCollection.insertMany(insertedList);
}

module.exports = { GetById, Save };
