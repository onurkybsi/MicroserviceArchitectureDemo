const productCollection = require("../collection/productCollection");

const getAllProducts = async () => await productCollection.find();

async function GetList(call, callback) {
  let products = await getAllProducts();

  callback(null, { products: products });
}

//  To be developed
async function InsertMany(insertedList) {
  await productCollection.insertMany(insertedList);
}

module.exports = { GetList, InsertMany };
