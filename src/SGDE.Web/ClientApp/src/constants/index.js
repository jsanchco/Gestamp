export const PROFESSIONS = "api/Professions";
export const USERS = "api/Users";
export const AUTHENTICATE = "api/Authenticate/login";
export const ORDERS = "api/Order";

const dev = {
  URL_API: "http://localhost:51667",
  URL_API1: "http://localhost:51567"
};

const prod = {
  URL_API: "http://localhost:8000",
  URL_API1: "http://localhost:8000"
};

export const config = process.env.NODE_ENV === "development" ? dev : prod;
