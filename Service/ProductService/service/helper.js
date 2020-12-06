function GetType(obj) {
  var type = typeof obj;

  if (type !== "object") return type;
  if (obj === null) return "null";

  var ctor = obj.constructor;
  var name = typeof ctor === "function" && ctor.name;

  return typeof name === "string" && name.length > 0 ? name : "object";
}

function GetDefaultValue(type) {
  if (typeof type !== "string") throw new TypeError("Type must be a string.");

  switch (type) {
    case "boolean":
      return false;
    case "function":
      return function () {};
    case "null":
      return null;
    case "number":
      return 0;
    case "object":
      return {};
    case "string":
      return "";
    case "symbol":
      return Symbol();
    case "undefined":
      return void 0;
  }

  try {
    var ctor = typeof this[type] === "function" ? this[type] : eval(type);

    return new ctor();
  } catch (e) {
    return {};
  }
}

function HasDefaultValue(obj) {
  let objType = GetType(obj);

  let defaultValueOfObj = GetDefaultValue(objType);

  return obj === defaultValueOfObj;
}

module.exports = {
  GetType,
  GetDefaultValue,
  HasDefaultValue,
};
