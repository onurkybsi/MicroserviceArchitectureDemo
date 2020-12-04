const mongoose = require("mongoose");

const ProductSchema = mongoose.Schema({
  id: {
    type: Number,
    required: true,
    unique: true,
  },
  name: String,
  category: String,
  description: String,
  price: Number,
  photo: String,
});

module.exports = mongoose.model("product", ProductSchema, "product");
