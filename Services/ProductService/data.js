const faker = require("faker");
faker.seed(100);

let categories = ["Books", "Electronics", "Music, CDs & Vinyl", "Sports & Outdors"];

let photoUrls = {
  "Books" : [
    "https://m.media-amazon.com/images/I/81-349iYbfL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71N4oeWwYlL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/91SvDEfN8GL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/91mePFAgywL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71C7PBhlB9L._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/411nhI-kPYL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/816vxVXANgL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/41cSJI7PfHL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/91KA1B7xWsL._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/81a4kCNuH+L._AC_UY327_QL65_.jpg"
  ],
  "Electronics": [
    "https://m.media-amazon.com/images/I/81Wx7hw9vwL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71dqjxW8g5L._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71k45hZkLmL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71pC69I3lzL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/61k5+dpy0yL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71ZIpEqxYFL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71c7GQvm79L._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71Cg0EbAWQL._AC_UY327_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/61S0sV1a57L._AC_UY327_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71SROBb4fCL._AC_UY327_QL65_.jpg"
  ],
  "Music, CDs & Vinyl": [
    "https://images-na.ssl-images-amazon.com/images/I/71YexWNNbHL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/81BROAmi6KL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/81ESohUha%2BL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/81CP1j-zprL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/81kWFwFPVdL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/51WC3YEANSL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/61ig5fK3-JL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/71ppskeiZgL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/71o%2BixK0coL._SX425_.jpg",
    "https://images-na.ssl-images-amazon.com/images/I/51QBqqMxyoL._SX425_.jpg"
  ],
  "Sports & Outdors": [
    "https://m.media-amazon.com/images/I/61zKUmCY0DL._AC_UL480_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/81ws75mUGxL._AC_UL480_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/61W7g13rV6L._AC_UL480_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/81E3+MlUF+L._AC_UL480_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/81IGqpXdnqL._AC_UL480_FMwebp_QL65_.jpg",
    "https://m.media-amazon.com/images/I/410Ln3GvsKL._AC_UL480_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71s23AwYLsL._AC_UL480_QL65_.jpg",
    "https://m.media-amazon.com/images/I/71HmL4kzLrL._AC_UL480_QL65_.jpg",
    "https://m.media-amazon.com/images/I/51vJe512YKL._AC_UL480_QL65_.jpg",
    "https://m.media-amazon.com/images/I/61GFa0iEx+L._AC_UL480_QL65_.jpg"
  ]
}

let products = [];

for (let i = 1; i <= 500; i++) {
  let category = faker.helpers.randomize(categories);
  products.push({
    name: faker.commerce.productName(),
    category: category,
    description: `${category}: ${faker.lorem.sentence(3)}`,
    price: Number(faker.commerce.price()),
    photo: photoUrls[category][faker.random.number(10)]
  });
}

//#region Orders
// let orders = [];
// for (let i = 1; i <= 100; i++) {
//   let fname = faker.name.firstName();
//   let sname = faker.name.lastName();
//   let order = {
//     id: i,
//     name: `${fname} ${sname}`,
//     email: faker.internet.email(fname, sname),
//     address: faker.address.streetAddress(),
//     city: faker.address.city(),
//     zip: faker.address.zipCode(),
//     country: faker.address.country(),
//     shipped: faker.random.boolean(),
//     products: [],
//   };

//   let productCount = faker.random.number({ min: 1, max: 5 });
//   let product_ids = [];

//   while (product_ids.length < productCount) {
//     let candidateId = faker.random.number({ min: 1, max: products.length });
//     if (product_ids.indexOf(candidateId) === -1) {
//       product_ids.push(candidateId);
//     }
//   }
//   for (let j = 0; j < productCount; j++) {
//     order.products.push({
//       quantity: faker.random.number({ min: 1, max: 10 }),
//       product_id: product_ids[j],
//     });
//   }
//   orders.push(order);
// }
//#endregion

module.exports = () => ({ categories, products });