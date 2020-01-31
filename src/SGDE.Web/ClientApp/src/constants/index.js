export const PROFESSIONS = "api/Professions";
export const USERS = "api/Users";
export const AUTHENTICATE = "api/Users/authenticate";

const dev = {
  URL_API: "http://localhost:51567"
};

const prod = {
  URL_API: "http://localhost:8000"
};

export const config = process.env.NODE_ENV === "development" ? dev : prod;
