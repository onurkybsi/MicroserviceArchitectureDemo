const productCollection = require("../data/productCollection");

const getAllProducts = async () => {
  return await productCollection.find();
};

async function GetList(call, callback) {
  let products = await getAllProducts();

  callback(null, { products: products });
}

async function Insert(insertedList) {
  await productCollection.insertMany(insertedList);
}

module.exports = { GetList, Insert };
