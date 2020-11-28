const { MongoClient } = require("mongodb");

const connectToDb = async (uri, dbName) => {
  let database;

  const client = new MongoClient(uri);

  try {
    await client.connect();

    database = client.db(dbName);
  } catch (e) {
    console.error(e);
  }

  return database;
};

module.exports = { connectToDb };
