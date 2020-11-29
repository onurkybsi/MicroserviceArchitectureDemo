const productCollection = require("../data/productCollection");

const getAllProducts = async () => await productCollection.find();

async function GetList(call, callback) {
  let products = await getAllProducts();

  callback(null, { products: products });
}

module.exports = { GetList };
