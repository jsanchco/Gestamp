import { config, AUTHENTICATE, USERS, PROFESSIONS } from "../constants";
import store from "../store/store";
import ACTION_AUTHENTICATION from "../actions/authenticationAction";
import ACTION_APPLICATION from "../actions/applicationAction";

export const TOKEN_KEY = "jwt";

export const login = (username, password, history) => {
  const url = `${config.URL_API}/${AUTHENTICATE}`;
  fetch(url, {
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json"
    },
    method: "POST",
    body: JSON.stringify({ username, password })
  })
    .then(data => data.json())
    .then(result => {
      if (result.username != null && result.token != null) {
        localStorage.setItem('user', JSON.stringify(result.username));
        localStorage.setItem(TOKEN_KEY, result.token);
        store.dispatch(ACTION_AUTHENTICATION.logIn(result.username, result.token));
        history.push("/dashboard");
      } else {
        if (result.message) {
          console.log("error ->", result.message);
          localStorage.removeItem("user");
          localStorage.removeItem(TOKEN_KEY);
          store.dispatch(ACTION_AUTHENTICATION.logOut());
          store.dispatch(ACTION_APPLICATION.showMessage({
            statusText: result.message,
            responseText: result.message,
            type: "danger"
          }));
        }
      }
    })
    .catch(error => {
      console.log("error ->", error);
      store.dispatch(ACTION_APPLICATION.showMessage({
        statusText: error,
        responseText: error,
        type: "danger"
      }));
    });
};

export const logout = () => {
  localStorage.removeItem(TOKEN_KEY);
};

export const isLogin = () => {
  if (localStorage.getItem(TOKEN_KEY)) {
    return true;
  }

  return false;
};

export const getUsers = () => {
  const url = `${config.URL_API}/${USERS}`;
  fetch(url, {
    method: "GET"
  })
    .then(data => data.json())
    .then(result => {
      console.log("result ->", result);
      return result;
    })
    .catch(error => {
      console.log("error ->", error);
    });
};

export const getProfessions = () => {
  const url = `${config.URL_API}/${PROFESSIONS}`;
  fetch(url, {
    method: "GET"
  })
    .then(data => data.json())
    .then(result => {
      console.log("result ->", result);
      return result;
    })
    .catch(error => {
      console.log("error ->", error);
    });
};
