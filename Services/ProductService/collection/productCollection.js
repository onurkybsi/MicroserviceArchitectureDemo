const mongoose = require("mongoose");

const ProductSchema = mongoose.Schema({
  id: mongoose.Schema.Types.ObjectId,
  name: String,
  category: String,
  description: String,
  price: Number,
  photo: String,
});

module.exports = mongoose.model("product", ProductSchema, "product");
